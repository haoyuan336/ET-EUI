
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgGoldInfoUIViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Text E_GoldText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GoldText == null )
     			{
		    		this.m_E_GoldText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Gold/E_Gold");
     			}
     			return this.m_E_GoldText;
     		}
     	}

		public UnityEngine.UI.Button E_AddGoldButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddGoldButton == null )
     			{
		    		this.m_E_AddGoldButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Gold/E_AddGold");
     			}
     			return this.m_E_AddGoldButton;
     		}
     	}

		public UnityEngine.UI.Image E_AddGoldImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddGoldImage == null )
     			{
		    		this.m_E_AddGoldImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Gold/E_AddGold");
     			}
     			return this.m_E_AddGoldImage;
     		}
     	}

		public UnityEngine.UI.Text E_PowerText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PowerText == null )
     			{
		    		this.m_E_PowerText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Power/E_Power");
     			}
     			return this.m_E_PowerText;
     		}
     	}

		public UnityEngine.UI.Button E_AddPowerButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddPowerButton == null )
     			{
		    		this.m_E_AddPowerButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Power/E_AddPower");
     			}
     			return this.m_E_AddPowerButton;
     		}
     	}

		public UnityEngine.UI.Image E_AddPowerImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddPowerImage == null )
     			{
		    		this.m_E_AddPowerImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Power/E_AddPower");
     			}
     			return this.m_E_AddPowerImage;
     		}
     	}

		public UnityEngine.UI.Button E_InfoButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_InfoButton == null )
     			{
		    		this.m_E_InfoButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Info");
     			}
     			return this.m_E_InfoButton;
     		}
     	}

		public UnityEngine.UI.Image E_InfoImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_InfoImage == null )
     			{
		    		this.m_E_InfoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Info");
     			}
     			return this.m_E_InfoImage;
     		}
     	}

		public UnityEngine.UI.Text E_DiamondText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DiamondText == null )
     			{
		    		this.m_E_DiamondText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Diamond/E_Diamond");
     			}
     			return this.m_E_DiamondText;
     		}
     	}

		public UnityEngine.UI.Button E_AddDiamondButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddDiamondButton == null )
     			{
		    		this.m_E_AddDiamondButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Diamond/E_AddDiamond");
     			}
     			return this.m_E_AddDiamondButton;
     		}
     	}

		public UnityEngine.UI.Image E_AddDiamondImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddDiamondImage == null )
     			{
		    		this.m_E_AddDiamondImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Diamond/E_AddDiamond");
     			}
     			return this.m_E_AddDiamondImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_GoldText = null;
			this.m_E_AddGoldButton = null;
			this.m_E_AddGoldImage = null;
			this.m_E_PowerText = null;
			this.m_E_AddPowerButton = null;
			this.m_E_AddPowerImage = null;
			this.m_E_InfoButton = null;
			this.m_E_InfoImage = null;
			this.m_E_DiamondText = null;
			this.m_E_AddDiamondButton = null;
			this.m_E_AddDiamondImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_GoldText = null;
		private UnityEngine.UI.Button m_E_AddGoldButton = null;
		private UnityEngine.UI.Image m_E_AddGoldImage = null;
		private UnityEngine.UI.Text m_E_PowerText = null;
		private UnityEngine.UI.Button m_E_AddPowerButton = null;
		private UnityEngine.UI.Image m_E_AddPowerImage = null;
		private UnityEngine.UI.Button m_E_InfoButton = null;
		private UnityEngine.UI.Image m_E_InfoImage = null;
		private UnityEngine.UI.Text m_E_DiamondText = null;
		private UnityEngine.UI.Button m_E_AddDiamondButton = null;
		private UnityEngine.UI.Image m_E_AddDiamondImage = null;
		public Transform uiTransform = null;
	}
}
