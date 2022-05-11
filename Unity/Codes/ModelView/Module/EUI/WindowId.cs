namespace ET
{
    public enum WindowID
    {
        WindowID_Invaild = 0,
        WindowID_MessageBox,
        WindowID_Lobby, //房间界面
        WindowID_Login, //登录界面
        WindowID_RedDot, //红点测试界面
        WindowID_Helper, //提示界面
        WindowID_MatchButton, //匹配游戏界面
        WindowID_ChooseServer, //选择服务器页面
        WindowID_RoomInfo, //房间的相信信息
        WindowID_GameUI, //游戏中UI
        WindowID_ProgressBar, //进度条UI
        WindowID_CallHeroLayer, //召唤英雄页面
        WindowID_MainScene, //游戏的主页面
        WindowID_BagLayer, //背部页面
        WindowID_EditorTroopLayer, //编辑队伍页面
        WindowID_GameWinUI, //游戏胜利UI
        WindowID_GoldInfoUI, //游戏金币体力钻石信息
        WindowID_AccountInfo, //用户信息 id 以及经验值
        WindowID_MessageTaskActiveInfo, //通知任务活动
        WindowID_SettingUI, //设置
        WindowID_FormationUI, //战斗相关按钮
        WindowID_HeroInfoLayerUI, //英雄信息层
        WindowID_ShowHeroInfoLayer, //显示英雄的详细信息
        WindowID_MainSceneBg,   //主背景的背景层
        WindowID_MainSceneMenu, //主页面按钮
        WindowID_LoadingLayer,  //加载游戏层
        WindowID_GameLoaseUI,   //游戏失败UI
        WindowID_Store,         //商店
        WindowID_PowerNotEnoughAlert,   //体力不足的提示
        WindowID_GameLevelLayer,    //当前游戏关卡显示层
        WindowID_ExitGameAlert,     //退出游戏提醒
    }
}