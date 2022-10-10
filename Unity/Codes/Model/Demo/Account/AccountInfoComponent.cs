namespace ET
{
    /// <summary>
    /// 保存登陆成功后的 账户信息
    /// </summary>
    [ComponentOf(typeof(Scene))]
    public class AccountInfoComponent : Entity,IAwake,IDestroy
    {
        public string Token;
        public long AccountId;
        public string RealmKey;
        public string RealmAddress;
    }
}