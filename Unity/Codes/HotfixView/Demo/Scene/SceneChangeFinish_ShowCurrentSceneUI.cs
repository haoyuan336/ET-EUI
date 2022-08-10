using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class SceneChangeFinish_ShowCurrentSceneUI: AEvent<EventType.SceneChangeFinish>
    {
        protected override async ETTask Run(EventType.SceneChangeFinish args)
        {
            // args.ZoneScene.GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Lobby);
            Log.Debug($"scene change finish {args.CurrentScene.Name}");

            UIComponent uiComponent = args.CurrentScene.GetComponent<UIComponent>();
            long AccountId = args.CurrentScene.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = args.CurrentScene.ZoneScene().GetComponent<SessionComponent>().Session;
            switch (args.CurrentScene.Name)
            {
                case "MainScene":
                    Log.Debug("进入了游戏主页面");

                    uiComponent.ShowWindow(WindowID.WindowID_MainScene).Coroutine();

                    break;
                case "PVEGameScene":
                    Log.Debug("进入了pve 游戏页面");

                    // Transform cm1 = UIFindHelper.FindDeepChild(GlobalComponent.Instance.Global.gameObject, "CM vcam1");
                    // cm1.gameObject.SetActive(true);
                    // Transform cm2 = UIFindHelper.FindDeepChild(GlobalComponent.Instance.Global.gameObject, "CM vcam2");
                    // cm2.gameObject.SetActive(true);
                    //隐藏战斗摄像头
                    // GameObject cm2 = GameObject.Find("CM vcam2");
                    // cm2.gameObject.SetActive(false);

                    List<ETTask> tasks = new List<ETTask>();
                    tasks.Add(AddressableComponent.Instance.LoadAssetsByLabelAsyncNotReturn<GameObject>("HeroCard"));
                    tasks.Add(AddressableComponent.Instance.LoadAssetsByLabelAsyncNotReturn<GameObject>("Unit"));
                    tasks.Add(AddressableComponent.Instance.LoadAssetsByLabelAsyncNotReturn<GameObject>("Effect"));
                    //切换摄像机

                    await ETTaskHelper.WaitAll(tasks);

                    session.Send(new C2M_GameReadyMessage() { AccountId = AccountId });

                    await args.ZoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GameLevelLayer);
                    await args.ZoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GameUI);

                    break;
                case "PVPGameScene":
                    //进入了对战层
                    session.Send(new C2M_GameReadyMessage() { AccountId = AccountId });

                    await args.ZoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GameUI);
                    
                    // args.ZoneScene.GetComponent<UIComponent>().HideWindow(WindowID.WindowID_PVPMatchFightLayer);
                    // args.ZoneScene.GetComponent<UIComponent>().HideWindow(WindowID.WindowID_PVPSceneLayer);
                    // args.ZoneScene.GetComponent<UIComponent>().HideWindow(WindowID.WindowID_PVPFightPrepareLayer);
                    // args.ZoneScene.GetComponent<UIComponent>().HideWindow(WindowID.WindowID_GoldInfoUI);
                    break;
            }

            await ETTask.CompletedTask;
        }
    }
}