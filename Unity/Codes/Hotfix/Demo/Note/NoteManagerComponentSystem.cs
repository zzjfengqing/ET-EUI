using System.Collections.Generic;

namespace ET
{
    public static class NoteManagerComponentSystem
    {
        public static void Init(this NoteManagerComponent self,float ratio,float speed)
        {
            self.ratio = ratio;
            self.speed = speed;
            self.time = 0;
            self.Pos = 0;
        }
        public static void CreateNote(this NoteManagerComponent self,NoteComponent note)
        {
            if (!self.notes.Contains(note))
            {
                self.notes.Add(note);
            }
        }
        
    }

    public class NoteManagerComponentAwakeSystem: AwakeSystem<NoteManagerComponent>
    {
        public override void Awake(NoteManagerComponent self)
        {
            self.notes = new List<NoteComponent>();
        }
    }

    public class NoteManagerComponentUpdateSystem: UpdateSystem<NoteManagerComponent>
    {
        public override void Update(NoteManagerComponent self)
        {
            if (self.gameStart)
            {
                self.time += TimeInfo.Instance.FrameTime * self.speed;
                self.Pos = self.time * self.ratio;
            }
        }
    }
}