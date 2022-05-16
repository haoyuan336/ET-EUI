
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgAllHeroBagLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Toggle E_RedToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RedToggle == null )
     			{
		    		this.m_E_RedToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ColorContent/E_Red");
     			}
     			return this.m_E_RedToggle;
     		}
     	}

		public UnityEngine.UI.Image E_RedImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RedImage == null )
     			{
		    		this.m_E_RedImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ColorContent/E_Red");
     			}
     			return this.m_E_RedImage;
     		}
     	}

		public UnityEngine.UI.Toggle E_YellowToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_YellowToggle == null )
     			{
		    		this.m_E_YellowToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ColorContent/E_Yellow");
     			}
     			return this.m_E_YellowToggle;
     		}
     	}

		public UnityEngine.UI.Image E_YellowImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_YellowImage == null )
     			{
		    		this.m_E_YellowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ColorContent/E_Yellow");
     			}
     			return this.m_E_YellowImage;
     		}
     	}

		public UnityEngine.UI.Toggle E_GreenToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GreenToggle == null )
     			{
		    		this.m_E_GreenToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ColorContent/E_Green");
     			}
     			return this.m_E_GreenToggle;
     		}
     	}

		public UnityEngine.UI.Image E_GreenImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GreenImage == null )
     			{
		    		this.m_E_GreenImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ColorContent/E_Green");
     			}
     			return this.m_E_GreenImage;
     		}
     	}

		public UnityEngine.UI.Toggle E_BlueToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BlueToggle == null )
     			{
		    		this.m_E_BlueToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ColorContent/E_Blue");
     			}
     			return this.m_E_BlueToggle;
     		}
     	}

		public UnityEngine.UI.Image E_BlueImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BlueImage == null )
     			{
		    		this.m_E_BlueImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ColorContent/E_Blue");
     			}
     			return this.m_E_BlueImage;
     		}
     	}

		public UnityEngine.UI.Toggle E_PurpleToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PurpleToggle == null )
     			{
		    		this.m_E_PurpleToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ColorContent/E_Purple");
     			}
     			return this.m_E_PurpleToggle;
     		}
     	}

		public UnityEngine.UI.Image E_PurpleImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PurpleImage == null )
     			{
		    		this.m_E_PurpleImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ColorContent/E_Purple");
     			}
     			return this.m_E_PurpleImage;
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

		public UnityEngine.UI.Image E_AllImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AllImage == null )
     			{
		    		this.m_E_AllImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ColorContent/E_All");
     			}
     			return this.m_E_AllImage;
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
		    		this.m_E_LoopScrollListHeroLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"E_LoopScrollListHero");
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
		    		this.m_E_ContentToggleGroup = UIFindHelper.FindDeepChild<UnityEngine.UI.ToggleGroup>(this.uiTransform.gameObject,"E_LoopScrollListHero/E_Content");
     			}
     			return this.m_E_ContentToggleGroup;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_RedToggle = null;
			this.m_E_RedImage = null;
			this.m_E_YellowToggle = null;
			this.m_E_YellowImage = null;
			this.m_E_GreenToggle = null;
			this.m_E_GreenImage = null;
			this.m_E_BlueToggle = null;
			this.m_E_BlueImage = null;
			this.m_E_PurpleToggle = null;
			this.m_E_PurpleImage = null;
			this.m_E_AllToggle = null;
			this.m_E_AllImage = null;
			this.m_E_LoopScrollListHeroLoopVerticalScrollRect = null;
			this.m_E_ContentToggleGroup = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Toggle m_E_RedToggle = null;
		private UnityEngine.UI.Image m_E_RedImage = null;
		private UnityEngine.UI.Toggle m_E_YellowToggle = null;
		private UnityEngine.UI.Image m_E_YellowImage = null;
		private UnityEngine.UI.Toggle m_E_GreenToggle = null;
		private UnityEngine.UI.Image m_E_GreenImage = null;
		private UnityEngine.UI.Toggle m_E_BlueToggle = null;
		private UnityEngine.UI.Image m_E_BlueImage = null;
		private UnityEngine.UI.Toggle m_E_PurpleToggle = null;
		private UnityEngine.UI.Image m_E_PurpleImage = null;
		private UnityEngine.UI.Toggle m_E_AllToggle = null;
		private UnityEngine.UI.Image m_E_AllImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_LoopScrollListHeroLoopVerticalScrollRect = null;
		private UnityEngine.UI.ToggleGroup m_E_ContentToggleGroup = null;
		public Transform uiTransform = null;
	}
}
