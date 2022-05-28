
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
		    		this.m_E_BaseAttackText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"InfoBgPlane/Scroll View/Viewport/Content/Label/E_BaseAttack");
     			}
     			return this.m_E_BaseAttackText;
     		}
     	}

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
		    		this.m_E_HPText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"InfoBgPlane/Scroll View/Viewport/Content/Label (1)/E_HP");
     			}
     			return this.m_E_HPText;
     		}
     	}

		public UnityEngine.UI.Text E_DefenceText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DefenceText == null )
     			{
		    		this.m_E_DefenceText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"InfoBgPlane/Scroll View/Viewport/Content/Label (2)/E_Defence");
     			}
     			return this.m_E_DefenceText;
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
		    		this.m_E_RankText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"InfoBgPlane/E_Rank");
     			}
     			return this.m_E_RankText;
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
		    		this.m_E_ComposeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"ButtonContent/E_Compose");
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
		    		this.m_E_ComposeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ButtonContent/E_Compose");
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
		    		this.m_E_WeaponButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"ButtonContent/E_Weapon");
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
		    		this.m_E_WeaponImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ButtonContent/E_Weapon");
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
		    		this.m_E_UpRankButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"ButtonContent/E_UpRank");
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
		    		this.m_E_UpRankImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ButtonContent/E_UpRank");
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
		    		this.m_E_UpStarButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"ButtonContent/E_UpStar");
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
		    		this.m_E_UpStarImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ButtonContent/E_UpStar");
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
		    		this.m_E_SkillButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"ButtonContent/E_Skill");
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
		    		this.m_E_SkillImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ButtonContent/E_Skill");
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
		    		this.m_E_TalentButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"ButtonContent/E_Talent");
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
		    		this.m_E_TalentImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ButtonContent/E_Talent");
     			}
     			return this.m_E_TalentImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_HeroNameText = null;
			this.m_E_StarContentImage = null;
			this.m_E_LevelText = null;
			this.m_E_BaseAttackText = null;
			this.m_E_HPText = null;
			this.m_E_DefenceText = null;
			this.m_E_ElementImage = null;
			this.m_E_RankText = null;
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
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
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_HeroNameText = null;
		private UnityEngine.UI.Image m_E_StarContentImage = null;
		private UnityEngine.UI.Text m_E_LevelText = null;
		private UnityEngine.UI.Text m_E_BaseAttackText = null;
		private UnityEngine.UI.Text m_E_HPText = null;
		private UnityEngine.UI.Text m_E_DefenceText = null;
		private UnityEngine.UI.Image m_E_ElementImage = null;
		private UnityEngine.UI.Text m_E_RankText = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
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
		public Transform uiTransform = null;
	}
}
