namespace ET
{
    public enum StateType
    {
        Destroy = 0, //销毁状态
        Active = 1, //激活状态
        Lock = 2, //锁定状态
    }

    public enum TaskStateType
    {
        UnComplete = 1, //未完成状态
        Completed = 2, //完成状态
        Awarded = 3, //完成并领取了奖励的状态
    }
}