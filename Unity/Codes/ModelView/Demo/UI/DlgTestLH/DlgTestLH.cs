using System.Collections.Generic;

namespace ET
{
	public  class DlgTestLH :Entity,IAwake
	{

		public DlgTestLHViewComponent View { get => this.Parent.GetComponent<DlgTestLHViewComponent>();}

		public Dictionary<int, Scroll_Item_serverTestLH> ScrollItemServerTestLhs;

	}
}
