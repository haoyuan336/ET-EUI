using UnityEngine;

namespace ET
{
    [ObjectSystem]
    public class GlobalComponentAwakeSystem: AwakeSystem<GlobalComponent>
    {
        public override void Awake(GlobalComponent self)
        {
            GlobalComponent.Instance = self;

            self.Global = GameObject.Find("/Global").transform;
            self.Unit = GameObject.Find("/Global/UnitRoot").transform;
            self.UI = GameObject.Find("/Global/UIRoot").transform;
            self.NormalRoot = GameObject.Find("Global/UIRoot/NormalRoot").transform;
            self.PopUpRoot = GameObject.Find("Global/UIRoot/PopUpRoot").transform;
            self.FixedRoot = GameObject.Find("Global/UIRoot/FixedRoot").transform;
            self.OtherRoot = GameObject.Find("Global/UIRoot/OtherRoot").transform;
            self.PoolRoot = GameObject.Find("Global/PoolRoot").transform;
            self.GameUIRoot = GameObject.Find("Global/UIRoot/GameUIRoot").transform;

            self.DiamondContent = GameObject.Find("Global/DiamondContent").transform;
            self.AudioResourceRoot = GameObject.Find("Global/UIRoot/AudioResourceRoot").transform;
        }
    }
}