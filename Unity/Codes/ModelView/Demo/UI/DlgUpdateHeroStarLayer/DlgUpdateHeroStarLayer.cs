namespace ET
{
    public class DlgUpdateHeroStarLayer: Entity, IAwake
    {
        public DlgUpdateHeroStarLayerViewComponent View
        {
            get => this.Parent.GetComponent<DlgUpdateHeroStarLayerViewComponent>();
        }

        public HeroCardInfo HeroCardInfo;
    }
}