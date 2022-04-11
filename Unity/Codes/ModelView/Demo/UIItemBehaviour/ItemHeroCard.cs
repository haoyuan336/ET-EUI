
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_ItemHeroCard : Entity,IAwake,IDestroy 
	{
		private bool isCacheNode = false;
		public HeroCardInfo HeroCardInfo;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_ItemHeroCard BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Button E_ClickButton
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
     				if( this.m_E_ClickButton == null )
     				{
		    			this.m_E_ClickButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Click");
     				}
     				return this.m_E_ClickButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Click");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_ClickImage
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
     				if( this.m_E_ClickImage == null )
     				{
		    			this.m_E_ClickImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Click");
     				}
     				return this.m_E_ClickImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Click");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_TextText
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
     				if( this.m_E_TextText == null )
     				{
		    			this.m_E_TextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Click/E_Text");
     				}
     				return this.m_E_TextText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Click/E_Text");
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

		public void DestroyWidget()
		{
			this.m_E_ClickButton = null;
			this.m_E_ClickImage = null;
			this.m_E_TextText = null;
			this.m_E_InTroopMarkImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_ClickButton = null;
		private UnityEngine.UI.Image m_E_ClickImage = null;
		private UnityEngine.UI.Text m_E_TextText = null;
		private UnityEngine.UI.Image m_E_InTroopMarkImage = null;
		public Transform uiTransform = null;
	}
}
