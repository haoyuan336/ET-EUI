
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgHeroInfoLayerUIViewComponent : Entity,IAwake,IDestroy 
	{
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

		public UnityEngine.UI.Button E_FilterButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_FilterButton == null )
     			{
		    		this.m_E_FilterButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Filter");
     			}
     			return this.m_E_FilterButton;
     		}
     	}

		public UnityEngine.UI.Image E_FilterImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_FilterImage == null )
     			{
		    		this.m_E_FilterImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Filter");
     			}
     			return this.m_E_FilterImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BagButton = null;
			this.m_E_BagImage = null;
			this.m_E_FilterButton = null;
			this.m_E_FilterImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BagButton = null;
		private UnityEngine.UI.Image m_E_BagImage = null;
		private UnityEngine.UI.Button m_E_FilterButton = null;
		private UnityEngine.UI.Image m_E_FilterImage = null;
		public Transform uiTransform = null;
	}
}
