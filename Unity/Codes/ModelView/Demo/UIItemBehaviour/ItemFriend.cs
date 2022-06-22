
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_ItemFriend : Entity,IAwake,IDestroy 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_ItemFriend BindTrans(Transform trans)
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

		public UnityEngine.UI.Button E_ChatButton
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
     				if( this.m_E_ChatButton == null )
     				{
		    			this.m_E_ChatButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Chat");
     				}
     				return this.m_E_ChatButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Chat");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_ChatImage
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
     				if( this.m_E_ChatImage == null )
     				{
		    			this.m_E_ChatImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Chat");
     				}
     				return this.m_E_ChatImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Chat");
     			}
     		}
     	}

		public UnityEngine.UI.Button E_GiftButton
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
     				if( this.m_E_GiftButton == null )
     				{
		    			this.m_E_GiftButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Gift");
     				}
     				return this.m_E_GiftButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Gift");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_GiftMarkImage
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
     				if( this.m_E_GiftMarkImage == null )
     				{
		    			this.m_E_GiftMarkImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Gift/Background/E_GiftMark");
     				}
     				return this.m_E_GiftMarkImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Gift/Background/E_GiftMark");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_NameText = null;
			this.m_E_TimeText = null;
			this.m_E_ChatButton = null;
			this.m_E_ChatImage = null;
			this.m_E_GiftButton = null;
			this.m_E_GiftMarkImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_NameText = null;
		private UnityEngine.UI.Text m_E_TimeText = null;
		private UnityEngine.UI.Button m_E_ChatButton = null;
		private UnityEngine.UI.Image m_E_ChatImage = null;
		private UnityEngine.UI.Button m_E_GiftButton = null;
		private UnityEngine.UI.Image m_E_GiftMarkImage = null;
		public Transform uiTransform = null;
	}
}
