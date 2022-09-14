
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public partial class Scroll_ItemGameCombo : Entity,IAwake,IDestroy 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_ItemGameCombo BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Text E_ComboText
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
     				if( this.m_E_ComboText == null )
     				{
		    			this.m_E_ComboText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Combo");
     				}
     				return this.m_E_ComboText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Combo");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_ComboText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_ComboText = null;
		public Transform uiTransform = null;
	}
}
