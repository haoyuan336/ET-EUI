namespace ET
{
    public class DlgUpdateHeroRankLayer: Entity, IAwake
    {
        public DlgUpdateHeroRankLayerViewComponent View
        {
            get => this.Parent.GetComponent<DlgUpdateHeroRankLayerViewComponent>();
        }

        public HeroCardInfo HeroCardInfo;
    }
}