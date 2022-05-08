using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgStoreSystem
    {
        public static void RegisterUIEvent(this DlgStore self)
        {
        }

        public static async void ShowWindow(this DlgStore self, Entity contextData = null)
        {
            //加载武器资源 
            var weaponSpriteAtlasStr = "Assets/Res/WeaponTextures/WeaponSpriteAtlas.spriteatlas";
            Sprite sp = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(weaponSpriteAtlasStr, "5s_ring_04");
            Log.Debug($"sp{sp.name}");
        }
    }
}