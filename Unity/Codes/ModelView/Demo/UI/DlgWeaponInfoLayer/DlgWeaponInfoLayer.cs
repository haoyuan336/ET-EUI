namespace ET
{
	public  class DlgWeaponInfoLayer :Entity,IAwake
	{

		public DlgWeaponInfoLayerViewComponent View { get => this.Parent.GetComponent<DlgWeaponInfoLayerViewComponent>();} 

		 

	}
}
