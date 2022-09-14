
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_ItemBuffDestroySystem : DestroySystem<Scroll_ItemBuff> 
	{
		public override void Destroy( Scroll_ItemBuff self )
		{
			self.DestroyWidget();
		}
	}
}
