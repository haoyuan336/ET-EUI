
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public partial class Scroll_ItemAddText : Entity,IAwake,IDestroy 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_ItemAddText BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Text E_ShowText
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
     				if( this.m_E_ShowText == null )
     				{
		    			this.m_E_ShowText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Show");
     				}
     				return this.m_E_ShowText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Show");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_ShowText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_ShowText = null;
		public Transform uiTransform = null;
	}
}
