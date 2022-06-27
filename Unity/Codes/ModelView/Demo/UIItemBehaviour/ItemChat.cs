
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_ItemChat : Entity,IAwake,IDestroy 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_ItemChat BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Image E_FriendItemImage
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
     				if( this.m_E_FriendItemImage == null )
     				{
		    			this.m_E_FriendItemImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_FriendItem");
     				}
     				return this.m_E_FriendItemImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_FriendItem");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_FriendHeadImage
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
     				if( this.m_E_FriendHeadImage == null )
     				{
		    			this.m_E_FriendHeadImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_FriendItem/E_FriendHead");
     				}
     				return this.m_E_FriendHeadImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_FriendItem/E_FriendHead");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_FriendNameText
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
     				if( this.m_E_FriendNameText == null )
     				{
		    			this.m_E_FriendNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_FriendItem/Text/E_FriendName");
     				}
     				return this.m_E_FriendNameText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_FriendItem/Text/E_FriendName");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_FriendChatText
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
     				if( this.m_E_FriendChatText == null )
     				{
		    			this.m_E_FriendChatText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_FriendItem/Text/Image/E_FriendChat");
     				}
     				return this.m_E_FriendChatText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_FriendItem/Text/Image/E_FriendChat");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_MyItemImage
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
     				if( this.m_E_MyItemImage == null )
     				{
		    			this.m_E_MyItemImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_MyItem");
     				}
     				return this.m_E_MyItemImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_MyItem");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_MyNameText
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
     				if( this.m_E_MyNameText == null )
     				{
		    			this.m_E_MyNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MyItem/Text/E_MyName");
     				}
     				return this.m_E_MyNameText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MyItem/Text/E_MyName");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_MyChatText
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
     				if( this.m_E_MyChatText == null )
     				{
		    			this.m_E_MyChatText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MyItem/Text/Image/E_MyChat");
     				}
     				return this.m_E_MyChatText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MyItem/Text/Image/E_MyChat");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_MyHeadImage
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
     				if( this.m_E_MyHeadImage == null )
     				{
		    			this.m_E_MyHeadImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_MyItem/E_MyHead");
     				}
     				return this.m_E_MyHeadImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_MyItem/E_MyHead");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_FriendItemImage = null;
			this.m_E_FriendHeadImage = null;
			this.m_E_FriendNameText = null;
			this.m_E_FriendChatText = null;
			this.m_E_MyItemImage = null;
			this.m_E_MyNameText = null;
			this.m_E_MyChatText = null;
			this.m_E_MyHeadImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_FriendItemImage = null;
		private UnityEngine.UI.Image m_E_FriendHeadImage = null;
		private UnityEngine.UI.Text m_E_FriendNameText = null;
		private UnityEngine.UI.Text m_E_FriendChatText = null;
		private UnityEngine.UI.Image m_E_MyItemImage = null;
		private UnityEngine.UI.Text m_E_MyNameText = null;
		private UnityEngine.UI.Text m_E_MyChatText = null;
		private UnityEngine.UI.Image m_E_MyHeadImage = null;
		public Transform uiTransform = null;
	}
}
