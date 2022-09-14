namespace ET
{
	public  class DlgPVESceneLayer :Entity,IAwake
	{

		public DlgPVESceneLayerViewComponent View { get => this.Parent.GetComponent<DlgPVESceneLayerViewComponent>();} 

		 

	}
}
