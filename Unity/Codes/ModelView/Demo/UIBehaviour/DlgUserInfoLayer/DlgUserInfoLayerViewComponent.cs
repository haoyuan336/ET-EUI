
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgUserInfoLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Image E_HeadIconImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeadIconImage == null )
     			{
		    		this.m_E_HeadIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/E_HeadIcon");
     			}
     			return this.m_E_HeadIconImage;
     		}
     	}

		public UnityEngine.UI.Image E_HeadFrameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeadFrameImage == null )
     			{
		    		this.m_E_HeadFrameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/E_HeadFrame");
     			}
     			return this.m_E_HeadFrameImage;
     		}
     	}

		public UnityEngine.UI.Button E_HeadButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeadButton == null )
     			{
		    		this.m_E_HeadButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Bg/E_Head");
     			}
     			return this.m_E_HeadButton;
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
		    		this.m_E_HeadImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/E_Head");
     			}
     			return this.m_E_HeadImage;
     		}
     	}

		public UnityEngine.UI.Text E_NameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NameText == null )
     			{
		    		this.m_E_NameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_Name");
     			}
     			return this.m_E_NameText;
     		}
     	}

		public UnityEngine.UI.Button E_EdItorButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EdItorButton == null )
     			{
		    		this.m_E_EdItorButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Bg/E_EdItor");
     			}
     			return this.m_E_EdItorButton;
     		}
     	}

		public UnityEngine.UI.Image E_EdItorImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EdItorImage == null )
     			{
		    		this.m_E_EdItorImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/E_EdItor");
     			}
     			return this.m_E_EdItorImage;
     		}
     	}

		public UnityEngine.UI.Text E_IDText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_IDText == null )
     			{
		    		this.m_E_IDText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_ID");
     			}
     			return this.m_E_IDText;
     		}
     	}

		public UnityEngine.UI.Button E_CopyButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CopyButton == null )
     			{
		    		this.m_E_CopyButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Bg/E_Copy");
     			}
     			return this.m_E_CopyButton;
     		}
     	}

		public UnityEngine.UI.Image E_CopyImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CopyImage == null )
     			{
		    		this.m_E_CopyImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/E_Copy");
     			}
     			return this.m_E_CopyImage;
     		}
     	}

		public UnityEngine.UI.Text E_LabText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LabText == null )
     			{
		    		this.m_E_LabText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_Lab");
     			}
     			return this.m_E_LabText;
     		}
     	}

		public UnityEngine.UI.Text E_ClearanceProgressText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ClearanceProgressText == null )
     			{
		    		this.m_E_ClearanceProgressText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_ClearanceProgress");
     			}
     			return this.m_E_ClearanceProgressText;
     		}
     	}

		public UnityEngine.UI.Text E_HeroProgressText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeroProgressText == null )
     			{
		    		this.m_E_HeroProgressText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/Image/E_HeroProgress");
     			}
     			return this.m_E_HeroProgressText;
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
		    		this.m_E_BackButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Bg/E_Back");
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
		    		this.m_E_BackImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/E_Back");
     			}
     			return this.m_E_BackImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_HeadIconImage = null;
			this.m_E_HeadFrameImage = null;
			this.m_E_HeadButton = null;
			this.m_E_HeadImage = null;
			this.m_E_NameText = null;
			this.m_E_EdItorButton = null;
			this.m_E_EdItorImage = null;
			this.m_E_IDText = null;
			this.m_E_CopyButton = null;
			this.m_E_CopyImage = null;
			this.m_E_LabText = null;
			this.m_E_ClearanceProgressText = null;
			this.m_E_HeroProgressText = null;
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_HeadIconImage = null;
		private UnityEngine.UI.Image m_E_HeadFrameImage = null;
		private UnityEngine.UI.Button m_E_HeadButton = null;
		private UnityEngine.UI.Image m_E_HeadImage = null;
		private UnityEngine.UI.Text m_E_NameText = null;
		private UnityEngine.UI.Button m_E_EdItorButton = null;
		private UnityEngine.UI.Image m_E_EdItorImage = null;
		private UnityEngine.UI.Text m_E_IDText = null;
		private UnityEngine.UI.Button m_E_CopyButton = null;
		private UnityEngine.UI.Image m_E_CopyImage = null;
		private UnityEngine.UI.Text m_E_LabText = null;
		private UnityEngine.UI.Text m_E_ClearanceProgressText = null;
		private UnityEngine.UI.Text m_E_HeroProgressText = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		public Transform uiTransform = null;
	}
}
