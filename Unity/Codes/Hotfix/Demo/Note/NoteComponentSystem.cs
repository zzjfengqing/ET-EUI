namespace ET
{
    public static class NoteComponentSystem
    {
        public static bool InRange(this NoteComponent self)
        {
            return self.Pos.InRange(self.manager.Pos - 40 - self.Lenth, self.manager.Pos + 40);
        }

        static bool InRange(this float value, float min, float max)
        {
            return value <= max && value >= min;
        }
    }

    public class NoteComponentAwakeSystem: AwakeSystem<NoteComponent>
    {
        public override void Awake(NoteComponent self)
        {
        }
    }

    public class NoteComponentUpdateSystem: UpdateSystem<NoteComponent>
    {
        public override void Update(NoteComponent self)
        {
            if (self.InRange())
            {
                // self.gameObject.SetActive(true);
            }
        }
    }
}