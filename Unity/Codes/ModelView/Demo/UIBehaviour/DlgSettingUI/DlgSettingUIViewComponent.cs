
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgSettingUIViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Image E_BGImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BGImage == null )
     			{
		    		this.m_E_BGImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_BG");
     			}
     			return this.m_E_BGImage;
     		}
     	}

		public UnityEngine.UI.Button E_SettingButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SettingButton == null )
     			{
		    		this.m_E_SettingButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_BG/MaskNode/E_Setting");
     			}
     			return this.m_E_SettingButton;
     		}
     	}

		public UnityEngine.UI.Image E_SettingImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SettingImage == null )
     			{
		    		this.m_E_SettingImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_BG/MaskNode/E_Setting");
     			}
     			return this.m_E_SettingImage;
     		}
     	}

		public UnityEngine.UI.Button E_MailButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MailButton == null )
     			{
		    		this.m_E_MailButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_BG/MaskNode/E_Mail");
     			}
     			return this.m_E_MailButton;
     		}
     	}

		public UnityEngine.UI.Image E_MailImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MailImage == null )
     			{
		    		this.m_E_MailImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_BG/MaskNode/E_Mail");
     			}
     			return this.m_E_MailImage;
     		}
     	}

		public UnityEngine.UI.Image E_RedDotImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RedDotImage == null )
     			{
		    		this.m_E_RedDotImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_BG/MaskNode/E_Mail/E_RedDot");
     			}
     			return this.m_E_RedDotImage;
     		}
     	}

		public UnityEngine.UI.Button E_FriendButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_FriendButton == null )
     			{
		    		this.m_E_FriendButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_BG/MaskNode/E_Friend");
     			}
     			return this.m_E_FriendButton;
     		}
     	}

		public UnityEngine.UI.Image E_FriendImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_FriendImage == null )
     			{
		    		this.m_E_FriendImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_BG/MaskNode/E_Friend");
     			}
     			return this.m_E_FriendImage;
     		}
     	}

		public UnityEngine.UI.Image E_NewChatMarkImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NewChatMarkImage == null )
     			{
		    		this.m_E_NewChatMarkImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_BG/MaskNode/E_Friend/E_NewChatMark");
     			}
     			return this.m_E_NewChatMarkImage;
     		}
     	}

		public UnityEngine.UI.Button E_BackGroundButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackGroundButton == null )
     			{
		    		this.m_E_BackGroundButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_BG/MaskNode/E_BackGround");
     			}
     			return this.m_E_BackGroundButton;
     		}
     	}

		public UnityEngine.UI.Image E_BackGroundImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackGroundImage == null )
     			{
		    		this.m_E_BackGroundImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_BG/MaskNode/E_BackGround");
     			}
     			return this.m_E_BackGroundImage;
     		}
     	}

		public UnityEngine.UI.Toggle E_ShowMenuToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ShowMenuToggle == null )
     			{
		    		this.m_E_ShowMenuToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_ShowMenu");
     			}
     			return this.m_E_ShowMenuToggle;
     		}
     	}

		public UnityEngine.UI.Image E_ShowImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ShowImage == null )
     			{
		    		this.m_E_ShowImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_ShowMenu/E_Show");
     			}
     			return this.m_E_ShowImage;
     		}
     	}

		public UnityEngine.UI.Button E_ChangeShowHeroModeButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ChangeShowHeroModeButton == null )
     			{
		    		this.m_E_ChangeShowHeroModeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_ChangeShowHeroMode");
     			}
     			return this.m_E_ChangeShowHeroModeButton;
     		}
     	}

		public UnityEngine.UI.Image E_ChangeShowHeroModeImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ChangeShowHeroModeImage == null )
     			{
		    		this.m_E_ChangeShowHeroModeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_ChangeShowHeroMode");
     			}
     			return this.m_E_ChangeShowHeroModeImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BGImage = null;
			this.m_E_SettingButton = null;
			this.m_E_SettingImage = null;
			this.m_E_MailButton = null;
			this.m_E_MailImage = null;
			this.m_E_RedDotImage = null;
			this.m_E_FriendButton = null;
			this.m_E_FriendImage = null;
			this.m_E_NewChatMarkImage = null;
			this.m_E_BackGroundButton = null;
			this.m_E_BackGroundImage = null;
			this.m_E_ShowMenuToggle = null;
			this.m_E_ShowImage = null;
			this.m_E_ChangeShowHeroModeButton = null;
			this.m_E_ChangeShowHeroModeImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_BGImage = null;
		private UnityEngine.UI.Button m_E_SettingButton = null;
		private UnityEngine.UI.Image m_E_SettingImage = null;
		private UnityEngine.UI.Button m_E_MailButton = null;
		private UnityEngine.UI.Image m_E_MailImage = null;
		private UnityEngine.UI.Image m_E_RedDotImage = null;
		private UnityEngine.UI.Button m_E_FriendButton = null;
		private UnityEngine.UI.Image m_E_FriendImage = null;
		private UnityEngine.UI.Image m_E_NewChatMarkImage = null;
		private UnityEngine.UI.Button m_E_BackGroundButton = null;
		private UnityEngine.UI.Image m_E_BackGroundImage = null;
		private UnityEngine.UI.Toggle m_E_ShowMenuToggle = null;
		private UnityEngine.UI.Image m_E_ShowImage = null;
		private UnityEngine.UI.Button m_E_ChangeShowHeroModeButton = null;
		private UnityEngine.UI.Image m_E_ChangeShowHeroModeImage = null;
		public Transform uiTransform = null;
	}
}
