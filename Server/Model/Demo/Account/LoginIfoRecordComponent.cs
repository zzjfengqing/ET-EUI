using System.Collections.Generic;

namespace ET
{
    public class LoginIfoRecordComponent:Entity,IAwake,IDestroy
    {
        public Dictionary<long, int> AccountLoginInfoDict = new Dictionary<long, int>();
    }
}