namespace ET
{
	public  class DlgAlertLayer :Entity,IAwake
	{

		public DlgAlertLayerViewComponent View { get => this.Parent.GetComponent<DlgAlertLayerViewComponent>();} 

		 

	}
}
