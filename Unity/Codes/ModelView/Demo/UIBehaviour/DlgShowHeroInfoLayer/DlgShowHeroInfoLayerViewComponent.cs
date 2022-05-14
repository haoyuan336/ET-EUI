
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgShowHeroInfoLayerViewComponent : Entity,IAwake,IDestroy 
	{
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
		    		this.m_E_HeroNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"InfoBgPlane/E_HeroName");
     			}
     			return this.m_E_HeroNameText;
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
		    		this.m_E_StarContentImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"InfoBgPlane/E_StarContent");
     			}
     			return this.m_E_StarContentImage;
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
		    		this.m_E_LevelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"InfoBgPlane/Level/E_Level");
     			}
     			return this.m_E_LevelText;
     		}
     	}

		public UnityEngine.UI.Text E_FightText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_FightText == null )
     			{
		    		this.m_E_FightText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"InfoBgPlane/Scroll View/Viewport/Content/HP/E_Fight");
     			}
     			return this.m_E_FightText;
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
		    		this.m_E_ElementImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"InfoBgPlane/E_Element");
     			}
     			return this.m_E_ElementImage;
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

		public UnityEngine.UI.Toggle E_ComposeToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ComposeToggle == null )
     			{
		    		this.m_E_ComposeToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ButtonContent/E_Compose");
     			}
     			return this.m_E_ComposeToggle;
     		}
     	}

		public UnityEngine.UI.Toggle E_WeaponToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WeaponToggle == null )
     			{
		    		this.m_E_WeaponToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ButtonContent/E_Weapon");
     			}
     			return this.m_E_WeaponToggle;
     		}
     	}

		public UnityEngine.UI.Toggle E_UpRankToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpRankToggle == null )
     			{
		    		this.m_E_UpRankToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ButtonContent/E_UpRank");
     			}
     			return this.m_E_UpRankToggle;
     		}
     	}

		public UnityEngine.UI.Toggle E_UpStarToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpStarToggle == null )
     			{
		    		this.m_E_UpStarToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ButtonContent/E_UpStar");
     			}
     			return this.m_E_UpStarToggle;
     		}
     	}

		public UnityEngine.UI.Toggle E_SkillToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SkillToggle == null )
     			{
		    		this.m_E_SkillToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ButtonContent/E_Skill");
     			}
     			return this.m_E_SkillToggle;
     		}
     	}

		public UnityEngine.UI.Toggle E_TalentToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TalentToggle == null )
     			{
		    		this.m_E_TalentToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ButtonContent/E_Talent");
     			}
     			return this.m_E_TalentToggle;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_HeroNameText = null;
			this.m_E_StarContentImage = null;
			this.m_E_LevelText = null;
			this.m_E_FightText = null;
			this.m_E_ElementImage = null;
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_ComposeToggle = null;
			this.m_E_WeaponToggle = null;
			this.m_E_UpRankToggle = null;
			this.m_E_UpStarToggle = null;
			this.m_E_SkillToggle = null;
			this.m_E_TalentToggle = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_HeroNameText = null;
		private UnityEngine.UI.Image m_E_StarContentImage = null;
		private UnityEngine.UI.Text m_E_LevelText = null;
		private UnityEngine.UI.Text m_E_FightText = null;
		private UnityEngine.UI.Image m_E_ElementImage = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Toggle m_E_ComposeToggle = null;
		private UnityEngine.UI.Toggle m_E_WeaponToggle = null;
		private UnityEngine.UI.Toggle m_E_UpRankToggle = null;
		private UnityEngine.UI.Toggle m_E_UpStarToggle = null;
		private UnityEngine.UI.Toggle m_E_SkillToggle = null;
		private UnityEngine.UI.Toggle m_E_TalentToggle = null;
		public Transform uiTransform = null;
	}
}
