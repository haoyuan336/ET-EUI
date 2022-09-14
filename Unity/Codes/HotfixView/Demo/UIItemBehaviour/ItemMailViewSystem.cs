
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_ItemMailDestroySystem : DestroySystem<Scroll_ItemMail> 
	{
		public override void Destroy( Scroll_ItemMail self )
		{
			self.DestroyWidget();
		}
	}
}
