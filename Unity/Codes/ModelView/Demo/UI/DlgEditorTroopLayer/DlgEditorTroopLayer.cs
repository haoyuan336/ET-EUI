using System.Collections.Generic;

namespace ET
{
    public class DlgEditorTroopLayer: Entity, IAwake,IDestroy
    {
        public DlgEditorTroopLayerViewComponent View
        {
            get => this.Parent.GetComponent<DlgEditorTroopLayerViewComponent>();
        }

        // public Dictionary<int, Scroll_ItemHeroTroop> ItemTroops = new Dictionary<int, Scroll_ItemHeroTroop>();

        // public Dictionary<int, Scroll_ItemTroopHeroCard> ItemTroopHeroCards = new Dictionary<int, Scroll_ItemTroopHeroCard>();

        // public Dictionary<int, Scroll_ItemHeroCard> ItemTroopHeroCards = new Dictionary<int, Scroll_ItemHeroCard>();
        // public Dictionary<int, Scroll_ItemHeroCard> ItemHeroCards = new Dictionary<int, Scroll_ItemHeroCard>();

        // public List<TroopInfo> TroopInfos = new List<TroopInfo>();

        // public List<HeroCardInfo> HeroCardInfos = new List<HeroCardInfo>();

        public List<HeroCardInfo> TroopHeroCardInfos = new List<HeroCardInfo>();

        public long CurrentChooseTroopId;   //当前选择的队伍id

        // public int CurrentChooseInTroopIndex;   //当前选择的 在队伍里面的index   

        // public int CurrentChooseFilterIndex = 5;    //当前选择的过滤index

        // public List<HeroCardInfo> HeroCardInfos = new List<HeroCardInfo>();


    }
}