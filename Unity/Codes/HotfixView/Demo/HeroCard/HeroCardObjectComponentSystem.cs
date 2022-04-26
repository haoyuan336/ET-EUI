using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ET
{
    public class HeroCardObjectComponentSystem: AwakeSystem<HeroCardObjectComponent, HeroCard>
    {
        public override async void Awake(HeroCardObjectComponent self, HeroCard heroCard)
        {
            //load herocard assetbound
            GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>("HeroCard");
            GameObject card = GameObject.Instantiate(prefab);
            self.HeroCard = card;

            card.transform.position = new Vector3(-3 + heroCard.InTroopIndex * 3, 0, -7 * (heroCard.CampIndex == 0? -1 : 1));
        }
    }
}