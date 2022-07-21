namespace ET
{
    public static partial class ErrorCode
    {
        public const int ERR_Success = 0;

        // 1-11004 是SocketError请看SocketError定义
        //-----------------------------------
        // 100000-109999是Core层的错误

        // 110000以下的错误请看ErrorCore.cs

        // 这里配置逻辑层的错误码
        // 110000 - 200000是抛异常的错误
        public const int ERR_AccountOrPasswordInputIsNull = 200001;

        public const int ERR_NetworkError = 200002;

        public const int ERR_LoginInfoIsNull = 200003;

        public const int ERR_PasswordFormateErr = 200004; //密码个数错误
        public const int ERR_AccountInBlackListError = 200005; //用户再黑名单里面
        public const int ERR_AccountPasswordError = 200006; //密码错误
        public const int ERR_RequestRepeatedly = 200007; //重复请求
        public const int ERR_TokenError = 200008; //token错误
        public const int ERR_NotMatching = 200009; //没有再匹配游戏

        public const int ERR_TroopCountIsFull = 200010; //队伍数已满
        public const int ERR_NotFoundHero = 200011; //未找到此英雄

        public const int ERR_NotFoundPlayer = 200012;
        public const int ERR_NotFoundRoom = 200013;
        public const int ERR_GoldNotEnough = 200014; //金币不足
        public const int ERR_DiamondNotEnough = 200015; //钻石不足
        public const int ERR_MaterialNotEnough = 200016; //材料不足
        public const int ERR_TroopIsFull = 200017; //队伍已经满员了
        public const int ERR_AlOwnWeapon = 200018; //已经拥有此装备。禁止重复购买
        public const int ERR_MAX_Rank = 200019; //已经达到英雄的最大阶数
        public const int ERR_MAX_Star = 200020; //已经达到英雄的最大星数

        public const int ERR_EXP_Not_Enough = 200021; //经验值不够

        public const int ERR_Not_Found_Weapon = 200022; //未找到武器
        public const int ERR_Not_Found_Mail = 200023; //我找到此邮件
        public const int ERR_Award_AlReceive = 200024; //奖励已经领取了
        public const int ERR_Not_Friend = 200025; //不是好友
        public const int ERR_Gift_Not_Enough = 200026; //礼物不够了
        public const int ERR_Gift_Gived = 200027; //礼物已经赠送了
        public const int ERR_No_Have_To_Gift_Friend = 200031; //没有可以赠送礼物的好友了

        public const int ERR_Not_Fount_GameTask = 200028; //未找到任务
        public const int ERR_GameTask_Award_AlGet = 200029; //任务奖励已经领取
        public const int ERR_GameTask_UnComplete = 200030; //任务还未完成

        // 200001以上不抛异常
    }
}