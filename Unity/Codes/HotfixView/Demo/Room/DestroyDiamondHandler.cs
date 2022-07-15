using System;
using ET.EventType;
using UnityEngine;

namespace ET
{
    public class DestroyDiamondHandler: AEvent<EventType.DestoryDiamondView>
    {
        protected override async ETTask Run(DestoryDiamondView a)
        {
            Diamond diamond = a.Diamond;
            int index = a.Index;
            GameObject go = diamond.GetComponent<GameObjectComponent>().GameObject;
            Vector3 localScale = go.transform.localScale;
            // float time = 0;

            DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(diamond.ConfigId);
            // Log.Debug($"destory diamond config {config.BoomType}");
            // GameObject effectPrefab;
            // if (config.BoomType == (int)BoomType.LazerH)
            // {
            //     //此为横向特殊珠
            //     // effectPrefab
            //     
            // }
            //
            // if (config.BoomType == (int) BoomType.LazerV)
            // {
            //     //此为纵向特殊住
            //     
            // }

            var effectPos = go.transform.position;
            var boomEffectRes = config.DestoryEffectRes;
            int waitTime = 100;
            if (config.BoomType != (int) BoomType.Invalide)
            {
                Log.Debug($"boom effect pos {config.BoomType}");
                waitTime = 0;
            }

            await TimerComponent.Instance.WaitAsync(waitTime * index);
            GameObject.Destroy(go);
            Log.Debug($"boom effect res{boomEffectRes}");
            if (!string.IsNullOrEmpty(boomEffectRes))
            {
                GameObject effect = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath(boomEffectRes);
                effect.transform.SetParent(GlobalComponent.Instance.DiamondContent);

                effect.transform.position = effectPos;
                await TimerComponent.Instance.WaitAsync(600);
                GameObject.Destroy(effect);
            }

            //加载爆炸特效
            diamond.Dispose();
            await ETTask.CompletedTask;
        }
    }
}