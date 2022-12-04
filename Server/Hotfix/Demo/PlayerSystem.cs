namespace ET
{
    [FriendClass(typeof(Player))]
    public static class PlayerSystem
    {
        [ObjectSystem]
        public class PlayerAwakeSystem : AwakeSystem<Player, long, long>
        {
            public override void Awake(Player self, long accountId, long unitId)
            {
                self.AccountId = accountId;
                self.UnitId = unitId;
            }
        }
    }
}