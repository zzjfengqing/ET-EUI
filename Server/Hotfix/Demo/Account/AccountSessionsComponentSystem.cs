using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class AccountSessionsComponentDestroySystem : DestroySystem<AccountSessionsComponent>
    {
        public override void Destroy(AccountSessionsComponent self)
        {
            self.AccountSessions.Clear();
        }
    }

    public static class AccountSessionsComponentSystem
    {
        public static long Get(this AccountSessionsComponent self, long accountId)
        {
            return self.AccountSessions.TryGetValue(accountId, out var instanceId)
                ? instanceId : 0;
        }

        public static void Add(this AccountSessionsComponent self, long accountId, long instanceId)
        {
            self.AccountSessions[accountId] = instanceId;
        }

        public static void Remove(this AccountSessionsComponent self, long accountId)
        {
            if (self.AccountSessions.ContainsKey(accountId))
            {
                self.AccountSessions.Remove(accountId);
            }
        }
    }
}