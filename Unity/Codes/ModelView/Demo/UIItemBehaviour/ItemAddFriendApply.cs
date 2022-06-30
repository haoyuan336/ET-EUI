
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_ItemAddFriendApply : Entity,IAwake,IDestroy 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_ItemAddFriendApply BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
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

		public UnityEngine.UI.Button E_HeadFrameButton
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
     				if( this.m_E_HeadFrameButton == null )
     				{
		    			this.m_E_HeadFrameButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_HeadFrame");
     				}
     				return this.m_E_HeadFrameButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_HeadFrame");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_HeadFrameImage
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
     				if( this.m_E_HeadFrameImage == null )
     				{
		    			this.m_E_HeadFrameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_HeadFrame");
     				}
     				return this.m_E_HeadFrameImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_HeadFrame");
     			}
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

		public UnityEngine.UI.Button E_AcceptButton
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
     				if( this.m_E_AcceptButton == null )
     				{
		    			this.m_E_AcceptButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Accept");
     				}
     				return this.m_E_AcceptButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Accept");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_AcceptImage
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
     				if( this.m_E_AcceptImage == null )
     				{
		    			this.m_E_AcceptImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Accept");
     				}
     				return this.m_E_AcceptImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Accept");
     			}
     		}
     	}

		public UnityEngine.UI.Button E_RefuseButton
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
     				if( this.m_E_RefuseButton == null )
     				{
		    			this.m_E_RefuseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Refuse");
     				}
     				return this.m_E_RefuseButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Refuse");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_RefuseImage
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
     				if( this.m_E_RefuseImage == null )
     				{
		    			this.m_E_RefuseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Refuse");
     				}
     				return this.m_E_RefuseImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Refuse");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_HeadImage = null;
			this.m_E_HeadFrameButton = null;
			this.m_E_HeadFrameImage = null;
			this.m_E_NameText = null;
			this.m_E_TimeText = null;
			this.m_E_AcceptButton = null;
			this.m_E_AcceptImage = null;
			this.m_E_RefuseButton = null;
			this.m_E_RefuseImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_HeadImage = null;
		private UnityEngine.UI.Button m_E_HeadFrameButton = null;
		private UnityEngine.UI.Image m_E_HeadFrameImage = null;
		private UnityEngine.UI.Text m_E_NameText = null;
		private UnityEngine.UI.Text m_E_TimeText = null;
		private UnityEngine.UI.Button m_E_AcceptButton = null;
		private UnityEngine.UI.Image m_E_AcceptImage = null;
		private UnityEngine.UI.Button m_E_RefuseButton = null;
		private UnityEngine.UI.Image m_E_RefuseImage = null;
		public Transform uiTransform = null;
	}
}
