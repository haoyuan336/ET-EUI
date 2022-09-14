
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgDayTaskLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Image EPointBarImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EPointBarImage == null )
     			{
		    		this.m_EPointBarImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Award/Scroll View/Viewport/Content/Progress/EPointBar");
     			}
     			return this.m_EPointBarImage;
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
		    		this.m_E_GetAward1Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Award/Scroll View/Viewport/Content/TaskAward/E_GetAward1");
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
		    		this.m_E_GetAward1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Award/Scroll View/Viewport/Content/TaskAward/E_GetAward1");
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
		    		this.m_E_GetAward2Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Award/Scroll View/Viewport/Content/TaskAward (1)/E_GetAward2");
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
		    		this.m_E_GetAward2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Award/Scroll View/Viewport/Content/TaskAward (1)/E_GetAward2");
     			}
     			return this.m_E_GetAward2Image;
     		}
     	}

		public UnityEngine.UI.Button E_GetAward3Button
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GetAward3Button == null )
     			{
		    		this.m_E_GetAward3Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Award/Scroll View/Viewport/Content/TaskAward (2)/E_GetAward3");
     			}
     			return this.m_E_GetAward3Button;
     		}
     	}

		public UnityEngine.UI.Image E_GetAward3Image
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GetAward3Image == null )
     			{
		    		this.m_E_GetAward3Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Award/Scroll View/Viewport/Content/TaskAward (2)/E_GetAward3");
     			}
     			return this.m_E_GetAward3Image;
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
		    		this.m_E_AwardListLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"TaskContent/E_AwardList");
     			}
     			return this.m_E_AwardListLoopVerticalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EPointBarImage = null;
			this.m_E_GetAward1Button = null;
			this.m_E_GetAward1Image = null;
			this.m_E_GetAward2Button = null;
			this.m_E_GetAward2Image = null;
			this.m_E_GetAward3Button = null;
			this.m_E_GetAward3Image = null;
			this.m_E_AwardListLoopVerticalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_EPointBarImage = null;
		private UnityEngine.UI.Button m_E_GetAward1Button = null;
		private UnityEngine.UI.Image m_E_GetAward1Image = null;
		private UnityEngine.UI.Button m_E_GetAward2Button = null;
		private UnityEngine.UI.Image m_E_GetAward2Image = null;
		private UnityEngine.UI.Button m_E_GetAward3Button = null;
		private UnityEngine.UI.Image m_E_GetAward3Image = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_AwardListLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
