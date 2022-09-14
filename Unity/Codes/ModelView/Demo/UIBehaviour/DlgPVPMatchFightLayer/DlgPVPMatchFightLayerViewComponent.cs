
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgPVPMatchFightLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_BackButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackButton == null )
     			{
		    		this.m_E_BackButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Back");
     			}
     			return this.m_E_BackButton;
     		}
     	}

		public UnityEngine.UI.Image E_BackImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackImage == null )
     			{
		    		this.m_E_BackImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Back");
     			}
     			return this.m_E_BackImage;
     		}
     	}

		public UnityEngine.UI.Text E_MatchTimeText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MatchTimeText == null )
     			{
		    		this.m_E_MatchTimeText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MatchTime");
     			}
     			return this.m_E_MatchTimeText;
     		}
     	}

		public UnityEngine.UI.Text E_MatchTimeCountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MatchTimeCountText == null )
     			{
		    		this.m_E_MatchTimeCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MatchTimeCount");
     			}
     			return this.m_E_MatchTimeCountText;
     		}
     	}

		public UnityEngine.UI.Button E_CancelMatchButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CancelMatchButton == null )
     			{
		    		this.m_E_CancelMatchButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_CancelMatch");
     			}
     			return this.m_E_CancelMatchButton;
     		}
     	}

		public UnityEngine.UI.Image E_CancelMatchImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CancelMatchImage == null )
     			{
		    		this.m_E_CancelMatchImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_CancelMatch");
     			}
     			return this.m_E_CancelMatchImage;
     		}
     	}

		public UnityEngine.UI.Text E_CancelText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CancelText == null )
     			{
		    		this.m_E_CancelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_CancelMatch/E_Cancel");
     			}
     			return this.m_E_CancelText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_MatchTimeText = null;
			this.m_E_MatchTimeCountText = null;
			this.m_E_CancelMatchButton = null;
			this.m_E_CancelMatchImage = null;
			this.m_E_CancelText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Text m_E_MatchTimeText = null;
		private UnityEngine.UI.Text m_E_MatchTimeCountText = null;
		private UnityEngine.UI.Button m_E_CancelMatchButton = null;
		private UnityEngine.UI.Image m_E_CancelMatchImage = null;
		private UnityEngine.UI.Text m_E_CancelText = null;
		public Transform uiTransform = null;
	}
}
