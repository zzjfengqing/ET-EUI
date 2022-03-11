using ET.EventType;
using UnityEngine;

namespace ET
{
    public class AppStartInitFinish_Demo:AEvent<AppStartInitFinish>
    {
        protected override async ETTask Run(AppStartInitFinish a)
        {
            await ETTask.CompletedTask;
            
            Debug.Log("AppStartInitFinish_Demo");
            
        }
    }
}