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
        Invalide = -1,
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
        SingleSelf = 6, //单体自己
        HealthLeast = 7, //血量最少
        // #攻击类型,#范围类型 RangeType， 1，敌方单体，2地方群体3，我方个体4我方群体5，全体，6 单体自己
    }

    public enum ActionMessagePlayType
    {
        Async = 1, //异步
        Sync = 2, //同步
    }

    public enum ActionMessageType
    {
        CrashDiamond = 1, //消除宝石
        MoveDiamond = 2, //下降宝石
        CreateDiamond = 3, //创建宝石
        HeroAttack = 4, //英雄攻击
        UpdateBuffInfo = 5, //更新Buff
        GameProcess = 6, //游戏结算
    }

    public enum BuffIsCanCover
    {
        Can = 1, //buff 是否可以覆盖
        Not = 2 //buff 不可以覆盖
    }

    public enum IsCanAttackType
    {
        Can = 1, //可以攻击 
        Not = 0, //不可以攻击
    }

    public enum FrozenType
    {
        Frozen = 1, //冰冻
        NotFrozen = 0, //非冰冻
    }

    public enum ToDeathType
    {
        Death = 1,
        Not = 0,
    }

    public enum ProvocationType //挑衅
    {
        Provocation = 1,
        Not = 0,
    }

    public enum AvoidDeathType //免死
    {
        AvoidDeath = 1,
        Not = 0,
    }

    public enum ImmuneType //免疫
    {
        Immune = 1,
        Not = 0
    }

    public enum BuffAddType //buff增益类型
    {
        Add = 1, //增益类型
        Sub = 2, //减益类型
    }

    public enum PurifyType //净化类型
    {
        Purify = 1, //净化
        Not = 2 //不净化
    }

    public enum InvincibleType
    {
        Invincible = 1, // 无敌
        Not = 2, //不无敌
    }

    public enum RecoveryType
    {
        Recovery = 1, //复苏
        Not = 2, //不可以复苏
    }

    public enum InvisibleType //是否为隐身状态
    {
        Invisible = 1, //隐身
        Not = 0, //非隐身
    }

    public enum DazzlingType
    {
        Dazzling = 1, //炫目
        Not = 0 //不炫目
    }

    public enum DirectkillType
    {
        Kill = 1, //直接看啥
        Not = 0, // 不砍杀
    }

    public enum AdditionalDamageType
    {
        Damage = 1, //是否造成额外伤害
        Not = 0, //无额外伤害
    }

    public enum HeroBagType
    {
        Invalide = -1,
        Hero = 1,
        Materail = 2,
        HeroAndMaterial = 3,
    }

    public enum HeroElementType
    {
        Invalide = -1,

        // ElementName
        Fire = 1,
        Dark = 2,
        Water = 3,
        Wind = 4,
        Light = 5
    }
}