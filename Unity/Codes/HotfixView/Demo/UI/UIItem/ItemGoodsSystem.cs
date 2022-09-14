using UnityEngine;
using UnityEngine.U2D;

namespace ET
{
    public static class ItemGoodsSystem
    {
        public static async void InitGoodsInfo(this Scroll_ItemGoods self, GoodsConfig config)
        {
            var defaultPath = ConstValue.CommonUIAtlasPath;
            var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(defaultPath, "bgpic");
            self.E_GoodsImage.sprite = sprite;
            self.E_DesText.text = "";
            switch (config.GoodsType)
            {
                case (int) GoodsType.Item:
                    ItemConfig itemConfig = ItemConfigCategory.Instance.Get(config.ConfigId);
                    self.E_DesText.text = itemConfig.Des;
                    break;
                case (int) GoodsType.Weapon:
                    WeaponsConfig weaponsConfig = WeaponsConfigCategory.Instance.Get(config.ConfigId);
                    // self.E_DesText.text = weaponsConfig.Name;
                    self.InitItemWithWeaponConfig(weaponsConfig);
                    break;
            }
        }

        public static async void InitItemWithWeaponConfig(this Scroll_ItemGoods self, WeaponsConfig config)
        {
            var path = ConstValue.WeaponAtlasPath;
            Sprite sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(path, config.IconResName);
            self.E_GoodsImage.sprite = sprite;
        }
    }
}