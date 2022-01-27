using System;
using System.Text.RegularExpressions;

namespace ET
{
    public class C2A_LoginAccountHandler : AMRpcHandler<C2A_LoginAccount, A2C_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2A_LoginAccount request, A2C_LoginAccount response, Action reply)
        {
            //判断场景是否是登录场景
            if (session.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"登录失败,当前场景为{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }
            //移除session五秒超时检测组件
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply?.Invoke();
                session.Disconnect().Coroutine();
                return;
            }

            //判断用户名和密码是否为空
            if (string.IsNullOrEmpty(request.AccountName) || string.IsNullOrEmpty(request.PassWord))
            {
                response.Error = ErrorCode.ERR_LoginInfoIsNull;
                reply?.Invoke();
                session.Disconnect().Coroutine();
                return;
            }
            //判断用户名密码是否符合规则
            if (!Regex.IsMatch(request.AccountName.Trim(), @"^[a-zA-Z0-9_]{6,15}$"))
            {
                Log.Error($"用户名格式错误{request.AccountName.Trim()}");
                response.Error = ErrorCode.ERR_LoginInfoError;
                reply?.Invoke();
                session.Disconnect().Coroutine();
                return;
            }
            //强密码,必须同时包含大小写字母和数字 @"^(?=.*[0-9].*)(?=.*[A-Z].*)(?=.*[a-z].*).{6,15}$"
            //普通密码,6-15位的数字或字母密码就行 @"^[a-zA-Z0-9_]{6,15}$"
            
            if (!Regex.IsMatch(request.PassWord.Trim(), @"^[a-zA-Z0-9_],[6,30]$"))
            {
                Log.Error($"密码格式错误{request.PassWord.Trim()}");
                response.Error = ErrorCode.ERR_LoginfoPassWordError;
                reply?.Invoke();
                session.Disconnect().Coroutine();
                return;
            }

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount, request.AccountName.Trim().GetHashCode()))
                {
                    //获取数据库账号列表
                    var accountInfoList = await DBManagerComponent.Instance.GetZoneDB
                        (session.DomainZone()).Query<Account>(d => d.AccountName.Equals(request.AccountName.Trim()));
                    Account account = null;
                    if (accountInfoList != null && accountInfoList.Count > 0)
                    {
                        account = accountInfoList[0];
                        session.AddChild(account);
                        //判断是否是黑名单
                        if (account.AccountType == (int)AccountType.BlackList)
                        {
                            response.Error = ErrorCode.ERR_AccountIsBlackListError;
                            reply?.Invoke();
                            session.Disconnect().Coroutine();
                            account?.Dispose();
                            return;
                        }
                        //密码是否正确
                        if (account.PassWord.Equals(request.PassWord))
                        {
                            response.Error = ErrorCode.ERR_LoginPassWordError;
                            reply?.Invoke();
                            session.Disconnect().Coroutine();
                            account?.Dispose();
                            return;
                        }
                    }
                    else
                    {
                        //创建账户
                        account = session.AddChild<Account>();
                        account.AccountName = request.AccountName; ;
                        account.PassWord = request.PassWord;
                        account.CreateTime = TimeHelper.ServerNow();
                        account.AccountType = (int)AccountType.General;
                        await DBManagerComponent.Instance.GetZoneDB(session.DomainZone()).Save(account);
                        Console.WriteLine($"创建账户成功 [账户:{account.Id}-密码:{account.PassWord}]");
                    }

                    StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "LoginCenter");
                    long loginCenterInstanceId = startSceneConfig.InstanceId;
                    L2A_LoginAccountResponse loginAccountResponse =
                            (L2A_LoginAccountResponse) await ActorMessageSenderComponent.Instance.Call(loginCenterInstanceId,
                                new A2L_LoginAccountRequest() { AccountId = account.Id });
                    if (loginAccountResponse.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = loginAccountResponse.Error;
                        reply();
                        session?.Disconnect().Coroutine();
                        account?.Dispose();
                        return;
                    }

                    long accountSessionInstancId = session.DomainScene().GetComponent<AccountSessionsComponent>().Get(account.Id);
                    Session otherSession = Game.EventSystem.Get(accountSessionInstancId) as Session;
                    otherSession?.Send(new A2C_Disconnect() { Error = 0 });
                    otherSession?.Disconnect();

                    session.DomainScene().GetComponent<AccountSessionsComponent>().Add(account.Id, session.InstanceId);

                    session.AddComponent<AccountCheckOutTimeComponent, long>(account.Id);

                    var Token = TimeHelper.ServerNow().ToString() + RandomHelper.RandomNumber(int.MinValue, int.MaxValue).ToString();
                    session.DomainScene().GetComponent<TokenComponent>().Remove(account.Id);
                    session.DomainScene().GetComponent<TokenComponent>().Add(account.Id, Token);
                    response.AccountId = account.Id;
                    response.Token = Token;
                    reply();
                    account?.Dispose();
                }
            }
        }
    }
}