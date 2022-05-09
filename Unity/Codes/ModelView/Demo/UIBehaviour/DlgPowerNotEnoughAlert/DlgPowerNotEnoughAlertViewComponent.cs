
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgPowerNotEnoughAlertViewComponent : Entity,IAwake,IDestroy 
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

		public void DestroyWidget()
		{
			this.m_E_OKButton = null;
			this.m_E_OKImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_OKButton = null;
		private UnityEngine.UI.Image m_E_OKImage = null;
		public Transform uiTransform = null;
	}
}
