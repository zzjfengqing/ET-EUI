using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgRedDotSystem
    {
        public static void RegisterUIEvent(this DlgRedDot self)
        {
            self.View.EButton_BagNode1Button.AddListener(() => self.OnBagNode1ClickHandler());
            self.View.EButton_BagNode2Button.AddListener(() => self.OnBagNode2ClickHandler());
            self.View.EButton_MailNode1Button.AddListener(() => self.OnMailNode1ClickHandler());
            self.View.EButton_MailNode2Button.AddListener(() => self.OnMailNode2ClickHandler());
        }

        public static void HideWindow(this DlgRedDot self)
        {
            RedDotHelper.RemoveRedDotNode(self.ZoneScene(), "Root");
        }

        public static void ShowWindow(this DlgRedDot self, Entity contextData = null)
        {
            self.ChangeESTile();
            //逻辑层
            var rdc = self.ZoneScene().GetComponent<RedDotComponent>();
            if (rdc.ToParentDict.ContainsKey(self.View.EButton_rootButton.name)) return;
            RedDotHelper.AddRedDotNode(self.ZoneScene(), "Root", self.View.EButton_rootButton.name, true);

            RedDotHelper.AddRedDotNode(self.ZoneScene(), self.View.EButton_rootButton.name, self.View.EButton_BagButton.name, true);
            RedDotHelper.AddRedDotNode(self.ZoneScene(), self.View.EButton_rootButton.name, self.View.EButton_MailButton.name, true);

            RedDotHelper.AddRedDotNode(self.ZoneScene(), self.View.EButton_BagButton.name, self.View.EButton_BagNode1Button.name, true);
            RedDotHelper.AddRedDotNode(self.ZoneScene(), self.View.EButton_BagButton.name, self.View.EButton_BagNode2Button.name, true);

            RedDotHelper.AddRedDotNode(self.ZoneScene(), self.View.EButton_MailButton.name, self.View.EButton_MailNode1Button.name, true);
            RedDotHelper.AddRedDotNode(self.ZoneScene(), self.View.EButton_MailButton.name, self.View.EButton_MailNode2Button.name, true);

            //显示层
            RedDotMonoView redDotMonoView = self.View.EButton_rootButton.GetComponent<RedDotMonoView>();
            RedDotHelper.AddRedDotNodeView(self.ZoneScene(), self.View.EButton_rootButton.name, redDotMonoView);

            RedDotHelper.AddRedDotNodeView(self.ZoneScene(), self.View.EButton_BagButton.name, self.View.EButton_BagButton.gameObject, Vector3.one,
                Vector3.zero);
            RedDotHelper.AddRedDotNodeView(self.ZoneScene(), self.View.EButton_BagNode1Button.name, self.View.EButton_BagNode1Button.gameObject,
                Vector3.one, Vector3.zero);
            RedDotHelper.AddRedDotNodeView(self.ZoneScene(), self.View.EButton_BagNode2Button.name, self.View.EButton_BagNode2Button.gameObject,
                Vector3.one, Vector3.zero);

            RedDotHelper.AddRedDotNodeView(self.ZoneScene(), self.View.EButton_MailButton.name, self.View.EButton_MailButton.gameObject, Vector3.one,
                Vector3.zero);
            RedDotHelper.AddRedDotNodeView(self.ZoneScene(), self.View.EButton_MailNode1Button.name, self.View.EButton_MailNode1Button.gameObject,
                Vector3.one, Vector3.zero);
            RedDotHelper.AddRedDotNodeView(self.ZoneScene(), self.View.EButton_MailNode2Button.name, self.View.EButton_MailNode2Button.gameObject,
                Vector3.one, Vector3.zero);

            RedDotHelper.ShowRedDotNode(self.ZoneScene(), self.View.EButton_BagNode1Button.name);
            RedDotHelper.ShowRedDotNode(self.ZoneScene(), self.View.EButton_BagNode2Button.name);
            RedDotHelper.ShowRedDotNode(self.ZoneScene(), self.View.EButton_MailNode1Button.name);
            RedDotHelper.ShowRedDotNode(self.ZoneScene(), self.View.EButton_MailNode2Button.name);

            RedDotHelper.RefreshRedDotViewCount(self.ZoneScene(), self.View.EButton_BagNode1Button.name, self.RedDotBagCount1);
            RedDotHelper.RefreshRedDotViewCount(self.ZoneScene(), self.View.EButton_BagNode2Button.name, self.RedDotBagCount2);
            RedDotHelper.RefreshRedDotViewCount(self.ZoneScene(), self.View.EButton_MailNode1Button.name, self.RedDotMailCount1);
            RedDotHelper.RefreshRedDotViewCount(self.ZoneScene(), self.View.EButton_MailNode2Button.name, self.RedDotMailCount2);
        }

        public static void ChangeESTile(this DlgRedDot self)
        {
            self.View.ESCommonUI.SetLabelContent("红点界面");
            self.View.ESCommonUI.E_ClickButton.onClick.RemoveAllListeners();
            self.View.ESCommonUI.E_ClickButton.onClick.AddListener(() =>
            {
                self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Test);
                self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_RedDot);
            });
        }

        public static void OnBagNode1ClickHandler(this DlgRedDot self)
        {
            self.RedDotBagCount1 += 1;
            RedDotHelper.RefreshRedDotViewCount(self.ZoneScene(), self.View.EButton_BagNode1Button.name, self.RedDotBagCount1);
        }

        public static void OnBagNode2ClickHandler(this DlgRedDot self)
        {
            self.RedDotBagCount2 -= 1;
            RedDotHelper.RefreshRedDotViewCount(self.ZoneScene(), self.View.EButton_BagNode2Button.name, self.RedDotBagCount2);
        }

        public static void OnMailNode1ClickHandler(this DlgRedDot self)
        {
            self.RedDotMailCount1 += 1;
            RedDotHelper.RefreshRedDotViewCount(self.ZoneScene(), self.View.EButton_MailNode1Button.name, self.RedDotMailCount1);
        }

        public static void OnMailNode2ClickHandler(this DlgRedDot self)
        {
            self.RedDotMailCount2 -= 1;
            RedDotHelper.RefreshRedDotViewCount(self.ZoneScene(), self.View.EButton_MailNode2Button.name, self.RedDotMailCount2);
        }
    }
}