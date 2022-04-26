using System;
using System.Collections.Generic;
using System.Linq;
using ET.EventType;
using UnityEngine;

namespace ET
{
    public class CreateHeroCardEventHandler: AEvent<EventType.CreateHeroCardView>
    {
        protected override async ETTask Run(CreateHeroCardView a)
        {
            // ResourcesLoaderComponent.
            // GameObject bundleGameObject = (GameObject) ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
            // GameObject prefab = bundleGameObject.Get<GameObject>("HeroCard");
            // GameObject prefab = AddressableComponent.Instance.LoadAssetByPath<GameObject>("HeroCard");
            Dictionary<int, List<HeroCard>> heroCardListMap = a.HeroCardListMap;
            foreach (var key in heroCardListMap.Keys)
            {
                List<HeroCard> heroCards = heroCardListMap[key];
                for (var i = 0; i < heroCards.Count; i++)
                {
                    HeroCard heroCard = heroCards[i];
                    HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroCard.ConfigId);
                    GameObject heroCardPrefab = AddressableComponent.Instance.LoadAssetByPath<GameObject>("HeroCard");
                    GameObject heroModePrefab = AddressableComponent.Instance.LoadAssetByPath<GameObject>(heroConfig.HeroMode);
                    HeroCardObjectComponent component = heroCard.AddComponent<HeroCardObjectComponent>();
                    HeroModeObjectCompoent modeCom = heroCard.AddComponent<HeroModeObjectCompoent>();
                    component.HeroCard = GameObject.Instantiate(heroCardPrefab);
                    modeCom.HeroMode = GameObject.Instantiate(heroModePrefab);

                    // GameObject go = GameObject.Instantiate(heroCardPrefab, GlobalComponent.Instance.Unit);
                    // Log.Debug("add hero card view component");
                    // if (heroCard.GetComponent<GameObjectComponent>() == null)
                    //     heroCard.AddComponent<GameObjectComponent>().GameObject = go;
                    float distance = 2.5f;
                    Vector3 position = new Vector3(heroCard.InTroopIndex * distance + (heroCards.Count - 1) * -0.5f * distance, 0,
                        6.5f * (key == 0? -1 : 1));
                    // // go.GetComponent<HeroCardViewCtl>().InitInfo(heroCard.ConfigId, heroCard.CampIndex);
                    component.HeroCard.transform.position = position;
                    component.HeroCard.GetComponent<HeroCardViewCtl>().UpdateHPView(heroCard.HP);
                    component.HeroCard.GetComponent<HeroCardViewCtl>().UpdateAttackView(heroCard.Attack.ToString());
                    // go.GetComponent<HeroCardViewCtl>().UpdateAngryView($"{heroCard.Angry}/{heroConfig.TotalAngry}");
                    // go.GetComponent<HeroCardViewCtl>().HeroMode = GameObject.Instantiate(heroModePrefab);
                }
            }

            await ETTask.CompletedTask;
        }
    }
}