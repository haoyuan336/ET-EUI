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

    public enum CrashType //消除类型
    {
        Normal = 1, //普通了类型
        Special = 2, //特殊珠类型
    }

    public enum GoldInfoUIType
    {
        MainScene = 1, //主页面
        HeroInfo = 2, //英雄详情页面
        WeaponInfo = 3, //武器详情页面
    }

    public enum MoveActionType
    {
        Normal = 1, //普通移动
        Jump = 2, //跳动
        CircleToPoint = 3, //转圈到某一点
    }

    public enum ItemMinType
    {
        Normal = 1, //普通
        HeadImage = 2, //头像
    }

    public enum SkillRangeType //技能的攻击范围类型
    {
        EnemySingle = 1, //敌方单体
        EnemyGroup = 2, //敌方群体
        FriendSingle = 3, //友方个体
        FriendGroup = 4, //友方群体
        All = 5, //全体

        // #攻击类型,#范围类型 RangeType， 1，敌方单体，2地方群体3，我方个体4我方群体5，全体
    }

}