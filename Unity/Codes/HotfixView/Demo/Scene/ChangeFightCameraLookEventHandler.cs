using ET.EventType;
using UnityEngine;

namespace ET
{
    public class ChangeFightCameraLookEventHandler: AEvent<EventType.ChangeFightCameraLook>
    {
        protected override async  ETTask Run(ChangeFightCameraLook a)
        {
            // bool value = a.Value;
            // var cm1 = UIFindHelper.FindDeepChild(GlobalComponent.Instance.Global.gameObject, "CM vcam1");
            // cm1.gameObject.SetActive(!value);
            // var cm2 = UIFindHelper.FindDeepChild(GlobalComponent.Instance.Global.gameObject, "CM vcam2");
            // cm2.gameObject.SetActive(value);
            //
            // await TimerComponent.Instance.WaitAsync(500);
            await ETTask.CompletedTask;

        }
    }
}