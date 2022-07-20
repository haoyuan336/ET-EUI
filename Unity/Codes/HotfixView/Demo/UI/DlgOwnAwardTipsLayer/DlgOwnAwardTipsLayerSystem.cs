using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgOwnAwardTipsLayerSystem
    {
        public static void RegisterUIEvent(this DlgOwnAwardTipsLayer self)
        {
            self.View.EOkButtonButton.AddListener(() =>
            {
                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                uiComponent.HideWindow(WindowID.WindowID_OwnAwardTipsLayer);
            });
        }

        public static void ShowWindow(this DlgOwnAwardTipsLayer self, Entity contextData = null)
        {
        }

        public static async void SetItemInfos(this DlgOwnAwardTipsLayer self, List<ItemInfo> itemInfos)
        {
            var prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>("ItemOwnAward");
            //根据道具信息 初始化道具实例
            for (int i = 0; i < itemInfos.Count; i++)
            {
                var itemInfo = itemInfos[i];
                var go = GameObject.Instantiate(prefab);
                go.transform.SetParent(self.View.uiTransform);
                var itemOwner = self.AddChild<Scroll_ItemOwnAward>();
                itemOwner.BindTrans(go.transform);
                self.InitItemInfo(itemOwner, itemInfo);
                go.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
        }

        public static void InitItemInfo(this DlgOwnAwardTipsLayer self, Scroll_ItemOwnAward itemOwnAward, ItemInfo itemInfo)
        {
            itemOwnAward.E_CountText.text = $"X{itemInfo.Count}";
            var config = ItemConfigCategory.Instance.Get(itemInfo.ConfigId);
            itemOwnAward.E_NameText.text = config.Name;
        }
    }
}