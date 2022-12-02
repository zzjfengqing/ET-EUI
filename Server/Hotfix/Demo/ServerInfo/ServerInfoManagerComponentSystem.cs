using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ServerInfoManagerComponentAwakeSystem : AwakeSystem<ServerInfoManagerComponent>
    {
        public override void Awake(ServerInfoManagerComponent self)
        {
            self.Init();
        }
    }

    public class ServerInfoManagerComponentDestroySystem : DestroySystem<ServerInfoManagerComponent>
    {
        public override void Destroy(ServerInfoManagerComponent self)
        {
            for (int i = 0; i < self.ServerInfos.Count; i++)
            {
                self.ServerInfos[i]?.Dispose();
            }
            self.ServerInfos.Clear();
        }
    }

    public class ServerInfoManagerComponentLoadSystem : LoadSystem<ServerInfoManagerComponent>
    {
        public override void Load(ServerInfoManagerComponent self)
        {
            self.Init();
        }
    }

    public static class ServerInfoManagerComponentSystem
    {
        public static async void Init(this ServerInfoManagerComponent self)
        {
            var serverInfos = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<ServerInfo>(info => true);
            self.ServerInfos.Clear();

            if (serverInfos?.Count <= 0)
            {
                Log.Warning("数据库中不存在 [ServerInfo] 信息");
                //undone:导入服务器信息
                var config = ServerInfoConfigCategory.Instance.GetAll();
                foreach (var info in config.Values)
                {
                    var newServerInfo = self.AddChildWithId<ServerInfo>(info.Id);
                    newServerInfo.ServerName = info.ServerName;
                    newServerInfo.Status = (int)ServerStatus.Normal;
                    self.ServerInfos.Add(newServerInfo);
                    self.AddChild(newServerInfo);
                    DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).SaveNotWait(newServerInfo).Coroutine();
                }
                return;
            }

            foreach (var serverInfo in serverInfos)
            {
                self.AddChild(serverInfo);
                self.ServerInfos.Add(serverInfo);
            }
        }
    }
}