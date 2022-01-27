
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class ESCommonUI : Entity,ET.IAwake<UnityEngine.Transform>,IDestroy 
	{
		public UnityEngine.UI.Image EImage_Test1Image
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EImage_Test1Image == null )
     			{
		    		this.m_EImage_Test1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EImage_Test1");
     			}
     			return this.m_EImage_Test1Image;
     		}
     	}

		public UnityEngine.UI.Text ELabel_Test2Text
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_Test2Text == null )
     			{
		    		this.m_ELabel_Test2Text = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"ELabel_Test2");
     			}
     			return this.m_ELabel_Test2Text;
     		}
     	}

		public UnityEngine.UI.Button E_ClickButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ClickButton == null )
     			{
		    		this.m_E_ClickButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Click");
     			}
     			return this.m_E_ClickButton;
     		}
     	}

		public UnityEngine.UI.Image E_ClickImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ClickImage == null )
     			{
		    		this.m_E_ClickImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Click");
     			}
     			return this.m_E_ClickImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EImage_Test1Image = null;
			this.m_ELabel_Test2Text = null;
			this.m_E_ClickButton = null;
			this.m_E_ClickImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_EImage_Test1Image = null;
		private UnityEngine.UI.Text m_ELabel_Test2Text = null;
		private UnityEngine.UI.Button m_E_ClickButton = null;
		private UnityEngine.UI.Image m_E_ClickImage = null;
		public Transform uiTransform = null;
	}
}
