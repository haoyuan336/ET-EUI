using ET.EventType;
using UnityEngine;

namespace ET
{
    public class CreateOneDiamondEventHandler: AEvent<EventType.CreateOneDiamondView>
    {
        protected override async ETTask Run(CreateOneDiamondView a)
        {
            PvPLevelConfig pvPLevelConfig = PvPLevelConfigCategory.Instance.Get(1);
            int hangCount = pvPLevelConfig.HangCount;
            int liecount = pvPLevelConfig.LieCount;
            // float distance = float.Parse(pvPLevelConfig.Distance);

            float distance = 1;
            var diamondInfo = a.DiamondInfo;
            DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(diamondInfo.ConfigId);
            // Log.Debug($"create one damond {a.DiamondInfo.ConfigId}");
            var modeStr = $"Assets/Bundles/Unit/DiamondPrefabs/{config.ModeName}.prefab";
            GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>(modeStr);
            GameObject go = GameObject.Instantiate(prefab);
            a.Diamond.AddComponent<GameObjectComponent>().GameObject = go;
            go.transform.position = new Vector3((a.Diamond.LieIndex - liecount * 0.5f + 0.5f) * distance, 0,
                (a.Diamond.HangIndex - hangCount * 0.5f + 0.5f) * distance);
            go.transform.forward = Vector3.back;
            await ETTask.CompletedTask;
        }
    }
}