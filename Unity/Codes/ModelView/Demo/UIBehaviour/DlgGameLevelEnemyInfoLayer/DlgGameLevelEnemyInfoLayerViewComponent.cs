﻿
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgGameLevelEnemyInfoLayerViewComponent : Entity,IAwake,IDestroy 
	{
		public void DestroyWidget()
		{
			this.uiTransform = null;
		}

		public Transform uiTransform = null;
	}
}
