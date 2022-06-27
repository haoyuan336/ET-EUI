
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_ItemChooseHeadDestroySystem : DestroySystem<Scroll_ItemChooseHead> 
	{
		public override void Destroy( Scroll_ItemChooseHead self )
		{
			self.DestroyWidget();
		}
	}
}
