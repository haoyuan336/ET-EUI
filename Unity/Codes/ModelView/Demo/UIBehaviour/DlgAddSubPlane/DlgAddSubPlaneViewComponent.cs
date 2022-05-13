
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgAddSubPlaneViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_BgButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BgButton == null )
     			{
		    		this.m_E_BgButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Bg");
     			}
     			return this.m_E_BgButton;
     		}
     	}

		public UnityEngine.UI.Image E_BgImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BgImage == null )
     			{
		    		this.m_E_BgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Bg");
     			}
     			return this.m_E_BgImage;
     		}
     	}

		public UnityEngine.UI.Image E_ContentImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ContentImage == null )
     			{
		    		this.m_E_ContentImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Content");
     			}
     			return this.m_E_ContentImage;
     		}
     	}

		public UnityEngine.UI.Button E_AddButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddButton == null )
     			{
		    		this.m_E_AddButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Content/E_Add");
     			}
     			return this.m_E_AddButton;
     		}
     	}

		public UnityEngine.UI.Image E_AddImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AddImage == null )
     			{
		    		this.m_E_AddImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Content/E_Add");
     			}
     			return this.m_E_AddImage;
     		}
     	}

		public UnityEngine.UI.Button E_SubButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SubButton == null )
     			{
		    		this.m_E_SubButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Content/E_Sub");
     			}
     			return this.m_E_SubButton;
     		}
     	}

		public UnityEngine.UI.Image E_SubImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SubImage == null )
     			{
		    		this.m_E_SubImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Content/E_Sub");
     			}
     			return this.m_E_SubImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BgButton = null;
			this.m_E_BgImage = null;
			this.m_E_ContentImage = null;
			this.m_E_AddButton = null;
			this.m_E_AddImage = null;
			this.m_E_SubButton = null;
			this.m_E_SubImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BgButton = null;
		private UnityEngine.UI.Image m_E_BgImage = null;
		private UnityEngine.UI.Image m_E_ContentImage = null;
		private UnityEngine.UI.Button m_E_AddButton = null;
		private UnityEngine.UI.Image m_E_AddImage = null;
		private UnityEngine.UI.Button m_E_SubButton = null;
		private UnityEngine.UI.Image m_E_SubImage = null;
		public Transform uiTransform = null;
	}
}
