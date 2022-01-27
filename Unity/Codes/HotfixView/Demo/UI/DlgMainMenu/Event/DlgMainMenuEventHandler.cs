namespace ET
{
    [AUIEvent(WindowID.WindowID_MainMenu)]
    public class DlgMainMenuEventHandler: IAUIEventHandler
    {
        public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.WindowData.windowType = UIWindowType.Normal;
        }

        public void OnInitComponent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.AddComponent<DlgMainMenuViewComponent>();
            uiBaseWindow.AddComponent<DlgMainMenu>();
        }

        public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<DlgMainMenu>().RegisterUIEvent();
        }

        public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
        {
            uiBaseWindow.GetComponent<DlgMainMenu>().enable = true;
            uiBaseWindow.GetComponent<DlgMainMenu>().ShowWindow(contextData);
        }

        public void OnHideWindow(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<DlgMainMenu>().enable = false;
        }

        public void BeforeUnload(UIBaseWindow uiBaseWindow)
        {
        }
    }
}