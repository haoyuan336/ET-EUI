
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgEditorTroopLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Image E_LoopTroopImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LoopTroopImage == null )
     			{
		    		this.m_E_LoopTroopImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_LoopTroop");
     			}
     			return this.m_E_LoopTroopImage;
     		}
     	}

		public UnityEngine.UI.LoopHorizontalScrollRect E_LoopTroopLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LoopTroopLoopHorizontalScrollRect == null )
     			{
		    		this.m_E_LoopTroopLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject,"E_LoopTroop");
     			}
     			return this.m_E_LoopTroopLoopHorizontalScrollRect;
     		}
     	}

		public UnityEngine.UI.ToggleGroup E_Content_TroopToggleGroup
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Content_TroopToggleGroup == null )
     			{
		    		this.m_E_Content_TroopToggleGroup = UIFindHelper.FindDeepChild<UnityEngine.UI.ToggleGroup>(this.uiTransform.gameObject,"E_LoopTroop/E_Content_Troop");
     			}
     			return this.m_E_Content_TroopToggleGroup;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect E_HeroBagLoopLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeroBagLoopLoopVerticalScrollRect == null )
     			{
		    		this.m_E_HeroBagLoopLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"E_HeroBagLoop");
     			}
     			return this.m_E_HeroBagLoopLoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.Image E_LoopTroopHeroImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LoopTroopHeroImage == null )
     			{
		    		this.m_E_LoopTroopHeroImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_LoopTroopHero");
     			}
     			return this.m_E_LoopTroopHeroImage;
     		}
     	}

		public UnityEngine.UI.LoopHorizontalScrollRect E_LoopTroopHeroLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LoopTroopHeroLoopHorizontalScrollRect == null )
     			{
		    		this.m_E_LoopTroopHeroLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject,"E_LoopTroopHero");
     			}
     			return this.m_E_LoopTroopHeroLoopHorizontalScrollRect;
     		}
     	}

		public UnityEngine.UI.ToggleGroup E_Content_TroopHeroToggleGroup
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Content_TroopHeroToggleGroup == null )
     			{
		    		this.m_E_Content_TroopHeroToggleGroup = UIFindHelper.FindDeepChild<UnityEngine.UI.ToggleGroup>(this.uiTransform.gameObject,"E_LoopTroopHero/E_Content_TroopHero");
     			}
     			return this.m_E_Content_TroopHeroToggleGroup;
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

		public UnityEngine.UI.Button E_StartGameButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StartGameButton == null )
     			{
		    		this.m_E_StartGameButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_StartGame");
     			}
     			return this.m_E_StartGameButton;
     		}
     	}

		public UnityEngine.UI.Image E_StartGameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StartGameImage == null )
     			{
		    		this.m_E_StartGameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_StartGame");
     			}
     			return this.m_E_StartGameImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_LoopTroopImage = null;
			this.m_E_LoopTroopLoopHorizontalScrollRect = null;
			this.m_E_Content_TroopToggleGroup = null;
			this.m_E_HeroBagLoopLoopVerticalScrollRect = null;
			this.m_E_LoopTroopHeroImage = null;
			this.m_E_LoopTroopHeroLoopHorizontalScrollRect = null;
			this.m_E_Content_TroopHeroToggleGroup = null;
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_TroopNameText = null;
			this.m_E_StartGameButton = null;
			this.m_E_StartGameImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_LoopTroopImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_E_LoopTroopLoopHorizontalScrollRect = null;
		private UnityEngine.UI.ToggleGroup m_E_Content_TroopToggleGroup = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_HeroBagLoopLoopVerticalScrollRect = null;
		private UnityEngine.UI.Image m_E_LoopTroopHeroImage = null;
		private UnityEngine.UI.LoopHorizontalScrollRect m_E_LoopTroopHeroLoopHorizontalScrollRect = null;
		private UnityEngine.UI.ToggleGroup m_E_Content_TroopHeroToggleGroup = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Text m_E_TroopNameText = null;
		private UnityEngine.UI.Button m_E_StartGameButton = null;
		private UnityEngine.UI.Image m_E_StartGameImage = null;
		public Transform uiTransform = null;
	}
}
