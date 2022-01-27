namespace ET
{
    public class DlgMainMenu : Entity, IAwake
    {
        public DlgMainMenuViewComponent View { get => this.Parent.GetComponent<DlgMainMenuViewComponent>(); }
        public bool enable;
    }
}