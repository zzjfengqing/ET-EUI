using System;

namespace ET
{
    [ActorMessageHandler]
    public class L2G_DisconnectGateUnitHandler:AMActorRpcHandler<Scene,L2G_DisconnectGateUnit,G2L_DisconnectGateUnit>
    {
        protected override async ETTask Run(Scene scene, L2G_DisconnectGateUnit request, G2L_DisconnectGateUnit response, Action reply)
        {
            long accountId = request.AccountId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GetLoginLock,accountId.GetHashCode()))
            {
                PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();
                Player gataUnit = playerComponent.Get(accountId);
                if (gataUnit==null)
                {
                    reply();
                    return;
                }
                
                playerComponent.Remove(accountId);
                
                gataUnit.Dispose();
            }

            reply();
            
            await ETTask.CompletedTask;
        }
    }
}