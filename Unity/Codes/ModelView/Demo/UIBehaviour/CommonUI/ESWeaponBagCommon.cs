
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public partial class ESWeaponBagCommon : Entity,ET.IAwake<UnityEngine.Transform>,IDestroy 
	{
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
		    		this.m_E_WeaponToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ButtonContent/E_Weapon");
     			}
     			return this.m_E_WeaponToggle;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect E_WeaponLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_WeaponLoopVerticalScrollRect == null )
     			{
		    		this.m_E_WeaponLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"E_Weapon");
     			}
     			return this.m_E_WeaponLoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.Toggle E_SuitToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SuitToggle == null )
     			{
		    		this.m_E_SuitToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ButtonContent/E_Suit");
     			}
     			return this.m_E_SuitToggle;
     		}
     	}

		public UnityEngine.UI.Toggle E_RingToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RingToggle == null )
     			{
		    		this.m_E_RingToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ButtonContent/E_Ring");
     			}
     			return this.m_E_RingToggle;
     		}
     	}

		public UnityEngine.UI.Toggle E_ShiPinToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ShiPinToggle == null )
     			{
		    		this.m_E_ShiPinToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ButtonContent/E_ShiPin");
     			}
     			return this.m_E_ShiPinToggle;
     		}
     	}

		public UnityEngine.UI.Toggle E_AllToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AllToggle == null )
     			{
		    		this.m_E_AllToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"ButtonContent/E_All");
     			}
     			return this.m_E_AllToggle;
     		}
     	}

		public UnityEngine.UI.Text E_BagCountTextText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BagCountTextText == null )
     			{
		    		this.m_E_BagCountTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_BagCountText");
     			}
     			return this.m_E_BagCountTextText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_WeaponToggle = null;
			this.m_E_WeaponLoopVerticalScrollRect = null;
			this.m_E_SuitToggle = null;
			this.m_E_RingToggle = null;
			this.m_E_ShiPinToggle = null;
			this.m_E_AllToggle = null;
			this.m_E_BagCountTextText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Toggle m_E_WeaponToggle = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_WeaponLoopVerticalScrollRect = null;
		private UnityEngine.UI.Toggle m_E_SuitToggle = null;
		private UnityEngine.UI.Toggle m_E_RingToggle = null;
		private UnityEngine.UI.Toggle m_E_ShiPinToggle = null;
		private UnityEngine.UI.Toggle m_E_AllToggle = null;
		private UnityEngine.UI.Text m_E_BagCountTextText = null;
		public Transform uiTransform = null;
	}
}
