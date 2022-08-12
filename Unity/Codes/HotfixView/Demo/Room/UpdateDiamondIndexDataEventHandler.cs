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

            switch (a.DiamondActionType)
            {
                case DiamondActionType.Move:
                    await diamond.GetComponent<GameObjectComponent>().MoveToPos(endPos);
                    break;
                case DiamondActionType.MoveDown:
                    await diamond.GetComponent<GameObjectComponent>().MoveDown(endPos);
                    break;
            }

            await ETTask.CompletedTask;
        }
    }
}