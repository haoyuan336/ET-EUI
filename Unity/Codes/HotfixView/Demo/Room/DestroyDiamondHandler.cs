using System;
using ET.EventType;
using UnityEngine;

namespace ET
{
    public class DestroyDiamondHandler: AEvent<EventType.DestoryDiamondView>
    {
        protected override async ETTask Run(DestoryDiamondView a)
        {
            if (a.Diamond.IsDisposed)
            {
                return;
            }
            DiamondAction diamondAction = a.DiamondAction;
            Scene scene = a.Scene;
            Diamond diamond = a.Diamond;
            GameObject go = diamond.GetComponent<GameObjectComponent>().GameObject;
            DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(diamond.ConfigId);
            var audioCrashStr = config.CrashAudio;
            Game.EventSystem.Publish(new EventType.PlayGameAudioEffect() { AudioStr = audioCrashStr, ZoneScene = scene.ZoneScene() });
            var effectPos = go.transform.position;

            //检查一下是否是是特殊珠消除
            GameObjectPoolHelper.ReturnObjectToPool(go);
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

            if (!string.IsNullOrEmpty(config.DestoryEffectRes))
            {
                GameObject effect = GameObjectPoolHelper.GetObjectFromPool(config.DestoryEffectRes, true, 1);
                effect.transform.SetParent(GlobalComponent.Instance.DiamondContent);

                if (config.BoomType == (int)BoomType.LazerH)
                {
                    effect.transform.position = new Vector3(0,effectPos.y ,effectPos.z);
                }else if (config.BoomType == (int)BoomType.LazerV)
                {
                    effect.transform.position = new Vector3(effectPos.x ,effectPos.y ,0);
                }
                else
                {
                    effect.transform.position = effectPos;
                }
                var time = config.DestoryEffectTime;
                await TimerComponent.Instance.WaitAsync(time);
                GameObjectPoolHelper.ReturnObjectToPool(effect);
            }

            diamond.Dispose();
            await ETTask.CompletedTask;
        }
    }
}