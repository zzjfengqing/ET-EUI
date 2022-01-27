
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_Item_ServerTest : Entity,IAwake,IDestroy 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_ServerTest BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Image EImage_serverTestImage
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
     				if( this.m_EImage_serverTestImage == null )
     				{
		    			this.m_EImage_serverTestImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EImage_serverTest");
     				}
     				return this.m_EImage_serverTestImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EImage_serverTest");
     			}
     		}
     	}

		public UnityEngine.UI.Text EText_serverTestText
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
     				if( this.m_EText_serverTestText == null )
     				{
		    			this.m_EText_serverTestText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EText_serverTest");
     				}
     				return this.m_EText_serverTestText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EText_serverTest");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EImage_serverTestImage = null;
			this.m_EText_serverTestText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_EImage_serverTestImage = null;
		private UnityEngine.UI.Text m_EText_serverTestText = null;
		public Transform uiTransform = null;
	}
}
