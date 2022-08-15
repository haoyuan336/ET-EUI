
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgEditorTroopLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.ToggleGroup E_TroopGroupContentToggleGroup
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TroopGroupContentToggleGroup == null )
     			{
		    		this.m_E_TroopGroupContentToggleGroup = UIFindHelper.FindDeepChild<UnityEngine.UI.ToggleGroup>(this.uiTransform.gameObject,"E_TroopGroupContent");
     			}
     			return this.m_E_TroopGroupContentToggleGroup;
     		}
     	}

		public UnityEngine.UI.Image E_TroopGroupContentImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TroopGroupContentImage == null )
     			{
		    		this.m_E_TroopGroupContentImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_TroopGroupContent");
     			}
     			return this.m_E_TroopGroupContentImage;
     		}
     	}

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
			this.m_E_TroopGroupContentToggleGroup = null;
			this.m_E_TroopGroupContentImage = null;
			this.m_E_TroopHeroCardItemImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.ToggleGroup m_E_TroopGroupContentToggleGroup = null;
		private UnityEngine.UI.Image m_E_TroopGroupContentImage = null;
		private UnityEngine.UI.Image m_E_TroopHeroCardItemImage = null;
		public Transform uiTransform = null;
	}
}
