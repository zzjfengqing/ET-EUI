using System;
using System.Threading.Tasks;

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
                serverInfoComponent.Add(serverInfo);
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

        /// <summary>
        /// 获取当前服务器(区)角色列表
        /// </summary>
        /// <param name="zoneScene"></param>
        /// <returns></returns>
        public static async ETTask<int> GetRoles(Scene zoneScene)
        {
            A2C_GetRoles response = null;
            try
            {
                response = await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetRoles()
                {
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                    ServerId = zoneScene.GetComponent<ServerInfosComponent>().CurServerId,
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().AccountId,
                }) as A2C_GetRoles;
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

            zoneScene.GetComponent<RoleInfosComponent>().RoleInfos.Clear();//UNDONE: 孩子节点并未清除
            for (int i = 0; i < response.NRoleInfos.Count; i++)
            {
                var roleInfo = zoneScene.GetComponent<RoleInfosComponent>().AddChild<RoleInfo>();
                roleInfo.FromNServerInfo(response.NRoleInfos[i]);
                zoneScene.GetComponent<RoleInfosComponent>().RoleInfos.Add(roleInfo);
            }

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> DeleteRole(Scene zoneScene, long roleId)
        {
            A2C_DelteRole response = null;
            try
            {
                response = await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_DelteRole()
                {
                    Token = zoneScene.GetComponent<AccountInfoComponent>().Token,
                    ServerId = zoneScene.GetComponent<ServerInfosComponent>().CurServerId,
                    RoleId = roleId
                }) as A2C_DelteRole;
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

            //删除本地数据
            var roleInfos = zoneScene.GetComponent<RoleInfosComponent>().RoleInfos;
            int index = roleInfos.FindIndex(role => role.Id == response.RoleId);
            roleInfos.RemoveAt(index);
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> GetRealmKey(Scene zoneScene)
        {
            A2C_GetRealmKey response = null;
            AccountInfoComponent accountInfoComponent = zoneScene.GetComponent<AccountInfoComponent>();
            try
            {
                response = await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetRealmKey()
                {
                    Token = accountInfoComponent.Token,
                    ServerId = zoneScene.GetComponent<ServerInfosComponent>().CurServerId,
                    AccountId = accountInfoComponent.AccountId
                }) as A2C_GetRealmKey;
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
            accountInfoComponent.RealmToken = response.RealmToken;
            accountInfoComponent.RealmAddress = response.RealmAddress;
            zoneScene.GetComponent<SessionComponent>().Session.Dispose();
            return ErrorCode.ERR_Success;
        }

        public static async Task<int> EnterGame(Scene zoneScene)
        {
            string realmAddress = zoneScene.GetComponent<AccountInfoComponent>().RealmAddress;
            Session session = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(realmAddress));

            #region 连接Realm,获取分配的Gate

            R2C_LoginRealm realmResponse = null;
            AccountInfoComponent accountInfoComponent = zoneScene.GetComponent<AccountInfoComponent>();
            try
            {
                realmResponse = await session.Call(new C2R_LoginRealm()
                {
                    RealmToken = accountInfoComponent.RealmToken,
                    AccountId = accountInfoComponent.AccountId,
                }) as R2C_LoginRealm;
            }
            catch (Exception e)
            {
                Log.Error(e);
                session?.Dispose();
                return ErrorCode.ERR_NetWorkError;
            }

            if (realmResponse.Error != ErrorCode.ERR_Success)
            {
                Log.Error(realmResponse.Error.ToString());
                session?.Dispose();
                return realmResponse.Error;
            }

            #endregion 连接Realm,获取分配的Gate

            #region 2. 连接分配的Gate网关服务器

            // TODO: 2. 连接分配的Gate网关服务器

            #endregion 2. 连接分配的Gate网关服务器

            return ErrorCode.ERR_Success;
        }
    }
}