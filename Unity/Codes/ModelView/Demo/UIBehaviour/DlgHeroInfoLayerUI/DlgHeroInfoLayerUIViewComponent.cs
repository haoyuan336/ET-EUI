
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgHeroInfoLayerUIViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_BagButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BagButton == null )
     			{
		    		this.m_E_BagButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Bag");
     			}
     			return this.m_E_BagButton;
     		}
     	}

		public UnityEngine.UI.Image E_BagImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BagImage == null )
     			{
		    		this.m_E_BagImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Bag");
     			}
     			return this.m_E_BagImage;
     		}
     	}

		public UnityEngine.UI.Button E_FilterButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_FilterButton == null )
     			{
		    		this.m_E_FilterButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Filter");
     			}
     			return this.m_E_FilterButton;
     		}
     	}

		public UnityEngine.UI.Image E_FilterImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_FilterImage == null )
     			{
		    		this.m_E_FilterImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Filter");
     			}
     			return this.m_E_FilterImage;
     		}
     	}

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
			this.m_E_BagButton = null;
			this.m_E_BagImage = null;
			this.m_E_FilterButton = null;
			this.m_E_FilterImage = null;
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
			this.m_E_LoopScrollListHeroLoopVerticalScrollRect = null;
			this.m_E_ContentToggleGroup = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BagButton = null;
		private UnityEngine.UI.Image m_E_BagImage = null;
		private UnityEngine.UI.Button m_E_FilterButton = null;
		private UnityEngine.UI.Image m_E_FilterImage = null;
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
		private UnityEngine.UI.LoopVerticalScrollRect m_E_LoopScrollListHeroLoopVerticalScrollRect = null;
		private UnityEngine.UI.ToggleGroup m_E_ContentToggleGroup = null;
		public Transform uiTransform = null;
	}
}
