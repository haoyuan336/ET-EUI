using UnityEngine;

namespace ET
{
    public class GameTaskAwakeSystem: AwakeSystem<GameTask>
    {
        public override void Awake(GameTask self)
        {
        }
    }


    public static class GameTaskSystem
    {
        
        public static GameTaskInfo GetInfo(this GameTask self)
        {
            return new GameTaskInfo() { TaskId = self.Id, TaskState = self.TaskState, ConfigId = self.ConfigId, CreateTime = self.CreateTime };
        }
    }
}