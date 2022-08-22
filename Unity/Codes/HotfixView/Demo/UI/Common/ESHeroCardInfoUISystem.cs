using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class ESHeroCardInfoUISystem
    {
        public static async void SetInfo(this ESHeroCardInfoUI self, HeroCardInfo heroCardInfo, HeroCardDataComponentInfo heroCardDataComponentInfo)
        {
            // self.E_CommonText.text = "";
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);

            ElementConfig elementConfig = ElementConfigCategory.Instance.Get(heroConfig.HeroColor);
            // heroConfig.HeroColor;
            var spriteAtlasPath = ConstValue.HeroCardAtlasPath;
            self.E_HeroElementIconImage.GetComponent<Image>().sprite = await
                    AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlasPath, elementConfig.IconImage);
        }

        public static async void SetBuffInfos(this ESHeroCardInfoUI self, List<BuffInfo> buffInfos)
        {
            var itemBuffs = self.GetChilds<Scroll_ItemBuff>();
            //都删掉，
            if (itemBuffs != null)
            {
                foreach (var itemBuff in itemBuffs)
                {
                    GameObjectPoolHelper.ReturnObjectToPool(itemBuff.uiTransform.gameObject);
                    itemBuff.Dispose();
                }
            }

            //然后创建
            foreach (var buffInfo in buffInfos)
            {
                self.AddChild<Scroll_ItemBuff, BuffInfo, Transform>(buffInfo, self.E_BufferContentImage.transform);
            }

            await ETTask.CompletedTask;
        }
    }
}