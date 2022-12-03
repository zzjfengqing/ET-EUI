
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_RoleDestroySystem : DestroySystem<Scroll_Item_Role> 
	{
		public override void Destroy( Scroll_Item_Role self )
		{
			self.DestroyWidget();
		}
	}
}
