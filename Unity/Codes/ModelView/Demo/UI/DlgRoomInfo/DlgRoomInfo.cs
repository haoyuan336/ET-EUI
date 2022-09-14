namespace ET
{
	public  class DlgRoomInfo :Entity,IAwake
	{

		public DlgRoomInfoViewComponent View { get => this.Parent.GetComponent<DlgRoomInfoViewComponent>();} 

		 

	}
}
