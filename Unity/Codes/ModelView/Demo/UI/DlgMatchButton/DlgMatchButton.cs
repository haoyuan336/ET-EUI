namespace ET
{
	public  class DlgMatchButton :Entity,IAwake
	{
		public DlgMatchButtonViewComponent View { get => this.Parent.GetComponent<DlgMatchButtonViewComponent>();} 
	}
}
