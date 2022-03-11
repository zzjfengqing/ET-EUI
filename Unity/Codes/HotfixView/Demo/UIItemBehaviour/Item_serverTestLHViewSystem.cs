
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_serverTestLHDestroySystem : DestroySystem<Scroll_Item_serverTestLH> 
	{
		public override void Destroy( Scroll_Item_serverTestLH self )
		{
			self.DestroyWidget();
		}
	}
}
