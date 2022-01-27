namespace ET
{

    public class LoginIfoRecordComponentDestroySystem: DestroySystem<LoginIfoRecordComponent>
    {
        public override void Destroy(LoginIfoRecordComponent self)
        {
           self.AccountLoginInfoDict.Clear();
        }
    }
    public static class LoginIfoRecordComponentSystem
    {
        public static void Add(this LoginIfoRecordComponent self, long key, int value)
        {
            if (self.AccountLoginInfoDict.ContainsKey(key))
            {
                self.AccountLoginInfoDict[key] = value;
                return;
            }
            self.AccountLoginInfoDict.Add(key, value);
        }

        public static void Remove(this LoginIfoRecordComponent self, long key)
        {
            if (self.AccountLoginInfoDict.ContainsKey(key))
            {
                self.AccountLoginInfoDict.Remove(key);
            }
        }

        public static int Get(this LoginIfoRecordComponent self, long key)
        {
            if (self.AccountLoginInfoDict.ContainsKey(key))
            {
                return self.AccountLoginInfoDict[key];
            }
            return -1;
        }

        public static bool IsExist(this LoginIfoRecordComponent self,long key)
        {
            return self.AccountLoginInfoDict.ContainsKey(key);
        }
    }
}