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
    }
}