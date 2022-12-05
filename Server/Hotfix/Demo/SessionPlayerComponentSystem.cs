namespace ET
{
    [FriendClass(typeof(SessionPlayerComponent))]
    public static class SessionPlayerComponentSystem
    {
        public class SessionPlayerComponentDestroySystem : DestroySystem<SessionPlayerComponent>
        {
            public override async void Destroy(SessionPlayerComponent self)
            {
                await DisconnectHelper.KickPlayer(self.GetMyPlayer());
                //// 发送断线消息
                //ActorLocationSenderComponent.Instance.Send(self.PlayerId, new G2M_SessionDisconnect());
                //self.Domain.GetComponent<PlayerComponent>()?.Remove(self.AccountId);
            }
        }

        public static Player GetMyPlayer(this SessionPlayerComponent self)
        {
            return self.Domain.GetComponent<PlayerComponent>().Get(self.AccountId);
        }
    }
}