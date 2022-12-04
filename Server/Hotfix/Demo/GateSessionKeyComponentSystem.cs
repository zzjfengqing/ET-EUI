namespace ET
{
    [FriendClass(typeof(GateSessionKeyComponent))]
    public static class GateSessionKeyComponentSystem
    {
        public static void Add(this GateSessionKeyComponent self, long accountId, string key, bool overwrite = true)
        {
            if (overwrite)
            {
                self.sessionKey[accountId] = key;
                self.TimeoutRemoveKey(accountId).Coroutine();
            }
            else if (self.sessionKey.ContainsKey(accountId))
            {
                self.sessionKey.Add(accountId, key);
                self.TimeoutRemoveKey(accountId).Coroutine();
            }
        }

        public static string Get(this GateSessionKeyComponent self, long accountId)
        {
            self.sessionKey.TryGetValue(accountId, out string key);
            return key;
        }

        public static void Remove(this GateSessionKeyComponent self, long key)
        {
            self.sessionKey.Remove(key);
        }

        private static async ETTask TimeoutRemoveKey(this GateSessionKeyComponent self, long key)
        {
            await TimerComponent.Instance.WaitAsync(20_000);
            self.sessionKey.Remove(key);
        }
    }
}