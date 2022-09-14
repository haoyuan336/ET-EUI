
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgWeaponInfoLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Text E_WeaponNameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WeaponNameText == null )
     			{
		    		this.m_E_WeaponNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/Image/E_WeaponName");
     			}
     			return this.m_E_WeaponNameText;
     		}
     	}

		public UnityEngine.UI.Image E_CurrentWeaponPosImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentWeaponPosImage == null )
     			{
		    		this.m_E_CurrentWeaponPosImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/E_CurrentWeaponPos");
     			}
     			return this.m_E_CurrentWeaponPosImage;
     		}
     	}

		public UnityEngine.UI.Text E_WeaponTypeText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WeaponTypeText == null )
     			{
		    		this.m_E_WeaponTypeText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_CurrentWeaponPos/E_WeaponType");
     			}
     			return this.m_E_WeaponTypeText;
     		}
     	}

		public UnityEngine.UI.Image E_CurrentHeroPosImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentHeroPosImage == null )
     			{
		    		this.m_E_CurrentHeroPosImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/E_CurrentHeroPos");
     			}
     			return this.m_E_CurrentHeroPosImage;
     		}
     	}

		public UnityEngine.UI.Button E_OffWeaponButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_OffWeaponButton == null )
     			{
		    		this.m_E_OffWeaponButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Bg/E_CurrentHeroPos/E_OffWeapon");
     			}
     			return this.m_E_OffWeaponButton;
     		}
     	}

		public UnityEngine.UI.Image E_OffWeaponImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_OffWeaponImage == null )
     			{
		    		this.m_E_OffWeaponImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/E_CurrentHeroPos/E_OffWeapon");
     			}
     			return this.m_E_OffWeaponImage;
     		}
     	}

		public UnityEngine.UI.Text E_DesText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DesText == null )
     			{
		    		this.m_E_DesText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_Des");
     			}
     			return this.m_E_DesText;
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
		    		this.m_E_WordBarGroupImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/WordBars/E_WordBarGroup");
     			}
     			return this.m_E_WordBarGroupImage;
     		}
     	}

		public UnityEngine.UI.Button E_WeaponStrengthButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WeaponStrengthButton == null )
     			{
		    		this.m_E_WeaponStrengthButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Bg/E_WeaponStrength");
     			}
     			return this.m_E_WeaponStrengthButton;
     		}
     	}

		public UnityEngine.UI.Image E_WeaponStrengthImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WeaponStrengthImage == null )
     			{
		    		this.m_E_WeaponStrengthImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/E_WeaponStrength");
     			}
     			return this.m_E_WeaponStrengthImage;
     		}
     	}

		public UnityEngine.UI.Button E_WeaponClearButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WeaponClearButton == null )
     			{
		    		this.m_E_WeaponClearButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Bg/E_WeaponClear");
     			}
     			return this.m_E_WeaponClearButton;
     		}
     	}

		public UnityEngine.UI.Image E_WeaponClearImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WeaponClearImage == null )
     			{
		    		this.m_E_WeaponClearImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/E_WeaponClear");
     			}
     			return this.m_E_WeaponClearImage;
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
			this.m_E_WeaponNameText = null;
			this.m_E_CurrentWeaponPosImage = null;
			this.m_E_WeaponTypeText = null;
			this.m_E_CurrentHeroPosImage = null;
			this.m_E_OffWeaponButton = null;
			this.m_E_OffWeaponImage = null;
			this.m_E_DesText = null;
			this.m_E_WordBarGroupImage = null;
			this.m_E_WeaponStrengthButton = null;
			this.m_E_WeaponStrengthImage = null;
			this.m_E_WeaponClearButton = null;
			this.m_E_WeaponClearImage = null;
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_WeaponNameText = null;
		private UnityEngine.UI.Image m_E_CurrentWeaponPosImage = null;
		private UnityEngine.UI.Text m_E_WeaponTypeText = null;
		private UnityEngine.UI.Image m_E_CurrentHeroPosImage = null;
		private UnityEngine.UI.Button m_E_OffWeaponButton = null;
		private UnityEngine.UI.Image m_E_OffWeaponImage = null;
		private UnityEngine.UI.Text m_E_DesText = null;
		private UnityEngine.UI.Image m_E_WordBarGroupImage = null;
		private UnityEngine.UI.Button m_E_WeaponStrengthButton = null;
		private UnityEngine.UI.Image m_E_WeaponStrengthImage = null;
		private UnityEngine.UI.Button m_E_WeaponClearButton = null;
		private UnityEngine.UI.Image m_E_WeaponClearImage = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		public Transform uiTransform = null;
	}
}
