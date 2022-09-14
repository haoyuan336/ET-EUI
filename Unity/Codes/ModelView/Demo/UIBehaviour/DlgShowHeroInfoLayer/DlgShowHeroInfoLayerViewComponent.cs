
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgShowHeroInfoLayerViewComponent : Entity,IAwake,IDestroy 
	{
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

		public UnityEngine.UI.Image E_StarContentImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StarContentImage == null )
     			{
		    		this.m_E_StarContentImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Image/E_StarContent");
     			}
     			return this.m_E_StarContentImage;
     		}
     	}

		public UnityEngine.UI.Text E_HeroNameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeroNameText == null )
     			{
		    		this.m_E_HeroNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Image/E_HeroName");
     			}
     			return this.m_E_HeroNameText;
     		}
     	}

		public UnityEngine.UI.Image E_ElementImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ElementImage == null )
     			{
		    		this.m_E_ElementImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Image/E_Element");
     			}
     			return this.m_E_ElementImage;
     		}
     	}

		public UnityEngine.UI.Image E_BgImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BgImage == null )
     			{
		    		this.m_E_BgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Bg");
     			}
     			return this.m_E_BgImage;
     		}
     	}

		public UnityEngine.UI.Text E_LevelText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LevelText == null )
     			{
		    		this.m_E_LevelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Bg/LevelInfo/Level/E_Level");
     			}
     			return this.m_E_LevelText;
     		}
     	}

		public UnityEngine.UI.Image E_ExpBarImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ExpBarImage == null )
     			{
		    		this.m_E_ExpBarImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Bg/LevelInfo/Level/Image (2)/E_ExpBar");
     			}
     			return this.m_E_ExpBarImage;
     		}
     	}

		public UnityEngine.UI.Text E_ExpText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ExpText == null )
     			{
		    		this.m_E_ExpText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Bg/LevelInfo/Level/E_Exp");
     			}
     			return this.m_E_ExpText;
     		}
     	}

		public UnityEngine.UI.Text E_RankText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RankText == null )
     			{
		    		this.m_E_RankText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Bg/LevelInfo/Level/E_Rank");
     			}
     			return this.m_E_RankText;
     		}
     	}

		public UnityEngine.UI.Button E_UpdateLevelButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpdateLevelButton == null )
     			{
		    		this.m_E_UpdateLevelButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Bg/LevelInfo/E_UpdateLevel");
     			}
     			return this.m_E_UpdateLevelButton;
     		}
     	}

		public UnityEngine.UI.Image E_UpdateLevelImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpdateLevelImage == null )
     			{
		    		this.m_E_UpdateLevelImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Bg/LevelInfo/E_UpdateLevel");
     			}
     			return this.m_E_UpdateLevelImage;
     		}
     	}

		public UnityEngine.UI.Text E_UpdateText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpdateText == null )
     			{
		    		this.m_E_UpdateText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Bg/LevelInfo/E_UpdateLevel/E_Update");
     			}
     			return this.m_E_UpdateText;
     		}
     	}

		public UnityEngine.UI.Button E_UpdateStarButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpdateStarButton == null )
     			{
		    		this.m_E_UpdateStarButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Bg/LevelInfo/E_UpdateStar");
     			}
     			return this.m_E_UpdateStarButton;
     		}
     	}

		public UnityEngine.UI.Image E_UpdateStarImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpdateStarImage == null )
     			{
		    		this.m_E_UpdateStarImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Bg/LevelInfo/E_UpdateStar");
     			}
     			return this.m_E_UpdateStarImage;
     		}
     	}

		public UnityEngine.UI.Text E_BaseAttackText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BaseAttackText == null )
     			{
		    		this.m_E_BaseAttackText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Bg/InfoBgPlane/Label/E_BaseAttack");
     			}
     			return this.m_E_BaseAttackText;
     		}
     	}

		public UnityEngine.UI.Text E_BaseHPText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BaseHPText == null )
     			{
		    		this.m_E_BaseHPText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Bg/InfoBgPlane/Label (1)/E_BaseHP");
     			}
     			return this.m_E_BaseHPText;
     		}
     	}

		public UnityEngine.UI.Text E_BaseDefText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BaseDefText == null )
     			{
		    		this.m_E_BaseDefText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Bg/InfoBgPlane/Label (2)/E_BaseDef");
     			}
     			return this.m_E_BaseDefText;
     		}
     	}

		public UnityEngine.UI.Image E_WeaponInfoImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WeaponInfoImage == null )
     			{
		    		this.m_E_WeaponInfoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_WeaponInfo");
     			}
     			return this.m_E_WeaponInfoImage;
     		}
     	}

		public UnityEngine.UI.Button E_ShowModelButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ShowModelButton == null )
     			{
		    		this.m_E_ShowModelButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_ShowModel");
     			}
     			return this.m_E_ShowModelButton;
     		}
     	}

		public UnityEngine.UI.Image E_ShowModelImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ShowModelImage == null )
     			{
		    		this.m_E_ShowModelImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_ShowModel");
     			}
     			return this.m_E_ShowModelImage;
     		}
     	}

		public UnityEngine.UI.Toggle E_LockToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LockToggle == null )
     			{
		    		this.m_E_LockToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_Lock");
     			}
     			return this.m_E_LockToggle;
     		}
     	}

		public UnityEngine.UI.Image E_LockImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LockImage == null )
     			{
		    		this.m_E_LockImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Lock");
     			}
     			return this.m_E_LockImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_StarContentImage = null;
			this.m_E_HeroNameText = null;
			this.m_E_ElementImage = null;
			this.m_E_BgImage = null;
			this.m_E_LevelText = null;
			this.m_E_ExpBarImage = null;
			this.m_E_ExpText = null;
			this.m_E_RankText = null;
			this.m_E_UpdateLevelButton = null;
			this.m_E_UpdateLevelImage = null;
			this.m_E_UpdateText = null;
			this.m_E_UpdateStarButton = null;
			this.m_E_UpdateStarImage = null;
			this.m_E_BaseAttackText = null;
			this.m_E_BaseHPText = null;
			this.m_E_BaseDefText = null;
			this.m_E_WeaponInfoImage = null;
			this.m_E_ShowModelButton = null;
			this.m_E_ShowModelImage = null;
			this.m_E_LockToggle = null;
			this.m_E_LockImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Image m_E_StarContentImage = null;
		private UnityEngine.UI.Text m_E_HeroNameText = null;
		private UnityEngine.UI.Image m_E_ElementImage = null;
		private UnityEngine.UI.Image m_E_BgImage = null;
		private UnityEngine.UI.Text m_E_LevelText = null;
		private UnityEngine.UI.Image m_E_ExpBarImage = null;
		private UnityEngine.UI.Text m_E_ExpText = null;
		private UnityEngine.UI.Text m_E_RankText = null;
		private UnityEngine.UI.Button m_E_UpdateLevelButton = null;
		private UnityEngine.UI.Image m_E_UpdateLevelImage = null;
		private UnityEngine.UI.Text m_E_UpdateText = null;
		private UnityEngine.UI.Button m_E_UpdateStarButton = null;
		private UnityEngine.UI.Image m_E_UpdateStarImage = null;
		private UnityEngine.UI.Text m_E_BaseAttackText = null;
		private UnityEngine.UI.Text m_E_BaseHPText = null;
		private UnityEngine.UI.Text m_E_BaseDefText = null;
		private UnityEngine.UI.Image m_E_WeaponInfoImage = null;
		private UnityEngine.UI.Button m_E_ShowModelButton = null;
		private UnityEngine.UI.Image m_E_ShowModelImage = null;
		private UnityEngine.UI.Toggle m_E_LockToggle = null;
		private UnityEngine.UI.Image m_E_LockImage = null;
		public Transform uiTransform = null;
	}
}
