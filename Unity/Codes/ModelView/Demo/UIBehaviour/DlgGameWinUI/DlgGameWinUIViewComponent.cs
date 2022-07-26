
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgGameWinUIViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Text E_WinText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WinText == null )
     			{
		    		this.m_E_WinText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Win");
     			}
     			return this.m_E_WinText;
     		}
     	}

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

		public UnityEngine.UI.Button E_NextLevelButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NextLevelButton == null )
     			{
		    		this.m_E_NextLevelButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_NextLevel");
     			}
     			return this.m_E_NextLevelButton;
     		}
     	}

		public UnityEngine.UI.Image E_NextLevelImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NextLevelImage == null )
     			{
		    		this.m_E_NextLevelImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_NextLevel");
     			}
     			return this.m_E_NextLevelImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_WinText = null;
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_NextLevelButton = null;
			this.m_E_NextLevelImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_WinText = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Button m_E_NextLevelButton = null;
		private UnityEngine.UI.Image m_E_NextLevelImage = null;
		public Transform uiTransform = null;
	}
}
