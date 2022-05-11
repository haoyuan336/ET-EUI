
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgGameLevelLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Text E_LevelText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LevelText == null )
     			{
		    		this.m_E_LevelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Level");
     			}
     			return this.m_E_LevelText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_LevelText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_LevelText = null;
		public Transform uiTransform = null;
	}
}
