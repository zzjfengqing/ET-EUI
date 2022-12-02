using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public enum ServerStatus
    {
        Normal,
        Stop
    }

    public class ServerInfo : Entity, IAwake
    {
        public int Status { get; set; }
        public string ServerName { get; set; }
    }
}