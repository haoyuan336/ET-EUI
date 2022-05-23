
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgBagLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public ESWeaponBagCommon ESWeaponBagCommon
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_esweaponbagcommon == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"ESWeaponBagCommon");
		    	   this.m_esweaponbagcommon = this.AddChild<ESWeaponBagCommon,Transform>(subTrans);
     			}
     			return this.m_esweaponbagcommon;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_esweaponbagcommon?.Dispose();
			this.m_esweaponbagcommon = null;
			this.uiTransform = null;
		}

		private ESWeaponBagCommon m_esweaponbagcommon = null;
		public Transform uiTransform = null;
	}
}
