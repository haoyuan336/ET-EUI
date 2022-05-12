namespace ET
{
	public  class DlgBuyAlert :Entity,IAwake
	{

		public DlgBuyAlertViewComponent View { get => this.Parent.GetComponent<DlgBuyAlertViewComponent>();}

		public WeaponConfig Config;

	}
}
