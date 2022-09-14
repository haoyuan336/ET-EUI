using UnityEngine;

namespace ET
{
    [ObjectSystem]
    public class ConfigComponent_SetConfigLoader_Awake: AwakeSystem<ConfigComponent>
    {
        public override void Awake(ConfigComponent self)
        {
            Log.Debug("ConfigComponent_SetConfigLoader_Awake");
            self.ConfigLoader = new ConfigLoader();
        }
    }
}