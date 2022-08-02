using ET.EventType;
using UnityEngine;

namespace ET
{
    public class UpdateDiamondIndexDataEventHandler: AEvent<EventType.UpdateDiamondIndex>
    {
        protected override async ETTask Run(UpdateDiamondIndex a)
        {
            Diamond diamond = a.Diamond;
            // GameObject go = diamond.GetComponent<GameObjectComponent>().GameObject;
            //
            Vector3 endPos = CustomHelper.GetDiamondPos(ConstValue.LieCount, ConstValue.HangCount, diamond.LieIndex, diamond.HangIndex,
                ConstValue.Distance, ConstValue.DiamondOffsetZ);
            
            await diamond.GetComponent<GameObjectComponent>().MoveToPos(endPos);
            await ETTask.CompletedTask;
        }
    }
}