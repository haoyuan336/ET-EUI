
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

		public UnityEngine.UI.Text E_ExpText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ExpText == null )
     			{
		    		this.m_E_ExpText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Exp");
     			}
     			return this.m_E_ExpText;
     		}
     	}

		public UnityEngine.UI.Text E_LevelText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LevelText == null )
     			{
		    		this.m_E_LevelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Level");
     			}
     			return this.m_E_LevelText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_AccountText = null;
			this.m_E_ExpText = null;
			this.m_E_LevelText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_AccountText = null;
		private UnityEngine.UI.Text m_E_ExpText = null;
		private UnityEngine.UI.Text m_E_LevelText = null;
		public Transform uiTransform = null;
	}
}
