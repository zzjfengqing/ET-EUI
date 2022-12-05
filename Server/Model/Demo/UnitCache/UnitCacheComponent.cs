using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class UnitCacheComponent : Entity, IAwake
    {
        public Dictionary<string, UnitCache> UnitCache = new Dictionary<string, UnitCache>();
        public List<string> UnitCacheKeys = new List<string>();
    }
}