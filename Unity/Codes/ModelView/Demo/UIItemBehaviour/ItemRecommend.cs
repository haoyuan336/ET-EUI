
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_ItemRecommend : Entity,IAwake,IDestroy 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_ItemRecommend BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
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
     			if (this.isCacheNode)
     			{
     				if( this.m_E_NameText == null )
     				{
		    			this.m_E_NameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Name");
     				}
     				return this.m_E_NameText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Name");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_TimeText
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
     				if( this.m_E_TimeText == null )
     				{
		    			this.m_E_TimeText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Time");
     				}
     				return this.m_E_TimeText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Time");
     			}
     		}
     	}

		public UnityEngine.UI.Button E_AddButton
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
     				if( this.m_E_AddButton == null )
     				{
		    			this.m_E_AddButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Add");
     				}
     				return this.m_E_AddButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Add");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_AddImage
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
     				if( this.m_E_AddImage == null )
     				{
		    			this.m_E_AddImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Add");
     				}
     				return this.m_E_AddImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Add");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_NameText = null;
			this.m_E_TimeText = null;
			this.m_E_AddButton = null;
			this.m_E_AddImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_NameText = null;
		private UnityEngine.UI.Text m_E_TimeText = null;
		private UnityEngine.UI.Button m_E_AddButton = null;
		private UnityEngine.UI.Image m_E_AddImage = null;
		public Transform uiTransform = null;
	}
}
