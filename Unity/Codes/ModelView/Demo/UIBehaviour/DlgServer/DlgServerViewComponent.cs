
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgServerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.LoopVerticalScrollRect EL_ServerListLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EL_ServerListLoopVerticalScrollRect == null )
     			{
		    		this.m_EL_ServerListLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"BackGround/EL_ServerList");
     			}
     			return this.m_EL_ServerListLoopVerticalScrollRect;
     		}
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
     			if( this.m_EB_ServerButton == null )
     			{
		    		this.m_EB_ServerButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BackGround/EL_ServerList/Content/Item_ServerInfo/EB_Server");
     			}
     			return this.m_EB_ServerButton;
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
     			if( this.m_EB_ServerImage == null )
     			{
		    		this.m_EB_ServerImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BackGround/EL_ServerList/Content/Item_ServerInfo/EB_Server");
     			}
     			return this.m_EB_ServerImage;
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
     			if( this.m_ET_ServerNameText == null )
     			{
		    		this.m_ET_ServerNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"BackGround/EL_ServerList/Content/Item_ServerInfo/EB_Server/ET_ServerName");
     			}
     			return this.m_ET_ServerNameText;
     		}
     	}

		public UnityEngine.UI.Button EB_ConfirmButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EB_ConfirmButton == null )
     			{
		    		this.m_EB_ConfirmButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BackGround/EB_Confirm");
     			}
     			return this.m_EB_ConfirmButton;
     		}
     	}

		public UnityEngine.UI.Image EB_ConfirmImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EB_ConfirmImage == null )
     			{
		    		this.m_EB_ConfirmImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BackGround/EB_Confirm");
     			}
     			return this.m_EB_ConfirmImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EL_ServerListLoopVerticalScrollRect = null;
			this.m_EB_ServerButton = null;
			this.m_EB_ServerImage = null;
			this.m_ET_ServerNameText = null;
			this.m_EB_ConfirmButton = null;
			this.m_EB_ConfirmImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.LoopVerticalScrollRect m_EL_ServerListLoopVerticalScrollRect = null;
		private UnityEngine.UI.Button m_EB_ServerButton = null;
		private UnityEngine.UI.Image m_EB_ServerImage = null;
		private UnityEngine.UI.Text m_ET_ServerNameText = null;
		private UnityEngine.UI.Button m_EB_ConfirmButton = null;
		private UnityEngine.UI.Image m_EB_ConfirmImage = null;
		public Transform uiTransform = null;
	}
}
