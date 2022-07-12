using System;
using ET.EventType;
using UnityEngine;

namespace ET
{
    public class CreateOneDiamondEventHandler: AEvent<EventType.CreateOneDiamondView>
    {
        protected override async ETTask Run(CreateOneDiamondView a)
        {
            var diamondInfo = a.DiamondInfo;
            DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(diamondInfo.ConfigId);
            var str = config.PrefabRes;
            GameObject go = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath(str);
            go.transform.SetParent(GlobalComponent.Instance.DiamondContent);
            if (a.Diamond.IsDisposed)
            {
                Log.Error("diamond al disposed");
            }

            go.name = $"diamond {a.Diamond.LieIndex}{a.Diamond.HangIndex}";
            a.Diamond.AddComponent<GameObjectComponent>().GameObject = go;
            go.transform.position = CustomHelper.GetDiamondPos(ConstValue.LieCount, ConstValue.HangCount, a.Diamond.LieIndex, a.Diamond.HangIndex,
                ConstValue.Distance, ConstValue.DiamondOffsetZ);
            Vector3 endScale = new Vector3(1f, 1f, 1f);
            float time = 0;
            TimerComponent.Instance.WaitAsync(1000);
            while (time < 0.2f)
            {
                go.transform.localScale = Vector3.Lerp(Vector3.zero, endScale, time * 1 / 0.2f);
                time += Time.deltaTime;
                await TimerComponent.Instance.WaitFrameAsync();
            }
            go.transform.localScale = endScale;
            await ETTask.CompletedTask;
        }
    }
}