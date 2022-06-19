namespace ET
{
    public enum ServerStatus
    {
        Normal = 0,
        Stop = 1,
    }
    public class ServerInfo : Entity,IAwake
    {
        public int Status;
        public string ServerName;
    }
}