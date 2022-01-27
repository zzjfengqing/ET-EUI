using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ET
{
    public static class DlgLoginSystem
    {
        public static void RegisterUIEvent(this DlgLogin self)
        {
            // self.View.E_LoginButton.AddListener(() => { self.OnLoginClickHandler();});
            self.View.E_LoginButton.AddListenerAsync(() => { return self.OnLoginClickHandler(); });
        }

        public static void ShowWindow(this DlgLogin self, Entity contextData = null)
        {
            self.View.ESCommonUI.SetLabelContent("测试界面_登录");
        }

        public static async ETTask OnLoginClickHandler(this DlgLogin self)
        {
            await ETTask.CompletedTask;
            try
            {
                int Error = await LoginHelper.Login(self.DomainScene(),
                    ConstValue.LoginAddress,
                    self.View.E_AccountInputField.GetComponent<InputField>().text,
                    self.View.E_PasswordInputField.GetComponent<InputField>().text);
                if (Error != ErrorCode.ERR_Success)
                {
                    Log.Error(Error.ToString());
                    return;
                }

                Log.Info("登录成功");
                
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Login);
                
                self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Lobby);
                
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void HideWindow(this DlgLogin self)
        {
        }
    }
}