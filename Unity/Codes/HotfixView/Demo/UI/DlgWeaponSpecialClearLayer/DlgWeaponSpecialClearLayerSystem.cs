using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgWeaponSpecialClearLayerSystem
    {
        public static void RegisterUIEvent(this DlgWeaponSpecialClearLayer self)
        {
            self.View.E_BaseClearButton.AddListenerAsync(self.OnBaseClearButtonClick);
            self.View.E_BackButton.AddListener(() =>
            {
                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                uiComponent.HideWindow(WindowID.WindowID_WeaponSpecialClearLayer);
            },ConstValue.BackButtonAudioStr);
            self.View.E_OkButtonButton.AddListenerAsync(self.OnOkButtonClick);
        }

        public static async ETTask OnOkButtonClick(this DlgWeaponSpecialClearLayer self)
        {
            // Log.Debug($"choose word bar infos {self.ChooseWordBarInfos.Count}");
            // await ETTask.CompletedTask;
            List<long> wordBarIds = new List<long>();
            foreach (var wordBarInfo in self.ChooseWordBarInfos)
            {
                wordBarIds.Add(wordBarInfo.WordBarId);
            }

            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_WeaponWordBarSpeicalClearRequest request = new C2M_WeaponWordBarSpeicalClearRequest()
            {
                AccountId = accountId, WeaponId = self.WeaponInfo.WeaponId, WordBarIds = wordBarIds
            };
            M2C_WeaponWordBarSpeicalClearResponse response = (M2C_WeaponWordBarSpeicalClearResponse) await session.Call(request);
            if (response.Error == ErrorCode.ERR_Success)
            {
                self.WordBarInfos = response.WordBarInfos;

                Log.Debug($"word bar infos  {self.WordBarInfos.Count}");
                self.SetTargetInfo(self.WeaponInfo, self.WordBarInfos);

                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                UIBaseWindow baseWindow =  uiComponent.GetUIBaseWindow(WindowID.WindowID_WeaponInfoLayer);
                baseWindow.GetComponent<DlgWeaponInfoLayer>().SetWordBarInfos(self.WordBarInfos);
            }
        }

        public static async ETTask OnBaseClearButtonClick(this DlgWeaponSpecialClearLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_WeaponClearLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_WeaponClearLayer);
            baseWindow.GetComponent<DlgWeaponClearLayer>().SetTargetInfo(self.WeaponInfo, self.WordBarInfos);
            uiComponent.HideWindow(WindowID.WindowID_WeaponSpecialClearLayer);
        }

        public static void ShowWindow(this DlgWeaponSpecialClearLayer self, Entity contextData = null)
        {
            self.View.E_OkButtonButton.interactable = false;
        }

        public static void HideWindow(this DlgWeaponSpecialClearLayer self)
        {
            foreach (var bar in self.ESCommonWordBars)
            {
                GameObject.Destroy(bar.uiTransform.gameObject);
                bar.Dispose();
            }

            self.ESCommonWordBars.Clear();
        }

        public static async void SetTargetInfo(this DlgWeaponSpecialClearLayer self, WeaponInfo weaponInfo, List<WordBarInfo> wordBarInfos)
        {
            self.WeaponInfo = weaponInfo;
            self.WordBarInfos = wordBarInfos;
            if (self.ESCommonWordBars.Count == 0)
            {
                GameObject prefab =
                        await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>("Assets/Bundles/UI/Common/ESCommonWordBar.prefab");
                WeaponsConfig config = WeaponsConfigCategory.Instance.Get(weaponInfo.ConfigId);
                for (int i = 0; i < config.WordBarCount + 1; i++)
                {
                    GameObject obj = GameObject.Instantiate(prefab, self.View.E_WordBarGroupImage.transform);
                    obj.GetComponent<RectTransform>().localScale = Vector3.one;
                    ESCommonWordBar bar = self.AddChildWithId<ESCommonWordBar, Transform>(IdGenerater.Instance.GenerateId(), obj.transform);
                    self.ESCommonWordBars.Add(bar);
                }
            }

            for (int i = 0; i < self.ESCommonWordBars.Count; i++)
            {
                ESCommonWordBar bar = self.ESCommonWordBars[i];

                if (i < self.WordBarInfos.Count)
                {
                    bar.SetInfo(self.WordBarInfos[i], self.WeaponInfo);
                    bar.E_ClickLockToggle.gameObject.SetActive(!self.WordBarInfos[i].IsMain);
                    bar.E_ClickLockToggle.onValueChanged.RemoveAllListeners();
                    bar.E_ClickLockToggle.AddListener(self.OnWordBarItemClick);
                }
                else
                {
                    bar.SetInfo(null, self.WeaponInfo);
                }
            }
        }

        public static void OnWordBarItemClick(this DlgWeaponSpecialClearLayer self, bool value)
        {
            //
            self.ChooseWordBarInfos.Clear();
            foreach (var bar in self.ESCommonWordBars)
            {
                if (bar.E_ClickLockToggle.isOn && bar.WordBarInfo != null && !bar.WordBarInfo.IsMain)
                {
                    self.ChooseWordBarInfos.Add(bar.WordBarInfo);
                }
            }

            self.View.E_OkButtonButton.interactable = self.ChooseWordBarInfos.Count > 0;
        }
    }
}