using System.Diagnostics.Eventing.Reader;
using UnityEngine.UI;

namespace ET
{
    public static class ItemWeaponCardViewSystem
    {
        public static async void InitWeaponCardView(this Scroll_ItemWeapon self, WeaponInfo weaponInfo)
        {
            Log.Debug("init weapon card view");
            WeaponsConfig config = WeaponsConfigCategory.Instance.Get(weaponInfo.ConfigId);
            var weaponAtlas = ConstValue.WeaponAtlasPath;
            var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(weaponAtlas, config.IconResName);
            self.E_WeaponImage.sprite = sprite;
            self.E_AddTextText.gameObject.SetActive(false);
            //如果是材料，那么需要显示材料个数
            self.E_CountText.gameObject.SetActive(config.MaterialType == (int) WeaponBagType.Materail);
            self.E_CountText.text = weaponInfo.Count.ToString();
            self.E_LevelText.gameObject.SetActive(config.MaterialType != (int) WeaponBagType.Materail);
            self.E_LevelText.text = $"Lv.{weaponInfo.Level}";
        }

        public static  void UnAableButtonClick(this  Scroll_ItemWeapon self)
        {
            self.E_ChooseToggle.GetComponent<Toggle>().interactable = false;
        }

        public static  async void ShowAddButtonState(this Scroll_ItemWeapon self)
        {
            //显示加号的状态
            var weaponAtlas = ConstValue.CommonUIAtlasPath;
            var bgPath = ConstValue.FrameBgPath;
            var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(weaponAtlas, bgPath);
            self.E_WeaponImage.sprite = sprite;
            self.E_AddTextText.gameObject.SetActive(true);
            self.E_LevelText.gameObject.SetActive(false);
            self.E_CountText.gameObject.SetActive(false);
            
        }
    }
}