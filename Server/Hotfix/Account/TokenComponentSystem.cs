using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class TokenComponentSystem
    {
        public static void Add(this TokenComponent self, long key, string token)
        {
            self.Tokens[key] = token;
            self.AutoRemoveKey(key, token).Coroutine();
        }

        public static string Get(this TokenComponent self, long key)
        {
            return self.Tokens.TryGetValue(key, out string token) ? token : null;
        }

        public static void Remove(this TokenComponent self, long key)
        {
            if (self.Tokens.ContainsKey(key))
            {
                self.Tokens.Remove(key);
            }
        }

        private static async ETTask AutoRemoveKey(this TokenComponent self, long key, string token)
        {
            await TimerComponent.Instance.WaitAsync(10 * 60 * 1000);
            string onlineToken = self.Get(key);
            if (!string.IsNullOrEmpty(onlineToken) && onlineToken == token)
            {
                self.Remove(key);
            }
        }
    }
}