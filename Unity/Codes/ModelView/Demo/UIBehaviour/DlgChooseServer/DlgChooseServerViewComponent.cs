
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgChooseServerViewComponent : Entity,IAwake,IDestroy 
	{
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

		public UnityEngine.UI.ToggleGroup E_ToggleGroupToggleGroup
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ToggleGroupToggleGroup == null )
     			{
		    		this.m_E_ToggleGroupToggleGroup = UIFindHelper.FindDeepChild<UnityEngine.UI.ToggleGroup>(this.uiTransform.gameObject,"E_ToggleGroup");
     			}
     			return this.m_E_ToggleGroupToggleGroup;
     		}
     	}

		public UnityEngine.UI.Toggle E_Choose1Toggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Choose1Toggle == null )
     			{
		    		this.m_E_Choose1Toggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_ToggleGroup/E_Choose1");
     			}
     			return this.m_E_Choose1Toggle;
     		}
     	}

		public UnityEngine.UI.Text E_Label1Text
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Label1Text == null )
     			{
		    		this.m_E_Label1Text = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_ToggleGroup/E_Choose1/E_Label1");
     			}
     			return this.m_E_Label1Text;
     		}
     	}

		public UnityEngine.UI.Toggle E_Choose2Toggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Choose2Toggle == null )
     			{
		    		this.m_E_Choose2Toggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_ToggleGroup/E_Choose2");
     			}
     			return this.m_E_Choose2Toggle;
     		}
     	}

		public UnityEngine.UI.Text E_Label2Text
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Label2Text == null )
     			{
		    		this.m_E_Label2Text = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_ToggleGroup/E_Choose2/E_Label2");
     			}
     			return this.m_E_Label2Text;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_OKButton = null;
			this.m_E_OKImage = null;
			this.m_E_ToggleGroupToggleGroup = null;
			this.m_E_Choose1Toggle = null;
			this.m_E_Label1Text = null;
			this.m_E_Choose2Toggle = null;
			this.m_E_Label2Text = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_OKButton = null;
		private UnityEngine.UI.Image m_E_OKImage = null;
		private UnityEngine.UI.ToggleGroup m_E_ToggleGroupToggleGroup = null;
		private UnityEngine.UI.Toggle m_E_Choose1Toggle = null;
		private UnityEngine.UI.Text m_E_Label1Text = null;
		private UnityEngine.UI.Toggle m_E_Choose2Toggle = null;
		private UnityEngine.UI.Text m_E_Label2Text = null;
		public Transform uiTransform = null;
	}
}
