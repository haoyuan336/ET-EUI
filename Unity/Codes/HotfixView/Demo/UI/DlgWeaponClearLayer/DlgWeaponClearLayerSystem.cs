using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgWeaponClearLayerSystem
    {
        public static void RegisterUIEvent(this DlgWeaponClearLayer self)
        {
            self.View.E_BackButton.AddListener(() =>
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_WeaponClearLayer);
            });

            self.View.E_OkButtonButton.AddListenerAsync(self.OnOkButtonClick);
            self.View.E_SpecialClearButton.AddListener(async () =>
            {
                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                await uiComponent.ShowWindow(WindowID.WindowID_WeaponSpecialClearLayer);
                uiComponent.HideWindow(WindowID.WindowID_WeaponClearLayer);
                UIBaseWindow uiBaseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_WeaponSpecialClearLayer);
                uiBaseWindow.GetComponent<DlgWeaponSpecialClearLayer>().SetTargetInfo(self.WeaponInfo,self.CurrentWordBarInfos);
            });
        }

        public static void ShowWindow(this DlgWeaponClearLayer self, Entity contextData = null)
        {
        }

        public static async void SetTargetInfo(this DlgWeaponClearLayer self, WeaponInfo weaponInfo, List<WordBarInfo> wordBarInfos)
        {
            self.WeaponInfo = weaponInfo;
            self.CurrentWordBarInfos = wordBarInfos;
            if (self.WordBarItems.Count == 0)
            {
                WeaponsConfig config = WeaponsConfigCategory.Instance.Get(weaponInfo.ConfigId);
                var wordBarCount = config.WordBarCount;
                GameObject prefab =
                        await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>("Assets/Bundles/UI/Common/ESCommonWordBar.prefab");
                for (int i = 0; i < wordBarCount + 1; i++)
                {
                    var obj = GameObject.Instantiate(prefab);
                    obj.transform.SetParent(self.View.E_WordBarGroupImage.transform);
                    obj.GetComponent<RectTransform>().localScale = Vector3.one;
                    ESCommonWordBar wordBar = self.AddChildWithId<ESCommonWordBar, Transform>(IdGenerater.Instance.GenerateId(), obj.transform);
                    self.WordBarItems.Add(wordBar);
                }
            }

     
            self.SetWordBarInfos(wordBarInfos);
            self.ChooseWordBarInfos.Clear();
            foreach (var item in self.WordBarItems)
            {
                if (item.WordBarInfo != null && !item.WordBarInfo.IsMain)
                {
                    self.ChooseWordBarInfos.Add(item.WordBarInfo);
                }
            }
        }

        public static void SetWordBarInfos(this DlgWeaponClearLayer self, List<WordBarInfo> wordBarInfos)
        {
            self.CurrentWordBarInfos = wordBarInfos;
            for (int i = 0; i < self.WordBarItems.Count; i++)
            {
                var item = self.WordBarItems[i];
                item.E_ClickLockToggle.gameObject.SetActive(false);

                if (i < wordBarInfos.Count)
                {
                    WordBarInfo info = wordBarInfos[i];
                    item.SetInfo(info, self.WeaponInfo);
                    if (!info.IsMain)
                    {
                        item.E_ClickLockToggle.gameObject.SetActive(true);
                        item.E_ClickLockToggle.onValueChanged.RemoveAllListeners();
                        item.E_ClickLockToggle.onValueChanged.AddListener(self.OnWordbarItemClick);
                    }
                }
                else
                {
                    item.SetInfo(null, self.WeaponInfo);
                }
            }
        }

        public static void HideWindow(this DlgWeaponClearLayer self)
        {
            foreach (var item in self.WordBarItems)
            {
                GameObject.Destroy(item.uiTransform.gameObject);
                item.Dispose();
            }

            self.ChooseWordBarInfos.Clear();
            
            self.WordBarItems.Clear();
        }

        public static void OnWordbarItemClick(this DlgWeaponClearLayer self, bool value)
        {
            List<WordBarInfo> wordBarInfos = new List<WordBarInfo>();
            foreach (var item in self.WordBarItems)
            {
                if (!item.E_ClickLockToggle.isOn && item.WordBarInfo != null && !item.WordBarInfo.IsMain)
                {
                    wordBarInfos.Add(item.WordBarInfo);
                }
            }

            Log.Debug($"word bar info {wordBarInfos.Count}");

            self.ChooseWordBarInfos = wordBarInfos;
            // self.View.E_SpecialClearButton
            self.View.E_OkButtonButton.interactable = (wordBarInfos.Count != 0);
        }

        public static async ETTask OnOkButtonClick(this DlgWeaponClearLayer self)
        {
            Log.Debug($"choose word bar count {self.ChooseWordBarInfos.Count}");

            if (self.ChooseWordBarInfos.Count == 0)
            {
                return;
            }

            List<long> idList = new List<long>();
            foreach (var item in self.ChooseWordBarInfos)
            {
                idList.Add(item.WordBarId);
            }

            var account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            var session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            var request = new C2M_WeaponWordBarClearNormalRequest() { AccountId = account, WeaponId = self.WeaponInfo.WeaponId, WordBarIds = idList };

            M2C_WeaponWordBarClearNormalResponse response = (M2C_WeaponWordBarClearNormalResponse) await session.Call(request);

            if (response.Error == ErrorCode.ERR_Success)
            {
                List<WordBarInfo> wordBarInfos = response.WordBarInfos;
                self.SetWordBarInfos(wordBarInfos);

                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_WeaponInfoLayer);
                baseWindow.GetComponent<DlgWeaponInfoLayer>().SetWordBarInfos(wordBarInfos);
            }

            await ETTask.CompletedTask;
        }
    }
}