using System.Collections.Generic;

namespace ET
{
    public interface IUnitCache
    {
    }

    public class UnitCache : Entity, IAwake, IDestroy
    {
        /// <summary>
        /// <para> Key: UnitId </para>
        /// <para> Value: Entity </para>
        /// </summary>
        public Dictionary<long, Entity> CacheComponents = new Dictionary<long, Entity>();

        public string EntityName;
    }
}