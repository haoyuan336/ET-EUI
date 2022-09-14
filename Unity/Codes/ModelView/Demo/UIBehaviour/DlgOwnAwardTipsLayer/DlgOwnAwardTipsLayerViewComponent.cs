
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgOwnAwardTipsLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button EOkButtonButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EOkButtonButton == null )
     			{
		    		this.m_EOkButtonButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EOkButton");
     			}
     			return this.m_EOkButtonButton;
     		}
     	}

		public UnityEngine.UI.Image EOkButtonImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EOkButtonImage == null )
     			{
		    		this.m_EOkButtonImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EOkButton");
     			}
     			return this.m_EOkButtonImage;
     		}
     	}

		public UnityEngine.UI.Image E_IconImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_IconImage == null )
     			{
		    		this.m_E_IconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Icon");
     			}
     			return this.m_E_IconImage;
     		}
     	}

		public UnityEngine.UI.Text E_NameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NameText == null )
     			{
		    		this.m_E_NameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Name");
     			}
     			return this.m_E_NameText;
     		}
     	}

		public UnityEngine.UI.Text E_CountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CountText == null )
     			{
		    		this.m_E_CountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Count");
     			}
     			return this.m_E_CountText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EOkButtonButton = null;
			this.m_EOkButtonImage = null;
			this.m_E_IconImage = null;
			this.m_E_NameText = null;
			this.m_E_CountText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_EOkButtonButton = null;
		private UnityEngine.UI.Image m_EOkButtonImage = null;
		private UnityEngine.UI.Image m_E_IconImage = null;
		private UnityEngine.UI.Text m_E_NameText = null;
		private UnityEngine.UI.Text m_E_CountText = null;
		public Transform uiTransform = null;
	}
}
