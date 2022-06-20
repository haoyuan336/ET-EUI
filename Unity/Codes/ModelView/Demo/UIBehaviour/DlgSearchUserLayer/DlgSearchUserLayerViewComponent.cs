
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgSearchUserLayerViewComponent : Entity,IAwake,IDestroy 
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
		    		this.m_E_BackButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"bg/E_Back");
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
		    		this.m_E_BackImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"bg/E_Back");
     			}
     			return this.m_E_BackImage;
     		}
     	}

		public UnityEngine.UI.Button E_SearchButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SearchButton == null )
     			{
		    		this.m_E_SearchButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"bg/E_Search");
     			}
     			return this.m_E_SearchButton;
     		}
     	}

		public UnityEngine.UI.Image E_SearchImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SearchImage == null )
     			{
		    		this.m_E_SearchImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"bg/E_Search");
     			}
     			return this.m_E_SearchImage;
     		}
     	}

		public UnityEngine.UI.Toggle E_RecommendToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RecommendToggle == null )
     			{
		    		this.m_E_RecommendToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"bg/E_Recommend");
     			}
     			return this.m_E_RecommendToggle;
     		}
     	}

		public UnityEngine.UI.Toggle E_ApplyToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ApplyToggle == null )
     			{
		    		this.m_E_ApplyToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"bg/E_Apply");
     			}
     			return this.m_E_ApplyToggle;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopRecommendLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopRecommendLoopVerticalScrollRect == null )
     			{
		    		this.m_ELoopRecommendLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"bg/ELoopRecommend");
     			}
     			return this.m_ELoopRecommendLoopVerticalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_SearchButton = null;
			this.m_E_SearchImage = null;
			this.m_E_RecommendToggle = null;
			this.m_E_ApplyToggle = null;
			this.m_ELoopRecommendLoopVerticalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Button m_E_SearchButton = null;
		private UnityEngine.UI.Image m_E_SearchImage = null;
		private UnityEngine.UI.Toggle m_E_RecommendToggle = null;
		private UnityEngine.UI.Toggle m_E_ApplyToggle = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopRecommendLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
