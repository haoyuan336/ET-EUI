namespace ET
{
	public  class DlgMainSceneMenu :Entity,IAwake
	{

		public DlgMainSceneMenuViewComponent View { get => this.Parent.GetComponent<DlgMainSceneMenuViewComponent>();} 

		 

	}
}
