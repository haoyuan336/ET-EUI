
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgCallHeroLayerViewComponent : Entity,IAwake,IDestroy 
	{
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

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollListHeroLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollListHeroLoopVerticalScrollRect == null )
     			{
		    		this.m_ELoopScrollListHeroLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"ELoopScrollListHero");
     			}
     			return this.m_ELoopScrollListHeroLoopVerticalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_CallHeroButton = null;
			this.m_E_CallHeroImage = null;
			this.m_ELoopScrollListHeroLoopVerticalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_CallHeroButton = null;
		private UnityEngine.UI.Image m_E_CallHeroImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollListHeroLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
