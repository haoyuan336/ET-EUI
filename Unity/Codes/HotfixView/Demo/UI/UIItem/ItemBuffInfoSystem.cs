using System;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public class ItemBuffInfoAwakeSystem: AwakeSystem<Scroll_ItemBuff, BuffInfo, Transform>
    {
        public override void Awake(Scroll_ItemBuff self, BuffInfo a, Transform parent)
        {
            self.Awake(a, parent);
        }
    }

    public static class ItemBuffInfoSystem
    {
        public static async void Awake(this Scroll_ItemBuff self, BuffInfo info, Transform parent)
        {
            BuffConfig buffConfig = BuffConfigCategory.Instance.Get(info.ConfigId);
            GameObject gameObject = GameObjectPoolHelper.GetObjectFromPool("ItemBuff", true, 5);
            gameObject.transform.localScale = Vector2.one;
            self.uiTransform = gameObject.transform;
            self.E_CountText.text = $"X{info.RoundCount}";

            self.uiTransform.GetComponent<Image>().sprite =
                    await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(buffConfig.SpriteAtlas, buffConfig.ImageStr);
            gameObject.transform.SetParent(parent);
        }
    }
}