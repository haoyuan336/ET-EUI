
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgGameLevelStoryLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Text E_ContentText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ContentText == null )
     			{
		    		this.m_E_ContentText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"bg/E_Content");
     			}
     			return this.m_E_ContentText;
     		}
     	}

		public UnityEngine.UI.Button E_NextButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NextButton == null )
     			{
		    		this.m_E_NextButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"bg/E_Next");
     			}
     			return this.m_E_NextButton;
     		}
     	}

		public UnityEngine.UI.Image E_NextImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NextImage == null )
     			{
		    		this.m_E_NextImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"bg/E_Next");
     			}
     			return this.m_E_NextImage;
     		}
     	}

		public UnityEngine.UI.Button E_CloseButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CloseButton == null )
     			{
		    		this.m_E_CloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"bg/E_Close");
     			}
     			return this.m_E_CloseButton;
     		}
     	}

		public UnityEngine.UI.Image E_CloseImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CloseImage == null )
     			{
		    		this.m_E_CloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"bg/E_Close");
     			}
     			return this.m_E_CloseImage;
     		}
     	}

		public UnityEngine.UI.Button E_AutoButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AutoButton == null )
     			{
		    		this.m_E_AutoButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"bg/E_Auto");
     			}
     			return this.m_E_AutoButton;
     		}
     	}

		public UnityEngine.UI.Image E_AutoImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AutoImage == null )
     			{
		    		this.m_E_AutoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"bg/E_Auto");
     			}
     			return this.m_E_AutoImage;
     		}
     	}

		public UnityEngine.UI.Text E_AutoMarkText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AutoMarkText == null )
     			{
		    		this.m_E_AutoMarkText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"bg/E_Auto/E_AutoMark");
     			}
     			return this.m_E_AutoMarkText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_ContentText = null;
			this.m_E_NextButton = null;
			this.m_E_NextImage = null;
			this.m_E_CloseButton = null;
			this.m_E_CloseImage = null;
			this.m_E_AutoButton = null;
			this.m_E_AutoImage = null;
			this.m_E_AutoMarkText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_ContentText = null;
		private UnityEngine.UI.Button m_E_NextButton = null;
		private UnityEngine.UI.Image m_E_NextImage = null;
		private UnityEngine.UI.Button m_E_CloseButton = null;
		private UnityEngine.UI.Image m_E_CloseImage = null;
		private UnityEngine.UI.Button m_E_AutoButton = null;
		private UnityEngine.UI.Image m_E_AutoImage = null;
		private UnityEngine.UI.Text m_E_AutoMarkText = null;
		public Transform uiTransform = null;
	}
}
