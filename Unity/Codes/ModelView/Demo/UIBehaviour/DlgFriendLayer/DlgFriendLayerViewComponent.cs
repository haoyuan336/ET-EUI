
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgFriendLayerViewComponent : Entity,IAwake,IDestroy 
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

		public UnityEngine.UI.Button E_SearchButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SearchButton == null )
     			{
		    		this.m_E_SearchButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Bg/bg/E_Search");
     			}
     			return this.m_E_SearchButton;
     		}
     	}

		public UnityEngine.UI.Image E_SearchImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SearchImage == null )
     			{
		    		this.m_E_SearchImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Bg/bg/E_Search");
     			}
     			return this.m_E_SearchImage;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopFriendListLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopFriendListLoopVerticalScrollRect == null )
     			{
		    		this.m_ELoopFriendListLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"Bg/Bg /ELoopFriendList");
     			}
     			return this.m_ELoopFriendListLoopVerticalScrollRect;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_SearchButton = null;
			this.m_E_SearchImage = null;
			this.m_ELoopFriendListLoopVerticalScrollRect = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.Button m_E_SearchButton = null;
		private UnityEngine.UI.Image m_E_SearchImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopFriendListLoopVerticalScrollRect = null;
		public Transform uiTransform = null;
	}
}
