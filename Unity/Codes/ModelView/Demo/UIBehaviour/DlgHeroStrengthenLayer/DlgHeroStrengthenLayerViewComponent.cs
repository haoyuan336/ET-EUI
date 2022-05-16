
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgHeroStrengthenLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.LoopHorizontalScrollRect ETargetHeroContentLoopHorizontalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ETargetHeroContentLoopHorizontalScrollRect == null )
     			{
		    		this.m_ETargetHeroContentLoopHorizontalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopHorizontalScrollRect>(this.uiTransform.gameObject,"ETargetHeroContent");
     			}
     			return this.m_ETargetHeroContentLoopHorizontalScrollRect;
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
			this.m_ETargetHeroContentLoopHorizontalScrollRect = null;
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_OKButton = null;
			this.m_E_OKImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.LoopHorizontalScrollRect m_ETargetHeroContentLoopHorizontalScrollRect = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Button m_E_OKButton = null;
		private UnityEngine.UI.Image m_E_OKImage = null;
		public Transform uiTransform = null;
	}
}
