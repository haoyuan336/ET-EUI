
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgFormationUIViewComponent : Entity,IAwake,IDestroy 
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

		public UnityEngine.UI.Button E_PVPButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PVPButton == null )
     			{
		    		this.m_E_PVPButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_PVP");
     			}
     			return this.m_E_PVPButton;
     		}
     	}

		public UnityEngine.UI.Image E_PVPImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PVPImage == null )
     			{
		    		this.m_E_PVPImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_PVP");
     			}
     			return this.m_E_PVPImage;
     		}
     	}

		public UnityEngine.UI.Button E_FormationButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_FormationButton == null )
     			{
		    		this.m_E_FormationButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Formation");
     			}
     			return this.m_E_FormationButton;
     		}
     	}

		public UnityEngine.UI.Image E_FormationImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_FormationImage == null )
     			{
		    		this.m_E_FormationImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Formation");
     			}
     			return this.m_E_FormationImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_PVEButton = null;
			this.m_E_PVEImage = null;
			this.m_E_PVPButton = null;
			this.m_E_PVPImage = null;
			this.m_E_FormationButton = null;
			this.m_E_FormationImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_PVEButton = null;
		private UnityEngine.UI.Image m_E_PVEImage = null;
		private UnityEngine.UI.Button m_E_PVPButton = null;
		private UnityEngine.UI.Image m_E_PVPImage = null;
		private UnityEngine.UI.Button m_E_FormationButton = null;
		private UnityEngine.UI.Image m_E_FormationImage = null;
		public Transform uiTransform = null;
	}
}
