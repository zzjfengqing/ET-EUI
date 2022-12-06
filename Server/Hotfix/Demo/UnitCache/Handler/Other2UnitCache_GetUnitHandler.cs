using System;
using System.Collections.Generic;

namespace ET
{
    [FriendClassAttribute(typeof(ET.UnitCacheComponent))]
    public class Other2UnitCache_GetUnitHandler : AMActorRpcHandler<Scene, Other2UnitCache_GetUnit, UnitCache2Other_GetUnit>
    {
        protected override async ETTask Run(Scene scene, Other2UnitCache_GetUnit request, UnitCache2Other_GetUnit response, Action reply)
        {
            UnitCacheComponent unitCacheComponent = scene.GetComponent<UnitCacheComponent>();
            Dictionary<string, Entity> dictionary = MonoPool.Instance.Fetch(typeof(Dictionary<string, Entity>)) as Dictionary<string, Entity>;
            try
            {
                if (request.ComponentNames.Count == 0)
                {
                    dictionary.Add(nameof(Unit), null);
                    foreach (var key in unitCacheComponent.UnitCacheNames)
                    {
                        dictionary.Add(key, null);
                    }
                }
                else
                {
                    foreach (var name in request.ComponentNames)
                    {
                        dictionary.Add(name, null);
                    }
                }
                foreach (var name in dictionary.Keys)
                {
                    Entity entity = await unitCacheComponent.Get(request.UnitId, name);
                    dictionary[name] = entity;
                    response.ComponentNames.Add(name);
                    response.Entities.Add(entity);
                }
            }
            finally
            {
                dictionary.Clear();
                MonoPool.Instance.Recycle(dictionary);
            }
            reply();
            await ETTask.CompletedTask;
        }
    }
}