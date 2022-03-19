using ET.EventType;
using UnityEngine;

namespace ET
{
    public class DestroyDiamondHandler: AEvent<EventType.DestoryDiamondView>
    {
        protected override async ETTask Run(DestoryDiamondView a)
        {
            // PvPLevelConfig pLevelConfig = PvPLevelConfigCategory.Instance.Get(1);
            // int LieCount = pLevelConfig.LieCount;
            // int HangCount = pLevelConfig.HangCount;
            // float distance = float.Parse(pLevelConfig.Distance);
            // int LieIndex = a.Diamond.LieIndex;
            // int HangIndex = a.Diamond.HangIndex;
            // Vector3 endPos = new Vector3((LieIndex - LieCount * 0.5f + 0.5f) * distance,
            //     (HangIndex - HangCount * 0.5f + 0.5f) * distance, 0);
            //
            // Ray ray = new Ray(endPos, endPos + Vector3.forward);
            // RaycastHit raycastHit;
            // int maskCode = LayerMask.GetMask("Default");
            // bool isHited = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, maskCode);
            // if (isHited)
            // {
            //     float time = 0;
            //     while (time < Mathf.PI)
            //     {
            //         raycastHit.transform.localScale = Vector3.one + Vector3.one * Mathf.Sin(time) * 0.2f;
            //         time += 0.01f;
            //         await TimerComponent.Instance.WaitAsync(1000/60);
            //     }
            //
            //     GameObject.Destroy(raycastHit.transform.gameObject);
            // }
            // // a.Diamond.Dispose();
            Diamond diamond = a.Diamond;
            GameObject go = diamond.GetComponent<GameObjectComponent>().GameObject;
            float time = 0;
            while (time < Mathf.PI * 0.5f)
            {
                time += 0.02f;
                go.transform.localScale = Vector3.one * Mathf.Cos(time);
                await TimerComponent.Instance.WaitFrameAsync();
            }

            diamond.Dispose();

            await ETTask.CompletedTask;
        }
    }
}