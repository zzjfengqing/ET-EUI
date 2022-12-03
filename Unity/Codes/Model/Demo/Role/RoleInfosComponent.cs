using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [ChildType(typeof(RoleInfo))]
    public class RoleInfosComponent : Entity, IAwake, IDestroy
    {
        public List<RoleInfo> RoleInfos { get; set; } = new List<RoleInfo>();
        public long CurrentRoleId { get; set; } = 0;
    }
}