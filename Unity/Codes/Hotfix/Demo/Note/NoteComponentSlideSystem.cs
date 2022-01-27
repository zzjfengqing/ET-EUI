namespace ET
{
    public class NoteSlideComponentSystem
    {
        
    }

    public class NoteSlideComponentUpdateSystem: UpdateSystem<NoteSlideComponent>
    {
        public override void Update(NoteSlideComponent self)
        {
            //Log.Debug($"一个slide正在移动[{self.InstanceId}]");
        }
    }
}