namespace ET
{
    public class MailAwakeSystem: AwakeSystem<Mail>
    {
        public override void Awake(Mail self)
        {
        }
    }

    public static class MailSystem
    {
        public static MailInfo GetInfo(this Mail self)
        {
            return new MailInfo()
            {
                MailId = self.Id,
                SendTime = self.SendTime,
                SendName = self.SendName,
                SendId = self.SendId,
                ReceiveId = self.ReceiveId,
                IsGet = self.IsGet,
                IsRead = self.IsRead,
                Title = self.MailTitle,
                Content = self.MailContent,
                MailType = (int)self.MailType
            };
        }
    }
}