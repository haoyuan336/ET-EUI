using UnityEngine;

namespace ET
{
    
    public class SceneChangeFinish_ShowCurrentSceneUI: AEvent<EventType.SceneChangeFinish>
    {
        protected override async ETTask Run(EventType.SceneChangeFinish args)
        {
            // args.ZoneScene.GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Lobby);
            Log.Debug($"scene change finish {args.CurrentScene.Name}");

            switch (args.CurrentScene.Name)
            {
                case "MainScene":
                    args.CurrentScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainScene);
                    break;
                case "PVEGameScene":
                    Log.Debug("进入了pve 游戏页面");
                    break;
            }
            await ETTask.CompletedTask;
        }
    }
    
}