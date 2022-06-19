using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgServerSystem
	{

		public static void RegisterUIEvent(this DlgServer self)
		{
			self.View.E_ConfirmButton.AddListenerAsync(() => { return self.OnConfirmClickHandler(); });
			self.View.E_ServerListLoopVerticalScrollRect.AddItemRefreshListener((Transform transform, int index) =>
			{
				self.OnScrollItemRefreshHandler(transform, index);
			});
		}

		public static void ShowWindow(this DlgServer self, Entity contextData = null)
		{
			int count = self.ZoneScene().GetComponent<ServerInfosComponent>().ServerInfoList.Count;
			self.AddUIScrollItems(ref self.ScrollItemServerTests, count);
			self.View.E_ServerListLoopVerticalScrollRect.SetVisible(true, count);
		}

		public static void HideWindow(this DlgServer self)
		{
			self.RemoveUIScrollItems(ref  self.ScrollItemServerTests);
		}
		    
		public static void OnScrollItemRefreshHandler(this DlgServer self, Transform transform, int index)
		{
			Scroll_Item_serverTest serverTest = self.ScrollItemServerTests[index].BindTrans(transform);
			ServerInfo info = self.ZoneScene().GetComponent<ServerInfosComponent>().ServerInfoList[index];
			serverTest.E_SelectImage.color = info.Id == self.ZoneScene().GetComponent<ServerInfosComponent>().CurrentServerId? Color.red : Color.cyan;
			serverTest.E_serverTestTipText.SetText(info.ServerName);
			serverTest.E_SelectButton.AddListener(() => { self.OnSelectServerItemHandler(info.Id); });
			
		}

		public static void OnSelectServerItemHandler(this DlgServer self, long serverId)
		{
			self.ZoneScene().GetComponent<ServerInfosComponent>().CurrentServerId = int.Parse(serverId.ToString());
			Log.Debug($"当前选择的服务器 Id 是:{serverId}");
			self.View.E_ServerListLoopVerticalScrollRect.RefillCells();
		}

		public static async ETTask OnConfirmClickHandler(this DlgServer self)
		{
			bool isSelect = self.ZoneScene().GetComponent<ServerInfosComponent>().CurrentServerId != 0;

			if (!isSelect)
			{
				Log.Error("请先选择区服");
			}

			try
			{
				int errorCode = await LoginHelper.GetRoles(self.ZoneScene());
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				
				self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Roles);
				self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Server);
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
		}

	}
}
