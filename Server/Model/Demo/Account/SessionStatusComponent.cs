using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public enum SessionStatus
    {
        Normal,
        Game
    }

    [ComponentOf(typeof(Session))]
    public class SessionStatusComponent : Entity, IAwake
    {
        public SessionStatus Status;
    }
}