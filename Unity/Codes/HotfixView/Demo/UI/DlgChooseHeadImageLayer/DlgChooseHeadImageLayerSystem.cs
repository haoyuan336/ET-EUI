using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ET
{
    public static class DlgChooseHeadImageLayerSystem
    {
        public static void RegisterUIEvent(this DlgChooseHeadImageLayer self)
        {
            self.View.E_HeadLabelToggle.onValueChanged.AddListener((bool value) => { self.OnToggleClick(HeadImageType.Head); });
            self.View.E_HeadFrameLabelToggle.onValueChanged.AddListener((bool value) => { self.OnToggleClick(HeadImageType.HeadFrame); });
            self.View.E_BgButton.AddListener(() =>
            {
                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                uiComponent.HideWindow(WindowID.WindowID_ChooseHeadImageLayer);
            });

            self.View.E_HeadLoopVerticalScrollRect.AddItemRefreshListener(self.OnHeadLoop);
        }

        public static void OnToggleClick(this DlgChooseHeadImageLayer self, HeadImageType type)
        {
            List<PlayerHeadImageResConfig> playerHeadImageResConfigs = PlayerHeadImageResConfigCategory.Instance.GetAll().Values.ToList();
            self.ImageResConfigs = playerHeadImageResConfigs.FindAll(a => { return a.Type == (int) type; });
            self.AddUIScrollItems(ref self.ItemChooseHeads, self.ImageResConfigs.Count);
            self.View.E_HeadLoopVerticalScrollRect.SetVisible(true, self.ImageResConfigs.Count);
        }

        public static void HideSelf(this DlgChooseHeadImageLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            uiComponent.HideWindow(WindowID.WindowID_ChooseHeadImageLayer);
        }

        public static async void OnHeadLoop(this DlgChooseHeadImageLayer self, Transform tr, int index)
        {
            Scroll_ItemChooseHead itemChooseHead = self.ItemChooseHeads[index].BindTrans(tr);
            if (index < self.ImageResConfigs.Count)
            {
                var config = self.ImageResConfigs[index];
                if (self.CurrentAccountInfo != null)
                {
                    if (config.Type == (int) HeadImageType.Head)
                    {
                        itemChooseHead.E_MarkImage.gameObject.SetActive(config.Id.Equals(self.CurrentAccountInfo.HeadImageConfigId));
                    }

                    if (config.Type == (int) HeadImageType.HeadFrame)
                    {
                        itemChooseHead.E_MarkImage.gameObject.SetActive(config.Id.Equals(self.CurrentAccountInfo.HeadFrameImageConfigId));
                    }
                }

                var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(config.SpriteAtlasRes, config.ImageRes);
                itemChooseHead.E_HeadImage.sprite = sprite;
                itemChooseHead.E_HeadButton.onClick.RemoveAllListeners();
                itemChooseHead.E_HeadButton.AddListener(() =>
                {
                    // self.ChangeHeadOrFrameRequest(HeadImageType.Head, config.Id);

                    if (self.ChooseHeadCallBackAction != null)
                    {
                        self.ChooseHeadCallBackAction((HeadImageType) config.Type, config.Id);
                    }

                    self.HideSelf();
                });
            }
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
        }

        public static void ShowWindow(this DlgChooseHeadImageLayer self, Entity contextData = null)
        {
        }

        public static void SetAccountInfo(this DlgChooseHeadImageLayer self, AccountInfo accountInfo)
        {
            self.CurrentAccountInfo = accountInfo;

            self.View.E_HeadLabelToggle.isOn = true;
            self.OnToggleClick(HeadImageType.Head);
        }
    }
}