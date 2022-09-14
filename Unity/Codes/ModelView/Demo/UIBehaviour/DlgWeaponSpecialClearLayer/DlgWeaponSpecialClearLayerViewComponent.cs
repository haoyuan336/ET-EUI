
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgWeaponSpecialClearLayerViewComponent : Entity,IAwake,IDestroy 
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

		public UnityEngine.UI.Button E_BaseClearButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BaseClearButton == null )
     			{
		    		this.m_E_BaseClearButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Bg/E_BaseClear");
     			}
     			return this.m_E_BaseClearButton;
     		}
     	}

		public UnityEngine.UI.Image E_BaseClearImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BaseClearImage == null )
     			{
		    		this.m_E_BaseClearImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/E_BaseClear");
     			}
     			return this.m_E_BaseClearImage;
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

		public void DestroyWidget()
		{
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_BaseClearButton = null;
			this.m_E_BaseClearImage = null;
			this.m_E_WordBarGroupImage = null;
			this.m_E_OkButtonButton = null;
			this.m_E_OkButtonImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Button m_E_BaseClearButton = null;
		private UnityEngine.UI.Image m_E_BaseClearImage = null;
		private UnityEngine.UI.Image m_E_WordBarGroupImage = null;
		private UnityEngine.UI.Button m_E_OkButtonButton = null;
		private UnityEngine.UI.Image m_E_OkButtonImage = null;
		public Transform uiTransform = null;
	}
}
