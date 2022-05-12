
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_ItemWeapon : Entity,IAwake,IDestroy 
	{
		private bool isCacheNode = false;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_ItemWeapon BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
		}

		public UnityEngine.UI.Button E_WeaponButton
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
     				if( this.m_E_WeaponButton == null )
     				{
		    			this.m_E_WeaponButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Weapon");
     				}
     				return this.m_E_WeaponButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Weapon");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_WeaponImage
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
     				if( this.m_E_WeaponImage == null )
     				{
		    			this.m_E_WeaponImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Weapon");
     				}
     				return this.m_E_WeaponImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Weapon");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_WeaponButton = null;
			this.m_E_WeaponImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_WeaponButton = null;
		private UnityEngine.UI.Image m_E_WeaponImage = null;
		public Transform uiTransform = null;
	}
}
