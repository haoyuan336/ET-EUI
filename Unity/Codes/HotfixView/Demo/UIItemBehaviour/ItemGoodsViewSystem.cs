
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_ItemGoodsDestroySystem : DestroySystem<Scroll_ItemGoods> 
	{
		public override void Destroy( Scroll_ItemGoods self )
		{
			self.DestroyWidget();
		}
	}
}
