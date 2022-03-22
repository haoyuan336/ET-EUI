using System.Collections.Generic;

namespace ET
{
    public class DlgEditorTroopLayer: Entity, IAwake
    {
        public DlgEditorTroopLayerViewComponent View
        {
            get => this.Parent.GetComponent<DlgEditorTroopLayerViewComponent>();
        }

        public Dictionary<int, Scroll_ItemHeroTroop> ItemTroops = new Dictionary<int, Scroll_ItemHeroTroop>();

        public Dictionary<int, Scroll_ItemTroopHeroCard> ItemTroopHeroCards = new Dictionary<int, Scroll_ItemTroopHeroCard>();

        public Dictionary<int, Scroll_ItemHeroCard> ItemHeroCards = new Dictionary<int, Scroll_ItemHeroCard>();

        public List<TroopInfo> TroopInfos = new List<TroopInfo>();

        public List<HeroCardInfo> HeroCardInfos = new List<HeroCardInfo>();


    }
}