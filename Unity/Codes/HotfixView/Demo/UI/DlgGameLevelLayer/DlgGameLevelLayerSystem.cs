using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgGameLevelLayerSystem
    {
        public static void RegisterUIEvent(this DlgGameLevelLayer self)
        {
        }

        public static void ShowWindow(this DlgGameLevelLayer self, Entity contextData = null)
        {
            // var levelNum = self.DomainScene().GetComponent<PlayerComponent>().CurrentLevelNum;
            // self.View.E_LevelText.text = $"第{levelNum}关";
        }
    }
}