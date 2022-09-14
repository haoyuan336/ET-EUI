
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
		    		this.m_E_BaseInfoButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Bg/Scroll View/Viewport/Content/E_BaseInfo");
     			}
     			return this.m_E_BaseInfoButton;
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
		    		this.m_E_CurrentAttackText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/Scroll View/Viewport/Content/E_BaseInfo/Group (1)/E_CurrentAttack");
     			}
     			return this.m_E_CurrentAttackText;
     		}
     	}

		public UnityEngine.UI.Text E_CurrentAttackAdditionText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentAttackAdditionText == null )
     			{
		    		this.m_E_CurrentAttackAdditionText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/Scroll View/Viewport/Content/E_BaseInfo/Group (6)/E_CurrentAttackAddition");
     			}
     			return this.m_E_CurrentAttackAdditionText;
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
		    		this.m_E_CurrentHPText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/Scroll View/Viewport/Content/E_BaseInfo/Group/E_CurrentHP");
     			}
     			return this.m_E_CurrentHPText;
     		}
     	}

		public UnityEngine.UI.Text E_CurrentHPAdditionText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentHPAdditionText == null )
     			{
		    		this.m_E_CurrentHPAdditionText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/Scroll View/Viewport/Content/E_BaseInfo/Group (7)/E_CurrentHPAddition");
     			}
     			return this.m_E_CurrentHPAdditionText;
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
		    		this.m_E_CurrentDefenceText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/Scroll View/Viewport/Content/E_BaseInfo/Group (2)/E_CurrentDefence");
     			}
     			return this.m_E_CurrentDefenceText;
     		}
     	}

		public UnityEngine.UI.Text E_CurrentDefenceAdditionText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentDefenceAdditionText == null )
     			{
		    		this.m_E_CurrentDefenceAdditionText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/Scroll View/Viewport/Content/E_BaseInfo/Group (9)/E_CurrentDefenceAddition");
     			}
     			return this.m_E_CurrentDefenceAdditionText;
     		}
     	}

		public UnityEngine.UI.Text E_CriticalHitText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CriticalHitText == null )
     			{
		    		this.m_E_CriticalHitText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/Scroll View/Viewport/Content/E_BaseInfo/Group (3)/E_CriticalHit");
     			}
     			return this.m_E_CriticalHitText;
     		}
     	}

		public UnityEngine.UI.Text E_CriticalHitDamageText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CriticalHitDamageText == null )
     			{
		    		this.m_E_CriticalHitDamageText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/Scroll View/Viewport/Content/E_BaseInfo/Group (4)/E_CriticalHitDamage");
     			}
     			return this.m_E_CriticalHitDamageText;
     		}
     	}

		public UnityEngine.UI.Text E_ToughnessText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ToughnessText == null )
     			{
		    		this.m_E_ToughnessText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/Scroll View/Viewport/Content/E_BaseInfo/Group (5)/E_Toughness");
     			}
     			return this.m_E_ToughnessText;
     		}
     	}

		public UnityEngine.UI.Text E_DamageAdditionText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DamageAdditionText == null )
     			{
		    		this.m_E_DamageAdditionText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/Scroll View/Viewport/Content/E_BaseInfo/Group (8)/E_DamageAddition");
     			}
     			return this.m_E_DamageAdditionText;
     		}
     	}

		public UnityEngine.UI.Text E_DamageReductionText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DamageReductionText == null )
     			{
		    		this.m_E_DamageReductionText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Bg/Scroll View/Viewport/Content/E_BaseInfo/Group (10)/E_DamageReduction");
     			}
     			return this.m_E_DamageReductionText;
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
			this.m_E_CurrentAttackText = null;
			this.m_E_CurrentAttackAdditionText = null;
			this.m_E_CurrentHPText = null;
			this.m_E_CurrentHPAdditionText = null;
			this.m_E_CurrentDefenceText = null;
			this.m_E_CurrentDefenceAdditionText = null;
			this.m_E_CriticalHitText = null;
			this.m_E_CriticalHitDamageText = null;
			this.m_E_ToughnessText = null;
			this.m_E_DamageAdditionText = null;
			this.m_E_DamageReductionText = null;
			this.m_E_WeaponImage = null;
			this.m_E_EquipImage = null;
			this.m_E_AccessoryImage = null;
			this.m_E_RingImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Button m_E_BaseInfoButton = null;
		private UnityEngine.UI.Text m_E_CurrentAttackText = null;
		private UnityEngine.UI.Text m_E_CurrentAttackAdditionText = null;
		private UnityEngine.UI.Text m_E_CurrentHPText = null;
		private UnityEngine.UI.Text m_E_CurrentHPAdditionText = null;
		private UnityEngine.UI.Text m_E_CurrentDefenceText = null;
		private UnityEngine.UI.Text m_E_CurrentDefenceAdditionText = null;
		private UnityEngine.UI.Text m_E_CriticalHitText = null;
		private UnityEngine.UI.Text m_E_CriticalHitDamageText = null;
		private UnityEngine.UI.Text m_E_ToughnessText = null;
		private UnityEngine.UI.Text m_E_DamageAdditionText = null;
		private UnityEngine.UI.Text m_E_DamageReductionText = null;
		private UnityEngine.UI.Image m_E_WeaponImage = null;
		private UnityEngine.UI.Image m_E_EquipImage = null;
		private UnityEngine.UI.Image m_E_AccessoryImage = null;
		private UnityEngine.UI.Image m_E_RingImage = null;
		public Transform uiTransform = null;
	}
}
