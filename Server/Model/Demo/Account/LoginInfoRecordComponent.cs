using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class LoginInfoRecordComponent : Entity, IAwake, IDestroy
    {
        /// <summary>
        /// <para>key:  帐号Id</para>
        /// <para>value:    (zone)区Id</para>
        /// </summary>
        public Dictionary<long, int> AccountLoginInfos { get; set; } = new Dictionary<long, int>();
    }
}