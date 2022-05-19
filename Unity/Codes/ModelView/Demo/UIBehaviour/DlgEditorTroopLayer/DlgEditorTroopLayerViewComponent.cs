
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgEditorTroopLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Text E_TroopNameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TroopNameText == null )
     			{
		    		this.m_E_TroopNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_TroopName");
     			}
     			return this.m_E_TroopNameText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_TroopNameText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_TroopNameText = null;
		public Transform uiTransform = null;
	}
}
