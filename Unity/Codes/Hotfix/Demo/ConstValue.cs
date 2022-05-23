namespace ET
{
    public static class ConstValue
    {

        public const int HangCount = 8;
        public const int LieCount = 8;
        
#if !SERVER
        public const string LoginAddress = "59.110.220.207:10007";
        // public const string LoginAddress = "127.0.0.1:10007";
        public const float Distance = 0.6f;
        public const string WeaponAtlasPath = "Assets/Res/WeaponTextures/WeaponSpriteAtlas.spriteatlas";
        public const string HeroCardAtlasPath = "Assets/Res/HeroCards/HeroCardSpriteAtlas.spriteatlas";
        public const string CommonUIAtlasPath = "Assets/Res/UI/CommonUIAtlas.spriteatlas"; //通用UI合图
#endif
    }
}