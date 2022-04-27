using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ET
{
    public class HeroModeObjectComponentSystem: AwakeSystem<HeroModeObjectCompoent, HeroCard>
    {
        public override async void Awake(HeroModeObjectCompoent self, HeroCard heroCard)
        {
            Log.Debug($"hero card config id {heroCard.ConfigId}");
            //加载英雄模型
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroCard.ConfigId);
            var heroModeStr = heroConfig.HeroMode;

            GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>(heroModeStr);
            self.HeroMode = GameObject.Instantiate(prefab);
            self.HeroMode.transform.position = new Vector3(-3 + heroCard.InTroopIndex * 3, 0, -7 * (heroCard.CampIndex == 0? 1 : -1));
            self.HeroMode.transform.forward = heroCard.CampIndex == 0? Vector3.forward : Vector3.back;
        }
    }
}