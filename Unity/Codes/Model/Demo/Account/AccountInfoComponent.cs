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
        public string Token { get; set; }
        public long AccountId { get; set; }
        public string RealmKey { get; set; }
        public string RealmAddress { get; set; }
    }
}