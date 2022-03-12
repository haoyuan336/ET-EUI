using ET.EventType;
using UnityEngine;
namespace ET
{
    public class DestroyDiamondHandler: AEvent<EventType.DestoryDiamondView>
    {
        protected override async ETTask Run(DestoryDiamondView a)
        {
            Log.Debug("Destroy Obj In View");
            PvPLevelConfig pLevelConfig = PvPLevelConfigCategory.Instance.Get(1);
            int LieCount = pLevelConfig.LieCount;
            int HangCount = pLevelConfig.HangCount;
            float distance = float.Parse(pLevelConfig.Distance);
            int LieIndex = a.LieIndex;
            int HangIndex = a.HangIndex;
            Vector3 endPos = new Vector3((LieIndex - LieCount * 0.5f + 0.5f) * distance,
                (HangIndex - HangCount * 0.5f + 0.5f) * distance, 0);

            
            Ray ray = new Ray(endPos,endPos + Vector3.forward);
            RaycastHit raycastHit;
            int maskCode = LayerMask.GetMask("Default");
            bool isHited = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, maskCode);
            if (isHited)
            {
                Log.Debug($"hited gameobject {raycastHit.transform.name}");
                GameObject.Destroy(raycastHit.transform.gameObject);
            }
            await ETTask.CompletedTask;
        }
    }
}