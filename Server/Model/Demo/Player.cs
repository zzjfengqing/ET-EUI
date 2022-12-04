namespace ET
{
    public enum PlayerStatus
    {
        Disconnect,
        Gate,
        Game,
    }

    public sealed class Player : Entity, IAwake<long, long>
    {
        public long AccountId { get; set; }
        public long SessionInstanceId { get; set; }
        public long UnitId { get; set; }
        public PlayerStatus Status { get; set; }
    }
}