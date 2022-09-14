namespace ET
{
    public enum HeroMoveType
    {
        Move = 1, //移动的英雄
        NoMove = 2, //非移动英雄
        Flash = 3,  //闪现
    }

    public enum TargetPosMoveType
    {
        Face = 1, //移动到敌人跟前
        Middle = 2, //移动到中间位置
    }
}