using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgGameLevelStoryLayerSystem
    {
        public static void RegisterUIEvent(this DlgGameLevelStoryLayer self)
        {
            self.View.E_AutoButton.AddListener(() =>
            {
                self.IsAutoPlay = !self.IsAutoPlay;
                
                
                
            });
            
            
        }

        public static void ShowWindow(this DlgGameLevelStoryLayer self, Entity contextData = null)
        {
        }

        public static async ETTask SetContent(this DlgGameLevelStoryLayer self, string content)
        {
            List<string> list = content.Split('.').ToList();

            foreach (var str in list)
            {
                self.ContentQuene.Enqueue(str);
            }

            await ETTask.CompletedTask;
        }
    }
}