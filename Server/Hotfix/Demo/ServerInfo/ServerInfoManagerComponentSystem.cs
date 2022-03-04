using MongoDB.Driver;
using MongoDB.Driver.Core.Servers;

namespace ET
{
    public class ServerInfoManagerComponentAwakeSystem: AwakeSystem<ServerInfoManagerComponent>
    {
        public override void Awake(ServerInfoManagerComponent self)
        {
            self.Awake().Coroutine();
        }
    }

    public class ServerInfoManagerComponentDestroySystem: DestroySystem<ServerInfoManagerComponent>
    {
        public override void Destroy(ServerInfoManagerComponent self)
        {
            foreach (var serverInfo in self.ServerInfos)
            {
                serverInfo?.Dispose();
            }

            self.ServerInfos.Clear();
        }
    }

    public static class ServerInfoManagerComponentSystem
    {
        public static async ETTask Awake(this ServerInfoManagerComponent self)
        {
            var serverInfoList = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<ServerInfo>(d => true);
            if (serverInfoList != null && serverInfoList.Count <=0)
            {
                self.ServerInfos.Clear();
                var serverInfoConfigs = ServerInfoConfigCategory.Instance.GetAll();
                foreach (var serverInfoConfig in serverInfoConfigs.Values)
                {
                    ServerInfo newServerInfo = self.AddChildWithId<ServerInfo>(serverInfoConfig.Id);
                    newServerInfo.ServerName = serverInfoConfig.ServerName;
                    newServerInfo.State = (int)ServerState.Normal;
                    self.ServerInfos.Add(newServerInfo);
                    await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(newServerInfo);
                }
                
                
                Log.Error("server info count is zero");
                return;
            }
            self.ServerInfos.Clear();
            foreach (var serverInfo in serverInfoList)
            {
                self.AddChild(serverInfo);
                self.ServerInfos.Add(serverInfo);
            }
            await ETTask.CompletedTask;
        }
    }
}