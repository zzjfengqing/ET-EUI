using System;

namespace ET
{
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene zoneScene, string address, string account, string password)
        {
            A2C_LoginAccount response = null;

            Session accountSession = null;
            try
            {
                accountSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
                var passwordMd5 = MD5Helper.StringMD5(password);
                response = await accountSession.Call(new C2A_LoginAccount() { AccountName = account, Password = passwordMd5 }) as A2C_LoginAccount;
            }
            catch (Exception e)
            {
                Log.Error(e);
                accountSession?.Dispose();
                return ErrorCode.ERR_NetWorkError;
            }
            if (response.Error != ErrorCode.ERR_Success)
            {
                accountSession?.Dispose();
                return response.Error;
            }
            zoneScene.AddComponent<SessionComponent>().Session = accountSession;
            zoneScene.GetComponent<SessionComponent>().Session.AddComponent<PingComponent>();

            zoneScene.GetComponent<AccountInfoComponent>().AccountId = response.AccountId;
            zoneScene.GetComponent<AccountInfoComponent>().Token = response.Token;

            return ErrorCode.ERR_Success;
        }

        /// <summary>
        /// 获取服务器列表
        /// </summary>
        /// <param name="zoneScene"></param>
        /// <returns>状态码</returns>
        public static async ETTask<int> GetServerInfos(Scene zoneScene)
        {
            A2C_GetServerInfos response;
            try
            {
                response = await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetServerInfos()
                {
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token
                }) as A2C_GetServerInfos;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return ErrorCode.ERR_NetWorkError;
            }

            if (response.Error != ErrorCode.ERR_Success)
            {
                return response.Error;
            }
            //todo:记录服务器列表信息
            var serverInfoComponent = zoneScene.GetComponent<ServerInfosComponent>();
            foreach (var nServerInfo in response.NServerInfos)
            {
                var serverInfo = serverInfoComponent.AddChild<ServerInfo>();
                serverInfo.FromNServerInfo(nServerInfo);
                //serverInfoComponent.Add(serverInfo);
            }
            return ErrorCode.ERR_Success;
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="zoneScene"></param>
        /// <param name="roleName">角色名</param>
        /// <returns>状态码</returns>
        public static async ETTask<int> CreateRole(Scene zoneScene, string roleName)
        {
            A2C_CreateRole response = null;
            try
            {
                response = await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_CreateRole()
                {
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                    Name = roleName,
                    ServerId = zoneScene.GetComponent<ServerInfosComponent>().CurServerId,
                }) as A2C_CreateRole;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return ErrorCode.ERR_NetWorkError;
            }

            if (response.Error != ErrorCode.ERR_Success)
            {
                Log.Error(response.Error.ToString());
                return response.Error;
            }

            var newRoleInfo = zoneScene.GetComponent<RoleInfosComponent>().AddChild<RoleInfo>();
            newRoleInfo.FromNServerInfo(response.NRoleInfo);

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> GetRoles(Scene zoneScene)
        {
        }
    }
}