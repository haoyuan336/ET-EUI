
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
		    			this.m_E_TextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Text");
     				}
     				return this.m_E_TextText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Text");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_TextText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_TextText = null;
		public Transform uiTransform = null;
	}
}
