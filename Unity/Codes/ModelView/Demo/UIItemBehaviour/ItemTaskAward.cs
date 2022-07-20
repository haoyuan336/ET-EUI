
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

		public UnityEngine.UI.Text E_TaskDesText
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
     				if( this.m_E_TaskDesText == null )
     				{
		    			this.m_E_TaskDesText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TaskDes");
     				}
     				return this.m_E_TaskDesText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TaskDes");
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

		public UnityEngine.UI.Text E_AwardDesText
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
     				if( this.m_E_AwardDesText == null )
     				{
		    			this.m_E_AwardDesText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_AwardDes");
     				}
     				return this.m_E_AwardDesText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_AwardDes");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_ActiveValueDesText
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
     				if( this.m_E_ActiveValueDesText == null )
     				{
		    			this.m_E_ActiveValueDesText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_ActiveValueDes");
     				}
     				return this.m_E_ActiveValueDesText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_ActiveValueDes");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_AwardImage = null;
			this.m_E_TaskDesText = null;
			this.m_E_GetButton = null;
			this.m_E_GetImage = null;
			this.m_E_GetTipText = null;
			this.m_E_AwardDesText = null;
			this.m_E_ActiveValueDesText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_AwardImage = null;
		private UnityEngine.UI.Text m_E_TaskDesText = null;
		private UnityEngine.UI.Button m_E_GetButton = null;
		private UnityEngine.UI.Image m_E_GetImage = null;
		private UnityEngine.UI.Text m_E_GetTipText = null;
		private UnityEngine.UI.Text m_E_AwardDesText = null;
		private UnityEngine.UI.Text m_E_ActiveValueDesText = null;
		public Transform uiTransform = null;
	}
}
