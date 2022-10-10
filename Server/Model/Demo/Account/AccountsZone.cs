namespace ET
{
    [ComponentOf(typeof(Session))]
    [ChildType(typeof(Account))]
    public class AccountsZone : Entity,IAwake
    {
        
    }
}