namespace ET
{
    public enum ServerState
    {
        Normal = 0,
        Stop = 1
    }
    public class ServerInfo: Entity, IAwake
    {
        public int State;
        public string ServerName;
    }
}