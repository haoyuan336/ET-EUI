
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgCallHeroLayerViewComponent : Entity,IAwake,IDestroy 
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

		public UnityEngine.UI.Button E_CallHeroButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CallHeroButton == null )
     			{
		    		this.m_E_CallHeroButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_CallHero");
     			}
     			return this.m_E_CallHeroButton;
     		}
     	}

		public UnityEngine.UI.Image E_CallHeroImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CallHeroImage == null )
     			{
		    		this.m_E_CallHeroImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_CallHero");
     			}
     			return this.m_E_CallHeroImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_CallHeroButton = null;
			this.m_E_CallHeroImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Button m_E_CallHeroButton = null;
		private UnityEngine.UI.Image m_E_CallHeroImage = null;
		public Transform uiTransform = null;
	}
}
