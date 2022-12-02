using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public enum RoleInfoStatus
    {
        Normal,
        Freeze
    }

    public class RoleInfo : Entity, IAwake
    {
        public string Name { get; set; }
        public int ServerId { get; set; }
        public int Status { get; set; }
        public long AccountId { get; set; }
        public long LastLoginTIme { get; set; }
        public long CreateTime { get; set; }
    }
}