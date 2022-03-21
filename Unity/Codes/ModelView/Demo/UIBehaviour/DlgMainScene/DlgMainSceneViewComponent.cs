
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgMainSceneViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_PVEButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PVEButton == null )
     			{
		    		this.m_E_PVEButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_PVE");
     			}
     			return this.m_E_PVEButton;
     		}
     	}

		public UnityEngine.UI.Image E_PVEImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PVEImage == null )
     			{
		    		this.m_E_PVEImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_PVE");
     			}
     			return this.m_E_PVEImage;
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

		public UnityEngine.UI.Button E_BagButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BagButton == null )
     			{
		    		this.m_E_BagButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Bag");
     			}
     			return this.m_E_BagButton;
     		}
     	}

		public UnityEngine.UI.Image E_BagImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BagImage == null )
     			{
		    		this.m_E_BagImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Bag");
     			}
     			return this.m_E_BagImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_PVEButton = null;
			this.m_E_PVEImage = null;
			this.m_E_CallHeroButton = null;
			this.m_E_CallHeroImage = null;
			this.m_E_BagButton = null;
			this.m_E_BagImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_PVEButton = null;
		private UnityEngine.UI.Image m_E_PVEImage = null;
		private UnityEngine.UI.Button m_E_CallHeroButton = null;
		private UnityEngine.UI.Image m_E_CallHeroImage = null;
		private UnityEngine.UI.Button m_E_BagButton = null;
		private UnityEngine.UI.Image m_E_BagImage = null;
		public Transform uiTransform = null;
	}
}
