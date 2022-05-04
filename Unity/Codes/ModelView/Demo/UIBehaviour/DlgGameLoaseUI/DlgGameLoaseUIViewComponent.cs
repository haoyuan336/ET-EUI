
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgGameLoaseUIViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_MainMenuButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MainMenuButton == null )
     			{
		    		this.m_E_MainMenuButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_MainMenu");
     			}
     			return this.m_E_MainMenuButton;
     		}
     	}

		public UnityEngine.UI.Image E_MainMenuImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MainMenuImage == null )
     			{
		    		this.m_E_MainMenuImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_MainMenu");
     			}
     			return this.m_E_MainMenuImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_MainMenuButton = null;
			this.m_E_MainMenuImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_MainMenuButton = null;
		private UnityEngine.UI.Image m_E_MainMenuImage = null;
		public Transform uiTransform = null;
	}
}
