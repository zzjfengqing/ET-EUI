namespace ET
{
    public class NoteComponent:Entity,IAwake,IUpdate
    {
        public NoteManagerComponent manager;
        /// <summary>
        /// 在哪竖
        /// </summary>
        public float Index;
        /// <summary>
        /// 轴上位置
        /// </summary>
        public float Pos;
        /// <summary>
        /// 结束位置--长度
        /// </summary>
        public float Lenth;
    }
}