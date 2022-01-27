namespace ET
{
    public class NoteLineComponentSystem
    {
        
    }
    public class NoteLineComponentUpdateSystem: UpdateSystem<NoteLineComponent>
    {
        public override void Update(NoteLineComponent self)
        {
            //Log.Debug($"一个line正在移动[{self.InstanceId}]");
        }
    }
}