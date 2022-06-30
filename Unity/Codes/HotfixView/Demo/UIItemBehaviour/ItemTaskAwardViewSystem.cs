
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_ItemTaskAwardDestroySystem : DestroySystem<Scroll_ItemTaskAward> 
	{
		public override void Destroy( Scroll_ItemTaskAward self )
		{
			self.DestroyWidget();
		}
	}
}
