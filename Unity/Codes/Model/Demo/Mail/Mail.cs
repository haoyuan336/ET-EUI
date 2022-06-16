namespace ET
{
    public class Mail: Entity, IAwake
    {
        public long ReceiveId; //发送者id
        public long SendId; //收者id
        public string SendTime; //发送时间
        public bool IsRead = false; //是否已读
        public bool IsGet = false; //是否已经领取
        public string SendName; //发送者名字
        public int State = (int) StateType.Active; //邮件状态
        public string MailTitle = "邮件标题"; //邮件标题
        public string MailContent = "邮件内容"; //邮件内容
    }
}