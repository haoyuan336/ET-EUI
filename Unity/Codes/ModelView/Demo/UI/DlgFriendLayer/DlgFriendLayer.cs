namespace ET
{
	public  class DlgFriendLayer :Entity,IAwake
	{

		public DlgFriendLayerViewComponent View { get => this.Parent.GetComponent<DlgFriendLayerViewComponent>();} 

		 

	}
}
