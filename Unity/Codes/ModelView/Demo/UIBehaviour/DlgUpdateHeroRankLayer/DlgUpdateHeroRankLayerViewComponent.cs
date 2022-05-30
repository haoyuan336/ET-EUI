
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgUpdateHeroRankLayerViewComponent : Entity,IAwake,IDestroy 
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

		public UnityEngine.UI.Image E_HeadImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeadImage == null )
     			{
		    		this.m_E_HeadImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/ItemHeroCard/E_Head");
     			}
     			return this.m_E_HeadImage;
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
		    		this.m_E_ElementImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/ItemHeroCard/E_Head/E_Element");
     			}
     			return this.m_E_ElementImage;
     		}
     	}

		public UnityEngine.UI.Text E_AddTextText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddTextText == null )
     			{
		    		this.m_E_AddTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/ItemHeroCard/E_Head/E_AddText");
     			}
     			return this.m_E_AddTextText;
     		}
     	}

		public UnityEngine.UI.Image E_FrameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_FrameImage == null )
     			{
		    		this.m_E_FrameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/ItemHeroCard/E_Frame");
     			}
     			return this.m_E_FrameImage;
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
		    		this.m_E_LevelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/ItemHeroCard/E_Level");
     			}
     			return this.m_E_LevelText;
     		}
     	}

		public UnityEngine.UI.Image E_InTroopMarkImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_InTroopMarkImage == null )
     			{
		    		this.m_E_InTroopMarkImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/ItemHeroCard/E_InTroopMark");
     			}
     			return this.m_E_InTroopMarkImage;
     		}
     	}

		public UnityEngine.UI.Text E_CountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CountText == null )
     			{
		    		this.m_E_CountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/ItemHeroCard/E_Count");
     			}
     			return this.m_E_CountText;
     		}
     	}

		public UnityEngine.UI.Text E_ChooseCountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ChooseCountText == null )
     			{
		    		this.m_E_ChooseCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/ItemHeroCard/E_ChooseCount");
     			}
     			return this.m_E_ChooseCountText;
     		}
     	}

		public UnityEngine.UI.Image E_QualityIconImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_QualityIconImage == null )
     			{
		    		this.m_E_QualityIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/ItemHeroCard/E_QualityIcon");
     			}
     			return this.m_E_QualityIconImage;
     		}
     	}

		public UnityEngine.UI.Toggle E_ChooseToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ChooseToggle == null )
     			{
		    		this.m_E_ChooseToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"Bg/ItemHeroCard/E_Choose");
     			}
     			return this.m_E_ChooseToggle;
     		}
     	}

		public UnityEngine.UI.Image E_CheckmarkImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CheckmarkImage == null )
     			{
		    		this.m_E_CheckmarkImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/ItemHeroCard/E_Choose/Background/E_Checkmark");
     			}
     			return this.m_E_CheckmarkImage;
     		}
     	}

		public UnityEngine.UI.Text E_SkillNameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SkillNameText == null )
     			{
		    		this.m_E_SkillNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/SkillInfoPlane/Image (1)/E_SkillName");
     			}
     			return this.m_E_SkillNameText;
     		}
     	}

		public UnityEngine.UI.Image E_SkillIconImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SkillIconImage == null )
     			{
		    		this.m_E_SkillIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/SkillInfoPlane/E_SkillIcon");
     			}
     			return this.m_E_SkillIconImage;
     		}
     	}

		public UnityEngine.UI.Text E_SkllDesText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SkllDesText == null )
     			{
		    		this.m_E_SkllDesText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/SkillInfoPlane/E_SkllDes");
     			}
     			return this.m_E_SkllDesText;
     		}
     	}

		public UnityEngine.UI.Text E_CurrentRankText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentRankText == null )
     			{
		    		this.m_E_CurrentRankText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_CurrentRank");
     			}
     			return this.m_E_CurrentRankText;
     		}
     	}

		public UnityEngine.UI.Text E_NextRankText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NextRankText == null )
     			{
		    		this.m_E_NextRankText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_NextRank");
     			}
     			return this.m_E_NextRankText;
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

		public UnityEngine.UI.Image E_MaterialIconImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MaterialIconImage == null )
     			{
		    		this.m_E_MaterialIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"NeedBg/E_MaterialIcon");
     			}
     			return this.m_E_MaterialIconImage;
     		}
     	}

		public UnityEngine.UI.Text E_NeedCountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NeedCountText == null )
     			{
		    		this.m_E_NeedCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"NeedBg/E_NeedCount");
     			}
     			return this.m_E_NeedCountText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_HeadImage = null;
			this.m_E_ElementImage = null;
			this.m_E_AddTextText = null;
			this.m_E_FrameImage = null;
			this.m_E_LevelText = null;
			this.m_E_InTroopMarkImage = null;
			this.m_E_CountText = null;
			this.m_E_ChooseCountText = null;
			this.m_E_QualityIconImage = null;
			this.m_E_ChooseToggle = null;
			this.m_E_CheckmarkImage = null;
			this.m_E_SkillNameText = null;
			this.m_E_SkillIconImage = null;
			this.m_E_SkllDesText = null;
			this.m_E_CurrentRankText = null;
			this.m_E_NextRankText = null;
			this.m_E_OkButtonButton = null;
			this.m_E_OkButtonImage = null;
			this.m_E_MaterialIconImage = null;
			this.m_E_NeedCountText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Image m_E_HeadImage = null;
		private UnityEngine.UI.Image m_E_ElementImage = null;
		private UnityEngine.UI.Text m_E_AddTextText = null;
		private UnityEngine.UI.Image m_E_FrameImage = null;
		private UnityEngine.UI.Text m_E_LevelText = null;
		private UnityEngine.UI.Image m_E_InTroopMarkImage = null;
		private UnityEngine.UI.Text m_E_CountText = null;
		private UnityEngine.UI.Text m_E_ChooseCountText = null;
		private UnityEngine.UI.Image m_E_QualityIconImage = null;
		private UnityEngine.UI.Toggle m_E_ChooseToggle = null;
		private UnityEngine.UI.Image m_E_CheckmarkImage = null;
		private UnityEngine.UI.Text m_E_SkillNameText = null;
		private UnityEngine.UI.Image m_E_SkillIconImage = null;
		private UnityEngine.UI.Text m_E_SkllDesText = null;
		private UnityEngine.UI.Text m_E_CurrentRankText = null;
		private UnityEngine.UI.Text m_E_NextRankText = null;
		private UnityEngine.UI.Button m_E_OkButtonButton = null;
		private UnityEngine.UI.Image m_E_OkButtonImage = null;
		private UnityEngine.UI.Image m_E_MaterialIconImage = null;
		private UnityEngine.UI.Text m_E_NeedCountText = null;
		public Transform uiTransform = null;
	}
}
