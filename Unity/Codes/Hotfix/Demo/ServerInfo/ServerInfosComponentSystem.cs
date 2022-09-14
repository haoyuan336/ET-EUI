namespace ET
{
    public class ServerInfosComponentDestroySystem: DestroySystem<ServerInfosComponent>
    {
        public override void Destroy(ServerInfosComponent self)
        {
            foreach (var serverInfo in self.ServerInfos)
            {
                serverInfo?.Dispose();
            }

            self.ServerInfos.Clear();
        }
    }

    public static class ServerInfosComponentSystem
    {
        public static void Add(this ServerInfosComponent self, ServerInfo serverInfo)
        {
            self.ServerInfos.Add(serverInfo);
        }

        public static void Clear(this ServerInfosComponent self)
        {
            self.ServerInfos.Clear();
        }

        public static void SetCurrentServerId(this ServerInfosComponent self, long value)
        {
            Log.Debug($"ser current server id {value}");
            self.CurrentServerId = value;
        }

        public static long GetCurrentServerId(this ServerInfosComponent self)
        {
            return self.CurrentServerId;
        }
    }
}