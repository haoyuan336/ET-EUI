
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class ESWeaponBagCommonAwakeSystem : AwakeSystem<ESWeaponBagCommon,Transform> 
	{
		public override void Awake(ESWeaponBagCommon self,Transform transform)
		{
			self.uiTransform = transform;
		}
	}


	[ObjectSystem]
	public class ESWeaponBagCommonDestroySystem : DestroySystem<ESWeaponBagCommon> 
	{
		public override void Destroy(ESWeaponBagCommon self)
		{
			self.DestroyWidget();
		}
	}
}
