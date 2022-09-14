
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_ItemAddTextDestroySystem : DestroySystem<Scroll_ItemAddText> 
	{
		public override void Destroy( Scroll_ItemAddText self )
		{
			self.DestroyWidget();
		}
	}
}
