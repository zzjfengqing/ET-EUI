
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[EnableMethod]
	public  class Scroll_Item_Role : Entity,IAwake,IDestroy,IUIScrollItem 
	{
		public long DataId {get;set;}
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_Role BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Button EB_RoleSelectButton
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
     				if( this.m_EB_RoleSelectButton == null )
     				{
		    			this.m_EB_RoleSelectButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EB_RoleSelect");
     				}
     				return this.m_EB_RoleSelectButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EB_RoleSelect");
     			}
     		}
     	}

		public UnityEngine.UI.Image EB_RoleSelectImage
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
     				if( this.m_EB_RoleSelectImage == null )
     				{
		    			this.m_EB_RoleSelectImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EB_RoleSelect");
     				}
     				return this.m_EB_RoleSelectImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EB_RoleSelect");
     			}
     		}
     	}

		public UnityEngine.UI.Text EL_RoleText
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
     				if( this.m_EL_RoleText == null )
     				{
		    			this.m_EL_RoleText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EL_Role");
     				}
     				return this.m_EL_RoleText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EL_Role");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EB_RoleSelectButton = null;
			this.m_EB_RoleSelectImage = null;
			this.m_EL_RoleText = null;
			this.uiTransform = null;
			this.DataId = 0;
		}

		private UnityEngine.UI.Button m_EB_RoleSelectButton = null;
		private UnityEngine.UI.Image m_EB_RoleSelectImage = null;
		private UnityEngine.UI.Text m_EL_RoleText = null;
		public Transform uiTransform = null;
	}
}
