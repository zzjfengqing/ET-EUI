using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [Timer(TimerType.PlayerOfflineOutTime)]
    public class PlayerOfflineOutTime : ATimer<PlayerOfflineOutTimeComponent>
    {
        public override void Run(PlayerOfflineOutTimeComponent self)
        {
            try
            {
                self.KickPlayer();
            }
            catch (Exception e)
            {
                Log.Error($"PlayerOffline timer error: {self.Id}\n{e}");
            }
        }
    }

    public static class PlayerOfflineOutTimeComponentSystem
    {
        [ObjectSystem]
        public class PlayerOfflineOutTimeComponentAwakeSystem : AwakeSystem<PlayerOfflineOutTimeComponent>
        {
            public override void Awake(PlayerOfflineOutTimeComponent self)
            {
                self.Timer = TimerComponent.Instance.NewOnceTimer(
                    TimeHelper.ServerNow() + 10 * TimeHelper.Second,
                    TimerType.PlayerOfflineOutTime,
                    self);
            }
        }

        [ObjectSystem]
        public class PlayerOfflineOutTimeComponentDestroySystem : DestroySystem<PlayerOfflineOutTimeComponent>
        {
            public override void Destroy(PlayerOfflineOutTimeComponent self)
            {
                TimerComponent.Instance.Remove(ref self.Timer);
            }
        }

        public static void KickPlayer(this PlayerOfflineOutTimeComponent self)
        {
            DisconnectHelper.KickPlayer(self.GetParent<Player>()).Coroutine();
        }
    }
}