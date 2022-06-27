namespace ET
{
	public  class DlgAccountInfo :Entity,IAwake
	{

		public DlgAccountInfoViewComponent View { get => this.Parent.GetComponent<DlgAccountInfoViewComponent>();}

		public AccountInfo AccountInfo;

	}
}
