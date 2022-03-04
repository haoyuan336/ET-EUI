namespace ET
{
    public static class ServerInfoSystem
    {
        public static void FromMessage(this ServerInfo self, ServerInfoProto serverInfoProto)
        {
            self.ServerName = serverInfoProto.ServerName;
            self.State = serverInfoProto.Status;
            self.Id = serverInfoProto.Id;
        }

        public static ServerInfoProto ToMessage(this ServerInfo self)
        {
            return new ServerInfoProto() { ServerName = self.ServerName, Id = self.Id, Status = self.State };
        }
    }
}