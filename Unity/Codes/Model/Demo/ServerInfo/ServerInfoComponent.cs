using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    [ChildType(typeof(ServerInfo))]
    public class ServerInfoComponent : Entity, IAwake, IDestroy
    {
        public List<ServerInfo> ServerInfos { get; set; } = new List<ServerInfo>();
    }
}