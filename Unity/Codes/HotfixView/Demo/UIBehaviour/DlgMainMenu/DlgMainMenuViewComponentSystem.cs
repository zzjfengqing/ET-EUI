
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgMainMenuViewComponentAwakeSystem : AwakeSystem<DlgMainMenuViewComponent> 
	{
		public override void Awake(DlgMainMenuViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgMainMenuViewComponentDestroySystem : DestroySystem<DlgMainMenuViewComponent> 
	{
		public override void Destroy(DlgMainMenuViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
