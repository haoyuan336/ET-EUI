using System.Collections.Generic;

namespace ET
{
    public class ServerInfosComponent: Entity, IAwake, IDestroy
    {
        public List<ServerInfo> ServerInfos = new List<ServerInfo>();
        public long CurrentServerId { get; set; }
    }
}