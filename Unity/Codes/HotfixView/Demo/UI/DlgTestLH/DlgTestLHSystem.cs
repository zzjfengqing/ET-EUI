using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgTestLHSystem
	{
		/// <summary>
		/// 注册按钮
		/// </summary>
		/// <param name="self"></param>
		public static void RegisterUIEvent(this DlgTestLH self)
		{
			self.View.E_EnterMapButton.AddListener(() => { self.OnClickEnterMap();});
			self.View.E_TestButton.AddListener(OnclickTest);
			//循环列表项
			self.View.ELoopScrollList_TestLoopHorizontalScrollRect.AddItemRefreshListener((Transform transform, int index) =>
			{
				OnLoopListItemRefresHandler(self,transform, index);
				
			});
		}
		
		/// <summary>
		/// 显示文本
		/// 这个走的是，真正打开UI的方法
		/// </summary>
		/// <param name="self"></param>
		/// <param name="contextData"></param>
		public static void ShowWindow(this DlgTestLH self, Entity contextData = null)
		{
			self.View.E_titleText.text = "1234567890";
			//公共UI
			self.View.ESCommonUILH.SetLabelContent("测试界面");
			
			//生成滚动列表
			int count = 100;
			self.AddUIScrollItems(ref self.ScrollItemServerTestLhs,count);
			self.View.ELoopScrollList_TestLoopHorizontalScrollRect.SetVisible(true,count);
		}

		public static void HideWindow(this DlgTestLH self)
		{
			self.RemoveUIScrollItems(ref self.ScrollItemServerTestLhs);
		}
		
		//------------------------------------------------------------------------------------------------------------------------------------------
		private static void OnclickTest()
		{
			Debug.Log("E_TestButton");
		}

		private static void OnClickEnterMap(this DlgTestLH self)
		{
			Debug.Log("Enter map");
		}

		private static void OnLoopListItemRefresHandler(this DlgTestLH self, Transform transform,int index)
		{
			Scroll_Item_serverTestLH scrollItemServerTestLh = self.ScrollItemServerTestLhs[index].BindTrans(transform);

			scrollItemServerTestLh.ELabel_serverTestText.text = $"{index}服";
			scrollItemServerTestLh.E_BtnButton.AddListener(() => {self.ScrollItemClick(); });
		}

		private static void ScrollItemClick(this DlgTestLH self)
		{
			Game.EventSystem.Publish(new LHEventType.ClickResponse(){});

		}
	}
}
