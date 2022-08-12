using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ET
{
    public static class DlgChooseHeadImageLayerSystem
    {
        public static void RegisterUIEvent(this DlgChooseHeadImageLayer self)
        {
            // self.View.E_HeadLabelToggle.onValueChanged.AddListener((bool value) => { self.OnToggleClick(HeadImageType.Head); });
            // self.View.E_HeadFrameLabelToggle.AddListener((bool value) => { self.OnToggleClick(HeadImageType.HeadFrame); });
            self.View.E_BgButton.AddListener(() =>
            {
                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                uiComponent.HideWindow(WindowID.WindowID_ChooseHeadImageLayer);
            });
            self.View.E_BackButton.AddListener(() =>
            {
                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                uiComponent.HideWindow(WindowID.WindowID_ChooseHeadImageLayer);
            });

            self.View.E_HeadLoopVerticalScrollRect.AddItemRefreshListener(self.OnHeadLoop);
        }

        // public static void OnToggleClick(this DlgChooseHeadImageLayer self, HeadImageType type)
        // {
        //     List<PlayerHeadImageResConfig> playerHeadImageResConfigs = PlayerHeadImageResConfigCategory.Instance.GetAll().Values.ToList();
        //     self.ImageResConfigs = playerHeadImageResConfigs.FindAll(a => { return a.Type == (int)type; });
        //     self.AddUIScrollItems(ref self.ItemChooseHeads, self.ImageResConfigs.Count);
        //     self.View.E_HeadLoopVerticalScrollRect.SetVisible(true, self.ImageResConfigs.Count);
        // }

        public static void HideSelf(this DlgChooseHeadImageLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            uiComponent.HideWindow(WindowID.WindowID_ChooseHeadImageLayer);
        }

        public static void OnHeadLoop(this DlgChooseHeadImageLayer self, Transform tr, int index)
        {
            Scroll_ItemChooseHead itemChooseHead = self.ItemChooseHeads[index].BindTrans(tr);
            ItemConfig config = self.ItemConfigs[index];
            itemChooseHead.SetConfig(config);
            Log.Debug($"config {config.Id}");
            ItemInfo itemInfo = self.HeadImageItemInfos.Find(a => a.ConfigId.Equals(config.Id));
            itemChooseHead.SetItemInfo(itemInfo);

            itemChooseHead.E_HeadButton.AddListener(() =>
            {
                // self.ChangeHeadOrFrameRequest(HeadImageType.Head, config.Id);

                if (self.ChooseHeadCallBackAction != null)
                {
                    self.ChooseHeadCallBackAction(HeadImageType.Head, itemInfo);
                }
            });

            // if (index < self.ImageResConfigs.Count)
            // {
            // var config = self.ImageResConfigs[index];
            // itemChooseHead.SetConfig(config);

            // var config = self.ImageResConfigs[index];
            // if (self.CurrentAccountInfo != null)
            // {
            //     if (config.Type == (int)HeadImageType.Head)
            //     {
            //         itemChooseHead.E_MarkImage.gameObject.SetActive(config.Id.Equals(self.CurrentAccountInfo.HeadImageConfigId));
            //     }
            //
            //     if (config.Type == (int)HeadImageType.HeadFrame)
            //     {
            //         itemChooseHead.E_MarkImage.gameObject.SetActive(config.Id.Equals(self.CurrentAccountInfo.HeadFrameImageConfigId));
            //     }
            // }
            //
            // var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(config.SpriteAtlasRes, config.ImageRes);
            // itemChooseHead.E_HeadImage.sprite = sprite;
            // itemChooseHead.E_HeadButton.onClick.RemoveAllListeners();
            // itemChooseHead.E_HeadButton.AddListener(() =>
            // {
            //     // self.ChangeHeadOrFrameRequest(HeadImageType.Head, config.Id);
            //
            // if (self.ChooseHeadCallBackAction != null)
            // {
            //     self.ChooseHeadCallBackAction(HeadImageType.Head, itemInfo.ItemId);
            // }
            //
            //     self.HideSelf();
            // });
            // }
        }

        public static void InitChooseHeadLoop(this DlgChooseHeadImageLayer self)
        {
            // List<PlayerHeadImageResConfig> configs = PlayerHeadImageResConfigCategory.Instance.GetAll().Values.ToList();
            // List<PlayerHeadImageResConfig> headConfigs = configs.FindAll(a => { return a.Type == (int) HeadImageType.Head; });
            //
            // self.HeadImageResConfigs = headConfigs;
            // self.AddUIScrollItems(ref self.ItemChooseHeads, headConfigs.Count);
            // self.View.E_HeadLoopVerticalScrollRect.SetVisible(true, headConfigs.Count);
        }

        public static void ShowHeadContent(this DlgChooseHeadImageLayer self)
        {
            self.ItemConfigs = ItemConfigCategory.Instance.GetAll().Values.ToList().FindAll(a => { return a.MinType == (int)ItemMinType.HeadImage; });
            self.AddUIScrollItems(ref self.ItemChooseHeads, self.ItemConfigs.Count);
            self.View.E_HeadLoopVerticalScrollRect.SetVisible(true, self.ItemConfigs.Count);
        }

        public static async void ShowWindow(this DlgChooseHeadImageLayer self, Entity contextData = null)
        {
            //
            self.ShowHeadContent();

            //取出玩家当前拥有的头像
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_GetAllItemRequest request = new C2M_GetAllItemRequest();
            M2C_GetAllItemResponse response = await session.Call(request) as M2C_GetAllItemResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                List<ItemInfo> itemInfos = response.ItemInfos;
                itemInfos = itemInfos.FindAll(a =>
                {
                    ItemConfig config = ItemConfigCategory.Instance.Get(a.ConfigId);
                    return config.MinType == (int)ItemMinType.HeadImage;
                });
                foreach (var itemInfo in itemInfos)
                {
                    Log.Debug($"item info {itemInfo}");
                }

                self.HeadImageItemInfos = itemInfos;
                self.View.E_HeadLoopVerticalScrollRect.RefreshCells();
            }
        }

        public static void SetAccountInfo(this DlgChooseHeadImageLayer self, AccountInfo accountInfo)
        {
            // self.CurrentAccountInfo = accountInfo;
            self.View.E_HeadLabelToggle.isOn = true;
            // self.OnToggleClick(HeadImageType.Head);
        }
    }
}