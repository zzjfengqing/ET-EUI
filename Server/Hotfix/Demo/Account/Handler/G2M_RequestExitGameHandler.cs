using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class G2M_RequestExitGameHandler : AMActorLocationRpcHandler<Unit, G2M_RequestExitGame, M2G_RequestExitGame>
    {
        protected override async ETTask Run(Unit unit, G2M_RequestExitGame request, M2G_RequestExitGame response, Action reply)
        {
            //TODO: 保存玩家数据到数据库,执行相关下线操作
            reply();
            //正是释放Unit
            await unit.RemoveLocation();
            UnitComponent unitComponent = unit.DomainScene().GetComponent<UnitComponent>();
            unitComponent.Remove(unit.Id);
        }
    }
}