using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ET
{
    public class C2R_LoginRealmHandler : AMRpcHandler<C2R_LoginRealm, R2C_LoginRealm>
    {
        protected override async ETTask Run(Session session, C2R_LoginRealm request, R2C_LoginRealm response, Action reply)
        {
            Scene scene = session.DomainScene();

            #region 校验

            //服务器类型校验
            if (scene.SceneType != SceneType.Realm)
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
            string token = scene.GetComponent<TokenComponent>().Get(request.AccountId);
            if (token is null || token != request.RealmToken)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                session.Disconnect();
                return;
            }
            scene.GetComponent<TokenComponent>().Remove(request.AccountId);

            #endregion 校验

            #region 分配Gate

            using (session.AddComponent<SessionLoginComponent>())
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginRealm, request.AccountId))
            {
                //根据帐号ID取模分配Gate
                StartSceneConfig config = RealmGateAddressHelper.GetGate(scene.Zone, request.AccountId);

                //向Gate请求一个Key,客户端需要使用这个Key连接Gate
                G2R_GetLoginGateKey gateResponse = await MessageHelper.CallActor(config.InstanceId,
                    new R2G_GetLoginGateKey()
                    {
                        AccountId = request.AccountId,
                    }) as G2R_GetLoginGateKey;

                if (gateResponse.Error != ErrorCode.ERR_Success)
                {
                    response.Error = gateResponse.Error;
                    reply();
                    return;
                }
                response.GateSessionToken = gateResponse.GateSessionToken;
                response.GateAddress = config.OuterIPPort.ToString();
                reply();
                session.Disconnect();
            }

            #endregion 分配Gate
        }
    }
}