using System.Runtime.InteropServices;
using ET.EventType;

namespace ET.Demo.Component.Event
{
    public class InstallComputer_AddComponent: AEvent<InstallComponent>
    {
        protected override async ETTask Run(InstallComponent arg)
        {
            Computer computer = arg.Computer;
            computer.AddComponent<PCCaseComponet>();
            computer.AddComponent<MonitorComponent>();
            computer.AddComponent<keyboardComponent>();
            computer.AddComponent<MouseComponent>();
            await ETTask.CompletedTask;
        }
    }
}