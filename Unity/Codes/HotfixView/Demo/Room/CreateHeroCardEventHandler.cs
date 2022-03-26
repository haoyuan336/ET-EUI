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
            // GameObject boundleGameObject = (GameObject) ResourcesComponent.Instance.GetAsset("Unity.unity3d", "HeroCard");
            // GameObject prefab = boundleGameObject.Get<GameObject>("HeroCard");
            GameObject go = GameObject.Instantiate(prefab, GlobalComponent.Instance.Unit);
            HeroCard heroCard = a.HeroCard;

            if (heroCard.GetComponent<GameObjectComponent>() == null)
                heroCard.AddComponent<GameObjectComponent>().GameObject = go;
            float distance = 2;
            go.transform.position = new Vector3(heroCard.InTroopIndex * distance + (3 - 1) * -0.5f * distance, 6, 0);

            SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
            AllHeroCardLibrary asset = go.Get<AllHeroCardLibrary>("AllHeroCardTextureLibrary");

            Sprite sprite;
            if (asset.HeroCardTextureDict.TryGetValue(heroCard.ConfigId, out sprite))
            {
                spriteRenderer.sprite = sprite;

            }
            else
            {
                Log.Error($"not found hero id {heroCard.ConfigId}");
            }

            ;
            await ETTask.CompletedTask;
        }
    }
}