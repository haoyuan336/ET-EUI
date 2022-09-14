
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgAllHeroBagLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Toggle E_FireToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_FireToggle == null )
     			{
		    		this.m_E_FireToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ColorContent/E_Fire");
     			}
     			return this.m_E_FireToggle;
     		}
     	}

		public UnityEngine.UI.Toggle E_DarkToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DarkToggle == null )
     			{
		    		this.m_E_DarkToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ColorContent/E_Dark");
     			}
     			return this.m_E_DarkToggle;
     		}
     	}

		public UnityEngine.UI.Toggle E_WaterToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WaterToggle == null )
     			{
		    		this.m_E_WaterToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ColorContent/E_Water");
     			}
     			return this.m_E_WaterToggle;
     		}
     	}

		public UnityEngine.UI.Toggle E_WindToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WindToggle == null )
     			{
		    		this.m_E_WindToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ColorContent/E_Wind");
     			}
     			return this.m_E_WindToggle;
     		}
     	}

		public UnityEngine.UI.Toggle E_LightToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LightToggle == null )
     			{
		    		this.m_E_LightToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ColorContent/E_Light");
     			}
     			return this.m_E_LightToggle;
     		}
     	}

		public UnityEngine.UI.Toggle E_AllToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AllToggle == null )
     			{
		    		this.m_E_AllToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ColorContent/E_All");
     			}
     			return this.m_E_AllToggle;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect E_LoopScrollListHeroLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LoopScrollListHeroLoopVerticalScrollRect == null )
     			{
		    		this.m_E_LoopScrollListHeroLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"Bg/Image (1)/E_LoopScrollListHero");
     			}
     			return this.m_E_LoopScrollListHeroLoopVerticalScrollRect;
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
		    		this.m_E_ContentToggleGroup = UIFindHelper.FindDeepChild<UnityEngine.UI.ToggleGroup>(this.uiTransform.gameObject,"Bg/Image (1)/E_LoopScrollListHero/E_Content");
     			}
     			return this.m_E_ContentToggleGroup;
     		}
     	}

		public UnityEngine.UI.Text E_BagCountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BagCountText == null )
     			{
		    		this.m_E_BagCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_BagCount");
     			}
     			return this.m_E_BagCountText;
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

		public void DestroyWidget()
		{
			this.m_E_FireToggle = null;
			this.m_E_DarkToggle = null;
			this.m_E_WaterToggle = null;
			this.m_E_WindToggle = null;
			this.m_E_LightToggle = null;
			this.m_E_AllToggle = null;
			this.m_E_LoopScrollListHeroLoopVerticalScrollRect = null;
			this.m_E_ContentToggleGroup = null;
			this.m_E_BagCountText = null;
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Toggle m_E_FireToggle = null;
		private UnityEngine.UI.Toggle m_E_DarkToggle = null;
		private UnityEngine.UI.Toggle m_E_WaterToggle = null;
		private UnityEngine.UI.Toggle m_E_WindToggle = null;
		private UnityEngine.UI.Toggle m_E_LightToggle = null;
		private UnityEngine.UI.Toggle m_E_AllToggle = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_LoopScrollListHeroLoopVerticalScrollRect = null;
		private UnityEngine.UI.ToggleGroup m_E_ContentToggleGroup = null;
		private UnityEngine.UI.Text m_E_BagCountText = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		public Transform uiTransform = null;
	}
}
