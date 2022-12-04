using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class G2M_RequestEnterGameStatusHandler : AMActorLocationRpcHandler<Unit, G2M_RequestEnterGameStatus, M2G_RequestEnterGameStatus>
    {
        protected override async ETTask Run(Unit unit, G2M_RequestEnterGameStatus request, M2G_RequestEnterGameStatus response, Action reply)
        {
            reply();
            await ETTask.CompletedTask;
        }
    }
}