
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_ItemChooseHead : Entity,IAwake,IDestroy 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_ItemChooseHead BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Button E_HeadButton
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
     				if( this.m_E_HeadButton == null )
     				{
		    			this.m_E_HeadButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Head");
     				}
     				return this.m_E_HeadButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Head");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_HeadImage
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
     				if( this.m_E_HeadImage == null )
     				{
		    			this.m_E_HeadImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Head");
     				}
     				return this.m_E_HeadImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Head");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_MarkImage
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
     				if( this.m_E_MarkImage == null )
     				{
		    			this.m_E_MarkImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Mark");
     				}
     				return this.m_E_MarkImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Mark");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_HeadButton = null;
			this.m_E_HeadImage = null;
			this.m_E_MarkImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_HeadButton = null;
		private UnityEngine.UI.Image m_E_HeadImage = null;
		private UnityEngine.UI.Image m_E_MarkImage = null;
		public Transform uiTransform = null;
	}
}
