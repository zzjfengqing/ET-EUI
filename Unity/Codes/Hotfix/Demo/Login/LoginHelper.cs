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
                response = await accountSession.Call(new C2A_LoginAccount() { AccountName = account, Password = password }) as A2C_LoginAccount;
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

            zoneScene.GetComponent<AccountInfoComponent>().AccountId = response.AccountId;
            zoneScene.GetComponent<AccountInfoComponent>().Token = response.Token;

            return ErrorCode.ERR_Success;
        }
    }
}