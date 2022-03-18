using System.Text;
using UnityEngine;

namespace ET
{
    public class DiamondAwakeSystem: AwakeSystem<Diamond>
    {
        public override void Awake(Diamond self)
        {
#if !SERVER
            Game.EventSystem.Publish(new EventType.CreateOneDiamondView() { Diamond = self });

#endif
        }
    }

    public static class DiamondSystem
    {
        public static void SetIndex(this Diamond self, int lieIndex, int hangIndex)
        {
            self.LieIndex = lieIndex;
            self.HangIndex = hangIndex;
        }

        public static DiamondInfo GetMessageInfo(this Diamond self)
        {
            return new DiamondInfo()
            {
                Id = self.Id,
                HangIndex = self.HangIndex,
                LieIndex = self.LieIndex,
                DiamondType = self.DiamondType,
                InitLieIndex = self.InitLieIndex,
                InitHangIndex = self.InitHangIndex,
                BoomType = self.BoomType
            };
        }

        public static void InitWithMessageInfo(this Diamond self, DiamondInfo diamondInfo)
        {
            self.Id = diamondInfo.Id;
            self.LieIndex = diamondInfo.LieIndex;
            self.HangIndex = diamondInfo.HangIndex;
            self.DiamondType = diamondInfo.DiamondType;
            self.InitHangIndex = diamondInfo.InitHangIndex;
            self.InitLieIndex = diamondInfo.InitLieIndex;
            self.BoomType = diamondInfo.BoomType;
            Log.Debug("Diamond Type = " + self.BoomType);
#if !SERVER
            Game.EventSystem.Publish(new EventType.UpdateDiamondData() { Diamond = self });

#endif
        }

        public static void UpdateIndex(this Diamond self, int LieIndex, int HangIndex)
        {
            self.LieIndex = LieIndex;
            self.HangIndex = HangIndex;
#if !SERVER
            Game.EventSystem.Publish(new EventType.UpdateDiamondIndex() { Diamond = self, LieIndex = LieIndex, HangIndex = HangIndex });

#endif
        }
    }
}