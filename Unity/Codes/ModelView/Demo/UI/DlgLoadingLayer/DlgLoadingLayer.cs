namespace ET
{
	public  class DlgLoadingLayer :Entity,IAwake
	{

		public DlgLoadingLayerViewComponent View { get => this.Parent.GetComponent<DlgLoadingLayerViewComponent>();} 

		 

	}
}
