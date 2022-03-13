using ET.EventType;

namespace ET
{
    public class UnLockTouchLockHandler: AEvent<EventType.UnLockTouchLock>
    {
        protected override async ETTask Run(UnLockTouchLock a)
        {
          
            Log.Debug("unlock touch lock");
            Scene zoneScene = a.ZoneScene;
            zoneScene.GetComponent<OperaComponent>().touchLock = false;
            await ETTask.CompletedTask;
        }
    }
}