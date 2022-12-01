using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ET
{
    public class C2A_LoginAccountHandler : AMRpcHandler<C2A_LoginAccount, A2C_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2A_LoginAccount request, A2C_LoginAccount response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"请求的Scene错误,当前Scene为:{session.DomainScene().SceneType}");
                session?.Dispose();
                return;
            }
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            if (string.IsNullOrEmpty(request.AccountName) || string.IsNullOrEmpty(request.Password))
            {
                response.Error = ErrorCode.ERR_AccountInfoIsNull;
                reply();
                session.Disconnect();
                return;
            }

            if (!Regex.IsMatch(request.AccountName.Trim(), @"^(?=.*[0-9].*)(?=.*[A-Z].*)(?=.*[a-z].*).{6,15}$"))
            {
                response.Error = ErrorCode.ERR_AccountNameFormError;
                reply();
                session.Disconnect();
                return;
            }
            if (!Regex.IsMatch(request.Password.Trim(), @"^(?=.*[0-9].*)(?=.*[A-Z].*)(?=.*[a-z].*).{6,15}$"))
            {
                response.Error = ErrorCode.ERR_PasswordFormError;
                reply();
                session.Disconnect();
                return;
            }

            var accountResult = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                .Query<Account>(d => d.AccountName.Equals(request.AccountName.Trim()));
            Account account = null;
            //数据库中存在  --> 登录
            if (accountResult.Count > 0)
            {
                account = accountResult[0];
                session.AddChild(account);
                if (account.AccountType == (int)AccountType.BlackList)
                {
                    response.Error = ErrorCode.ERR_AccountStatusAbnormal;
                    reply();
                    session.Disconnect();
                    return;
                }
            }
            //数据库中不存在 --> 创建
            else
            {
                account = session.AddChild<Account>();
                account.AccountName = request.AccountName;
                account.Password = request.Password;
                account.CreateTime = TimeHelper.ServerNow();
                account.AccountType = (int)AccountType.General;
                await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save(account);
            }

            string token = TimeHelper.ServerNow().ToString() + RandomHelper.RandomNumber(int.MinValue, int.MinValue).ToString();
            session.DomainScene().GetComponent<TokenComponent>().Remove(account.Id);
            session.DomainScene().GetComponent<TokenComponent>().Add(account.Id, token);

            response.AccountId = account.Id;
            response.Token = token;
            reply();
            account.Dispose();
        }
    }
}