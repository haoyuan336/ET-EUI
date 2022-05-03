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
            Vector3 localScale = go.transform.localScale;
            float time = 0;
            while (time < Mathf.PI * 0.5f)
            {
                time += Time.deltaTime * 8;
                go.transform.localScale = localScale * Mathf.Cos(time);
                await TimerComponent.Instance.WaitFrameAsync();
            }

            // diamond.Dispose();
            await ETTask.CompletedTask;
        }
    }
}