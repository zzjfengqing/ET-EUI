using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class C2A_GetRolesHandler : AMRpcHandler<C2A_GetRoles, A2C_GetRoles>
    {
        protected override async ETTask Run(Session session, C2A_GetRoles request, A2C_GetRoles response, Action reply)
        {
            #region 校验

            //服务器类型校验
            if (!session.CheckSceneType(SceneType.Account))
                return;
            //重复请求校验
            if (session.GetComponent<SessionLoginComponent>() != null) //避免重复查询
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                session.Disconnect();
                return;
            }

            //令牌校验
            string token = session.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId); ;

            if (token is null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                session.Disconnect();
                return;
            }

            #endregion 校验

            #region 获取角色信息

            using (session.AddComponent<SessionLoginComponent>())//查询角色时不能进行创建角色
            {
                //获取角色信息
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GetRoles, request.AccountId))
                {
                    List<RoleInfo> roleInfos = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                        .Query<RoleInfo>(r => r.AccountId == request.AccountId && r.ServerId == request.ServerId && r.Status == ((int)RoleInfoStatus.Normal));
                    //发送响应
                    foreach (RoleInfo roleInfo in roleInfos)
                    {
                        response.NRoleInfos.Add(roleInfo.ToNServerInfo());
                    }
                    reply();
                }
            }

            #endregion 获取角色信息
        }
    }
}