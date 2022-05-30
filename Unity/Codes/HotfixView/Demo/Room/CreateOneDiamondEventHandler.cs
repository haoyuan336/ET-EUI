using ET.EventType;
using UnityEngine;

namespace ET
{
    public class CreateOneDiamondEventHandler: AEvent<EventType.CreateOneDiamondView>
    {
        protected override async ETTask Run(CreateOneDiamondView a)
        {

            // await TimerComponent.Instance.WaitAsync(1000);
            PvPLevelConfig pvPLevelConfig = PvPLevelConfigCategory.Instance.Get(1);
            int hangCount = pvPLevelConfig.HangCount;
            int liecount = pvPLevelConfig.LieCount;
            // float distance = float.Parse(pvPLevelConfig.Distance);

            float distance = ConstValue.Distance;
            var diamondInfo = a.DiamondInfo;
            DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(diamondInfo.ConfigId);

            var imageStr = $"Assets/Res/Texture/Diamond/{config.TextureName}.png";
            Texture texture = await AddressableComponent.Instance.LoadAssetByPathAsync<Texture>(imageStr);
            var str = "Assets/Bundles/Unit/DiamondPrefabs/DiamondPrefab.prefab";
            GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>(str);
            GameObject go = GameObject.Instantiate(prefab, GlobalComponent.Instance.DiamondContent);
            if (a.Diamond.IsDisposed)
            {
                Log.Error("diamond al disposed");
            }

            a.Diamond.AddComponent<GameObjectComponent>().GameObject = go;
            MeshRenderer meshRenderer = go.GetComponent<MeshRenderer>();
            meshRenderer.material.SetTexture("_BaseMap", texture);

            go.transform.position = new Vector3((a.Diamond.LieIndex - liecount * 0.5f + 0.5f) * distance, 0,
                (a.Diamond.HangIndex - hangCount * 0.5f + 0.5f) * distance);
            go.transform.forward = Vector3.back;
            Vector3 endScale = go.transform.localScale;
            // go.transform.localScale = Vector3.zero;
            float time = 0;
            while (time < 0.2f)
            {
                go.transform.localScale = Vector3.Lerp(Vector3.zero, endScale, time * 1/0.2f);
                time += Time.deltaTime;
                await TimerComponent.Instance.WaitFrameAsync();

            }
            await ETTask.CompletedTask;
        }
    }
}