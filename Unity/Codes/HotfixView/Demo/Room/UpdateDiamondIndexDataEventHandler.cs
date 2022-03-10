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
            var distance = 1.1f;

            PvPLevelConfig pLevelConfig = PvPLevelConfigCategory.Instance.Get(1);
            int LieCount = pLevelConfig.LieCount;
            int HangCount = pLevelConfig.HangCount;
            Vector3 endPos = new Vector3((a.Diamond.LieIndex - LieCount * 0.5f + 0.5f) * distance,
                (a.Diamond.HangIndex - HangCount * 0.5f + 0.5f) * distance, 0);
            // go.transform.position = ;
            while (true)
            {
                Vector3 prePos = Vector3.Lerp(go.transform.position, endPos, 0.15f);
                await TimerComponent.Instance.WaitAsync(10);
                go.transform.position = prePos;
                if (Vector3.Distance(go.transform.position, endPos) < 0.1f)
                {
                    go.transform.position = endPos;
                    break;
                }
            }

            await ETTask.CompletedTask;
        }
    }
}