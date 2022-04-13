
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgCallHeroLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_LeftButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LeftButton == null )
     			{
		    		this.m_E_LeftButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Top/E_Left");
     			}
     			return this.m_E_LeftButton;
     		}
     	}

		public UnityEngine.UI.Image E_LeftImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LeftImage == null )
     			{
		    		this.m_E_LeftImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Top/E_Left");
     			}
     			return this.m_E_LeftImage;
     		}
     	}

		public UnityEngine.UI.Button E_RightButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RightButton == null )
     			{
		    		this.m_E_RightButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Top/E_Right");
     			}
     			return this.m_E_RightButton;
     		}
     	}

		public UnityEngine.UI.Image E_RightImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RightImage == null )
     			{
		    		this.m_E_RightImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Top/E_Right");
     			}
     			return this.m_E_RightImage;
     		}
     	}

		public UnityEngine.UI.Button E_NormalCallButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NormalCallButton == null )
     			{
		    		this.m_E_NormalCallButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Top/Content/E_NormalCall");
     			}
     			return this.m_E_NormalCallButton;
     		}
     	}

		public UnityEngine.UI.Image E_NormalCallImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NormalCallImage == null )
     			{
		    		this.m_E_NormalCallImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Top/Content/E_NormalCall");
     			}
     			return this.m_E_NormalCallImage;
     		}
     	}

		public UnityEngine.UI.Button E_FriendCallButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_FriendCallButton == null )
     			{
		    		this.m_E_FriendCallButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Top/Content/E_FriendCall");
     			}
     			return this.m_E_FriendCallButton;
     		}
     	}

		public UnityEngine.UI.Image E_FriendCallImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_FriendCallImage == null )
     			{
		    		this.m_E_FriendCallImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Top/Content/E_FriendCall");
     			}
     			return this.m_E_FriendCallImage;
     		}
     	}

		public UnityEngine.UI.Button E_NoFreeeCallButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NoFreeeCallButton == null )
     			{
		    		this.m_E_NoFreeeCallButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Top/Content/E_NoFreeeCall");
     			}
     			return this.m_E_NoFreeeCallButton;
     		}
     	}

		public UnityEngine.UI.Image E_NoFreeeCallImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NoFreeeCallImage == null )
     			{
		    		this.m_E_NoFreeeCallImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Top/Content/E_NoFreeeCall");
     			}
     			return this.m_E_NoFreeeCallImage;
     		}
     	}

		public UnityEngine.UI.Button E_CurrentUPTenButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentUPTenButton == null )
     			{
		    		this.m_E_CurrentUPTenButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Panel/Ten/E_CurrentUPTen");
     			}
     			return this.m_E_CurrentUPTenButton;
     		}
     	}

		public UnityEngine.UI.Image E_CurrentUPTenImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentUPTenImage == null )
     			{
		    		this.m_E_CurrentUPTenImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Panel/Ten/E_CurrentUPTen");
     			}
     			return this.m_E_CurrentUPTenImage;
     		}
     	}

		public UnityEngine.UI.Button E_TenButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TenButton == null )
     			{
		    		this.m_E_TenButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Panel/Ten/E_Ten");
     			}
     			return this.m_E_TenButton;
     		}
     	}

		public UnityEngine.UI.Image E_TenImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TenImage == null )
     			{
		    		this.m_E_TenImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Panel/Ten/E_Ten");
     			}
     			return this.m_E_TenImage;
     		}
     	}

		public UnityEngine.UI.Text E_DiamondTenText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DiamondTenText == null )
     			{
		    		this.m_E_DiamondTenText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Panel/Ten/E_Ten/E_DiamondTen");
     			}
     			return this.m_E_DiamondTenText;
     		}
     	}

		public UnityEngine.UI.Text E_RateTenText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RateTenText == null )
     			{
		    		this.m_E_RateTenText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Panel/Ten/E_RateTen");
     			}
     			return this.m_E_RateTenText;
     		}
     	}

		public UnityEngine.UI.Button E_CurrentUPButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentUPButton == null )
     			{
		    		this.m_E_CurrentUPButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Panel/One/E_CurrentUP");
     			}
     			return this.m_E_CurrentUPButton;
     		}
     	}

		public UnityEngine.UI.Image E_CurrentUPImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CurrentUPImage == null )
     			{
		    		this.m_E_CurrentUPImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Panel/One/E_CurrentUP");
     			}
     			return this.m_E_CurrentUPImage;
     		}
     	}

		public UnityEngine.UI.Button E_OneButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_OneButton == null )
     			{
		    		this.m_E_OneButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Panel/One/E_One");
     			}
     			return this.m_E_OneButton;
     		}
     	}

		public UnityEngine.UI.Image E_OneImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_OneImage == null )
     			{
		    		this.m_E_OneImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Panel/One/E_One");
     			}
     			return this.m_E_OneImage;
     		}
     	}

		public UnityEngine.UI.Text E_DiamondOneText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_DiamondOneText == null )
     			{
		    		this.m_E_DiamondOneText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Panel/One/E_One/E_DiamondOne");
     			}
     			return this.m_E_DiamondOneText;
     		}
     	}

		public UnityEngine.UI.Text E_RateText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RateText == null )
     			{
		    		this.m_E_RateText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Panel/One/E_Rate");
     			}
     			return this.m_E_RateText;
     		}
     	}

		public UnityEngine.UI.Button E_Call10Button
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Call10Button == null )
     			{
		    		this.m_E_Call10Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Panel/One/E_Call10");
     			}
     			return this.m_E_Call10Button;
     		}
     	}

		public UnityEngine.UI.Image E_Call10Image
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Call10Image == null )
     			{
		    		this.m_E_Call10Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Panel/One/E_Call10");
     			}
     			return this.m_E_Call10Image;
     		}
     	}

		public UnityEngine.UI.Button E_CallButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CallButton == null )
     			{
		    		this.m_E_CallButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Panel/One/E_Call");
     			}
     			return this.m_E_CallButton;
     		}
     	}

		public UnityEngine.UI.Image E_CallImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CallImage == null )
     			{
		    		this.m_E_CallImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Panel/One/E_Call");
     			}
     			return this.m_E_CallImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_LeftButton = null;
			this.m_E_LeftImage = null;
			this.m_E_RightButton = null;
			this.m_E_RightImage = null;
			this.m_E_NormalCallButton = null;
			this.m_E_NormalCallImage = null;
			this.m_E_FriendCallButton = null;
			this.m_E_FriendCallImage = null;
			this.m_E_NoFreeeCallButton = null;
			this.m_E_NoFreeeCallImage = null;
			this.m_E_CurrentUPTenButton = null;
			this.m_E_CurrentUPTenImage = null;
			this.m_E_TenButton = null;
			this.m_E_TenImage = null;
			this.m_E_DiamondTenText = null;
			this.m_E_RateTenText = null;
			this.m_E_CurrentUPButton = null;
			this.m_E_CurrentUPImage = null;
			this.m_E_OneButton = null;
			this.m_E_OneImage = null;
			this.m_E_DiamondOneText = null;
			this.m_E_RateText = null;
			this.m_E_Call10Button = null;
			this.m_E_Call10Image = null;
			this.m_E_CallButton = null;
			this.m_E_CallImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_LeftButton = null;
		private UnityEngine.UI.Image m_E_LeftImage = null;
		private UnityEngine.UI.Button m_E_RightButton = null;
		private UnityEngine.UI.Image m_E_RightImage = null;
		private UnityEngine.UI.Button m_E_NormalCallButton = null;
		private UnityEngine.UI.Image m_E_NormalCallImage = null;
		private UnityEngine.UI.Button m_E_FriendCallButton = null;
		private UnityEngine.UI.Image m_E_FriendCallImage = null;
		private UnityEngine.UI.Button m_E_NoFreeeCallButton = null;
		private UnityEngine.UI.Image m_E_NoFreeeCallImage = null;
		private UnityEngine.UI.Button m_E_CurrentUPTenButton = null;
		private UnityEngine.UI.Image m_E_CurrentUPTenImage = null;
		private UnityEngine.UI.Button m_E_TenButton = null;
		private UnityEngine.UI.Image m_E_TenImage = null;
		private UnityEngine.UI.Text m_E_DiamondTenText = null;
		private UnityEngine.UI.Text m_E_RateTenText = null;
		private UnityEngine.UI.Button m_E_CurrentUPButton = null;
		private UnityEngine.UI.Image m_E_CurrentUPImage = null;
		private UnityEngine.UI.Button m_E_OneButton = null;
		private UnityEngine.UI.Image m_E_OneImage = null;
		private UnityEngine.UI.Text m_E_DiamondOneText = null;
		private UnityEngine.UI.Text m_E_RateText = null;
		private UnityEngine.UI.Button m_E_Call10Button = null;
		private UnityEngine.UI.Image m_E_Call10Image = null;
		private UnityEngine.UI.Button m_E_CallButton = null;
		private UnityEngine.UI.Image m_E_CallImage = null;
		public Transform uiTransform = null;
	}
}
