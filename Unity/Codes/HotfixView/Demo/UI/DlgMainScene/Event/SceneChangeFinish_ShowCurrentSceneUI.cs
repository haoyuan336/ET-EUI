using System.Collections.Generic;
using System.Threading.Tasks;
using ET.Account;
using UnityEditor.UI;
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

            switch (args.CurrentScene.Name)
            {
                case "MainScene":
                    Log.Debug("进入了游戏主页面");

                    uiComponent.ShowWindow(WindowID.WindowID_MainScene).Coroutine();

                    break;
                case "PVEGameScene":
                    Log.Debug("进入了pve 游戏页面");

                    List<ETTask> tasks = new List<ETTask>();
                    tasks.Add(AddressableComponent.Instance.LoadAssetsByLabelAsyncNotReturn<GameObject>("HeroCard"));
                    tasks.Add(AddressableComponent.Instance.LoadAssetsByLabelAsyncNotReturn<GameObject>("Unit"));
                    tasks.Add(AddressableComponent.Instance.LoadAssetsByLabelAsyncNotReturn<GameObject>("Effect"));
                    await ETTaskHelper.WaitAll(tasks);
                    long AccountId = args.CurrentScene.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
                    args.CurrentScene.ZoneScene().GetComponent<SessionComponent>().Session.Send(new C2M_GameReadyMessage() { AccountId = AccountId });

                    await args.ZoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GameLevelLayer);
                    await args.ZoneScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GameUI);

                    break;
            }

            await ETTask.CompletedTask;
        }
    }
}