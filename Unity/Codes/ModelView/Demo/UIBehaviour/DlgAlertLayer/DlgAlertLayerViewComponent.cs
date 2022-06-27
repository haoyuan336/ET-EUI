
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgAlertLayerViewComponent : Entity,IAwake,IDestroy 
	{
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

		public UnityEngine.UI.Text E_InfoText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_InfoText == null )
     			{
		    		this.m_E_InfoText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Image/E_Info");
     			}
     			return this.m_E_InfoText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_OkButtonButton = null;
			this.m_E_OkButtonImage = null;
			this.m_E_InfoText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_OkButtonButton = null;
		private UnityEngine.UI.Image m_E_OkButtonImage = null;
		private UnityEngine.UI.Text m_E_InfoText = null;
		public Transform uiTransform = null;
	}
}
