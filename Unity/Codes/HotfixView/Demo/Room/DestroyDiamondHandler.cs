using System;
using ET.EventType;
using UnityEngine;

namespace ET
{
    public class DestroyDiamondHandler: AEvent<EventType.DestoryDiamondView>
    {
        protected override async ETTask Run(DestoryDiamondView a)
        {
            DiamondAction diamondAction = a.DiamondAction;
            Scene scene = a.Scene;

            Diamond diamond = a.Diamond;
            int index = a.Index;
            GameObject go = diamond.GetComponent<GameObjectComponent>().GameObject;
            DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(diamond.ConfigId);
            var audioCrashStr = config.CrashAudio;
            Log.Debug($"audio {audioCrashStr}");
            if (!string.IsNullOrEmpty(audioCrashStr))
            {
                //加载音频资源
                // var audio = await AddressableComponent.Instance.LoadAssetByPathAsync<AudioClip>(audioCrashStr);
                // AudioSource audioSource = go.GetComponent<AudioSource>();
                // audioSource.clip = audio;
                // audioSource.Play();
                Game.EventSystem.Publish(new EventType.PlayGameAudioEffect()
                {
                    AudioStr = audioCrashStr,
                    ZoneScene = scene.ZoneScene()
                });
            }
            var effectPos = go.transform.position;
            var boomEffectRes = config.DestoryEffectRes;
            // go.transform.localScale = Vector3.zero;
            GameObject.Destroy(go);
            Log.Debug($"boom effect res{boomEffectRes}");

            if (diamondAction.AddAngryActions.Count != 0)
            {
                HeroCardComponent heroCardComponent = scene.GetComponent<HeroCardComponent>();
                foreach (var addItemAction in diamondAction.AddAngryActions)
                {
                    HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(addItemAction.HeroCardInfo.HeroId);
                    Game.EventSystem.Publish(new EventType.PlayAddAngryViewAnim()
                    {
                        HeroCard = heroCard, StartPos = effectPos, DiamondInfo = diamond.GetMessageInfo(), AddItemAction = addItemAction
                    });
                }
            }
            if (!string.IsNullOrEmpty(boomEffectRes))
            {
                GameObject effect = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath(boomEffectRes);
                effect.transform.SetParent(GlobalComponent.Instance.DiamondContent);

                effect.transform.position = effectPos;
                var time = config.DestoryEffectTime;
                await TimerComponent.Instance.WaitAsync(time);
            }
            // GameObject.Destroy(go);
            diamond.Dispose();
            await ETTask.CompletedTask;
        }
    }
}