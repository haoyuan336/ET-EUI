namespace ET
{
    public class DlgPVPMatchFightLayer: Entity, IAwake
    {
        public DlgPVPMatchFightLayerViewComponent View
        {
            get => this.Parent.GetComponent<DlgPVPMatchFightLayerViewComponent>();
        }

        public bool IsMatching = true;

        public float MatchTime = 0;

        public bool IsShow = false;
    }
}