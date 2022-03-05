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

        // 200001以上不抛异常
    }
}