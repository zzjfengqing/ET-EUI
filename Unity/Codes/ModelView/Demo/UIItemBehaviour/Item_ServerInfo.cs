
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class Scroll_Item_ServerInfo : Entity,IAwake,IDestroy,IUIScrollItem 
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_ServerInfo BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Button EB_ServerButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_EB_ServerButton == null )
     				{
		    			this.m_EB_ServerButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EB_Server");
     				}
     				return this.m_EB_ServerButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EB_Server");
     			}
     		}
     	}

		public UnityEngine.UI.Image EB_ServerImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_EB_ServerImage == null )
     				{
		    			this.m_EB_ServerImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EB_Server");
     				}
     				return this.m_EB_ServerImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EB_Server");
     			}
     		}
     	}

		public UnityEngine.UI.Text ET_ServerNameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_ET_ServerNameText == null )
     				{
		    			this.m_ET_ServerNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EB_Server/ET_ServerName");
     				}
     				return this.m_ET_ServerNameText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EB_Server/ET_ServerName");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EB_ServerButton = null;
			this.m_EB_ServerImage = null;
			this.m_ET_ServerNameText = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Button m_EB_ServerButton = null;
		private UnityEngine.UI.Image m_EB_ServerImage = null;
		private UnityEngine.UI.Text m_ET_ServerNameText = null;
		public Transform uiTransform = null;
	}
}
