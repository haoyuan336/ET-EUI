
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgTaskLayerViewComponent : Entity,IAwake,IDestroy 
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

		public UnityEngine.UI.Button E_GetAward1Button
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GetAward1Button == null )
     			{
		    		this.m_E_GetAward1Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Image (1)/Image/TaskAward/E_GetAward1");
     			}
     			return this.m_E_GetAward1Button;
     		}
     	}

		public UnityEngine.UI.Image E_GetAward1Image
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GetAward1Image == null )
     			{
		    		this.m_E_GetAward1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Image (1)/Image/TaskAward/E_GetAward1");
     			}
     			return this.m_E_GetAward1Image;
     		}
     	}

		public UnityEngine.UI.Button E_GetAward2Button
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GetAward2Button == null )
     			{
		    		this.m_E_GetAward2Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Image (1)/Image/TaskAward (1)/E_GetAward2");
     			}
     			return this.m_E_GetAward2Button;
     		}
     	}

		public UnityEngine.UI.Image E_GetAward2Image
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GetAward2Image == null )
     			{
		    		this.m_E_GetAward2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Image (1)/Image/TaskAward (1)/E_GetAward2");
     			}
     			return this.m_E_GetAward2Image;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect E_AwardListLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AwardListLoopVerticalScrollRect == null )
     			{
		    		this.m_E_AwardListLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"Image (1)/Image (2)/E_AwardList");
     			}
     			return this.m_E_AwardListLoopVerticalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_GetAward1Button = null;
			this.m_E_GetAward1Image = null;
			this.m_E_GetAward2Button = null;
			this.m_E_GetAward2Image = null;
			this.m_E_AwardListLoopVerticalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Button m_E_GetAward1Button = null;
		private UnityEngine.UI.Image m_E_GetAward1Image = null;
		private UnityEngine.UI.Button m_E_GetAward2Button = null;
		private UnityEngine.UI.Image m_E_GetAward2Image = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_AwardListLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
