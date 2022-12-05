using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class G2L_RemoveLoginRecordHandler : AMActorRpcHandler<Scene, G2L_RemoveLoginRecord, L2G_RemoveLoginRecord>
    {
        protected override async ETTask Run(Scene scene, G2L_RemoveLoginRecord request, L2G_RemoveLoginRecord response, Action reply)
        {
            long accountId = request.AccountId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginCenterLock, accountId))
            {
                var loginInfoRecordComponent = scene.GetComponent<LoginInfoRecordComponent>();
                int zone = loginInfoRecordComponent.Get(accountId);
                if (request.ServerId == zone)
                {
                    loginInfoRecordComponent.Remove(accountId);
                }
            }
            reply();
        }
    }
}