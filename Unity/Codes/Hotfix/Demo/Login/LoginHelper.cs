using System;

namespace ET
{
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene zoneScene, string address, string account, string password)
        {
            A2C_LoginAccount a2C_LoginAccount = null;
            Session session = null;
            try
            {
                session = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
                password = MD5Helper.StringMD5(password);
                a2C_LoginAccount = (A2C_LoginAccount) await session.Call(new C2A_LoginAccount() { AccountName = account, PassWord = password });
                Log.Debug(a2C_LoginAccount.Error.ToString());
            }
            catch (Exception e)
            {
                session.Dispose();
                Log.Error(e);
                return ErrorCode.ERR_NetworkError;
            }
            if (a2C_LoginAccount.Error != ErrorCode.ERR_Success)
            {
                session?.Dispose();
                return a2C_LoginAccount.Error;
            }
            zoneScene.AddComponent<SessionComponent>().Session = session;
            //心跳包
            //zoneScene.GetComponent<SessionComponent>().AddComponent<PingComponent>();

            zoneScene.GetComponent<AccountInfoComponent>().Token = a2C_LoginAccount.Token;
            zoneScene.GetComponent<AccountInfoComponent>().AccountId = a2C_LoginAccount.AccountId;
            return ErrorCode.ERR_Success;
        }
    }
}