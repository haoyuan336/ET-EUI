using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class DlgAllHeroBagLayer: Entity, IAwake
    {
        public DlgAllHeroBagLayerViewComponent View
        {
            get => this.Parent.GetComponent<DlgAllHeroBagLayerViewComponent>();
        }

        public List<HeroCardInfo> HeroCardInfos = new List<HeroCardInfo>();
        public Dictionary<int, Scroll_ItemHeroCard> ItemHeroCards = new Dictionary<int, Scroll_ItemHeroCard>();
        public int CurrentChooseTypeIndex = 5;

        public Dictionary<int, Transform> ItemTransformMap = new Dictionary<int, Transform>();
        // public Action<HeroCardInfo> OnHeroItemClick;

        public HeroCardInfo UnAbleHeroCardInfo = null; //单独禁用的信息
        public List<HeroCardInfo> EnabelHeroCardInfos = null; //不禁用的英雄信息列表
        public List<HeroCardInfo> AllChooseHeroCardInfos = null; //当前已经选择的英雄列表
        public Action<HeroCardInfo, Scroll_ItemHeroCard, bool> OnHeroItemInfoClick;
    }
}