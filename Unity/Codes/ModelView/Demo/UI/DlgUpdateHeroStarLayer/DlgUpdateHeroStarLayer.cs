using UnityEngine;

namespace ET
{
    public class DlgUpdateHeroStarLayer: Entity, IAwake
    {
        public DlgUpdateHeroStarLayerViewComponent View
        {
            get => this.Parent.GetComponent<DlgUpdateHeroStarLayerViewComponent>();
        }

        public HeroCardInfo HeroCardInfo;
        public HeroCardInfo ChooseCardInfo; //选择的英雄卡牌信息

        public GameObject CurrentHeroCardObject; //当前的英雄
        public GameObject ChooseHeroCardObject; //选择的英雄信息
        public GameObject NextStarHeroCardObject; //下一星级的英雄信息
    }
}