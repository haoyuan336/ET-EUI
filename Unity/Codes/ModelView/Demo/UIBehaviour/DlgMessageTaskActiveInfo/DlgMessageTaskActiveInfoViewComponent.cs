
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgMessageTaskActiveInfoViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_MessageButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MessageButton == null )
     			{
		    		this.m_E_MessageButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Message");
     			}
     			return this.m_E_MessageButton;
     		}
     	}

		public UnityEngine.UI.Image E_MessageImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MessageImage == null )
     			{
		    		this.m_E_MessageImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Message");
     			}
     			return this.m_E_MessageImage;
     		}
     	}

		public UnityEngine.UI.Text E_MessageText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MessageText == null )
     			{
		    		this.m_E_MessageText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Message/E_Message");
     			}
     			return this.m_E_MessageText;
     		}
     	}

		public UnityEngine.UI.Button E_TaskButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TaskButton == null )
     			{
		    		this.m_E_TaskButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Task");
     			}
     			return this.m_E_TaskButton;
     		}
     	}

		public UnityEngine.UI.Image E_TaskImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TaskImage == null )
     			{
		    		this.m_E_TaskImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Task");
     			}
     			return this.m_E_TaskImage;
     		}
     	}

		public UnityEngine.UI.Text E_TaskText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TaskText == null )
     			{
		    		this.m_E_TaskText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Task/E_Task");
     			}
     			return this.m_E_TaskText;
     		}
     	}

		public UnityEngine.UI.Button E_Active1Button
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Active1Button == null )
     			{
		    		this.m_E_Active1Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Active1");
     			}
     			return this.m_E_Active1Button;
     		}
     	}

		public UnityEngine.UI.Image E_Active1Image
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Active1Image == null )
     			{
		    		this.m_E_Active1Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Active1");
     			}
     			return this.m_E_Active1Image;
     		}
     	}

		public UnityEngine.UI.Text E_Active1Text
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Active1Text == null )
     			{
		    		this.m_E_Active1Text = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Active1/E_Active1");
     			}
     			return this.m_E_Active1Text;
     		}
     	}

		public UnityEngine.UI.Button E_Active2Button
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Active2Button == null )
     			{
		    		this.m_E_Active2Button = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Active2");
     			}
     			return this.m_E_Active2Button;
     		}
     	}

		public UnityEngine.UI.Image E_Active2Image
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Active2Image == null )
     			{
		    		this.m_E_Active2Image = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Active2");
     			}
     			return this.m_E_Active2Image;
     		}
     	}

		public UnityEngine.UI.Text E_Active2Text
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_Active2Text == null )
     			{
		    		this.m_E_Active2Text = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Active2/E_Active2");
     			}
     			return this.m_E_Active2Text;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_MessageButton = null;
			this.m_E_MessageImage = null;
			this.m_E_MessageText = null;
			this.m_E_TaskButton = null;
			this.m_E_TaskImage = null;
			this.m_E_TaskText = null;
			this.m_E_Active1Button = null;
			this.m_E_Active1Image = null;
			this.m_E_Active1Text = null;
			this.m_E_Active2Button = null;
			this.m_E_Active2Image = null;
			this.m_E_Active2Text = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_MessageButton = null;
		private UnityEngine.UI.Image m_E_MessageImage = null;
		private UnityEngine.UI.Text m_E_MessageText = null;
		private UnityEngine.UI.Button m_E_TaskButton = null;
		private UnityEngine.UI.Image m_E_TaskImage = null;
		private UnityEngine.UI.Text m_E_TaskText = null;
		private UnityEngine.UI.Button m_E_Active1Button = null;
		private UnityEngine.UI.Image m_E_Active1Image = null;
		private UnityEngine.UI.Text m_E_Active1Text = null;
		private UnityEngine.UI.Button m_E_Active2Button = null;
		private UnityEngine.UI.Image m_E_Active2Image = null;
		private UnityEngine.UI.Text m_E_Active2Text = null;
		public Transform uiTransform = null;
	}
}
