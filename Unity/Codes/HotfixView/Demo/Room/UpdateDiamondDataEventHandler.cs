using System;
using System.Runtime.InteropServices;
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
            float distance = float.Parse(pvPLevelConfig.Distance);

            DiamondTypeConfig diamondTypeConfig = DiamondTypeConfigCategory.Instance.Get(a.Diamond.DiamondType);
            string colorStr = diamondTypeConfig.ColorValue;
            string[] list = colorStr.Split(',');
            if (list.Length != 3)
            {
                Log.Error("color error");
            }

            // Color color = new Color(int.Parse(list[0]), int.Parse(list[1]), int.Parse(list[2]));
            go.transform.position = new Vector3((a.Diamond.LieIndex - liecount * 0.5f + 0.5f) * distance,
                (a.Diamond.HangIndex - hangCount * 0.5f + 0.5f) * distance, 0);
            if (go.GetComponent<SpriteRenderer>() != null)
            {
                AllDiamondLibrary allDiamondLibrary = go.GetComponent<DiamondLibraryCtl>().AllDiamondLibrary;
                DiamondLibrary diamondLibrary = null;
                if (allDiamondLibrary.DiamondSpriteMap.TryGetValue(a.Diamond.DiamondType, out diamondLibrary))
                {
                    Sprite sprite = diamondLibrary.normalTexture;
                    Log.Debug("boom type = " + a.Diamond.BoomType);
                    switch (a.Diamond.BoomType)
                    {
                        case (int) BoomType.Boom:
                            sprite = diamondLibrary.BoomTexture;
                            break;
                        case (int) BoomType.BlackHole:
                            sprite = diamondLibrary.BlackHoleTexture;
                            break;
                        case (int) BoomType.LazerH:
                            sprite = diamondLibrary.LazerHTexture;
                            break;
                        case (int) BoomType.LazerV:
                            sprite = diamondLibrary.LazerVTexture;

                            break;
                    }

                    go.GetComponent<SpriteRenderer>().sprite = sprite;
                    if (a.Diamond.BoomType != (int) BoomType.Invalide)
                    {
                        float time = 0;
                        while (time < Mathf.PI)
                        {
                            await TimerComponent.Instance.WaitFrameAsync();
                            time += 0.04f;
                            go.transform.localScale = Vector3.one + Vector3.one * Mathf.Sin(time) * 0.5f;
                        }

                    }
                }
            }

            await ETTask.CompletedTask;
        }
    }
}