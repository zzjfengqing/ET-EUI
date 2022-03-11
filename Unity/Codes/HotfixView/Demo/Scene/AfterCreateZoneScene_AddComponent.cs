namespace ET
{
    public class AfterCreateZoneScene_AddComponent: AEvent<EventType.AfterCreateZoneScene>
    {
        protected override async ETTask Run(EventType.AfterCreateZoneScene args)
        {
            Scene zoneScene = args.ZoneScene;
            zoneScene.AddComponent<UIComponent>();
            zoneScene.AddComponent<UIPathComponent>();
            zoneScene.AddComponent<UIEventComponent>();
            zoneScene.AddComponent<RedDotComponent>();
            zoneScene.AddComponent<ResourcesLoaderComponent>();
            
            /*
             * 个人理解：
             * 1.zoneScene添加UICommponent脚本后，利用UIComponentSystem，调用IAUIEventHandler中的方法
             * 2.在zoneScene上添加UIEventComponent脚本，该脚本控制IAUIEventHandler，通过反射和生成的的AUIEvent脚本关联，从而实现打开及关闭、以及按钮的响应
             * 3.UIBaseWindows
             * 4.调用流程：zoneScene.GetComponent<UIComponent>().ShowWindow()->UIComponentSystem的ShowWindow()->LoadBaseWindows()-> UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnRegisterUIEvent(baseWindow);
             */
        
            // zoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Login);
            // zoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_TestLH);
            zoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_RedDot);
            await ETTask.CompletedTask;
        }
    }
}