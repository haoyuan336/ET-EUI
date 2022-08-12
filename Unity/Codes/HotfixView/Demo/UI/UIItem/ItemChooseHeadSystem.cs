using UnityEngine;

namespace ET
{
    public static class ItemChooseHeadSystem
    {
        public static void SetLockState(this Scroll_ItemChooseHead self)
        {
            self.E_HeadButton.interactable = false;
        }

        public static void SetChooseState(this Scroll_ItemChooseHead self)
        {
            self.E_MarkImage.gameObject.SetActive(true);
        }

        public static async void SetConfig(this Scroll_ItemChooseHead self, ItemConfig config)
        {
            string imageStr = config.IconImageStr;
            string sptiteAtlas = config.AtlasSprite;
            Sprite sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(sptiteAtlas, imageStr);
            self.E_HeadImage.sprite = sprite;
        }

        public static void SetItemInfo(this Scroll_ItemChooseHead self, ItemInfo itemInfo)
        {
            self.E_HeadButton.interactable = itemInfo != null && itemInfo.Count != 0;
        }
    }
}