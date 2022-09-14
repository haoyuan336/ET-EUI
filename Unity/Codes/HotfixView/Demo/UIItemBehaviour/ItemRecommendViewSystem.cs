
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_ItemRecommendDestroySystem : DestroySystem<Scroll_ItemRecommend> 
	{
		public override void Destroy( Scroll_ItemRecommend self )
		{
			self.DestroyWidget();
		}
	}
}
