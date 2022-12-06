namespace ET
{
    [FriendClass(typeof(UnitCache))]
    [FriendClass(typeof(UnitCacheComponent))]
    public static class UnitCacheComponentSystem
    {
        [FriendClass(typeof(UnitCache))]
        public class UnitCacheComponentAwakeSystem : AwakeSystem<UnitCacheComponent>
        {
            public override void Awake(UnitCacheComponent self)
            {
                self.UnitCacheNames.Clear();
                foreach (var type in Game.EventSystem.GetTypes().Values)
                {
                    if (type != typeof(IUnitCache) && typeof(IUnitCache).IsAssignableFrom(type))
                    {
                        self.UnitCacheNames.Add(type.Name);
                    }
                }
                foreach (var key in self.UnitCacheNames)
                {
                    var unitCache = self.AddChild<UnitCache>();
                    unitCache.EntityName = key;
                    self.UnitCaches.Add(key, unitCache);
                }
            }
        }

        public class UnitCacheComponentDestroySystem : DestroySystem<UnitCacheComponent>
        {
            public override void Destroy(UnitCacheComponent self)
            {
                foreach (var unitCache in self.UnitCaches.Values)
                {
                    unitCache?.Dispose();
                }
                self.UnitCaches.Clear();
            }
        }

        public static async ETTask AddOrUpdate(this UnitCacheComponent self, long unitId, ListComponent<Entity> entityList)
        {
            //using ListComponent<Entity> newList = ListComponent<Entity>.Create();
            foreach (var entity in entityList)
            {
                string key = entity.GetType().Name;
                if (!self.UnitCaches.TryGetValue(key, out UnitCache unitCache))
                {
                    unitCache = self.AddChild<UnitCache>();
                    unitCache.EntityName = key;
                    self.UnitCaches[key] = unitCache;
                }
                unitCache.AddOrUpdate(entity);
                //newList.Add(entity);
            }
            //if (newList.Count > 0)
            //{
            //await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(unitId, newList);
            //}
            await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(unitId, entityList);
        }

        public static void Delete(this UnitCacheComponent self, long unitId)
        {
            foreach (var cache in self.UnitCaches.Values)
            {
                cache.Delete(unitId);
            }
        }

        public static async ETTask<Entity> Get(this UnitCacheComponent self, long unitId, string key)
        {
            if (!self.UnitCaches.TryGetValue(key, out var unitCache))
            {
                unitCache = self.AddChild<UnitCache>();
                unitCache.EntityName = key;
                self.UnitCaches[key] = unitCache;
            }
            return await unitCache.Get(unitId);
        }
    }
}