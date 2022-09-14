using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public static class DlgPVESceneLayerSystem
    {
        public static void RegisterUIEvent(this DlgPVESceneLayer self)
        {
            self.View.E_StartGameButton.AddListenerAsync(self.StartGameClickAction, ConstValue.MakeSureFightAudioStr);
            self.View.E_BackButton.AddListener(self.OnBackButtonClick,ConstValue.BackButtonAudioStr);
        }
        public static async ETTask<bool> CheckPowerIsEnough(this DlgPVESceneLayer self)
        {
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_GetGoldInfoResponse response = (M2C_GetGoldInfoResponse) await session.Call(new C2M_GetGoldInfoRequest() { AccountId = AccountId });
            if (response.Error == ErrorCode.ERR_Success)
            {
             
                var powerCount = 0;
                foreach (var itemCountInfo in response.ItemInfos)
                {
                    if (itemCountInfo.ConfigId == 1003)
                    {
                        powerCount = itemCountInfo.Count;
                    }
                }

                if (powerCount > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public static async ETTask StartGameClickAction(this DlgPVESceneLayer self)
        {
            
            bool isPowerEnough = await self.CheckPowerIsEnough();
            if (!isPowerEnough)
            {
                await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_PowerNotEnoughAlert);
                return;
            }

            var Account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;

            var inputText = self.View.E_LevelInputInputField.text;
            if (!string.IsNullOrEmpty(inputText))
            {
                Log.Debug($"玩家输入了关卡数{inputText}");
                if (int.Parse(inputText) >= LevelConfigCategory.Instance.GetAll().Count)
                {
                    await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_AlertLayer);

                    self.DomainScene().GetComponent<UIComponent>().GetUIBaseWindow(WindowID.WindowID_AlertLayer).GetComponent<DlgAlertLayer>()
                            .SetText("输入的关卡数超出配置的关卡数");
                    return;
                }

                var response = await session.Call(new C2M_PlayerChooseLevelNumRequest() { Account = Account, LevelNum = int.Parse(inputText) });
                if (response.Error != ErrorCode.ERR_Success)
                {
                    return;
                }
            }
            
            M2C_StartPVEGameResponse m2CStartPveGameResponse =
                    (M2C_StartPVEGameResponse) await session.Call(new C2M_StartPVEGameRequest()
                    {
                        AccountId = Account
                    });
            if (m2CStartPveGameResponse.Error == ErrorCode.ERR_Success)
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_PVESceneLayer);
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GoldInfoUI);
            }
        }
        public static async  void ShowWindow(this DlgPVESceneLayer self, Entity contextData = null)
        {

            // await self.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_EditorTroopLayer);
            await ETTask.CompletedTask;
        }
        public static void OnBackButtonClick(this DlgPVESceneLayer self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_PVESceneLayer);
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainScene).Coroutine();
        }

        public static void HideWindow(this DlgPVESceneLayer self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_BackButton);
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_EditorTroopLayer);
        }
    }
}