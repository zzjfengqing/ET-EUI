using System;
using System.Text.RegularExpressions;

namespace ET
{
    [FriendClass(typeof(Account))]
    public  class C2A_LoginAccountHandler: AMRpcHandler<C2A_LoginAccount,A2C_LoginAccount>
    {
        protected  override async  ETTask Run(Session session, C2A_LoginAccount request, A2C_LoginAccount response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"请求的Scene错误，当前Scene为：{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }
            session.RemoveComponent<SessionAcceptTimeoutComponent>();
            
            
            
            if (string.IsNullOrEmpty(request.AccountName) || string.IsNullOrEmpty(request.Password))
            {
                response.Error = ErrorCode.ERR_LoginInfoIsNull;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            if (!Regex.IsMatch(request.AccountName.Trim(),@"^(?=.*[0-9].*)(?=.*[A-Z].*)(?=.*[a-z].*).{6,15}$"))
            {
                response.Error = ErrorCode.ERR_AccountNameFormError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }
   
            if (!Regex.IsMatch(request.Password.Trim(),@"^[A-Za-z0-9]+$"))
            {
                response.Error = ErrorCode.ERR_PasswordFormError;
                reply();
                session.Disconnect().Coroutine();
                return;
            }

            var accountInfoList =await session.GetZoneDB().Query<Account>(d => d.AccountName.Equals(request.AccountName.Trim()));
            Account account     = null;
            if (accountInfoList!=null && accountInfoList.Count > 0)
            {
                account = accountInfoList[0];
                session.GetComponent<AccountsZone>().AddChild(account);
                if (account.AccountType == (int)AccountType.BlackList)
                {
                    response.Error = ErrorCode.ERR_AccountInBlackListError;
                    reply();
                    session.Disconnect().Coroutine();
                    account?.Dispose();
                    return;
                }


                if (!account.Password.Equals(request.Password))
                {
                    response.Error = ErrorCode.ERR_LoginPasswordError;
                    reply();
                    session.Disconnect().Coroutine();
                    account?.Dispose();
                    return;
                }
            }
            else
            {
                account             = session.GetComponent<AccountsZone>().AddChild<Account>();
                account.AccountName = request.AccountName.Trim();
                account.Password    = request.Password;
                account.CreateTime  = TimeHelper.ServerNow();
                account.AccountType = (int)AccountType.General;
                await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save<Account>(account);
            }
            await ETTask.CompletedTask;
        }
    }
}