using System;
using ET.EventType;
using ET.Test;
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
            float distance = float.Parse(pvPLevelConfig.Distance);

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
                

                // TestClassBase
                go.GetComponent<SpriteRenderer>().color = color;
                // GameObject bundleGameObject = (GameObject) ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
                // String spriteNameStr = $"item_0{a.Diamond.DiamondType}";
                // Log.Debug($"sprite name str ={spriteNameStr} ");
                // Sprite sprite = bundleGameObject.Get<Sprite>(spriteNameStr);
                // // library.GetComponent<>()
                // go.GetComponent<SpriteRenderer>().sprite = sprite;
            }

            await ETTask.CompletedTask;
        }
    }
}