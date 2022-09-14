using System.Collections.Generic;
using System.Linq;
using ET.EventType;

namespace ET
{
    public class InitObjectPoolEventHandler: AEvent<EventType.InitObjectPool>
    {
        protected override async ETTask Run(InitObjectPool a)
        {
            Scene scene = a.Scene;

            //首先初始化钻石

            List<DiamondTypeConfig> diamondTypeConfigs = DiamondTypeConfigCategory.Instance.GetAll().Values.ToList();
            
            Dictionary<string, bool> destoryEffectRess = new Dictionary<string, bool>();
            Dictionary<string, bool> prefabRess = new Dictionary<string, bool>();
            Dictionary<string, bool> flyEffectAttackRess = new Dictionary<string, bool>();
            Dictionary<string, bool> flyEffectAngryRess = new Dictionary<string, bool>();
            
            List<ETTask> tasks = new List<ETTask>();

            foreach (var diamondTypeConfig in diamondTypeConfigs)
            {
                if (!destoryEffectRess.ContainsKey(diamondTypeConfig.DestoryEffectRes))
                {
                    destoryEffectRess.Add(diamondTypeConfig.DestoryEffectRes, true);
                    tasks.Add(GameObjectPoolHelper.InitPoolAsync(diamondTypeConfig.DestoryEffectRes, 5));
            
                }
                
                if (!prefabRess.ContainsKey(diamondTypeConfig.PrefabRes))
                {
                    prefabRess.Add(diamondTypeConfig.PrefabRes, true);
                    tasks.Add(GameObjectPoolHelper.InitPoolAsync(diamondTypeConfig.PrefabRes, 5));
            
                }
                
                if (!flyEffectAttackRess.ContainsKey(diamondTypeConfig.FlyEffectAttackRes))
                {
                    flyEffectAttackRess.Add(diamondTypeConfig.FlyEffectAttackRes, true);
                    tasks.Add(GameObjectPoolHelper.InitPoolAsync(diamondTypeConfig.FlyEffectAttackRes, 5));
            
                }
                
                if (flyEffectAngryRess.ContainsKey(diamondTypeConfig.FlyEffectAngryRes))
                {
                    flyEffectAngryRess.Add(diamondTypeConfig.FlyEffectAngryRes, true);
                    tasks.Add(GameObjectPoolHelper.InitPoolAsync(diamondTypeConfig.FlyEffectAngryRes, 5));
            
                }
            }

            await ETTaskHelper.WaitAll(tasks);
            await ETTask.CompletedTask;
        }
    }
}