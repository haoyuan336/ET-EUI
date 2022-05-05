
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgShowHeroInfoLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Text E_HPText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HPText == null )
     			{
		    		this.m_E_HPText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"InfoBgPlane/Image/Scroll View/Viewport/Content/HP/E_HP");
     			}
     			return this.m_E_HPText;
     		}
     	}

		public UnityEngine.UI.Text E_ATKText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ATKText == null )
     			{
		    		this.m_E_ATKText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"InfoBgPlane/Image/Scroll View/Viewport/Content/ATK/E_ATK");
     			}
     			return this.m_E_ATKText;
     		}
     	}

		public UnityEngine.UI.Text E_DEFText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DEFText == null )
     			{
		    		this.m_E_DEFText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"InfoBgPlane/Image/Scroll View/Viewport/Content/DEF/E_DEF");
     			}
     			return this.m_E_DEFText;
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
		    		this.m_E_HeroNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"InfoBgPlane/E_HeroName");
     			}
     			return this.m_E_HeroNameText;
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

		public UnityEngine.UI.Button E_ComposeButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ComposeButton == null )
     			{
		    		this.m_E_ComposeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Compose");
     			}
     			return this.m_E_ComposeButton;
     		}
     	}

		public UnityEngine.UI.Image E_ComposeImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ComposeImage == null )
     			{
		    		this.m_E_ComposeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Compose");
     			}
     			return this.m_E_ComposeImage;
     		}
     	}

		public UnityEngine.UI.Button E_WeaponButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WeaponButton == null )
     			{
		    		this.m_E_WeaponButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Weapon");
     			}
     			return this.m_E_WeaponButton;
     		}
     	}

		public UnityEngine.UI.Image E_WeaponImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WeaponImage == null )
     			{
		    		this.m_E_WeaponImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Weapon");
     			}
     			return this.m_E_WeaponImage;
     		}
     	}

		public UnityEngine.UI.Button E_UpRankButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpRankButton == null )
     			{
		    		this.m_E_UpRankButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_UpRank");
     			}
     			return this.m_E_UpRankButton;
     		}
     	}

		public UnityEngine.UI.Image E_UpRankImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpRankImage == null )
     			{
		    		this.m_E_UpRankImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_UpRank");
     			}
     			return this.m_E_UpRankImage;
     		}
     	}

		public UnityEngine.UI.Button E_UpStarButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpStarButton == null )
     			{
		    		this.m_E_UpStarButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_UpStar");
     			}
     			return this.m_E_UpStarButton;
     		}
     	}

		public UnityEngine.UI.Image E_UpStarImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_UpStarImage == null )
     			{
		    		this.m_E_UpStarImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_UpStar");
     			}
     			return this.m_E_UpStarImage;
     		}
     	}

		public UnityEngine.UI.Button E_SkillButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SkillButton == null )
     			{
		    		this.m_E_SkillButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Skill");
     			}
     			return this.m_E_SkillButton;
     		}
     	}

		public UnityEngine.UI.Image E_SkillImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SkillImage == null )
     			{
		    		this.m_E_SkillImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Skill");
     			}
     			return this.m_E_SkillImage;
     		}
     	}

		public UnityEngine.UI.Button E_TalentButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TalentButton == null )
     			{
		    		this.m_E_TalentButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Talent");
     			}
     			return this.m_E_TalentButton;
     		}
     	}

		public UnityEngine.UI.Image E_TalentImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TalentImage == null )
     			{
		    		this.m_E_TalentImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Talent");
     			}
     			return this.m_E_TalentImage;
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

		public UnityEngine.UI.Text E_FightValueText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_FightValueText == null )
     			{
		    		this.m_E_FightValueText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_FightValue");
     			}
     			return this.m_E_FightValueText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_HPText = null;
			this.m_E_ATKText = null;
			this.m_E_DEFText = null;
			this.m_E_HeroNameText = null;
			this.m_E_LevelText = null;
			this.m_E_ComposeButton = null;
			this.m_E_ComposeImage = null;
			this.m_E_WeaponButton = null;
			this.m_E_WeaponImage = null;
			this.m_E_UpRankButton = null;
			this.m_E_UpRankImage = null;
			this.m_E_UpStarButton = null;
			this.m_E_UpStarImage = null;
			this.m_E_SkillButton = null;
			this.m_E_SkillImage = null;
			this.m_E_TalentButton = null;
			this.m_E_TalentImage = null;
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_FightValueText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_HPText = null;
		private UnityEngine.UI.Text m_E_ATKText = null;
		private UnityEngine.UI.Text m_E_DEFText = null;
		private UnityEngine.UI.Text m_E_HeroNameText = null;
		private UnityEngine.UI.Text m_E_LevelText = null;
		private UnityEngine.UI.Button m_E_ComposeButton = null;
		private UnityEngine.UI.Image m_E_ComposeImage = null;
		private UnityEngine.UI.Button m_E_WeaponButton = null;
		private UnityEngine.UI.Image m_E_WeaponImage = null;
		private UnityEngine.UI.Button m_E_UpRankButton = null;
		private UnityEngine.UI.Image m_E_UpRankImage = null;
		private UnityEngine.UI.Button m_E_UpStarButton = null;
		private UnityEngine.UI.Image m_E_UpStarImage = null;
		private UnityEngine.UI.Button m_E_SkillButton = null;
		private UnityEngine.UI.Image m_E_SkillImage = null;
		private UnityEngine.UI.Button m_E_TalentButton = null;
		private UnityEngine.UI.Image m_E_TalentImage = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Text m_E_FightValueText = null;
		public Transform uiTransform = null;
	}
}
