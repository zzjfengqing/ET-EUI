using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [ChildType(typeof(RoleInfo))]
    [ComponentOf(typeof(Scene))]
    public class RoleInfosComponent : Entity, IAwake, IDestroy
    {
        public List<RoleInfo> RoleInfos { get; set; } = new List<RoleInfo>();
        public long CurRoleId { get; set; } = 0;
    }
}