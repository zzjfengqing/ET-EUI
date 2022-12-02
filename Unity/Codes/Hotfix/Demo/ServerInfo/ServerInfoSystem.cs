using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class ServerInfoSystem
    {
        public static void FromMessage(this ServerInfo self, NServerInfo nServerInfo)
        {
            self.Id = nServerInfo.Id;
            self.Status = nServerInfo.Status;
            self.ServerName = nServerInfo.ServerName;
        }

        public static NServerInfo ToMessage(this ServerInfo self)
        {
            return new NServerInfo()
            {
                Id = (int)self.Id,
                Status = self.Status,
                ServerName = self.ServerName,
            };
        }
    }
}