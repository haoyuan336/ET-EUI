
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgPVPEditorTroopLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_BackButtonButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackButtonButton == null )
     			{
		    		this.m_E_BackButtonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_BackButton");
     			}
     			return this.m_E_BackButtonButton;
     		}
     	}

		public UnityEngine.UI.Image E_BackButtonImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackButtonImage == null )
     			{
		    		this.m_E_BackButtonImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_BackButton");
     			}
     			return this.m_E_BackButtonImage;
     		}
     	}

		public UnityEngine.UI.Button E_MatchPlayerButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MatchPlayerButton == null )
     			{
		    		this.m_E_MatchPlayerButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_MatchPlayer");
     			}
     			return this.m_E_MatchPlayerButton;
     		}
     	}

		public UnityEngine.UI.Image E_MatchPlayerImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MatchPlayerImage == null )
     			{
		    		this.m_E_MatchPlayerImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_MatchPlayer");
     			}
     			return this.m_E_MatchPlayerImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BackButtonButton = null;
			this.m_E_BackButtonImage = null;
			this.m_E_MatchPlayerButton = null;
			this.m_E_MatchPlayerImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BackButtonButton = null;
		private UnityEngine.UI.Image m_E_BackButtonImage = null;
		private UnityEngine.UI.Button m_E_MatchPlayerButton = null;
		private UnityEngine.UI.Image m_E_MatchPlayerImage = null;
		public Transform uiTransform = null;
	}
}
