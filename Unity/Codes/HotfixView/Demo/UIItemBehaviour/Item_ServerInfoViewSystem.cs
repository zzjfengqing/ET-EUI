
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_ServerInfoDestroySystem : DestroySystem<Scroll_Item_ServerInfo> 
	{
		public override void Destroy( Scroll_Item_ServerInfo self )
		{
			self.DestroyWidget();
		}
	}
}
