namespace ET
{
    [FriendClass(typeof(GateSessionKeyComponent))]
    public static class GateSessionKeyComponentSystem
    {
        public static void Add(this GateSessionKeyComponent self, long key, string account, bool overwrite = true)
        {
            if (overwrite)
            {
                self.sessionKey[key] = account;
                self.TimeoutRemoveKey(key).Coroutine();
            }
            else if (self.sessionKey.ContainsKey(key))
            {
                self.sessionKey.Add(key, account);
                self.TimeoutRemoveKey(key).Coroutine();
            }
        }

        public static string Get(this GateSessionKeyComponent self, long key)
        {
            string account = null;
            self.sessionKey.TryGetValue(key, out account);
            return account;
        }

        public static void Remove(this GateSessionKeyComponent self, long key)
        {
            self.sessionKey.Remove(key);
        }

        private static async ETTask TimeoutRemoveKey(this GateSessionKeyComponent self, long key)
        {
            await TimerComponent.Instance.WaitAsync(20000);
            self.sessionKey.Remove(key);
        }
    }
}