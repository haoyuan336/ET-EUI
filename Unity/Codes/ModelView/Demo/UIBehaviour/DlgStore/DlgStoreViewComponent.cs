
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgStoreViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Toggle E_StoreToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StoreToggle == null )
     			{
		    		this.m_E_StoreToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"Image/ToggleGroup/E_Store");
     			}
     			return this.m_E_StoreToggle;
     		}
     	}

		public UnityEngine.UI.Toggle E_WeaponToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WeaponToggle == null )
     			{
		    		this.m_E_WeaponToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"Image/ToggleGroup/E_Weapon");
     			}
     			return this.m_E_WeaponToggle;
     		}
     	}

		public UnityEngine.UI.Toggle E_ItemToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ItemToggle == null )
     			{
		    		this.m_E_ItemToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"Image/ToggleGroup/E_Item");
     			}
     			return this.m_E_ItemToggle;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollListLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollListLoopVerticalScrollRect == null )
     			{
		    		this.m_ELoopScrollListLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"Image/ELoopScrollList");
     			}
     			return this.m_ELoopScrollListLoopVerticalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_StoreToggle = null;
			this.m_E_WeaponToggle = null;
			this.m_E_ItemToggle = null;
			this.m_ELoopScrollListLoopVerticalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Toggle m_E_StoreToggle = null;
		private UnityEngine.UI.Toggle m_E_WeaponToggle = null;
		private UnityEngine.UI.Toggle m_E_ItemToggle = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollListLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
