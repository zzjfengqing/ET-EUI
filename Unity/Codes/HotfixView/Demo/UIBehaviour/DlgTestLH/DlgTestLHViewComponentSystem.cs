
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgTestLHViewComponentAwakeSystem : AwakeSystem<DlgTestLHViewComponent> 
	{
		public override void Awake(DlgTestLHViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgTestLHViewComponentDestroySystem : DestroySystem<DlgTestLHViewComponent> 
	{
		public override void Destroy(DlgTestLHViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
