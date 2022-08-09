
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

		public UnityEngine.UI.InputField E_LevelInputInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LevelInputInputField == null )
     			{
		    		this.m_E_LevelInputInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"E_LevelInput");
     			}
     			return this.m_E_LevelInputInputField;
     		}
     	}

		public UnityEngine.UI.Image E_LevelInputImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LevelInputImage == null )
     			{
		    		this.m_E_LevelInputImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_LevelInput");
     			}
     			return this.m_E_LevelInputImage;
     		}
     	}

		public UnityEngine.UI.Image E_TroopHeroCardItemImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TroopHeroCardItemImage == null )
     			{
		    		this.m_E_TroopHeroCardItemImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_TroopHeroCardItem");
     			}
     			return this.m_E_TroopHeroCardItemImage;
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
			this.m_E_BackButton = null;
			this.m_E_BackImage = null;
			this.m_E_LevelInputInputField = null;
			this.m_E_LevelInputImage = null;
			this.m_E_TroopHeroCardItemImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_EditorTroopButton = null;
		private UnityEngine.UI.Image m_E_EditorTroopImage = null;
		private UnityEngine.UI.Button m_E_EditorSkillButton = null;
		private UnityEngine.UI.Image m_E_EditorSkillImage = null;
		private UnityEngine.UI.Text m_E_LevelText = null;
		private UnityEngine.UI.Button m_E_StartGameButton = null;
		private UnityEngine.UI.Image m_E_StartGameImage = null;
		private UnityEngine.UI.Button m_E_BackButton = null;
		private UnityEngine.UI.Image m_E_BackImage = null;
		private UnityEngine.UI.InputField m_E_LevelInputInputField = null;
		private UnityEngine.UI.Image m_E_LevelInputImage = null;
		private UnityEngine.UI.Image m_E_TroopHeroCardItemImage = null;
		public Transform uiTransform = null;
	}
}
