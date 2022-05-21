
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgAccountInfoViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Text E_AccountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AccountText == null )
     			{
		    		this.m_E_AccountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"AccountId/E_Account");
     			}
     			return this.m_E_AccountText;
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
		    		this.m_E_NameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Name/E_Name");
     			}
     			return this.m_E_NameText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_AccountText = null;
			this.m_E_NameText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_AccountText = null;
		private UnityEngine.UI.Text m_E_NameText = null;
		public Transform uiTransform = null;
	}
}
