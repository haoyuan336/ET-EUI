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

            switch (args.CurrentScene.Name)
            {
                case "MainScene":
                    Log.Debug("进入了游戏主页面");

                    await args.CurrentScene.GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainScene);

                    // DiamondComponent diamondComponent =  args.CurrentScene.GetComponent<DiamondComponent>();
                    // diamondComponent.RemoveAllChild();

                    // DiamondComponent diamondComponent = args.CurrentScene.ZoneScene().GetComponent<DiamondComponent>();
                    // diamondComponent.RemoveAllChild<Diamond>();
                    //
                    // HeroCardComponent heroCardComponent = args.CurrentScene.ZoneScene().GetComponent<HeroCardComponent>();
                    // heroCardComponent.RemoveAllChild<HeroCard>();
                    // foreach (var child in children)
                    // {
                    //     child?.Dispose();
                    // }
                    break;
                case "PVEGameScene":
                    Log.Debug("进入了pve 游戏页面");

                    await AddressableComponent.Instance.LoadAssetsByLabelAsync<GameObject>("HeroCard", (result) => { });
                    await AddressableComponent.Instance.LoadAssetsByLabelAsync<GameObject>("Unit", (result) => { });
                    await AddressableComponent.Instance.LoadAssetsByLabelAsync<GameObject>("Effect", (result) => { });

                    long AccountId = args.CurrentScene.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
                    args.CurrentScene.ZoneScene().GetComponent<SessionComponent>().Session.Send(new C2M_GameReadyMessage() { AccountId = AccountId });
                    break;
            }

            await ETTask.CompletedTask;
        }
    }
}