using UnityEngine;

namespace ET
{
	public  class DlgMainScene :Entity,IAwake
	{

		public DlgMainSceneViewComponent View { get => this.Parent.GetComponent<DlgMainSceneViewComponent>();}

		public GameObject HeroMode;


	}
}
