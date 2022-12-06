using System;

namespace ET
{
    public class Other2UnitCache_DeleteUnitHandler : AMActorRpcHandler<Scene, Other2UnitCache_DeleteUnit, UnitCache2Other_DeleteUnit>
    {
        protected override async ETTask Run(Scene scene, Other2UnitCache_DeleteUnit request, UnitCache2Other_DeleteUnit response, Action reply)
        {
            UnitCacheComponent unitCacheComponent = scene.GetComponent<UnitCacheComponent>();
            unitCacheComponent.Delete(request.UnitId);
            reply();
            await ETTask.CompletedTask;
        }
    }
}