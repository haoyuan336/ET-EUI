using System.Diagnostics.Eventing.Reader;

namespace ET
{
    public static class ItemWeaponCardViewSystem
    {
        public static async void InitWeaponCardView(this Scroll_ItemWeapon self, WeaponInfo weaponInfo)
        {
            WeaponsConfig config = WeaponsConfigCategory.Instance.Get(weaponInfo.ConfigId);
            var weaponAtlas = ConstValue.WeaponAtlasPath;
            var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(weaponAtlas, config.IconResName);
            self.E_WeaponImage.sprite = sprite;
            //如果是材料，那么需要显示材料个数
            self.E_CountText.gameObject.SetActive(config.MaterialType == (int) WeaponBagType.Materail);
            self.E_CountText.text = weaponInfo.Count.ToString();
            self.E_LevelText.gameObject.SetActive(config.MaterialType != (int) WeaponBagType.Materail);
            self.E_LevelText.text = $"Lv.{weaponInfo.Level}";
        }
    }
}