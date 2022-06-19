
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_roleDestroySystem : DestroySystem<Scroll_Item_role> 
	{
		public override void Destroy( Scroll_Item_role self )
		{
			self.DestroyWidget();
		}
	}
}
