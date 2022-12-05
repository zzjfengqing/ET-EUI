using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public interface IUnitCache
    {
    }

    public class UnitCache : Entity, IAwake, IDestroy
    {
        public string Key;
        public Dictionary<long, Entity> CacheCOmponents = new Dictionary<long, Entity>();
    }
}