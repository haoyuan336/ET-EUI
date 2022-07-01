
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgMailLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_LoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_LoopVerticalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_LoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"BgNode/Image/ELoopScrollList_");
     			}
     			return this.m_ELoopScrollList_LoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.Button E_DelAllButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DelAllButton == null )
     			{
		    		this.m_E_DelAllButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BgNode/E_DelAll");
     			}
     			return this.m_E_DelAllButton;
     		}
     	}

		public UnityEngine.UI.Image E_DelAllImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DelAllImage == null )
     			{
		    		this.m_E_DelAllImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BgNode/E_DelAll");
     			}
     			return this.m_E_DelAllImage;
     		}
     	}

		public UnityEngine.UI.Button E_GetAllButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GetAllButton == null )
     			{
		    		this.m_E_GetAllButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BgNode/E_GetAll");
     			}
     			return this.m_E_GetAllButton;
     		}
     	}

		public UnityEngine.UI.Image E_GetAllImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GetAllImage == null )
     			{
		    		this.m_E_GetAllImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BgNode/E_GetAll");
     			}
     			return this.m_E_GetAllImage;
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

		public void DestroyWidget()
		{
			this.m_ELoopScrollList_LoopVerticalScrollRect = null;
			this.m_E_DelAllButton = null;
			this.m_E_DelAllImage = null;
			this.m_E_GetAllButton = null;
			this.m_E_GetAllImage = null;
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_LoopVerticalScrollRect = null;
		private UnityEngine.UI.Button m_E_DelAllButton = null;
		private UnityEngine.UI.Image m_E_DelAllImage = null;
		private UnityEngine.UI.Button m_E_GetAllButton = null;
		private UnityEngine.UI.Image m_E_GetAllImage = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		public Transform uiTransform = null;
	}
}
