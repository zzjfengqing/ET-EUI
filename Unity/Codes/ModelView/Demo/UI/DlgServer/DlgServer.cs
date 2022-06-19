using System.Collections.Generic;

namespace ET
{
	public  class DlgServer :Entity,IAwake,IUILogic
	{

		public DlgServerViewComponent View { get => this.Parent.GetComponent<DlgServerViewComponent>();}

		public Dictionary<int, Scroll_Item_serverTest> ScrollItemServerTests;

	}
}
