namespace ET
{
    public class LoginFinish_CreateLobbyUI: AEvent<EventType.LoginFinish>
    {
        protected override async ETTask Run(EventType.LoginFinish args)
        {
            Log.Debug("Login success event run");
            args.ZoneScene.GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Login);
            await args.ZoneScene.GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_Lobby);
            // Computer computer= args.ZoneScene.AddChild<Computer>();
            // computer.AddComponent<PCCaseComponet>();
            // computer.AddComponent<MonitorComponent>();
            // computer.AddComponent<keyboardComponent>();
            // computer.AddComponent<MouseComponent>();
            // computer.Start();
            // await TimerComponent.Instance.WaitAsync(3000);
            // computer.Dispose();
            // Game.EventSystem.Publish(new EventType.InstallComponent(){Computer = computer});
            // computer.Start();

        }
    }
}