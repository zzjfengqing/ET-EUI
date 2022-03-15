namespace ET
{
    public class AccountCheckOutTimeComponent : Entity,IAwake<long>,IDestroy
    {
        public long Timer = 0;
        public long AccountId = 0;
    }
}