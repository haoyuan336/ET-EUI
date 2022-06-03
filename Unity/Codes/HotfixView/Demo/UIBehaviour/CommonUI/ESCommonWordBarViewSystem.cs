
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class ESCommonWordBarAwakeSystem : AwakeSystem<ESCommonWordBar,Transform> 
	{
		public override void Awake(ESCommonWordBar self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}


	[ObjectSystem]
	public class ESCommonWordBarDestroySystem : DestroySystem<ESCommonWordBar> 
	{
		public override void Destroy(ESCommonWordBar self)
		{
			self.DestroyWidget();
		}
	}
}
