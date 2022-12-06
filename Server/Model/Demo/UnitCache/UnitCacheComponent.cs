using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    [ChildType(typeof(UnitCache))]
    public class UnitCacheComponent : Entity, IAwake, IDestroy
    {
        public List<string> UnitCacheNames = new List<string>();
        public Dictionary<string, UnitCache> UnitCaches = new Dictionary<string, UnitCache>();
    }
}