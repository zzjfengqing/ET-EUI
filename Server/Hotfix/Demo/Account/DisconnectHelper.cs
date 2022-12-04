using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class DisconnectHelper
    {
        public static async void Disconnect(this Session self)
        {
            if (self == null || self.IsDisposed)
            {
                return;
            }
            var instanceId = self.InstanceId;

            await TimerComponent.Instance.WaitAsync(1000);

            if (instanceId != self.InstanceId)
                return;
            self.Dispose();
        }

        internal static async void KickPlayer(Player player)
        {
            if (player is null || player.IsDisposed)
                return;
            long instanceId = player.InstanceId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate, player.AccountId))
            {
                if (player.IsDisposed || instanceId != player.InstanceId)
                    return;
                switch (player.Status)
                {
                    case PlayerStatus.Disconnect:
                        break;

                    case PlayerStatus.Gate:
                        break;

                    case PlayerStatus.Game:
                        //TODO: 通知游戏逻辑服下线Unit角色逻辑,并将数据存入数据库
                        break;
                }
                player.Status = PlayerStatus.Disconnect;
                player.DomainScene().GetComponent<PlayerComponent>()?.Remove(player.AccountId);
                player.Dispose();
            }
            await TimerComponent.Instance.WaitAsync(300);
        }
    }
}