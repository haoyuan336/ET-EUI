
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_ItemGameComboDestroySystem : DestroySystem<Scroll_ItemGameCombo> 
	{
		public override void Destroy( Scroll_ItemGameCombo self )
		{
			self.DestroyWidget();
		}
	}
}
