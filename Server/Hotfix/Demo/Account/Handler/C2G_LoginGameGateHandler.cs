using System;

namespace ET
{
    [FriendClass(typeof(SessionStatusComponent))]
    [FriendClass(typeof(SessionPlayerComponent))]
    public class C2G_LoginGameGateHandler : AMRpcHandler<C2G_LoginGameGate, G2C_LoginGameGate>
    {
        protected override async ETTask Run(Session session, C2G_LoginGameGate request, G2C_LoginGameGate response, Action reply)
        {
            Scene scene = session.DomainScene();

            #region 校验

            //服务器类型校验
            if (scene.SceneType != SceneType.Gate)
            {
                Log.Error($"请求的Scene错误,当前Scene为:{scene.SceneType}");
                response.Error = ErrorCode.ERR_RequestSceneTypeError;
                reply();
                session.Disconnect();
                return;
            }

            //避免重复请求
            if (session.GetComponent<SessionLoginComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                session.Disconnect();
                return;
            }

            //令牌校验
            string key = scene.GetComponent<GateSessionKeyComponent>().Get(request.AccountId);
            if (key is null || key != request.Key)
            {
                response.Error = ErrorCode.ERR_ConnectGateKeyError;
                response.Message = "Gate Key 校验失败";
                reply();
                session.Disconnect();
                return;
            }
            scene.GetComponent<GateSessionKeyComponent>().Remove(request.AccountId);

            #endregion 校验

            #region 登陆游戏

            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            long instanceId = session.InstanceId;
            using (session.AddComponent<SessionLoginComponent>())
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate, request.AccountId))
            {
                if (instanceId != session.InstanceId)
                    return;
                //通知登录中心服 记录本次登陆的服务器zone
                StartSceneConfig loginCenterConfig = StartSceneConfigCategory.Instance.LoginCenterConfig;
                L2G_AddLoginRecord loginCenterResponse = await MessageHelper.CallActor(loginCenterConfig.InstanceId,
                    new G2L_AddLoginRecord()
                    {
                        AccountId = request.AccountId,
                        ServerId = scene.Zone
                    }) as L2G_AddLoginRecord;
                if (loginCenterResponse?.Error != ErrorCode.ERR_Success)
                {
                    response.Error = loginCenterResponse.Error;
                    reply();
                    session.Disconnect();
                    return;
                }

                //添加当前会话状态组件
                SessionStatusComponent sessionStatusComponent = session.GetComponent<SessionStatusComponent>();
                sessionStatusComponent ??= session.AddComponent<SessionStatusComponent>();
                sessionStatusComponent.Status = SessionStatus.Normal;

                //生成玩家对象unit
                Player player = scene.GetComponent<PlayerComponent>().Get(request.AccountId);
                if (player == null)
                {
                    // [player.Id] = [player.UnitId] = [RoleId]
                    player = scene.GetComponent<PlayerComponent>().AddChildWithId<Player, long, long>(request.RoleId, request.AccountId, request.RoleId);
                    player.Status = PlayerStatus.Gate;
                    session.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);
                }
                else
                {
                    player.RemoveComponent<PlayerOfflineOutTimeComponent>();
                }
                session.AddComponent<SessionPlayerComponent>().PlayerId = player.Id;
                session.GetComponent<SessionPlayerComponent>().PlayerInstanceId = player.InstanceId;
                session.GetComponent<SessionPlayerComponent>().AccountId = request.AccountId;
                player.SessionInstanceId = session.InstanceId;
            }
            reply();

            #endregion 登陆游戏
        }
    }
}