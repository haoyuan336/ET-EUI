
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgBuyAlertViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_OKButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_OKButton == null )
     			{
		    		this.m_E_OKButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_OK");
     			}
     			return this.m_E_OKButton;
     		}
     	}

		public UnityEngine.UI.Image E_OKImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_OKImage == null )
     			{
		    		this.m_E_OKImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_OK");
     			}
     			return this.m_E_OKImage;
     		}
     	}

		public UnityEngine.UI.Button E_CancelButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CancelButton == null )
     			{
		    		this.m_E_CancelButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Cancel");
     			}
     			return this.m_E_CancelButton;
     		}
     	}

		public UnityEngine.UI.Image E_CancelImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CancelImage == null )
     			{
		    		this.m_E_CancelImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Cancel");
     			}
     			return this.m_E_CancelImage;
     		}
     	}

		public UnityEngine.UI.Image E_IconImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_IconImage == null )
     			{
		    		this.m_E_IconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Icon");
     			}
     			return this.m_E_IconImage;
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
		    		this.m_E_NameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Icon/E_Name");
     			}
     			return this.m_E_NameText;
     		}
     	}

		public UnityEngine.UI.Image E_MoneyIconImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MoneyIconImage == null )
     			{
		    		this.m_E_MoneyIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_MoneyIcon");
     			}
     			return this.m_E_MoneyIconImage;
     		}
     	}

		public UnityEngine.UI.Text E_MoneyText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MoneyText == null )
     			{
		    		this.m_E_MoneyText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MoneyIcon/E_Money");
     			}
     			return this.m_E_MoneyText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_OKButton = null;
			this.m_E_OKImage = null;
			this.m_E_CancelButton = null;
			this.m_E_CancelImage = null;
			this.m_E_IconImage = null;
			this.m_E_NameText = null;
			this.m_E_MoneyIconImage = null;
			this.m_E_MoneyText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_OKButton = null;
		private UnityEngine.UI.Image m_E_OKImage = null;
		private UnityEngine.UI.Button m_E_CancelButton = null;
		private UnityEngine.UI.Image m_E_CancelImage = null;
		private UnityEngine.UI.Image m_E_IconImage = null;
		private UnityEngine.UI.Text m_E_NameText = null;
		private UnityEngine.UI.Image m_E_MoneyIconImage = null;
		private UnityEngine.UI.Text m_E_MoneyText = null;
		public Transform uiTransform = null;
	}
}
