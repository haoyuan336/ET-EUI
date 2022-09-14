
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgWeaponStrengthenPreviewLayerViewComponent : Entity,IAwake,IDestroy 
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

		public UnityEngine.UI.Image E_WeaponItemGroupImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WeaponItemGroupImage == null )
     			{
		    		this.m_E_WeaponItemGroupImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/E_WeaponItemGroup");
     			}
     			return this.m_E_WeaponItemGroupImage;
     		}
     	}

		public UnityEngine.UI.Image E_WordBarGroupImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WordBarGroupImage == null )
     			{
		    		this.m_E_WordBarGroupImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"WordBars/E_WordBarGroup");
     			}
     			return this.m_E_WordBarGroupImage;
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
		    		this.m_E_CurrentLevelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"XP/E_CurrentLevel");
     			}
     			return this.m_E_CurrentLevelText;
     		}
     	}

		public UnityEngine.UI.Image ExpBarGbImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ExpBarGbImage == null )
     			{
		    		this.m_ExpBarGbImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"XP/ExpBarGb");
     			}
     			return this.m_ExpBarGbImage;
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
		    		this.m_E_ExpBarImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"XP/ExpBarGb/E_ExpBar");
     			}
     			return this.m_E_ExpBarImage;
     		}
     	}

		public UnityEngine.UI.Text E_ExpInfoText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ExpInfoText == null )
     			{
		    		this.m_E_ExpInfoText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"XP/ExpBarGb/E_ExpInfo");
     			}
     			return this.m_E_ExpInfoText;
     		}
     	}

		public UnityEngine.UI.Text E_AddExpInfoText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddExpInfoText == null )
     			{
		    		this.m_E_AddExpInfoText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"XP/ExpBarGb/E_AddExpInfo");
     			}
     			return this.m_E_AddExpInfoText;
     		}
     	}

		public UnityEngine.UI.Text E_AddLevelText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddLevelText == null )
     			{
		    		this.m_E_AddLevelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"XP/E_AddLevel");
     			}
     			return this.m_E_AddLevelText;
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

		public UnityEngine.UI.Button E_QuickChooseButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_QuickChooseButton == null )
     			{
		    		this.m_E_QuickChooseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_QuickChoose");
     			}
     			return this.m_E_QuickChooseButton;
     		}
     	}

		public UnityEngine.UI.Image E_QuickChooseImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_QuickChooseImage == null )
     			{
		    		this.m_E_QuickChooseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_QuickChoose");
     			}
     			return this.m_E_QuickChooseImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_WeaponItemGroupImage = null;
			this.m_E_WordBarGroupImage = null;
			this.m_E_CurrentLevelText = null;
			this.m_ExpBarGbImage = null;
			this.m_E_ExpBarImage = null;
			this.m_E_ExpInfoText = null;
			this.m_E_AddExpInfoText = null;
			this.m_E_AddLevelText = null;
			this.m_E_OkButtonButton = null;
			this.m_E_OkButtonImage = null;
			this.m_E_QuickChooseButton = null;
			this.m_E_QuickChooseImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Image m_E_WeaponItemGroupImage = null;
		private UnityEngine.UI.Image m_E_WordBarGroupImage = null;
		private UnityEngine.UI.Text m_E_CurrentLevelText = null;
		private UnityEngine.UI.Image m_ExpBarGbImage = null;
		private UnityEngine.UI.Image m_E_ExpBarImage = null;
		private UnityEngine.UI.Text m_E_ExpInfoText = null;
		private UnityEngine.UI.Text m_E_AddExpInfoText = null;
		private UnityEngine.UI.Text m_E_AddLevelText = null;
		private UnityEngine.UI.Button m_E_OkButtonButton = null;
		private UnityEngine.UI.Image m_E_OkButtonImage = null;
		private UnityEngine.UI.Button m_E_QuickChooseButton = null;
		private UnityEngine.UI.Image m_E_QuickChooseImage = null;
		public Transform uiTransform = null;
	}
}
