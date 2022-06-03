using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgWeaponInfoLayerSystem
    {
        public static void RegisterUIEvent(this DlgWeaponInfoLayer self)
        {
            self.View.E_BackButton.AddListener(() =>
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_WeaponInfoLayer);
            });
        }

        public static void HideWindow(this DlgWeaponInfoLayer self)
        {
            if (self.CurrentHeroCardItem != null)
            {
                GameObject.Destroy(self.CurrentHeroCardItem.uiTransform.gameObject);
                self.CurrentHeroCardItem?.Dispose();
                self.CurrentHeroCardItem = null;
            }
        }

        public static async ETTask InitWordBarItems(this DlgWeaponInfoLayer self)
        {
            if (self.WordBarItems.Count != 0)
            {
                return;
            }

            var prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>("Assets/Bundles/UI/Common/ESCommonWordBar.prefab");
            for (int i = 0; i < 4; i++)
            {
                // var gameObject = AddressableComponent.
                var gameObject = GameObject.Instantiate(prefab);
                gameObject.transform.SetParent(self.View.E_WordBarGroupImage.transform);
                gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
                var esCommonWordBar = self.AddChildWithId<ESCommonWordBar, Transform>(IdGenerater.Instance.GenerateId(), gameObject.transform);
                self.WordBarItems.Add(esCommonWordBar);
            }
        }

        public static void ShowWindow(this DlgWeaponInfoLayer self, Entity contextData = null)
        {
        }

        public static async void SetTargetInfo(this DlgWeaponInfoLayer self, WeaponInfo weaponInfo)
        {
            self.WeaponInfo = weaponInfo;
            await self.InitCurrentWeaponItem(weaponInfo);
            await self.InitWordBarItems();
            await self.InitOnWeaponHeroItem();
            await self.GetWeaponWordsInfoRequest();
        }

        public static async ETTask InitOnWeaponHeroItem(this DlgWeaponInfoLayer self)
        {
            //首先取出来装备此装备的英雄
            var heroId = self.WeaponInfo.OnWeaponHeroId;
            //获取此英雄信息
            Log.Debug($"hero id {heroId}");
            Log.Debug($"weapon id {self.WeaponInfo.WeaponId}");
            if (heroId != 0)
            {
                if (self.CurrentHeroCardItem == null)
                {
                    self.CurrentHeroCardItem = self.AddChildWithId<Scroll_ItemHeroCard>(IdGenerater.Instance.GenerateId());
                    var obj = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath("ItemHeroCard");
                    obj.transform.SetParent(self.View.E_CurrentHeroPosImage.transform);
                    self.CurrentHeroCardItem.BindTrans(obj.transform);
                    RectTransform rectTransform = obj.GetComponent<RectTransform>();
                    rectTransform.offsetMax = Vector2.zero;
                    rectTransform.offsetMin = Vector2.zero;
                    rectTransform.anchorMin = Vector2.zero;
                    rectTransform.anchorMax = Vector2.one;
                    rectTransform.localScale = Vector3.one;
                }
                // self.CurrentHeroCardItem.InitHeroCard();

                long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
                Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
                C2M_GetHeroCardByIdRequest request = new C2M_GetHeroCardByIdRequest() { Account = account, HeroId = heroId };
                M2C_GetHeroCardByIdResponse response = (M2C_GetHeroCardByIdResponse) await session.Call(request);

                if (response.Error == ErrorCode.ERR_Success)
                {
                    self.CurrentHeroCardItem.InitHeroCard(response.HeroCardInfo);
                }
            }
        }

        public static async ETTask InitCurrentWeaponItem(this DlgWeaponInfoLayer self, WeaponInfo weaponInfo)
        {
            if (self.CurerntWeaponItem == null)
            {
                self.CurerntWeaponItem = self.AddChildWithId<Scroll_ItemWeapon>(IdGenerater.Instance.GenerateId());
                var obj = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath("ItemWeapon");
                obj.transform.SetParent(self.View.E_CurrentWeaponPosImage.transform);
                self.CurerntWeaponItem.BindTrans(obj.transform);
                RectTransform rectTransform = obj.GetComponent<RectTransform>();
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;
                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;
                rectTransform.localScale = Vector3.one;
            }

            self.CurerntWeaponItem.InitWeaponCardView(weaponInfo);

            WeaponsConfig config = WeaponsConfigCategory.Instance.Get(weaponInfo.ConfigId);
            self.View.E_WeaponTypeText.text = config.WeaponTypeName;
            self.View.E_DesText.text = config.WeaponDes;
            self.View.E_WeaponNameText.text = config.Name;
            // self.CurerntWeaponItem.
        }

        public static async ETTask GetWeaponWordsInfoRequest(this DlgWeaponInfoLayer self)
        {
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_GetWeaponWordBarsRequest request = new C2M_GetWeaponWordBarsRequest() { Account = account, WeaponId = self.WeaponInfo.WeaponId };
            M2C_GetWeaponWordBarsResponse response = (M2C_GetWeaponWordBarsResponse) await session.Call(request);

            if (response.Error == ErrorCode.ERR_Success)
            {
                // List<WordBarInfo> wordbars = new List<WordBarInfo>();
                // wordbars.Add(self.WeaponInfo);
                // foreach (var wordBar in response.WordBarInfos)
                // {
                // wordbars.Add(wordbars);
                // }
                List<WordBarInfo> infos = response.WordBarInfos;
                Log.Debug("获取装备词条成功");
                //分别设置四个词条的信息
                for (int i = 0; i < self.WordBarItems.Count; i++)
                {
                    ESCommonWordBar bar = self.WordBarItems[i];
                    if (i < infos.Count)
                    {
                        bar.SetInfo(infos[i]);
                    }
                }
            }

            await ETTask.CompletedTask;
        }
    }
}