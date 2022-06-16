
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class ESHeroCardInfoUI : Entity,ET.IAwake<UnityEngine.Transform>,IDestroy 
	{
		public UnityEngine.UI.Image E_AngryBarImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AngryBarImage == null )
     			{
		    		this.m_E_AngryBarImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"AngryProgress/E_AngryBar");
     			}
     			return this.m_E_AngryBarImage;
     		}
     	}

		public UnityEngine.UI.Image E_HpBarImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HpBarImage == null )
     			{
		    		this.m_E_HpBarImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"HpProgress/E_HpBar");
     			}
     			return this.m_E_HpBarImage;
     		}
     	}

		public UnityEngine.UI.Image E_AttackBarImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AttackBarImage == null )
     			{
		    		this.m_E_AttackBarImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"AttackProgress/E_AttackBar");
     			}
     			return this.m_E_AttackBarImage;
     		}
     	}

		public UnityEngine.UI.Image E_HeroElementIconImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_HeroElementIconImage == null )
     			{
		    		this.m_E_HeroElementIconImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_HeroElementIcon");
     			}
     			return this.m_E_HeroElementIconImage;
     		}
     	}

		public UnityEngine.UI.Text E_CommonText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CommonText == null )
     			{
		    		this.m_E_CommonText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Common");
     			}
     			return this.m_E_CommonText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_AngryBarImage = null;
			this.m_E_HpBarImage = null;
			this.m_E_AttackBarImage = null;
			this.m_E_HeroElementIconImage = null;
			this.m_E_CommonText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Image m_E_AngryBarImage = null;
		private UnityEngine.UI.Image m_E_HpBarImage = null;
		private UnityEngine.UI.Image m_E_AttackBarImage = null;
		private UnityEngine.UI.Image m_E_HeroElementIconImage = null;
		private UnityEngine.UI.Text m_E_CommonText = null;
		public Transform uiTransform = null;
	}
}
