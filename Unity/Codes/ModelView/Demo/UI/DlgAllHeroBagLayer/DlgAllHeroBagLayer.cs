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
        public Action<HeroCardInfo, Scroll_ItemHeroCard, bool> OnHeroItemInfoClick;

        public HeroBagType BagType = HeroBagType.HeroAndMaterial;
        public HeroCardInfo UnAbleHeroCardInfo = null; //单独禁用的信息
        public List<HeroCardInfo> UnableElementHeroCardInfos = null; //需要禁用的相同元素的英雄列表
        public List<HeroCardInfo> UnableNameHeroCardInfos = null; //需要禁用的相同名称的英雄列表
        public List<HeroCardInfo> EnabelHeroCardInfos = null; //不禁用的英雄信息列表
        public List<HeroCardInfo> AllChooseHeroCardInfos = null; //当前已经选择的英雄列表
        public HeroCardInfo EnabelSameStarCountHeroCardInfo = null; //可用的相同星数目的卡牌信息

        // public List<HeroCardInfo> Enable
        public int BagCount = 0;
        public int HeroCount = 0;
        public Sprite DefaultHeadSprite = null;

        public HeroElementType[] ElementTypesList =
        {
            HeroElementType.Fire, HeroElementType.Dark, HeroElementType.Water, HeroElementType.Wind, HeroElementType.Light
        };
    }
}