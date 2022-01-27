using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgTestSystem
    {
        public static void RegisterUIEvent(this DlgTest self)
        {
            self.View.E_EnterMapButton.onClick.AddListener(self.OnEnterMapClickHandler);
            self.View.E_btn_TestButton.AddListener(self.TestAction);
            self.View.ELoopScrollList_TestLoopHorizontalScrollRect.AddItemRefreshListener
                ((tran, i) => self.OnLoopListItemRefreshHandler(tran, i));
        }

        public static void ShowWindow(this DlgTest self, Entity contextData = null)
        {
            self.View.E_text_TestText.text = "测试文本";
            self.View.ESCommonUI.SetLabelContent("开始界面");
            int creatNum = 10000;
            self.AddUIScrollItems(ref self.ScrollItemServerTestDic, creatNum);
            self.View.ELoopScrollList_TestLoopHorizontalScrollRect.SetVisible(true, creatNum);
            self.View.ESCommonUI.E_ClickButton.onClick.RemoveAllListeners();
            self.View.ESCommonUI.E_ClickButton.onClick.AddListener(() =>
            {
                self.View.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_RedDot);
                self.View.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Test);
            });
        }

        public static void HideWindow(this DlgTest self)
        {
            self.RemoveUIScrollItems(ref self.ScrollItemServerTestDic);
        }

        public static void OnEnterMapClickHandler(this DlgTest self)
        {
            //Log.Debug("Enter Map");
            Scene zoneScene = self.ZoneScene();
            zoneScene.GetComponent<UIComponent>().CloseWindow(WindowID.WindowID_Test);
            zoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_RedDot);
        }

        public static void TestAction(this DlgTest dlg)
        {
            Log.Debug("Test");
        }

        public static void OnLoopListItemRefreshHandler(this DlgTest self, Transform tran, int index)
        {
            Scroll_Item_ServerTest item = self.ScrollItemServerTestDic[index].BindTrans(tran);
            item.EText_serverTestText.text = $"测试文本{index}";
        }
    }
}