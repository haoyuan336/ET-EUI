
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgPVPSceneLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Text E_TitleText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TitleText == null )
     			{
		    		this.m_E_TitleText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Title");
     			}
     			return this.m_E_TitleText;
     		}
     	}

		public UnityEngine.UI.Text E_OverTimeCountDownText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_OverTimeCountDownText == null )
     			{
		    		this.m_E_OverTimeCountDownText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_OverTimeCountDown");
     			}
     			return this.m_E_OverTimeCountDownText;
     		}
     	}

		public UnityEngine.UI.Image E_RankImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RankImage == null )
     			{
		    		this.m_E_RankImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Rank");
     			}
     			return this.m_E_RankImage;
     		}
     	}

		public UnityEngine.UI.Text E_ScoreBlockText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ScoreBlockText == null )
     			{
		    		this.m_E_ScoreBlockText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_ScoreBlock");
     			}
     			return this.m_E_ScoreBlockText;
     		}
     	}

		public UnityEngine.UI.Text E_CurrentScoreText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentScoreText == null )
     			{
		    		this.m_E_CurrentScoreText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_CurrentScore");
     			}
     			return this.m_E_CurrentScoreText;
     		}
     	}

		public UnityEngine.UI.Button E_StartFightButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StartFightButton == null )
     			{
		    		this.m_E_StartFightButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_StartFight");
     			}
     			return this.m_E_StartFightButton;
     		}
     	}

		public UnityEngine.UI.Image E_StartFightImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StartFightImage == null )
     			{
		    		this.m_E_StartFightImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_StartFight");
     			}
     			return this.m_E_StartFightImage;
     		}
     	}

		public UnityEngine.UI.Image E_AwardImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AwardImage == null )
     			{
		    		this.m_E_AwardImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Award");
     			}
     			return this.m_E_AwardImage;
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
			this.m_E_TitleText = null;
			this.m_E_OverTimeCountDownText = null;
			this.m_E_RankImage = null;
			this.m_E_ScoreBlockText = null;
			this.m_E_CurrentScoreText = null;
			this.m_E_StartFightButton = null;
			this.m_E_StartFightImage = null;
			this.m_E_AwardImage = null;
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_TitleText = null;
		private UnityEngine.UI.Text m_E_OverTimeCountDownText = null;
		private UnityEngine.UI.Image m_E_RankImage = null;
		private UnityEngine.UI.Text m_E_ScoreBlockText = null;
		private UnityEngine.UI.Text m_E_CurrentScoreText = null;
		private UnityEngine.UI.Button m_E_StartFightButton = null;
		private UnityEngine.UI.Image m_E_StartFightImage = null;
		private UnityEngine.UI.Image m_E_AwardImage = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		public Transform uiTransform = null;
	}
}
