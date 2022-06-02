
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgHeroWeaponPreviewLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_BackButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackButton == null )
     			{
		    		this.m_E_BackButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Back");
     			}
     			return this.m_E_BackButton;
     		}
     	}

		public UnityEngine.UI.Image E_BackImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BackImage == null )
     			{
		    		this.m_E_BackImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Back");
     			}
     			return this.m_E_BackImage;
     		}
     	}

		public UnityEngine.UI.Button E_BaseInfoButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BaseInfoButton == null )
     			{
		    		this.m_E_BaseInfoButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Bg/E_BaseInfo");
     			}
     			return this.m_E_BaseInfoButton;
     		}
     	}

		public UnityEngine.UI.Text E_CurrentHPText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentHPText == null )
     			{
		    		this.m_E_CurrentHPText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_BaseInfo/Group/E_CurrentHP");
     			}
     			return this.m_E_CurrentHPText;
     		}
     	}

		public UnityEngine.UI.Text E_CurrentAttackText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentAttackText == null )
     			{
		    		this.m_E_CurrentAttackText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_BaseInfo/Group (1)/E_CurrentAttack");
     			}
     			return this.m_E_CurrentAttackText;
     		}
     	}

		public UnityEngine.UI.Text E_CurrentDefenceText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentDefenceText == null )
     			{
		    		this.m_E_CurrentDefenceText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/E_BaseInfo/Group (2)/E_CurrentDefence");
     			}
     			return this.m_E_CurrentDefenceText;
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
     			if( this.m_E_WeaponImage == null )
     			{
		    		this.m_E_WeaponImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Weapon");
     			}
     			return this.m_E_WeaponImage;
     		}
     	}

		public UnityEngine.UI.Image E_EquipImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EquipImage == null )
     			{
		    		this.m_E_EquipImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Equip");
     			}
     			return this.m_E_EquipImage;
     		}
     	}

		public UnityEngine.UI.Image E_AccessoryImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AccessoryImage == null )
     			{
		    		this.m_E_AccessoryImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Accessory");
     			}
     			return this.m_E_AccessoryImage;
     		}
     	}

		public UnityEngine.UI.Image E_RingImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RingImage == null )
     			{
		    		this.m_E_RingImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Ring");
     			}
     			return this.m_E_RingImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_BaseInfoButton = null;
			this.m_E_CurrentHPText = null;
			this.m_E_CurrentAttackText = null;
			this.m_E_CurrentDefenceText = null;
			this.m_E_WeaponImage = null;
			this.m_E_EquipImage = null;
			this.m_E_AccessoryImage = null;
			this.m_E_RingImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Button m_E_BaseInfoButton = null;
		private UnityEngine.UI.Text m_E_CurrentHPText = null;
		private UnityEngine.UI.Text m_E_CurrentAttackText = null;
		private UnityEngine.UI.Text m_E_CurrentDefenceText = null;
		private UnityEngine.UI.Image m_E_WeaponImage = null;
		private UnityEngine.UI.Image m_E_EquipImage = null;
		private UnityEngine.UI.Image m_E_AccessoryImage = null;
		private UnityEngine.UI.Image m_E_RingImage = null;
		public Transform uiTransform = null;
	}
}
