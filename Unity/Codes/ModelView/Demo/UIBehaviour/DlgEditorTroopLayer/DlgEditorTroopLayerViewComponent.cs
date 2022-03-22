
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgEditorTroopLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Image ELoopScrollList_TroopImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_TroopImage == null )
     			{
		    		this.m_ELoopScrollList_TroopImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ELoopScrollList_Troop");
     			}
     			return this.m_ELoopScrollList_TroopImage;
     		}
     	}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_TroopLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_TroopLoopHorizontalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_TroopLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject,"ELoopScrollList_Troop");
     			}
     			return this.m_ELoopScrollList_TroopLoopHorizontalScrollRect;
     		}
     	}

		public UnityEngine.UI.ToggleGroup E_ContentToggleGroup
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ContentToggleGroup == null )
     			{
		    		this.m_E_ContentToggleGroup = UIFindHelper.FindDeepChild<UnityEngine.UI.ToggleGroup>(this.uiTransform.gameObject,"ELoopScrollList_Troop/E_Content");
     			}
     			return this.m_E_ContentToggleGroup;
     		}
     	}

		public UnityEngine.UI.Button E_BackButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackButton == null )
     			{
		    		this.m_E_BackButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Back");
     			}
     			return this.m_E_BackButton;
     		}
     	}

		public UnityEngine.UI.Image E_BackImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackImage == null )
     			{
		    		this.m_E_BackImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Back");
     			}
     			return this.m_E_BackImage;
     		}
     	}

		public UnityEngine.UI.Text E_TroopNameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TroopNameText == null )
     			{
		    		this.m_E_TroopNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TroopName");
     			}
     			return this.m_E_TroopNameText;
     		}
     	}

		public UnityEngine.UI.Image ELoopScrollList_TroopHeroImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_TroopHeroImage == null )
     			{
		    		this.m_ELoopScrollList_TroopHeroImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ELoopScrollList_TroopHero");
     			}
     			return this.m_ELoopScrollList_TroopHeroImage;
     		}
     	}

		public UnityEngine.UI.LoopHorizontalScrollRect ELoopScrollList_TroopHeroLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_TroopHeroLoopHorizontalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_TroopHeroLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject,"ELoopScrollList_TroopHero");
     			}
     			return this.m_ELoopScrollList_TroopHeroLoopHorizontalScrollRect;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_HeroLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_HeroLoopVerticalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_HeroLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"ELoopScrollList_Hero");
     			}
     			return this.m_ELoopScrollList_HeroLoopVerticalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_ELoopScrollList_TroopImage = null;
			this.m_ELoopScrollList_TroopLoopHorizontalScrollRect = null;
			this.m_E_ContentToggleGroup = null;
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_TroopNameText = null;
			this.m_ELoopScrollList_TroopHeroImage = null;
			this.m_ELoopScrollList_TroopHeroLoopHorizontalScrollRect = null;
			this.m_ELoopScrollList_HeroLoopVerticalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_ELoopScrollList_TroopImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_TroopLoopHorizontalScrollRect = null;
		private UnityEngine.UI.ToggleGroup m_E_ContentToggleGroup = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Text m_E_TroopNameText = null;
		private UnityEngine.UI.Image m_ELoopScrollList_TroopHeroImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_ELoopScrollList_TroopHeroLoopHorizontalScrollRect = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_HeroLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
