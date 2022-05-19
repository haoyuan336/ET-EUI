
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

		public ESTroopHeroCards ESTroopHeroCards
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_estroopherocards == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"ESTroopHeroCards");
		    	   this.m_estroopherocards = this.AddChild<ESTroopHeroCards,Transform>(subTrans);
     			}
     			return this.m_estroopherocards;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_TroopNameText = null;
			this.m_estroopherocards?.Dispose();
			this.m_estroopherocards = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_TroopNameText = null;
		private ESTroopHeroCards m_estroopherocards = null;
		public Transform uiTransform = null;
	}
}
