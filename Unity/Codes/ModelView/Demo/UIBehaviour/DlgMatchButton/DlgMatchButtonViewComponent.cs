
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgMatchButtonViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_MatchButtonButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MatchButtonButton == null )
     			{
		    		this.m_E_MatchButtonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_MatchButton");
     			}
     			return this.m_E_MatchButtonButton;
     		}
     	}

		public UnityEngine.UI.Image E_MatchButtonImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MatchButtonImage == null )
     			{
		    		this.m_E_MatchButtonImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_MatchButton");
     			}
     			return this.m_E_MatchButtonImage;
     		}
     	}

		public UnityEngine.UI.Text E_MatchingCountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MatchingCountText == null )
     			{
		    		this.m_E_MatchingCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MatchingCount");
     			}
     			return this.m_E_MatchingCountText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_MatchButtonButton = null;
			this.m_E_MatchButtonImage = null;
			this.m_E_MatchingCountText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_MatchButtonButton = null;
		private UnityEngine.UI.Image m_E_MatchButtonImage = null;
		private UnityEngine.UI.Text m_E_MatchingCountText = null;
		public Transform uiTransform = null;
	}
}
