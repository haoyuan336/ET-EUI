namespace ET
{
    public enum SceneType
    {
        Process = 0,
        Manager = 1,
        Realm = 2,
        Gate = 3,
        Http = 4,
        Location = 5,
        Map = 6,
        Account = 7,
        LoginCenter = 9, //中心服服务器
        MainScene = 10, //主页面cene
        PVEGameScene = 11,
        MailScene = 12, //邮箱服务器
        
        // 客户端Model层
        Client = 30,
        Zone = 31,
        Login = 32,
        Robot = 33,
        Current = 34,
    }
}