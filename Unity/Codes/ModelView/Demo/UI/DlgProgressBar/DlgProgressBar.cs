namespace ET
{
	public  class DlgProgressBar :Entity,IAwake
	{

		public DlgProgressBarViewComponent View { get => this.Parent.GetComponent<DlgProgressBarViewComponent>();} 

		 

	}
}
