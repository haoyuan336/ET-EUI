
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public partial class Scroll_ItemBuff : Entity,IAwake,IDestroy
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_ItemBuff BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Text E_CountText
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
     				if( this.m_E_CountText == null )
     				{
		    			this.m_E_CountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Count");
     				}
     				return this.m_E_CountText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Count");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_CountText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_CountText = null;
		public Transform uiTransform = null;
	}
}
