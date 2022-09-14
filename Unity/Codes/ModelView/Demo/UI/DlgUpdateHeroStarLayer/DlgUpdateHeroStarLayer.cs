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

        // public ESCommonHeroCard CurrentCommonHeroCard;    //当前的英雄
        // public ESCommonHeroCard ChooseCommonHeroCard;   //当前选择的英雄
        // public ESCommonHeroCard NextStarCommonHeroCard; //下一个星级的行用

        public Scroll_ItemHeroCard CurrentItemHeroCard; //当前的英雄卡牌
        public Scroll_ItemHeroCard ChooseItemHeroCard; //当前选择的英雄卡牌
        public Scroll_ItemHeroCard NextStarHeroCard;    //下一个星级的英雄卡牌
    }
}