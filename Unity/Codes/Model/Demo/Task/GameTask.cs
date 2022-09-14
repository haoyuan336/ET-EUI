using UnityEngine;

namespace ET
{
    public class GameTask: Entity, IAwake
    {
        public long CreateTime = TimeHelper.ServerNow(); //创建时间
        public int ConfigId; //配置id
        public long OwnerId; //拥有者Id
        public int State = (int) StateType.Active; //销毁状态
        public int TaskState = (int) TaskStateType.UnComplete; //任务状态
    }
}