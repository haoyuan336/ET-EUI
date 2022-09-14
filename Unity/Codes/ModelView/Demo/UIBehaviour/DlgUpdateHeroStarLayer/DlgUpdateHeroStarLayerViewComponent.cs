
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgUpdateHeroStarLayerViewComponent : Entity,IAwake,IDestroy 
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

		public UnityEngine.UI.Button E_StarGroupButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StarGroupButton == null )
     			{
		    		this.m_E_StarGroupButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Bg/E_StarGroup");
     			}
     			return this.m_E_StarGroupButton;
     		}
     	}

		public UnityEngine.UI.Image E_CurrentHeroCardPosImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentHeroCardPosImage == null )
     			{
		    		this.m_E_CurrentHeroCardPosImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/E_CurrentHeroCardPos");
     			}
     			return this.m_E_CurrentHeroCardPosImage;
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
		    		this.m_E_BaseInfoButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Bg/E_BaseInfo");
     			}
     			return this.m_E_BaseInfoButton;
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
		    		this.m_E_CurrentHPText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_BaseInfo/Group/E_CurrentHP");
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
		    		this.m_E_NextHPText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_BaseInfo/Group/E_NextHP");
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
		    		this.m_E_CurrentAttackText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_BaseInfo/Group (1)/E_CurrentAttack");
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
		    		this.m_E_NextAttackText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_BaseInfo/Group (1)/E_NextAttack");
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
		    		this.m_E_CurrentDefenceText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_BaseInfo/Group (2)/E_CurrentDefence");
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
		    		this.m_E_NextDefenceText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_BaseInfo/Group (2)/E_NextDefence");
     			}
     			return this.m_E_NextDefenceText;
     		}
     	}

		public UnityEngine.UI.Text E_MaxText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MaxText == null )
     			{
		    		this.m_E_MaxText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_Max");
     			}
     			return this.m_E_MaxText;
     		}
     	}

		public UnityEngine.UI.Image E_ChooseImageImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ChooseImageImage == null )
     			{
		    		this.m_E_ChooseImageImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_ChooseImage");
     			}
     			return this.m_E_ChooseImageImage;
     		}
     	}

		public UnityEngine.UI.Image E_SuccessImageImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SuccessImageImage == null )
     			{
		    		this.m_E_SuccessImageImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_SuccessImage");
     			}
     			return this.m_E_SuccessImageImage;
     		}
     	}

		public UnityEngine.UI.Text E_RateText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RateText == null )
     			{
		    		this.m_E_RateText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Rate");
     			}
     			return this.m_E_RateText;
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

		public UnityEngine.UI.Text E_CastText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CastText == null )
     			{
		    		this.m_E_CastText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_OkButton/E_Cast");
     			}
     			return this.m_E_CastText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_StarGroupButton = null;
			this.m_E_CurrentHeroCardPosImage = null;
			this.m_E_BaseInfoButton = null;
			this.m_E_CurrentHPText = null;
			this.m_E_NextHPText = null;
			this.m_E_CurrentAttackText = null;
			this.m_E_NextAttackText = null;
			this.m_E_CurrentDefenceText = null;
			this.m_E_NextDefenceText = null;
			this.m_E_MaxText = null;
			this.m_E_ChooseImageImage = null;
			this.m_E_SuccessImageImage = null;
			this.m_E_RateText = null;
			this.m_E_OkButtonButton = null;
			this.m_E_OkButtonImage = null;
			this.m_E_CastText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Button m_E_StarGroupButton = null;
		private UnityEngine.UI.Image m_E_CurrentHeroCardPosImage = null;
		private UnityEngine.UI.Button m_E_BaseInfoButton = null;
		private UnityEngine.UI.Text m_E_CurrentHPText = null;
		private UnityEngine.UI.Text m_E_NextHPText = null;
		private UnityEngine.UI.Text m_E_CurrentAttackText = null;
		private UnityEngine.UI.Text m_E_NextAttackText = null;
		private UnityEngine.UI.Text m_E_CurrentDefenceText = null;
		private UnityEngine.UI.Text m_E_NextDefenceText = null;
		private UnityEngine.UI.Text m_E_MaxText = null;
		private UnityEngine.UI.Image m_E_ChooseImageImage = null;
		private UnityEngine.UI.Image m_E_SuccessImageImage = null;
		private UnityEngine.UI.Text m_E_RateText = null;
		private UnityEngine.UI.Button m_E_OkButtonButton = null;
		private UnityEngine.UI.Image m_E_OkButtonImage = null;
		private UnityEngine.UI.Text m_E_CastText = null;
		public Transform uiTransform = null;
	}
}
