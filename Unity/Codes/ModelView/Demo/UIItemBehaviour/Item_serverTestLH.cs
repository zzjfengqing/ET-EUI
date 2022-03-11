
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_Item_serverTestLH : Entity,IAwake,IDestroy 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_serverTestLH BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Button E_BtnButton
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
     				if( this.m_E_BtnButton == null )
     				{
		    			this.m_E_BtnButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Btn");
     				}
     				return this.m_E_BtnButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Btn");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_BtnImage
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
     				if( this.m_E_BtnImage == null )
     				{
		    			this.m_E_BtnImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Btn");
     				}
     				return this.m_E_BtnImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Btn");
     			}
     		}
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

		public UnityEngine.UI.Text ELabel_serverTestText
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
     				if( this.m_ELabel_serverTestText == null )
     				{
		    			this.m_ELabel_serverTestText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"ELabel_serverTest");
     				}
     				return this.m_ELabel_serverTestText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"ELabel_serverTest");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BtnButton = null;
			this.m_E_BtnImage = null;
			this.m_EImage_serverTestImage = null;
			this.m_ELabel_serverTestText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BtnButton = null;
		private UnityEngine.UI.Image m_E_BtnImage = null;
		private UnityEngine.UI.Image m_EImage_serverTestImage = null;
		private UnityEngine.UI.Text m_ELabel_serverTestText = null;
		public Transform uiTransform = null;
	}
}
