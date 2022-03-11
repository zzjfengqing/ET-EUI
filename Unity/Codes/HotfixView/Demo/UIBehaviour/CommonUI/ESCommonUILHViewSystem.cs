
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class ESCommonUILHAwakeSystem : AwakeSystem<ESCommonUILH,Transform> 
	{
		public override void Awake(ESCommonUILH self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}


	[ObjectSystem]
	public class ESCommonUILHDestroySystem : DestroySystem<ESCommonUILH> 
	{
		public override void Destroy(ESCommonUILH self)
		{
			self.DestroyWidget();
		}
	}
}
