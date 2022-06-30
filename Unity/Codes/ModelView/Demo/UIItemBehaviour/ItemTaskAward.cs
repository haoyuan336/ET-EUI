
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_ItemTaskAward : Entity,IAwake,IDestroy 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_ItemTaskAward BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Image E_AwardImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_AwardImage == null )
     				{
		    			this.m_E_AwardImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Award");
     				}
     				return this.m_E_AwardImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Award");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_TaskNameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_TaskNameText == null )
     				{
		    			this.m_E_TaskNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TaskName");
     				}
     				return this.m_E_TaskNameText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TaskName");
     			}
     		}
     	}

		public UnityEngine.UI.Button E_GetButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_GetButton == null )
     				{
		    			this.m_E_GetButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Get");
     				}
     				return this.m_E_GetButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Get");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_GetImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_GetImage == null )
     				{
		    			this.m_E_GetImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Get");
     				}
     				return this.m_E_GetImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Get");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_GetTipText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_GetTipText == null )
     				{
		    			this.m_E_GetTipText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Get/E_GetTip");
     				}
     				return this.m_E_GetTipText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Get/E_GetTip");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_DesText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_DesText == null )
     				{
		    			this.m_E_DesText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Des");
     				}
     				return this.m_E_DesText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Des");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_AwardImage = null;
			this.m_E_TaskNameText = null;
			this.m_E_GetButton = null;
			this.m_E_GetImage = null;
			this.m_E_GetTipText = null;
			this.m_E_DesText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_AwardImage = null;
		private UnityEngine.UI.Text m_E_TaskNameText = null;
		private UnityEngine.UI.Button m_E_GetButton = null;
		private UnityEngine.UI.Image m_E_GetImage = null;
		private UnityEngine.UI.Text m_E_GetTipText = null;
		private UnityEngine.UI.Text m_E_DesText = null;
		public Transform uiTransform = null;
	}
}
