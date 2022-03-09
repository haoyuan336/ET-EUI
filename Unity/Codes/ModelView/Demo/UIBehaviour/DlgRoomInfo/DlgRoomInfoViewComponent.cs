
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgRoomInfoViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Text E_RoomIDText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RoomIDText == null )
     			{
		    		this.m_E_RoomIDText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_RoomID");
     			}
     			return this.m_E_RoomIDText;
     		}
     	}

		public UnityEngine.UI.Text E_TurnIndexText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TurnIndexText == null )
     			{
		    		this.m_E_TurnIndexText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TurnIndex");
     			}
     			return this.m_E_TurnIndexText;
     		}
     	}

		public UnityEngine.UI.Text E_MySeatIndexText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MySeatIndexText == null )
     			{
		    		this.m_E_MySeatIndexText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MySeatIndex");
     			}
     			return this.m_E_MySeatIndexText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_RoomIDText = null;
			this.m_E_TurnIndexText = null;
			this.m_E_MySeatIndexText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_RoomIDText = null;
		private UnityEngine.UI.Text m_E_TurnIndexText = null;
		private UnityEngine.UI.Text m_E_MySeatIndexText = null;
		public Transform uiTransform = null;
	}
}
