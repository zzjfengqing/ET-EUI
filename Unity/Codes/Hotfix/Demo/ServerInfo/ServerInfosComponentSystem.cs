using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ServerInfosComponentDestroySystem : DestroySystem<ServerInfosComponent>
    {
        public override void Destroy(ServerInfosComponent self)
        {
            for (int i = 0; i < self.ServerInfos.Count; i++)
            {
                self.ServerInfos[i]?.Dispose();
            }
            self.ServerInfos.Clear();
            self.CurServerId = 0;
        }
    }

    public static class ServerInfosComponentSystem
    {
        public static void Add(this ServerInfosComponent self, ServerInfo serverInfo)
        {
            self.ServerInfos.Add(serverInfo);
        }
    }
}