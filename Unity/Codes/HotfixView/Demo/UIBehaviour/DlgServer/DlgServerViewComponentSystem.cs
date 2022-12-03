
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgServerViewComponentAwakeSystem : AwakeSystem<DlgServerViewComponent> 
	{
		public override void Awake(DlgServerViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgServerViewComponentDestroySystem : DestroySystem<DlgServerViewComponent> 
	{
		public override void Destroy(DlgServerViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
