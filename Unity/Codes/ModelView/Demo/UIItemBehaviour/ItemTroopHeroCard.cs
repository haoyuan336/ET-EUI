
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_ItemTroopHeroCard : Entity,IAwake,IDestroy 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_ItemTroopHeroCard BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Toggle E_ToggleToggle
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
     				if( this.m_E_ToggleToggle == null )
     				{
		    			this.m_E_ToggleToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_Toggle");
     				}
     				return this.m_E_ToggleToggle;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_Toggle");
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
		    			this.m_E_TextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Toggle/Background/E_Text");
     				}
     				return this.m_E_TextText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Toggle/Background/E_Text");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_ToggleToggle = null;
			this.m_E_TextText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Toggle m_E_ToggleToggle = null;
		private UnityEngine.UI.Text m_E_TextText = null;
		public Transform uiTransform = null;
	}
}
