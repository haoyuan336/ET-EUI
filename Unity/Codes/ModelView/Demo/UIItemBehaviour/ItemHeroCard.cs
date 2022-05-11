
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_ItemHeroCard : Entity,IAwake,IDestroy 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_ItemHeroCard BindTrans(Transform trans)
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

		public UnityEngine.UI.Image E_FrameImage
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
     				if( this.m_E_FrameImage == null )
     				{
		    			this.m_E_FrameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Frame");
     				}
     				return this.m_E_FrameImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Frame");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_InTroopMarkImage
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
     				if( this.m_E_InTroopMarkImage == null )
     				{
		    			this.m_E_InTroopMarkImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_InTroopMark");
     				}
     				return this.m_E_InTroopMarkImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_InTroopMark");
     			}
     		}
     	}

		public UnityEngine.UI.Toggle E_ChooseToggle
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
     				if( this.m_E_ChooseToggle == null )
     				{
		    			this.m_E_ChooseToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_Choose");
     				}
     				return this.m_E_ChooseToggle;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_Choose");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_HeadImage = null;
			this.m_E_FrameImage = null;
			this.m_E_InTroopMarkImage = null;
			this.m_E_ChooseToggle = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_HeadImage = null;
		private UnityEngine.UI.Image m_E_FrameImage = null;
		private UnityEngine.UI.Image m_E_InTroopMarkImage = null;
		private UnityEngine.UI.Toggle m_E_ChooseToggle = null;
		public Transform uiTransform = null;
	}
}
