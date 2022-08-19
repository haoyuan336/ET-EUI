using System;

namespace ET
{
    public static class ConstValue
    {
        public const int HangCount = 6;
        public const int LieCount = 7;
        public const int DomineeringBaseValue = 300; //霸气值基数

        public const int FriendsCountMax = 30; //好友个数上限
        public const int PreDayFreePowerGiftCount = 10; //每天免费的体力礼物数量
        public const float DiamondOffsetZ = 1;
        public const float FlyEffectFlySpeed = 2; //飞行特效的飞行速度
        public const int PowerCountMax = 5; //体力的最大值
        public const int PowerCountMaxExtar = 20; //体力的爆体最大值
        public const int RoomPlayerCount = 2; //房间里面的玩家个数
        public const int PVPLevelConfigId = 100000; //对战关卡地图

        public const int ExpItemConfigId = 1008; //经验值道具的configid；
        public const int AutoSaveTime = 360000; //自动保存事件
        public const int MaxHeroStarCount = 5; //最大的英雄星的个数
        public const float Wan = 10000.0f; //一万 配置表数值需要除次值

#if !SERVER
        // public const string LoginAddress = "59.110.220.207:10007";
        public const string LoginAddress = "192.168.100.8:10007";

        // public const string LoginAddress = "127.0.0.1:10007";
        public const float Distance = 0.6f;
        public const string WeaponAtlasPath = "Assets/Res/WeaponTextures/WeaponSpriteAtlas.spriteatlas";
        public const string HeroCardAtlasPath = "Assets/Res/HeroCards/HeroCardSpriteAtlas.spriteatlas";
        public const string CommonUIAtlasPath = "Assets/Res/UI/CommonUIAtlas.spriteatlas"; //通用UI合图
        public const string FrameBgPath = "hero_frame003";

        public const int CrashWaitTime = 0;
        public const int CrashItemWaitTime = 200;
        public const string ButtonClickAudioStr = "Assets/Res/Audios/返回键.wav";
        public const string PVPPVEButtonClickAudioStr = "Assets/Res/Audios/点击UIPVPPVE.mp3";
        public const string MakeSureFightAudioStr = "Assets/Res/Audios/按键音-确认战斗.mp3";
        public const string CallHeroAudioStr = "Assets/Res/Audios/召唤.mp3"; //召唤音频
        public const string PVEBgMusicStr = "Assets/Res/Audios/12_dungeon_pvp_bgm_loop.mp3"; //pve背景音乐
        public const string PVPBgMusicStr = "Assets/Res/Audios/竞技场.wav"; //竞技场背景音乐
        public const string PVPFightLayerMusicStr = ""; //竞技场主页面背景音乐
        public const string BackButtonAudioStr = "Assets/Res/Audios/返回键.wav"; //返回键
        public const string UpdateLevelAudioStr = "Assets/Res/Audios/升级.mp3"; //升级音效

        public const string DiamondPoolName = "Diamond"; //宝石对象池名称

#endif
    }
}