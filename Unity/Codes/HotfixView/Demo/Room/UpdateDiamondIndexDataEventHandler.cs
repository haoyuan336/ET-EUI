using ET.EventType;
using UnityEngine;

namespace ET
{
    public class UpdateDiamondIndexDataEventHandler: AEvent<EventType.UpdateDiamondIndex>
    {
        protected override async ETTask Run(UpdateDiamondIndex a)
        {
            Diamond diamond = a.Diamond;
            GameObject go = diamond.GetComponent<GameObjectComponent>().GameObject;

            int LieCount = ConstValue.LieCount;
            int HangCount = ConstValue.HangCount;
            var distance = ConstValue.Distance;

            Vector3 endPos = new Vector3((a.Diamond.LieIndex - LieCount * 0.5f + 0.5f) * distance, 0,
                (a.Diamond.HangIndex - HangCount * 0.5f + 0.5f) * distance);
            float time = 0;
            Vector3 startPos = go.transform.position;
            while (time < Mathf.PI * 0.5f)
            {
                var value = Mathf.Sin(time);
                time += Time.deltaTime * 5;
                Vector3 prePos = Vector3.Lerp(startPos, endPos, value);
                go.transform.position = prePos;
                await TimerComponent.Instance.WaitFrameAsync();
            }

            await ETTask.CompletedTask;
        }
    }
}