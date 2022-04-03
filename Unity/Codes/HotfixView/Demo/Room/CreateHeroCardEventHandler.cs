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
            GameObject bundleGameObject = (GameObject) ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
            GameObject prefab = bundleGameObject.Get<GameObject>("HeroCard");
            Dictionary<int, List<HeroCard>> heroCardListMap = a.HeroCardListMap;

            foreach (var key in heroCardListMap.Keys)
            {
                List<HeroCard> heroCards = heroCardListMap[key];
                for (var i = 0; i < heroCards.Count; i++)
                {
                    HeroCard heroCard = heroCards[i];
                    GameObject go = GameObject.Instantiate(prefab, GlobalComponent.Instance.Unit);
                    heroCard.AddComponent<HeroCardView>();
                    if (heroCard.GetComponent<GameObjectComponent>() == null)
                        heroCard.AddComponent<GameObjectComponent>().GameObject = go;
                    float distance = 2.5f;
                    go.transform.position = new Vector3(heroCard.InTroopIndex * distance + (heroCards.Count - 1) * -0.5f * distance,0,
                        6.5f * (key == 0? -1 : 1));
                    go.GetComponent<HeroCardViewCtl>().InitInfo(heroCard.ConfigId, heroCard.CampIndex);
                    go.GetComponent<HeroCardViewCtl>().UpdateHPView(heroCard.HP);
                    go.GetComponent<HeroCardViewCtl>().UpdateAttackView(heroCard.Attack);
                    go.GetComponent<HeroCardViewCtl>().UpdateAngryView(heroCard.Angry);
                  
                }
            }

            await ETTask.CompletedTask;
        }
    }
}