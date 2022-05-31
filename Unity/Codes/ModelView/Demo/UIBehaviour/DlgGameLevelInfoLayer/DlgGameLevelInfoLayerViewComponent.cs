
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgGameLevelInfoLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_EditorTroopButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EditorTroopButton == null )
     			{
		    		this.m_E_EditorTroopButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_EditorTroop");
     			}
     			return this.m_E_EditorTroopButton;
     		}
     	}

		public UnityEngine.UI.Image E_EditorTroopImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EditorTroopImage == null )
     			{
		    		this.m_E_EditorTroopImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_EditorTroop");
     			}
     			return this.m_E_EditorTroopImage;
     		}
     	}

		public UnityEngine.UI.Button E_EditorSkillButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EditorSkillButton == null )
     			{
		    		this.m_E_EditorSkillButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_EditorSkill");
     			}
     			return this.m_E_EditorSkillButton;
     		}
     	}

		public UnityEngine.UI.Image E_EditorSkillImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EditorSkillImage == null )
     			{
		    		this.m_E_EditorSkillImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_EditorSkill");
     			}
     			return this.m_E_EditorSkillImage;
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
     			if( this.m_E_LevelText == null )
     			{
		    		this.m_E_LevelText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Image (1)/E_Level");
     			}
     			return this.m_E_LevelText;
     		}
     	}

		public UnityEngine.UI.Button E_StartGameButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StartGameButton == null )
     			{
		    		this.m_E_StartGameButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_StartGame");
     			}
     			return this.m_E_StartGameButton;
     		}
     	}

		public UnityEngine.UI.Image E_StartGameImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_StartGameImage == null )
     			{
		    		this.m_E_StartGameImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_StartGame");
     			}
     			return this.m_E_StartGameImage;
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

		public void DestroyWidget()
		{
			this.m_E_EditorTroopButton = null;
			this.m_E_EditorTroopImage = null;
			this.m_E_EditorSkillButton = null;
			this.m_E_EditorSkillImage = null;
			this.m_E_LevelText = null;
			this.m_E_StartGameButton = null;
			this.m_E_StartGameImage = null;
			this.m_estroopherocards?.Dispose();
			this.m_estroopherocards = null;
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_EditorTroopButton = null;
		private UnityEngine.UI.Image m_E_EditorTroopImage = null;
		private UnityEngine.UI.Button m_E_EditorSkillButton = null;
		private UnityEngine.UI.Image m_E_EditorSkillImage = null;
		private UnityEngine.UI.Text m_E_LevelText = null;
		private UnityEngine.UI.Button m_E_StartGameButton = null;
		private UnityEngine.UI.Image m_E_StartGameImage = null;
		private ESTroopHeroCards m_estroopherocards = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		public Transform uiTransform = null;
	}
}
