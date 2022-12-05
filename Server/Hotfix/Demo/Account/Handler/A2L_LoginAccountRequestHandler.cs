using ICSharpCode.SharpZipLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class A2L_LoginAccountRequestHandler : AMActorRpcHandler<Scene, A2L_LoginAccountRequest, L2A_LoginAccountResponse>
    {
        protected override async ETTask Run(Scene scene, A2L_LoginAccountRequest request, L2A_LoginAccountResponse response, Action reply)
        {
            long accountId = request.AccountId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginCenterLock, accountId.GetHashCode()))
            {
                //当前帐号未登录,可正常登录
                LoginInfoRecordComponent loginInfoRecordComponent = scene.GetComponent<LoginInfoRecordComponent>();
                if (!loginInfoRecordComponent.IsExist(accountId))
                {
                    reply();
                    return;
                }

                //当前帐号已登录,需强制下线
                int zone = loginInfoRecordComponent.Get(accountId);
                StartSceneConfig gateConfig = RealmGateAddressHelper.GetGate(zone, accountId);
                var g2L_DisconnectGateUnit = await MessageHelper.CallActor(gateConfig.InstanceId,
                    new L2G_DisconnectGateUnit()
                    {
                        AccountId = accountId
                    }) as G2L_DisconnectGateUnit;

                response.Error = g2L_DisconnectGateUnit.Error;
                reply();
            }
        }
    }
}