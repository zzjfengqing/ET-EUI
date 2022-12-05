using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class UnitCacheHelper
    {
        /// <summary>
        /// 保存或更新玩家缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        public static async void AddOrUpdateUnitCache<T>(this T self) where T : Entity, IUnitCache
        {
            var message = new Other2UnitCache_AddOrUpdateUnit() { UnitId = self.Id, };
            message.EntityTypes.Add(typeof(T).FullName);
            message.EntityBytes.Add(MongoHelper.ToBson(self));
            await MessageHelper.CallActor(StartSceneConfigCategory.Instance.GetUnitCacheConfig(self.Id).InstanceId, message);
        }

        /// <summary>
        /// 获取玩家组件缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static async ETTask<T> GetUnitComponentCache<T>(long unitId) where T : Entity, IUnitCache
        {
            var message = new Other2UnitCache_GetUnit() { UnitId = unitId, };
            message.ComponentNames.Add(typeof(T).Name);
            long instanceId = StartSceneConfigCategory.Instance.GetUnitCacheConfig(unitId).InstanceId;
            var cacheResponse = await MessageHelper.CallActor(instanceId, message) as UnitCache2Other_GetUnit;
            if (cacheResponse.Error == ErrorCode.ERR_Success && cacheResponse.Entities.Count > 0)
            {
                return cacheResponse.Entities[0] as T;
            }
            return null;
        }

        /// <summary>
        /// 删除玩家缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="unitId"></param>
        public static async void DeleteUnitCache<T>(long unitId) where T : Entity, IUnitCache
        {
            var message = new Other2UnitCache_DeleteUnit() { UnitId = unitId, };
            long instanceId = StartSceneConfigCategory.Instance.GetUnitCacheConfig(unitId).InstanceId;
            await MessageHelper.CallActor(instanceId, message);
        }
    }
}