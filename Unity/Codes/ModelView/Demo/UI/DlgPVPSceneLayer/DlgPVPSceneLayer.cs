namespace ET
{
	public  class DlgPVPSceneLayer :Entity,IAwake
	{

		public DlgPVPSceneLayerViewComponent View { get => this.Parent.GetComponent<DlgPVPSceneLayerViewComponent>();} 

		 

	}
}
