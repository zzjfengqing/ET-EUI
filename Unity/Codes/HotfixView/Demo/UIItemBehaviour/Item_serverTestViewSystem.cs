
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_ServerTestDestroySystem : DestroySystem<Scroll_Item_ServerTest> 
	{
		public override void Destroy( Scroll_Item_ServerTest self )
		{
			self.DestroyWidget();
		}
	}
}
