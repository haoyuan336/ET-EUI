
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgMailInfoLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Text E_MailContentText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MailContentText == null )
     			{
		    		this.m_E_MailContentText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Image/Image (1)/E_MailContent");
     			}
     			return this.m_E_MailContentText;
     		}
     	}

		public UnityEngine.UI.Text E_MailTitleText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MailTitleText == null )
     			{
		    		this.m_E_MailTitleText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Image/E_MailTitle");
     			}
     			return this.m_E_MailTitleText;
     		}
     	}

		public UnityEngine.UI.Image E_AwardContentImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AwardContentImage == null )
     			{
		    		this.m_E_AwardContentImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Image/E_AwardContent");
     			}
     			return this.m_E_AwardContentImage;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopAwardLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopAwardLoopVerticalScrollRect == null )
     			{
		    		this.m_ELoopAwardLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"Image/E_AwardContent/ELoopAward");
     			}
     			return this.m_ELoopAwardLoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.Button E_GetClickButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GetClickButton == null )
     			{
		    		this.m_E_GetClickButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Image/E_GetClick");
     			}
     			return this.m_E_GetClickButton;
     		}
     	}

		public UnityEngine.UI.Image E_GetClickImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GetClickImage == null )
     			{
		    		this.m_E_GetClickImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Image/E_GetClick");
     			}
     			return this.m_E_GetClickImage;
     		}
     	}

		public UnityEngine.UI.Text E_GetText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GetText == null )
     			{
		    		this.m_E_GetText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Image/E_GetClick/E_Get");
     			}
     			return this.m_E_GetText;
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
		    		this.m_E_BackButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Image/E_Back");
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
		    		this.m_E_BackImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Image/E_Back");
     			}
     			return this.m_E_BackImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_MailContentText = null;
			this.m_E_MailTitleText = null;
			this.m_E_AwardContentImage = null;
			this.m_ELoopAwardLoopVerticalScrollRect = null;
			this.m_E_GetClickButton = null;
			this.m_E_GetClickImage = null;
			this.m_E_GetText = null;
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_MailContentText = null;
		private UnityEngine.UI.Text m_E_MailTitleText = null;
		private UnityEngine.UI.Image m_E_AwardContentImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopAwardLoopVerticalScrollRect = null;
		private UnityEngine.UI.Button m_E_GetClickButton = null;
		private UnityEngine.UI.Image m_E_GetClickImage = null;
		private UnityEngine.UI.Text m_E_GetText = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		public Transform uiTransform = null;
	}
}
