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
            
            
            
            await ETTask.CompletedTask;
        }
    }
}