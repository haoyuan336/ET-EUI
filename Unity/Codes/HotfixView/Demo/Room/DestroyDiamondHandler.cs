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
            var effectPos = go.transform.position;
            var boomEffectRes = config.DestoryEffectRes;
            int waitTime = ConstValue.CrashWaitTime;
            if (config.BoomType != (int) BoomType.Invalide)
            {
                Log.Debug($"boom effect pos {config.BoomType}");
                waitTime = 0;
            }

            await TimerComponent.Instance.WaitAsync(waitTime * index);
            GameObject.Destroy(go);
            Log.Debug($"boom effect res{boomEffectRes}");

            if (diamondAction.AddAngryActions.Count != 0)
            {
                HeroCardComponent heroCardComponent = scene.GetComponent<HeroCardComponent>();
                foreach (var addItemAction in diamondAction.AddAngryActions)
                {
                    HeroCard heroCard = heroCardComponent.GetChild<HeroCard>(addItemAction.HeroCardInfo.HeroId);
                    // Game.EventSystem.pubs

                    // AddItemActions = diamondActionItem.AddAngryItemActions, Scene = session.ZoneScene().CurrentScene()

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
                // await TimerComponent.Instance.WaitAsync(ConstValue.CrashWaitTime);
                // GameObject.Destroy(effect, ConstValue.CrashItemWaitTime / 1000.0f);
            }

            //加载爆炸特效
            diamond.Dispose();
            await ETTask.CompletedTask;
        }
    }
}