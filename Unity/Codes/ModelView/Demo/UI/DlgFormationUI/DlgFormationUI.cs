namespace ET
{
	public  class DlgFormationUI :Entity,IAwake
	{

		public DlgFormationUIViewComponent View { get => this.Parent.GetComponent<DlgFormationUIViewComponent>();} 

		 

	}
}
