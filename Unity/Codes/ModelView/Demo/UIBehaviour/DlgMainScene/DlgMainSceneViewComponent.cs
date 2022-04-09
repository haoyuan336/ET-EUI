
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgMainSceneViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Toggle E_MainToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MainToggle == null )
     			{
		    		this.m_E_MainToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"Content/E_Main");
     			}
     			return this.m_E_MainToggle;
     		}
     	}

		public UnityEngine.UI.Image E_MainImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MainImage == null )
     			{
		    		this.m_E_MainImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Content/E_Main");
     			}
     			return this.m_E_MainImage;
     		}
     	}

		public UnityEngine.UI.Toggle E_CallToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CallToggle == null )
     			{
		    		this.m_E_CallToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"Content/E_Call");
     			}
     			return this.m_E_CallToggle;
     		}
     	}

		public UnityEngine.UI.Image E_CallImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CallImage == null )
     			{
		    		this.m_E_CallImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Content/E_Call");
     			}
     			return this.m_E_CallImage;
     		}
     	}

		public UnityEngine.UI.Toggle E_HeroToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeroToggle == null )
     			{
		    		this.m_E_HeroToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"Content/E_Hero");
     			}
     			return this.m_E_HeroToggle;
     		}
     	}

		public UnityEngine.UI.Image E_HeroImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeroImage == null )
     			{
		    		this.m_E_HeroImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Content/E_Hero");
     			}
     			return this.m_E_HeroImage;
     		}
     	}

		public UnityEngine.UI.Toggle E_BagToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BagToggle == null )
     			{
		    		this.m_E_BagToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"Content/E_Bag");
     			}
     			return this.m_E_BagToggle;
     		}
     	}

		public UnityEngine.UI.Image E_BagImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BagImage == null )
     			{
		    		this.m_E_BagImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Content/E_Bag");
     			}
     			return this.m_E_BagImage;
     		}
     	}

		public UnityEngine.UI.Toggle E_ShopToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ShopToggle == null )
     			{
		    		this.m_E_ShopToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"Content/E_Shop");
     			}
     			return this.m_E_ShopToggle;
     		}
     	}

		public UnityEngine.UI.Image E_ShopImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ShopImage == null )
     			{
		    		this.m_E_ShopImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Content/E_Shop");
     			}
     			return this.m_E_ShopImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_MainToggle = null;
			this.m_E_MainImage = null;
			this.m_E_CallToggle = null;
			this.m_E_CallImage = null;
			this.m_E_HeroToggle = null;
			this.m_E_HeroImage = null;
			this.m_E_BagToggle = null;
			this.m_E_BagImage = null;
			this.m_E_ShopToggle = null;
			this.m_E_ShopImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Toggle m_E_MainToggle = null;
		private UnityEngine.UI.Image m_E_MainImage = null;
		private UnityEngine.UI.Toggle m_E_CallToggle = null;
		private UnityEngine.UI.Image m_E_CallImage = null;
		private UnityEngine.UI.Toggle m_E_HeroToggle = null;
		private UnityEngine.UI.Image m_E_HeroImage = null;
		private UnityEngine.UI.Toggle m_E_BagToggle = null;
		private UnityEngine.UI.Image m_E_BagImage = null;
		private UnityEngine.UI.Toggle m_E_ShopToggle = null;
		private UnityEngine.UI.Image m_E_ShopImage = null;
		public Transform uiTransform = null;
	}
}
