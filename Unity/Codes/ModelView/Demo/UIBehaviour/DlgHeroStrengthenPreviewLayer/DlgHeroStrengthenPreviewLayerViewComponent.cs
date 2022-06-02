
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgHeroStrengthenPreviewLayerViewComponent : Entity,IAwake,IDestroy 
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

		public UnityEngine.UI.Image E_CurrentImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentImage == null )
     			{
		    		this.m_E_CurrentImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/E_Current");
     			}
     			return this.m_E_CurrentImage;
     		}
     	}

		public UnityEngine.UI.Button E_BaseInfoButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BaseInfoButton == null )
     			{
		    		this.m_E_BaseInfoButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_BaseInfo");
     			}
     			return this.m_E_BaseInfoButton;
     		}
     	}

		public UnityEngine.UI.Text E_CurrentLevelText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentLevelText == null )
     			{
		    		this.m_E_CurrentLevelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_BaseInfo/Grouplevel/E_CurrentLevel");
     			}
     			return this.m_E_CurrentLevelText;
     		}
     	}

		public UnityEngine.UI.Text E_NextLevelText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NextLevelText == null )
     			{
		    		this.m_E_NextLevelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_BaseInfo/Grouplevel/E_NextLevel");
     			}
     			return this.m_E_NextLevelText;
     		}
     	}

		public UnityEngine.UI.Text E_CurrentHPText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentHPText == null )
     			{
		    		this.m_E_CurrentHPText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_BaseInfo/Group/E_CurrentHP");
     			}
     			return this.m_E_CurrentHPText;
     		}
     	}

		public UnityEngine.UI.Text E_NextHPText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NextHPText == null )
     			{
		    		this.m_E_NextHPText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_BaseInfo/Group/E_NextHP");
     			}
     			return this.m_E_NextHPText;
     		}
     	}

		public UnityEngine.UI.Text E_CurrentAttackText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentAttackText == null )
     			{
		    		this.m_E_CurrentAttackText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_BaseInfo/Group (1)/E_CurrentAttack");
     			}
     			return this.m_E_CurrentAttackText;
     		}
     	}

		public UnityEngine.UI.Text E_NextAttackText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NextAttackText == null )
     			{
		    		this.m_E_NextAttackText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_BaseInfo/Group (1)/E_NextAttack");
     			}
     			return this.m_E_NextAttackText;
     		}
     	}

		public UnityEngine.UI.Text E_CurrentDefenceText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentDefenceText == null )
     			{
		    		this.m_E_CurrentDefenceText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_BaseInfo/Group (2)/E_CurrentDefence");
     			}
     			return this.m_E_CurrentDefenceText;
     		}
     	}

		public UnityEngine.UI.Text E_NextDefenceText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NextDefenceText == null )
     			{
		    		this.m_E_NextDefenceText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_BaseInfo/Group (2)/E_NextDefence");
     			}
     			return this.m_E_NextDefenceText;
     		}
     	}

		public UnityEngine.UI.Image ExpBarBgImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ExpBarBgImage == null )
     			{
		    		this.m_ExpBarBgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ExpBarBg");
     			}
     			return this.m_ExpBarBgImage;
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
		    		this.m_E_ExpBarImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"ExpBarBg/E_ExpBar");
     			}
     			return this.m_E_ExpBarImage;
     		}
     	}

		public UnityEngine.UI.Text E_CurLevelText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurLevelText == null )
     			{
		    		this.m_E_CurLevelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_CurLevel");
     			}
     			return this.m_E_CurLevelText;
     		}
     	}

		public UnityEngine.UI.Text E_EndLevelText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EndLevelText == null )
     			{
		    		this.m_E_EndLevelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_EndLevel");
     			}
     			return this.m_E_EndLevelText;
     		}
     	}

		public UnityEngine.UI.Text E_LastExpText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LastExpText == null )
     			{
		    		this.m_E_LastExpText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_LastExp");
     			}
     			return this.m_E_LastExpText;
     		}
     	}

		public UnityEngine.UI.Text E_TotalExpText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TotalExpText == null )
     			{
		    		this.m_E_TotalExpText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TotalExp");
     			}
     			return this.m_E_TotalExpText;
     		}
     	}

		public UnityEngine.UI.Image E_ChooseCardGroupImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ChooseCardGroupImage == null )
     			{
		    		this.m_E_ChooseCardGroupImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_ChooseCardGroup");
     			}
     			return this.m_E_ChooseCardGroupImage;
     		}
     	}

		public UnityEngine.UI.Button E_OkButtonButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_OkButtonButton == null )
     			{
		    		this.m_E_OkButtonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_OkButton");
     			}
     			return this.m_E_OkButtonButton;
     		}
     	}

		public UnityEngine.UI.Image E_OkButtonImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_OkButtonImage == null )
     			{
		    		this.m_E_OkButtonImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_OkButton");
     			}
     			return this.m_E_OkButtonImage;
     		}
     	}

		public UnityEngine.UI.Text E_AddExpText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddExpText == null )
     			{
		    		this.m_E_AddExpText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_AddExp");
     			}
     			return this.m_E_AddExpText;
     		}
     	}

		public UnityEngine.UI.Button E_AutoChooseButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AutoChooseButton == null )
     			{
		    		this.m_E_AutoChooseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_AutoChoose");
     			}
     			return this.m_E_AutoChooseButton;
     		}
     	}

		public UnityEngine.UI.Image E_AutoChooseImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AutoChooseImage == null )
     			{
		    		this.m_E_AutoChooseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_AutoChoose");
     			}
     			return this.m_E_AutoChooseImage;
     		}
     	}

		public UnityEngine.UI.Text E_NextLevelExpInfoText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NextLevelExpInfoText == null )
     			{
		    		this.m_E_NextLevelExpInfoText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_NextLevelExpInfo");
     			}
     			return this.m_E_NextLevelExpInfoText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_CurrentImage = null;
			this.m_E_BaseInfoButton = null;
			this.m_E_CurrentLevelText = null;
			this.m_E_NextLevelText = null;
			this.m_E_CurrentHPText = null;
			this.m_E_NextHPText = null;
			this.m_E_CurrentAttackText = null;
			this.m_E_NextAttackText = null;
			this.m_E_CurrentDefenceText = null;
			this.m_E_NextDefenceText = null;
			this.m_ExpBarBgImage = null;
			this.m_E_ExpBarImage = null;
			this.m_E_CurLevelText = null;
			this.m_E_EndLevelText = null;
			this.m_E_LastExpText = null;
			this.m_E_TotalExpText = null;
			this.m_E_ChooseCardGroupImage = null;
			this.m_E_OkButtonButton = null;
			this.m_E_OkButtonImage = null;
			this.m_E_AddExpText = null;
			this.m_E_AutoChooseButton = null;
			this.m_E_AutoChooseImage = null;
			this.m_E_NextLevelExpInfoText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Image m_E_CurrentImage = null;
		private UnityEngine.UI.Button m_E_BaseInfoButton = null;
		private UnityEngine.UI.Text m_E_CurrentLevelText = null;
		private UnityEngine.UI.Text m_E_NextLevelText = null;
		private UnityEngine.UI.Text m_E_CurrentHPText = null;
		private UnityEngine.UI.Text m_E_NextHPText = null;
		private UnityEngine.UI.Text m_E_CurrentAttackText = null;
		private UnityEngine.UI.Text m_E_NextAttackText = null;
		private UnityEngine.UI.Text m_E_CurrentDefenceText = null;
		private UnityEngine.UI.Text m_E_NextDefenceText = null;
		private UnityEngine.UI.Image m_ExpBarBgImage = null;
		private UnityEngine.UI.Image m_E_ExpBarImage = null;
		private UnityEngine.UI.Text m_E_CurLevelText = null;
		private UnityEngine.UI.Text m_E_EndLevelText = null;
		private UnityEngine.UI.Text m_E_LastExpText = null;
		private UnityEngine.UI.Text m_E_TotalExpText = null;
		private UnityEngine.UI.Image m_E_ChooseCardGroupImage = null;
		private UnityEngine.UI.Button m_E_OkButtonButton = null;
		private UnityEngine.UI.Image m_E_OkButtonImage = null;
		private UnityEngine.UI.Text m_E_AddExpText = null;
		private UnityEngine.UI.Button m_E_AutoChooseButton = null;
		private UnityEngine.UI.Image m_E_AutoChooseImage = null;
		private UnityEngine.UI.Text m_E_NextLevelExpInfoText = null;
		public Transform uiTransform = null;
	}
}
