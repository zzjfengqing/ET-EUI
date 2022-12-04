using System;

namespace ET
{
    public class L2G_DisconnectGateUnitHandler : AMActorRpcHandler<Scene, L2G_DisconnectGateUnit, G2L_DisconnectGateUnit>
    {
        protected override async ETTask Run(Scene scene, L2G_DisconnectGateUnit request, G2L_DisconnectGateUnit response, Action reply)
        {
            var accountId = request.AccountId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GateLoginLock, accountId))
            {
                PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();
                var player = playerComponent.Get(accountId);
                if (player == null)
                {
                    reply();
                    return;
                }
                playerComponent.Remove(accountId);
                //Todo:强制玩家下线
                player.Dispose();
            }
            reply();
        }
    }
}