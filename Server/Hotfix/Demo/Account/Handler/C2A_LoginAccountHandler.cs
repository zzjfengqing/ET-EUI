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
            Scene scene = session.DomainScene();

            #region 校验

            //服务器类型校验
            if (scene.SceneType != SceneType.Account)
            {
                Log.Error($"请求的Scene错误,当前Scene为:{scene.SceneType}");
                response.Error = ErrorCode.ERR_RequestSceneTypeError;
                reply();
                session.Disconnect();
                return;
            }

            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            //避免重复请求
            if (session.GetComponent<SessionLoginComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                session.Disconnect();
                return;
            }
            //校验账号信息
            if (string.IsNullOrEmpty(request.AccountName) || string.IsNullOrEmpty(request.Password))
            {
                response.Error = ErrorCode.ERR_AccountInfoIsNull;
                reply();
                session.Disconnect();
                return;
            }

            if (!Regex.IsMatch(request.AccountName.Trim(), @"^[a-zA-Z0-9_-]{4,20}$"))
            {
                response.Error = ErrorCode.ERR_AccountNameFormError;
                reply();
                session.Disconnect();
                return;
            }
            //if (!Regex.IsMatch(request.Password.Trim(), @"^[a-zA-Z0-9_-]{4,20}$"))
            //{
            //    response.Error = ErrorCode.ERR_PasswordFormError;
            //    reply();
            //    session.Disconnect();
            //    return;
            //}

            #endregion 校验

            #region 登录/创建帐号

            using (session.AddComponent<SessionLoginComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount, request.AccountName.Trim().GetHashCode()))
                {
                    var accountResult = await DBManagerComponent.Instance.GetZoneDB(session.DomainZone())
                        .Query<Account>(d => d.AccountName.Equals(request.AccountName.Trim()));
                    Account account = null;
                    //数据库中存在  --> 登录
                    if (accountResult?.Count > 0)
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

                    //通知登录中心
                    long loginCenterInstanceId = StartSceneConfigCategory.Instance.LoginCenterConfig.InstanceId;
                    var loginAccountResponse = await MessageHelper.CallActor(loginCenterInstanceId,
                        new A2L_LoginAccountRequest()
                        {
                            AccountId = account.Id
                        }) as L2A_LoginAccountResponse;
                    if (loginAccountResponse?.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = loginAccountResponse.Error;
                        reply();
                        session?.Disconnect();
                        account?.Dispose();
                        return;
                    }

                    //获取当前帐号登陆情况并强制下线
                    long accountSessionInstanceId = scene.GetComponent<AccountSessionsComponent>().Get(account.Id);
                    var otherSession = Game.EventSystem.Get(accountSessionInstanceId) as Session;
                    otherSession?.Send(new A2C_Disconnect() { Error = 0 });
                    otherSession?.Disconnect();

                    //发放登陆令牌
                    string token = TimeHelper.ServerNow().ToString() + RandomHelper.RandomNumber(int.MinValue, int.MinValue).ToString();
                    //scene.GetComponent<TokenComponent>().Remove(account.Id);
                    scene.GetComponent<TokenComponent>().AddOrModify(account.Id, token);

                    //添加超时检测组件
                    session.AddComponent<AccountCheckOutTimeComponent, long>(account.Id);

                    response.AccountId = account.Id;
                    response.Token = token;

                    reply();
                    account?.Dispose();
                }
            }

            #endregion 登录/创建帐号
        }
    }
}