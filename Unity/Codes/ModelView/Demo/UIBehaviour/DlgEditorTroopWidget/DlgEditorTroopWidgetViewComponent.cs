
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgEditorTroopWidgetViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Image E_TroopHeroCardItemImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TroopHeroCardItemImage == null )
     			{
		    		this.m_E_TroopHeroCardItemImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_TroopHeroCardItem");
     			}
     			return this.m_E_TroopHeroCardItemImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_TroopHeroCardItemImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_TroopHeroCardItemImage = null;
		public Transform uiTransform = null;
	}
}
