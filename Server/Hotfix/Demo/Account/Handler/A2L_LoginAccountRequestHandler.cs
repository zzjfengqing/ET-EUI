using System;

namespace ET
{
    [ActorMessageHandler]
    public class A2L_LoginAccountRequestHandler :AMActorRpcHandler<Scene,A2L_LoginAccountRequest,L2A_LoginAccountResponse>
    {
        protected override async ETTask Run(Scene scene, A2L_LoginAccountRequest request, L2A_LoginAccountResponse response, Action reply)
        {
            long accountId = request.AccountId;
            using (await  CoroutineLockComponent.Instance.Wait( CoroutineLockType.LoginCenterLock,accountId.GetHashCode()))
            {
                if (!scene.GetComponent<LoginIfoRecordComponent>().IsExist(accountId))
                {
                    reply();
                    return;
                }

                int zone = scene.GetComponent<LoginIfoRecordComponent>().Get(accountId);

                StartSceneConfig gateConfig = RealmGateAddressHelper.GetGate(zone,accountId);

              G2L_DisconnectGateUnit g2LDisconnectGateUnit =( G2L_DisconnectGateUnit)  await MessageHelper.CallActor(gateConfig.InstanceId, new L2G_DisconnectGateUnit() { AccountId = accountId });
              response.Error = g2LDisconnectGateUnit.Error;
              reply();
            }
            
            await ETTask.CompletedTask;
        }
    }
}