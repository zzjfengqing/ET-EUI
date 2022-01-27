using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class TokenComponent : Entity, IAwake
    {
        public readonly Dictionary<long, string> TokenDic = new Dictionary<long, string>();
    }
}