using System;
using System.Collections.Generic;

namespace ET
{
    public class DlgEditorTroopLayer: Entity, IAwake,IDestroy
    {
        public DlgEditorTroopLayerViewComponent View
        {
            get => this.Parent.GetComponent<DlgEditorTroopLayerViewComponent>();
        }

        public List<HeroCardInfo> TroopHeroCardInfos = new List<HeroCardInfo>();

        public long CurrentChooseTroopId;   //当前选择的队伍id

        public Action HideEditorTroopLayerAction;

        public Action<List<HeroCardInfo>> EditorHeroCardAction;

        public List<Scroll_ItemHeroCard> ItemHeroCards = new List<Scroll_ItemHeroCard>();

    }
}