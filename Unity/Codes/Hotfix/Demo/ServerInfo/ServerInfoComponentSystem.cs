using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ServerInfoComponentDestroySystem : DestroySystem<ServerInfoComponent>
    {
        public override void Destroy(ServerInfoComponent self)
        {
            for (int i = 0; i < self.ServerInfos.Count; i++)
            {
                self.ServerInfos[i]?.Dispose();
            }
            self.ServerInfos.Clear();
            self.CurServerId = 0;
        }
    }

    public static class ServerInfoComponentSystem
    {
        public static void Add(this ServerInfoComponent self, ServerInfo serverInfo)
        {
            self.ServerInfos.Add(serverInfo);
        }
    }
}