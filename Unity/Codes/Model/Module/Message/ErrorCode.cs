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
        // 200001以上不抛异常

        public const int ERR_NetWorkError = 200002;

        /// <summary>
        /// 帐号信息为空
        /// </summary>
        public const int ERR_AccountInfoIsNull = 200003;

        /// <summary>
        /// 账户名格式错误
        /// </summary>
        public const int ERR_AccountNameFormError = 200004;

        /// <summary>
        /// 账号密码错误
        /// </summary>
        public const int ERR_PasswordFormError = 200005;

        /// <summary>
        /// 账户名或密码错误
        /// </summary>
        public const int ERR_AccountInfoError = 200006;

        /// <summary>
        /// 帐号状态异常
        /// </summary>
        public const int ERR_AccountStatusAbnormal = 200007;

        /// <summary>
        /// 重复请求
        /// </summary>
        public const int ERR_RequestRepeatedly = 200008;

        /// <summary>
        /// 令牌错误
        /// </summary>
        public const int ERR_TokenError = 200009;

        /// <summary>
        /// 角色名为空
        /// </summary>
        public const int ERR_RoleNameIsNull = 200010;

        public const int ERR_RoleNameSame = 200011;
    }
}