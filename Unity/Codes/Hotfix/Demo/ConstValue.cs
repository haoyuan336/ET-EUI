namespace ET
{
    public static class ConstValue
    {
        public const int HangCount = 6;
        public const int LieCount = 7;
        public const int DomineeringBaseValue = 300; //霸气值基数

        public const int PreDayFreePowerGiftCount = 5;  //每天免费的体力礼物数量
        public const float DiamondOffsetZ = 1;
        public const float FlyEffectFlySpeed = 2;   //飞行特效的飞行速度
        
        
#if !SERVER
        public const string LoginAddress = "59.110.220.207:10007";
        // public const string LoginAddress = "127.0.0.1:10007";
        public const float Distance = 0.6f;
        public const string WeaponAtlasPath = "Assets/Res/WeaponTextures/WeaponSpriteAtlas.spriteatlas";
        public const string HeroCardAtlasPath = "Assets/Res/HeroCards/HeroCardSpriteAtlas.spriteatlas";
        public const string CommonUIAtlasPath = "Assets/Res/UI/CommonUIAtlas.spriteatlas"; //通用UI合图
        public const string FrameBgPath = "hero_frame003";

        public const int CrashWaitTime = 0;
        public const int CrashItemWaitTime = 200;
#endif
    }
}



