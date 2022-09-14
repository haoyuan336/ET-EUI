using System.Collections;
using System.Collections.Generic;
using System;
using ILRuntime.Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace ET
{
    public static class DlgMailInfoLayerSystem
    {
        public static void RegisterUIEvent(this DlgMailInfoLayer self)
        {
            self.View.E_BackButton.AddListener(self.OnBackButtonClick, ConstValue.BackButtonAudioStr);
            self.View.ELoopAwardLoopVerticalScrollRect.AddItemRefreshListener(self.OnLoopEventListener);
            self.View.E_GetClickButton.AddListenerAsync(self.OnGetClickButton);
        }

        public static async ETTask OnGetClickButton(this DlgMailInfoLayer self)
        {
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_ReceiveAllAwardRequest request = new C2M_ReceiveAllAwardRequest() { AccountId = account, OwnerId = self.MailInfo.MailId };
            var response = await session.Call(request) as M2C_ReceiveAllAwardResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                MailInfo info = response.MailInfo;
                self.UpdateMailInfoView(info);

                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_MailLayer);
                if (baseWindow != null)
                {
                    baseWindow.GetComponent<DlgMailLayer>().UpdateMailInfo(info);
                }
            }

            await ETTask.CompletedTask;
        }

        public static void OnLoopEventListener(this DlgMailInfoLayer self, Transform tr, int index)
        {
            Scroll_ItemAward itemAward = self.ItemAwards[index].BindTrans(tr);
            self.InitAwardItemInfo(itemAward, index);
        }

        public static void InitAwardItemInfo(this DlgMailInfoLayer self, Scroll_ItemAward itemAward, int index)
        {
            int[] indexs =
            {
                0, self.AwardHeroCardInfos.Count, self.AwardHeroCardInfos.Count + self.AwardWeaponInfos.Count,
                self.AwardHeroCardInfos.Count + self.AwardWeaponInfos.Count + self.AwardItemInfos.Count
            };
            var value = 0;
            for (int i = 0; i < indexs.Length - 1; i++)
            {
                if (index >= indexs[i] && index <= indexs[i + 1])
                {
                    value = i;
                }
            }

            Log.Debug($"value {value}");

            switch (value)
            {
                case 0:
                    self.InitHeroAwardItemInfo(self.AwardHeroCardInfos[index], itemAward);
                    break;
                case 1:
                    self.InitWeaponAwardItemInfo(self.AwardWeaponInfos[index - self.AwardHeroCardInfos.Count], itemAward);
                    break;
                case 2:
                    self.InitItemAwardItemInfo(self.AwardItemInfos[index - self.AwardHeroCardInfos.Count - self.AwardWeaponInfos.Count], itemAward);
                    break;
            }
        }

        public static async void InitItemAwardItemInfo(this DlgMailInfoLayer self, ItemInfo itemInfo, Scroll_ItemAward itemAward)
        {
            itemAward.E_IconImage.gameObject.SetActive(false);
            itemAward.E_QualityIconImage.gameObject.SetActive(false);
            itemAward.E_ElementIconImage.gameObject.SetActive(false);
            await ETTask.CompletedTask;
        }

        public static async void InitWeaponAwardItemInfo(this DlgMailInfoLayer self, WeaponInfo weaponInfo, Scroll_ItemAward itemAward)
        {
            itemAward.E_IconImage.gameObject.SetActive(true);
            WeaponsConfig config = WeaponsConfigCategory.Instance.Get(weaponInfo.ConfigId);
            itemAward.E_ElementIconImage.gameObject.SetActive(false);
            itemAward.E_QualityIconImage.gameObject.SetActive(false);
            var weaponPath = ConstValue.WeaponAtlasPath;
            itemAward.E_IconImage.sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(weaponPath, config.IconResName);
        }

        public static async void InitHeroAwardItemInfo(this DlgMailInfoLayer self, HeroCardInfo heroCardInfo, Scroll_ItemAward itemAward)
        {
            itemAward.E_IconImage.gameObject.SetActive(true);
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            var spritePath = ConstValue.HeroCardAtlasPath;
            itemAward.E_IconImage.sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spritePath, heroConfig.HeroIconImage);
            var elementConfig = ElementConfigCategory.Instance.Get(heroConfig.HeroColor);
            // itemAward.
            itemAward.E_ElementIconImage.gameObject.SetActive(true);
            itemAward.E_ElementIconImage.sprite =
                    await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spritePath, elementConfig.IconImage);
            var qualityConfig = HeroQualityTypeConfigCategory.Instance.Get(heroConfig.HeroQuality);
            itemAward.E_QualityIconImage.gameObject.SetActive(true);
            itemAward.E_QualityIconImage.sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spritePath, qualityConfig.Icon);
        }

        public static void OnBackButtonClick(this DlgMailInfoLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            uiComponent.HideWindow(WindowID.WindowID_MailInfoLayer);
        }

        public static void ShowWindow(this DlgMailInfoLayer self, Entity contextData = null)
        {
        }

        public static async void SetMailInfo(this DlgMailInfoLayer self, MailInfo mailInfo)
        {
            self.MailInfo = mailInfo;
            self.UpdateMailInfoView(mailInfo);
            await self.RequestMailAward(mailInfo);
        }

        public static void UpdateMailInfoView(this DlgMailInfoLayer self, MailInfo mailInfo)
        {
            self.View.E_MailTitleText.text = mailInfo.Title;
            self.View.E_MailContentText.text = mailInfo.Content;
            self.View.E_GetText.text = mailInfo.IsGet? "已领取" : "领取";
            self.View.E_GetClickButton.interactable = !mailInfo.IsGet;
        }

        public static async ETTask RequestMailAward(this DlgMailInfoLayer self, MailInfo mailInfo)
        {
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_GetAllAwardInfoRequest request = new C2M_GetAllAwardInfoRequest() { AccountId = accountId, OwnerId = mailInfo.MailId };
            M2C_GetAllAwardInfoResponse response = await session.Call(request) as M2C_GetAllAwardInfoResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                self.AwardHeroCardInfos = response.HeroCardInfos;
                self.AwardWeaponInfos = response.WeaponInfos;
                self.AwardItemInfos = response.ItemInfos;

                Log.Debug($"获取邮件奖励信息{self.AwardHeroCardInfos.Count}");
                Log.Debug($"获取邮件奖励信息{self.AwardWeaponInfos.Count}");
                Log.Debug($"获取邮件奖励信息{self.AwardItemInfos.Count}");
                self.AddUIScrollItems(ref self.ItemAwards, self.AwardHeroCardInfos.Count + self.AwardWeaponInfos.Count + self.AwardItemInfos.Count);

                self.View.ELoopAwardLoopVerticalScrollRect.SetVisible(true,
                    self.AwardHeroCardInfos.Count + self.AwardWeaponInfos.Count + self.AwardItemInfos.Count);
            }
        }
    }
}