namespace ET
{
    public enum AccountType
    {
        General = 0,
        BlackList = 1
    }

    public class Account : Entity, IAwake
    {
        public string AccountName;
        public string PassWord;
        public long CreateTime;
        public int AccountType;
    }
}