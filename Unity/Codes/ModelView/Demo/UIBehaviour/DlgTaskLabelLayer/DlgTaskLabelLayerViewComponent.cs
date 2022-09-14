
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgTaskLabelLayerViewComponent : Entity,IAwake,IDestroy 
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

		public UnityEngine.UI.Button EDayTaskButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EDayTaskButton == null )
     			{
		    		this.m_EDayTaskButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EDayTask");
     			}
     			return this.m_EDayTaskButton;
     		}
     	}

		public UnityEngine.UI.Image EDayTaskImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EDayTaskImage == null )
     			{
		    		this.m_EDayTaskImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EDayTask");
     			}
     			return this.m_EDayTaskImage;
     		}
     	}

		public UnityEngine.UI.Button EWeekTaskButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EWeekTaskButton == null )
     			{
		    		this.m_EWeekTaskButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EWeekTask");
     			}
     			return this.m_EWeekTaskButton;
     		}
     	}

		public UnityEngine.UI.Image EWeekTaskImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EWeekTaskImage == null )
     			{
		    		this.m_EWeekTaskImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EWeekTask");
     			}
     			return this.m_EWeekTaskImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_EDayTaskButton = null;
			this.m_EDayTaskImage = null;
			this.m_EWeekTaskButton = null;
			this.m_EWeekTaskImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Button m_EDayTaskButton = null;
		private UnityEngine.UI.Image m_EDayTaskImage = null;
		private UnityEngine.UI.Button m_EWeekTaskButton = null;
		private UnityEngine.UI.Image m_EWeekTaskImage = null;
		public Transform uiTransform = null;
	}
}
