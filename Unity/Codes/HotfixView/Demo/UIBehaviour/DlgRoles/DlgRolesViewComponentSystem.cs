
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgRolesViewComponentAwakeSystem : AwakeSystem<DlgRolesViewComponent> 
	{
		public override void Awake(DlgRolesViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgRolesViewComponentDestroySystem : DestroySystem<DlgRolesViewComponent> 
	{
		public override void Destroy(DlgRolesViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
