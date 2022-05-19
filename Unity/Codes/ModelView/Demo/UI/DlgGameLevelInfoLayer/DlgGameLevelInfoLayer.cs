namespace ET
{
	public  class DlgGameLevelInfoLayer :Entity,IAwake
	{

		public DlgGameLevelInfoLayerViewComponent View { get => this.Parent.GetComponent<DlgGameLevelInfoLayerViewComponent>();}


		public long CurrentChooseTroopId;

	}
}
