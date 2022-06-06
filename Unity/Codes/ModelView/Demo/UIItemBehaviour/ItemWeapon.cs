
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

		public UnityEngine.UI.Image E_ElementImage
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
     				if( this.m_E_ElementImage == null )
     				{
		    			this.m_E_ElementImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Weapon/E_Element");
     				}
     				return this.m_E_ElementImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Weapon/E_Element");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_AddTextText
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
     				if( this.m_E_AddTextText == null )
     				{
		    			this.m_E_AddTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Weapon/E_AddText");
     				}
     				return this.m_E_AddTextText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Weapon/E_AddText");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_LevelText
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
     				if( this.m_E_LevelText == null )
     				{
		    			this.m_E_LevelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Level");
     				}
     				return this.m_E_LevelText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Level");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_InTroopMarkImage
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
     				if( this.m_E_InTroopMarkImage == null )
     				{
		    			this.m_E_InTroopMarkImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_InTroopMark");
     				}
     				return this.m_E_InTroopMarkImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_InTroopMark");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_ChooseCountText
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
     				if( this.m_E_ChooseCountText == null )
     				{
		    			this.m_E_ChooseCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_ChooseCount");
     				}
     				return this.m_E_ChooseCountText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_ChooseCount");
     			}
     		}
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

		public UnityEngine.UI.Text E_WeaponTypeText
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
     				if( this.m_E_WeaponTypeText == null )
     				{
		    			this.m_E_WeaponTypeText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_WeaponType");
     				}
     				return this.m_E_WeaponTypeText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_WeaponType");
     			}
     		}
     	}

		public UnityEngine.UI.Toggle E_ChooseToggle
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
     				if( this.m_E_ChooseToggle == null )
     				{
		    			this.m_E_ChooseToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_Choose");
     				}
     				return this.m_E_ChooseToggle;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_Choose");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_CheckmarkImage
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
     				if( this.m_E_CheckmarkImage == null )
     				{
		    			this.m_E_CheckmarkImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Choose/Background/E_Checkmark");
     				}
     				return this.m_E_CheckmarkImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Choose/Background/E_Checkmark");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_WeaponImage = null;
			this.m_E_ElementImage = null;
			this.m_E_AddTextText = null;
			this.m_E_LevelText = null;
			this.m_E_InTroopMarkImage = null;
			this.m_E_ChooseCountText = null;
			this.m_E_CountText = null;
			this.m_E_QualityIconImage = null;
			this.m_E_WeaponTypeText = null;
			this.m_E_ChooseToggle = null;
			this.m_E_CheckmarkImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_WeaponImage = null;
		private UnityEngine.UI.Image m_E_ElementImage = null;
		private UnityEngine.UI.Text m_E_AddTextText = null;
		private UnityEngine.UI.Text m_E_LevelText = null;
		private UnityEngine.UI.Image m_E_InTroopMarkImage = null;
		private UnityEngine.UI.Text m_E_ChooseCountText = null;
		private UnityEngine.UI.Text m_E_CountText = null;
		private UnityEngine.UI.Image m_E_QualityIconImage = null;
		private UnityEngine.UI.Text m_E_WeaponTypeText = null;
		private UnityEngine.UI.Toggle m_E_ChooseToggle = null;
		private UnityEngine.UI.Image m_E_CheckmarkImage = null;
		public Transform uiTransform = null;
	}
}
