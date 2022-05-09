namespace ET
{
	public  class DlgPowerNotEnoughAlert :Entity,IAwake
	{

		public DlgPowerNotEnoughAlertViewComponent View { get => this.Parent.GetComponent<DlgPowerNotEnoughAlertViewComponent>();} 

		 

	}
}
