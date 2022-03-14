namespace ET
{
	public  class DlgGameUI :Entity,IAwake
	{

		public DlgGameUIViewComponent View { get => this.Parent.GetComponent<DlgGameUIViewComponent>();} 

		 

	}
}
