
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_ItemAward : Entity,IAwake,IDestroy 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_ItemAward BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Image E_IconImage
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
     				if( this.m_E_IconImage == null )
     				{
		    			this.m_E_IconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Icon");
     				}
     				return this.m_E_IconImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Icon");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_QualityIconImage
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
     				if( this.m_E_QualityIconImage == null )
     				{
		    			this.m_E_QualityIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_QualityIcon");
     				}
     				return this.m_E_QualityIconImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_QualityIcon");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_ElementIconImage
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
     				if( this.m_E_ElementIconImage == null )
     				{
		    			this.m_E_ElementIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_ElementIcon");
     				}
     				return this.m_E_ElementIconImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_ElementIcon");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_IconImage = null;
			this.m_E_QualityIconImage = null;
			this.m_E_ElementIconImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_IconImage = null;
		private UnityEngine.UI.Image m_E_QualityIconImage = null;
		private UnityEngine.UI.Image m_E_ElementIconImage = null;
		public Transform uiTransform = null;
	}
}
