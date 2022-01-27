using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgMainMenuSystem
    {
        public static void RegisterUIEvent(this DlgMainMenu self)
        {
            self.View.E_StartGameButton.onClick.RemoveAllListeners();
            self.View.E_StartGameButton.onClick.AddListener(self.StartGame);
        }

        private static async void StartGameTextAnima(this DlgMainMenu self)
        {
            int speed = -1;
            Text text = self.View.E_StartGameText;
            Color tempColor = text.color;
            while (self.enable)
            {
                await TimerComponent.Instance.WaitFrameAsync();
                tempColor.a += Time.deltaTime * speed;
                text.color = tempColor;
                if (text.color.a > 1)
                    speed = -1;
                if (text.color.a < 0)
                    speed = 1;
            }
        }

        private static void StartGame(this DlgMainMenu self)
        {
            self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Test);
            self.ZoneScene().GetComponent<UIComponent>().CloseWindow(WindowID.WindowID_MainMenu);
        }

        public static void ShowWindow(this DlgMainMenu self, Entity contextData = null)
        {
            //self.StartGameTextAnima();
        }
    }
}