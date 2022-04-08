
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

		public void DestroyWidget()
		{
			this.m_E_GoldText = null;
			this.m_E_PowerText = null;
			this.m_E_DiamondText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_GoldText = null;
		private UnityEngine.UI.Text m_E_PowerText = null;
		private UnityEngine.UI.Text m_E_DiamondText = null;
		public Transform uiTransform = null;
	}
}
