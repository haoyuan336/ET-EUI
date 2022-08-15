using System;

namespace ET
{
    public enum AccountType
    {
        General = 0,
        BlackList = 1
    }

    public class Account: Entity, IAwake
    {
        public string NickName; //昵称
        public string AccountName;
        public string Password;
        public long CreateTime;
        public int AccountType;
        public int PVELevelNumber = 1; //pve模式下，玩家玩到第几关了

        public long CurrentTroopId; //当前选择的队伍id
        public int CurrentTroopIndex = 0; //当前队伍的index

        // public int GoldCount; //金币个数
        // public int PowerCount; //体力个数
        // public int DiamondCount; //钻石个数
        public int Level; //等级
        public int Exp; //经验值
        public int State = (int)StateType.Active;
        public bool IsRegisterMailBox = false; //是否注册了邮箱

        public long LastLogonTime = 0;

        // public int HeadImageConfigId = 1;
        // public int HeadFrameImageConfigId = 7;
        public long HeadImageItemId; //头像道具的id
    }
}