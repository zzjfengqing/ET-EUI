﻿
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgTestViewComponentAwakeSystem : AwakeSystem<DlgTestViewComponent> 
	{
		public override void Awake(DlgTestViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgTestViewComponentDestroySystem : DestroySystem<DlgTestViewComponent> 
	{
		public override void Destroy(DlgTestViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
