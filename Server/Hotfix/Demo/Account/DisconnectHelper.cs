using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class DisconnectHelper
    {
        public static async void Disconnect(this Session session)
        {
            if (session == null || session.IsDisposed)
            {
                return;
            }
            var instanceId = session.InstanceId;

            await TimerComponent.Instance.WaitAsync(1000);

            if (instanceId != session.InstanceId)
                return;
            session.Dispose();
        }

        internal static async ETTask KickPlayer(Player player, bool isException = false)
        {
            if (player is null || player.IsDisposed)
                return;
            long instanceId = player.InstanceId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate, player.AccountId))
            {
                if (player.IsDisposed || instanceId != player.InstanceId)
                    return;
                if (!isException)
                {
                    switch (player.Status)
                    {
                        case PlayerStatus.Disconnect:
                            break;

                        case PlayerStatus.Gate:
                            break;

                        case PlayerStatus.Game:
                            //TODO: 通知游戏逻辑服下线Unit角色逻辑,并将数据存入数据库
                            var mapResponse = await MessageHelper.CallLocationActor(player.UnitId,
                                new G2M_RequestExitGame()) as M2G_RequestExitGame;
                            //通知移除账号角色登录信息
                            long loginCenterConfigSceneId = StartSceneConfigCategory.Instance.LoginCenterConfig.InstanceId;
                            var loginCenterResponse = await MessageHelper.CallActor(loginCenterConfigSceneId,
                                new G2L_RemoveLoginRecord()
                                {
                                    AccountId = instanceId,
                                    ServerId = player.DomainZone()
                                }) as L2G_RemoveLoginRecord;
                            break;
                    }
                }
                player.Status = PlayerStatus.Disconnect;
                player.DomainScene().GetComponent<PlayerComponent>()?.Remove(player.AccountId);
                player.Dispose();
            }
            await TimerComponent.Instance.WaitAsync(300);
        }
    }
}