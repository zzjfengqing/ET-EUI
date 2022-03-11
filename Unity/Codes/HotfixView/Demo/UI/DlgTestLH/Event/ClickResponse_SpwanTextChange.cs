using UnityEngine;

namespace ET
{
    public class ClickResponse_SpwanTextChange:AEvent<LHEventType.ClickResponse>
    {
        protected override async ETTask Run(LHEventType.ClickResponse a)
        {
            Debug.Log($"---------------------");
            await ETTask.CompletedTask;
        }

       
    }
    
}