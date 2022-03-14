
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgGameUIViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Image E_ProgressBar_1Image
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ProgressBar_1Image == null )
     			{
		    		this.m_E_ProgressBar_1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_ProgressBar_1");
     			}
     			return this.m_E_ProgressBar_1Image;
     		}
     	}

		public UnityEngine.UI.Image E_ProgressBar_2Image
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ProgressBar_2Image == null )
     			{
		    		this.m_E_ProgressBar_2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_ProgressBar_2");
     			}
     			return this.m_E_ProgressBar_2Image;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_ProgressBar_1Image = null;
			this.m_E_ProgressBar_2Image = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_ProgressBar_1Image = null;
		private UnityEngine.UI.Image m_E_ProgressBar_2Image = null;
		public Transform uiTransform = null;
	}
}
