
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

		public UnityEngine.UI.Button E_NameButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NameButton == null )
     			{
		    		this.m_E_NameButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Name");
     			}
     			return this.m_E_NameButton;
     		}
     	}

		public UnityEngine.UI.Image E_NameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NameImage == null )
     			{
		    		this.m_E_NameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Name");
     			}
     			return this.m_E_NameImage;
     		}
     	}

		public UnityEngine.UI.Text E_NameContentText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NameContentText == null )
     			{
		    		this.m_E_NameContentText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Name/E_NameContent");
     			}
     			return this.m_E_NameContentText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_AccountText = null;
			this.m_E_NameButton = null;
			this.m_E_NameImage = null;
			this.m_E_NameContentText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_AccountText = null;
		private UnityEngine.UI.Button m_E_NameButton = null;
		private UnityEngine.UI.Image m_E_NameImage = null;
		private UnityEngine.UI.Text m_E_NameContentText = null;
		public Transform uiTransform = null;
	}
}
