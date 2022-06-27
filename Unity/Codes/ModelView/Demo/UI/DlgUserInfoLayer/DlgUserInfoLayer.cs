namespace ET
{
	public  class DlgUserInfoLayer :Entity,IAwake
	{

		public DlgUserInfoLayerViewComponent View { get => this.Parent.GetComponent<DlgUserInfoLayerViewComponent>();}

		public AccountInfo AccountInfo;

	}
}
