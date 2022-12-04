using System.Linq;

namespace ET
{
    [FriendClass(typeof(PlayerComponent))]
    public static class PlayerComponentSystem
    {
        public class AwakeSystem : AwakeSystem<PlayerComponent>
        {
            public override void Awake(PlayerComponent self)
            {
            }
        }

        [ObjectSystem]
        public class PlayerComponentDestroySystem : DestroySystem<PlayerComponent>
        {
            public override void Destroy(PlayerComponent self)
            {
            }
        }

        public static void Add(this PlayerComponent self, Player player)
        {
            self.idPlayers.Add(player.AccountId, player);
        }

        public static Player Get(this PlayerComponent self, long accountId)
        {
            self.idPlayers.TryGetValue(accountId, out Player gamer);
            return gamer;
        }

        public static void Remove(this PlayerComponent self, long accountId)
        {
            self.idPlayers.Remove(accountId);
        }

        public static Player[] GetAll(this PlayerComponent self)
        {
            return self.idPlayers.Values.ToArray();
        }
    }
}