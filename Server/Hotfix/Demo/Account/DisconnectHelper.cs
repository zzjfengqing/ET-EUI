using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class DisconnectHelper
    {
        public static async ETTask Disconnect(this Session self)
        {
            if (self == null || self.IsDisposed)
            {
                return;
            }
            long instanceId = self.InstanceId;
            //等待一秒后再释放
            await TimerComponent.Instance.WaitAsync(1000);
            //防止多次释放
            if (self.InstanceId != instanceId)
            {
                return;
            }
            self.Dispose();
        }
    }
}