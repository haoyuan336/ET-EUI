
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class ESHeroCard : Entity,ET.IAwake<UnityEngine.Transform>,IDestroy, IAwake
	{
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
		    		this.m_E_HeadImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Head");
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
		    		this.m_E_ElementImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Head/E_Element");
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
		    		this.m_E_AddTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Head/E_AddText");
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
		    		this.m_E_FrameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Frame");
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
		    		this.m_E_LevelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Level");
     			}
     			return this.m_E_LevelText;
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
		    		this.m_E_QualityIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_QualityIcon");
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
		    		this.m_E_ChooseToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_Choose");
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
		    		this.m_E_CheckmarkImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Choose/Background/E_Checkmark");
     			}
     			return this.m_E_CheckmarkImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_HeadImage = null;
			this.m_E_ElementImage = null;
			this.m_E_AddTextText = null;
			this.m_E_FrameImage = null;
			this.m_E_LevelText = null;
			this.m_E_QualityIconImage = null;
			this.m_E_ChooseToggle = null;
			this.m_E_CheckmarkImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_HeadImage = null;
		private UnityEngine.UI.Image m_E_ElementImage = null;
		private UnityEngine.UI.Text m_E_AddTextText = null;
		private UnityEngine.UI.Image m_E_FrameImage = null;
		private UnityEngine.UI.Text m_E_LevelText = null;
		private UnityEngine.UI.Image m_E_QualityIconImage = null;
		private UnityEngine.UI.Toggle m_E_ChooseToggle = null;
		private UnityEngine.UI.Image m_E_CheckmarkImage = null;
		public Transform uiTransform = null;
	}
}
