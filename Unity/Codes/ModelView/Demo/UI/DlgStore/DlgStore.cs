namespace ET
{
	public  class DlgStore :Entity,IAwake
	{

		public DlgStoreViewComponent View { get => this.Parent.GetComponent<DlgStoreViewComponent>();} 

		 

	}
}
