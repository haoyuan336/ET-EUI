using ET.EventType;
using UnityEngine;

namespace ET
{
    public class PlayDiamondContentAnimEventHandler: AEvent<EventType.PlayDiamondContentAnim>
    {
        protected override async ETTask Run(PlayDiamondContentAnim a)
        {
            // Log.Debug($"移动宝石位置{a.Value}");
            var value = a.Value;
            Transform diamondContent = GlobalComponent.Instance.DiamondContent;
            float time = 0;

            var startPos = Vector3.zero;
            var endPos = Vector3.down;
            if (!value)
            {
                startPos = Vector3.zero;
                endPos = Vector3.down * 1f;
            }
            else
            {
                startPos = Vector3.down * 1f;
                endPos = Vector3.zero;
            }

            while (time < 1f)
            {
                var prePos = Vector3.Lerp(startPos, endPos, time);
                time += Time.deltaTime;
                diamondContent.position = prePos;
                await TimerComponent.Instance.WaitFrameAsync();
            }

            diamondContent.position = endPos;
        }
    }
}