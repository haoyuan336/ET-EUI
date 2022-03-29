using ET.EventType;
using UnityEngine;

namespace ET
{
    public class DestroyDiamondHandler: AEvent<EventType.DestoryDiamondView>
    {
        protected override async ETTask Run(DestoryDiamondView a)
        {
        
            Diamond diamond = a.Diamond;
            GameObject go = diamond.GetComponent<GameObjectComponent>().GameObject;
            float time = 0;
            while (time < Mathf.PI * 0.5f)
            {
                time += 0.04f;
                go.transform.localScale = Vector3.one * Mathf.Cos(time);
                await TimerComponent.Instance.WaitFrameAsync();
            }

            diamond.Dispose();

            await ETTask.CompletedTask;
        }
    }
}