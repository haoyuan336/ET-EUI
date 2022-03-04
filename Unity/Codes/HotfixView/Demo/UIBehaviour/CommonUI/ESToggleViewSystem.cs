
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class ESToggleAwakeSystem : AwakeSystem<ESToggle,Transform> 
	{
		public override void Awake(ESToggle self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}


	[ObjectSystem]
	public class ESToggleDestroySystem : DestroySystem<ESToggle> 
	{
		public override void Destroy(ESToggle self)
		{
			self.DestroyWidget();
		}
	}
}
