
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_ItemAwardDestroySystem : DestroySystem<Scroll_ItemAward> 
	{
		public override void Destroy( Scroll_ItemAward self )
		{
			self.DestroyWidget();
		}
	}
}
