
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgChooseHeadImageLayerViewComponent : Entity,IAwake,IDestroy 
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

		public UnityEngine.UI.Toggle E_HeadLabelToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeadLabelToggle == null )
     			{
		    		this.m_E_HeadLabelToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_Bg/ToggleGroup/E_HeadLabel");
     			}
     			return this.m_E_HeadLabelToggle;
     		}
     	}

		public UnityEngine.UI.Toggle E_HeadFrameLabelToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeadFrameLabelToggle == null )
     			{
		    		this.m_E_HeadFrameLabelToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_Bg/ToggleGroup/E_HeadFrameLabel");
     			}
     			return this.m_E_HeadFrameLabelToggle;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect E_HeadLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeadLoopVerticalScrollRect == null )
     			{
		    		this.m_E_HeadLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"E_Bg/E_Head");
     			}
     			return this.m_E_HeadLoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect E_HeadFrameLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeadFrameLoopVerticalScrollRect == null )
     			{
		    		this.m_E_HeadFrameLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"E_Bg/E_HeadFrame");
     			}
     			return this.m_E_HeadFrameLoopVerticalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BgButton = null;
			this.m_E_BgImage = null;
			this.m_E_HeadLabelToggle = null;
			this.m_E_HeadFrameLabelToggle = null;
			this.m_E_HeadLoopVerticalScrollRect = null;
			this.m_E_HeadFrameLoopVerticalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BgButton = null;
		private UnityEngine.UI.Image m_E_BgImage = null;
		private UnityEngine.UI.Toggle m_E_HeadLabelToggle = null;
		private UnityEngine.UI.Toggle m_E_HeadFrameLabelToggle = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_HeadLoopVerticalScrollRect = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_HeadFrameLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
