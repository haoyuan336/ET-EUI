using ET.EventType;
using UnityEngine;

namespace ET
{
    public class UpdateDiamondDataEventHandler: AEvent<EventType.UpdateDiamondData>
    {
        protected override async ETTask Run(UpdateDiamondData a)
        {
            GameObject go = a.Diamond.GetComponent<GameObjectComponent>().GameObject;
            // go.GetComponent<SpriteRenderer>().color = 
            PvPLevelConfig pvPLevelConfig = PvPLevelConfigCategory.Instance.Get(1);
            int hangCount = pvPLevelConfig.HangCount;
            int liecount = pvPLevelConfig.LieCount;
            float distance = 1.1f;

            DiamondTypeConfig diamondTypeConfig = DiamondTypeConfigCategory.Instance.Get(a.Diamond.DiamondType);
            string colorStr = diamondTypeConfig.ColorValue;
            string[] list = colorStr.Split(',');
            if (list.Length != 3)
            {
                Log.Error("color error");
            }

            Color color = new Color(int.Parse(list[0]), int.Parse(list[1]), int.Parse(list[2]));
            go.transform.position = new Vector3((a.Diamond.LieIndex - liecount * 0.5f + 0.5f) * distance,
                (a.Diamond.HangIndex - hangCount * 0.5f + 0.5f) * distance, 0);
            if (go.GetComponent<SpriteRenderer>() != null)
            {
                go.GetComponent<SpriteRenderer>().color = color;
            }

            await ETTask.CompletedTask;
        }
    }
}