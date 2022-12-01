using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class AccountInfoComponent : Entity, IAwake, IDestroy
    {
        public string Token;
        public long AccountId;
    }
}