using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ObjectSystem]
    public class Scroll_ItemWeaponDestroySystem: DestroySystem<Scroll_ItemWeapon>
    {
        public override void Destroy(Scroll_ItemWeapon self)
        {
            self.DestroyWidget();
        }
    }

    public static class Scroll_ItemWeaponSystem
    {
        public static async void SetInfo(this Scroll_ItemWeapon self, WeaponConfig config)
        {
            
            var resStr = config.IconResName;
            var weaponSpriteAtlas = "Assets/Res/WeaponTextures/WeaponSpriteAtlas.spriteatlas";
            Sprite sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(weaponSpriteAtlas, resStr);
            self.E_WeaponImage.GetComponent<Image>().sprite = sprite;
        }
    }
}