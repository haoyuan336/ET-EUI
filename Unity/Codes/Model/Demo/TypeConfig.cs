namespace ET
{
    public enum MailType
    {
        Mail = 1, //普通邮件
        AddFriendRequest = 2, //添加好友申请邮件
        Notice = 3, //通知
    }

    public enum ApplyProcessType
    {
        Accept = 1, //接受
        Refuse = 2, //拒绝
    }

    public enum ItemType
    {
        Normal = 1, //普通道具
        PreDayPowerGift = 2, //每日礼物道具
    }

    public enum HeadImageType
    {
        Head = 1, //头像
        HeadFrame = 2, //头像框
    }

    public enum TaskType //任务类型
    {
        DayTask = 1, //每日任务
        WeekTask = 2, //周任务
        GrowUpTask = 3, //成长任务
    }
}