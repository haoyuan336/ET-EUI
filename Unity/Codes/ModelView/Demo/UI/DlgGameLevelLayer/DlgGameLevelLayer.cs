namespace ET
{
	public  class DlgGameLevelLayer :Entity,IAwake
	{

		public DlgGameLevelLayerViewComponent View { get => this.Parent.GetComponent<DlgGameLevelLayerViewComponent>();} 

		 

	}
}
