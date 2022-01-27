using System.Collections.Generic;

namespace ET
{
    public class NoteManagerComponent : Entity,IAwake,IUpdate
    {
        public List<NoteComponent> notes;
        public bool gameStart;
        public float ratio;
        public float speed;
        public float time;
        public float Pos;

    }
}