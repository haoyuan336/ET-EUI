using ET.EventType;
using UnityEngine;

namespace ET
{
    public class InitDiamondAndMoveDownHandler: AEvent<EventType.InitDiamondAndMoveDown>
    {
        protected override async ETTask Run(InitDiamondAndMoveDown a)
        {
            PvPLevelConfig pvPLevelConfig = PvPLevelConfigCategory.Instance.Get(1);
            int hangCount = pvPLevelConfig.HangCount;
            int liecount = pvPLevelConfig.LieCount;
            float distance = float.Parse(pvPLevelConfig.Distance);
            Diamond diamond = a.Diamond;
            GameObject go = diamond.GetComponent<GameObjectComponent>().GameObject;
            Vector3 initPos = new Vector3((a.Diamond.InitLieIndex - liecount * 0.5f + 0.5f) * distance,0,
                (a.Diamond.InitHangIndex - hangCount * 0.5f + 0.5f) * distance);
            go.transform.position = initPos;
            await Game.EventSystem.PublishAsync(new UpdateDiamondIndex(){Diamond = diamond});
            await ETTask.CompletedTask;
        }
    }
}