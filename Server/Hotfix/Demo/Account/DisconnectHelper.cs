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

        internal static void KickPlayer(Player player)
        {
            throw new NotImplementedException();
        }
    }
}