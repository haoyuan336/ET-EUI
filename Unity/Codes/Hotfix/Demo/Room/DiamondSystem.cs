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
            return new DiamondInfo() { Id = self.Id, HangIndex = self.HangIndex, LieIndex = self.LieIndex, DiamondType = self.DiamondType };
        }

        public static void InitWithMessageInfo(this Diamond self, DiamondInfo diamondInfo)
        {
            self.Id = diamondInfo.Id;
            self.LieIndex = diamondInfo.LieIndex;
            self.HangIndex = diamondInfo.HangIndex;
            self.DiamondType = diamondInfo.DiamondType;
#if !SERVER
            Game.EventSystem.Publish(new EventType.UpdateDiamondData() { Diamond = self });
            
#endif
        }
    }
}