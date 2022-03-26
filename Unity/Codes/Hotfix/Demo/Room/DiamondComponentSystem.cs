using System;
using System.Collections.Generic;
using System.Linq;

namespace ET
{
    public class DiamondComponentAwakeSystem: AwakeSystem<DiamondComponent>
    {
        public override void Awake(DiamondComponent self)
        {
            // Log.Debug("diamond component awake system");
        }
    }

    public static class DiamondComponentSystem
    {
        public static List<DiamondInfo> InitDiamonds(this DiamondComponent self, LevelConfig levelConfig)
        {
            self.LevelConfig = levelConfig;
            self.Diamonds = new Diamond[self.LevelConfig.LieCount, self.LevelConfig.HangCount];
            // diamondComponent.CreateOneDiamond();
            List<DiamondInfo> diamondInfos = new List<DiamondInfo>();
            int[,] map =
            {
                { 1, 2, 3, 4, 5, 6, 1, 1 }, { 2, 3, 4, 5, 6, 1, 2, 3 }, { 3, 2, 5, 4, 1, 2, 3, 2 }, { 4, 1, 6, 2, 2, 5, 2, 1 },
                { 5, 6, 1, 2, 1, 1, 5, 6 }, { 6, 5, 2, 1, 4, 2, 6, 5 }, { 1, 4, 3, 6, 5, 1, 1, 4 }, { 2, 3, 4, 5, 6, 1, 2, 3 }
            };
            for (var i = 0; i < self.LevelConfig.HangCount; i++)
            {
                for (var j = 0; j < self.LevelConfig.LieCount; j++)
                {
                    Diamond diamond = self.CreateOneDiamond();
                    diamond.SetIndex(j, i);
                    diamond.DiamondType = map[i, j];
                    self.Diamonds[j, i] = diamond;
                    diamondInfos.Add(diamond.GetMessageInfo());
                }
            }

            return diamondInfos;
           
        }

        public static Diamond CreateOneDiamond(this DiamondComponent self)
        {
            long id = IdGenerater.Instance.GenerateId();
            Diamond diamond = self.AddChildWithId<Diamond>(id);

            int[] keys = DiamondTypeConfigCategory.Instance.GetAll().Keys.ToArray();
            //todo test
            keys = new[] { 1, 2, 3 };
            var randomIndex = RandomHelper.RandomNumber(0, keys.Length);
            int configIndex = keys[randomIndex];
            diamond.DiamondType = configIndex;

            return diamond;
        }

        public static Diamond CreateDiamoneWithMessage(this DiamondComponent self, DiamondInfo diamondInfo)
        {
            long id = diamondInfo.Id;
            Diamond diamond = self.AddChildWithId<Diamond>(id);
            diamond.InitWithMessageInfo(diamondInfo);
            return diamond;
        }
        public static Diamond GetDiamond(this DiamondComponent self, int LieIndex, int HangIndex)
        {
            // Diamond diamond = null;
            if (LieIndex < 0 || HangIndex < 0 || LieIndex >= self.LevelConfig.LieCount || HangIndex >= self.LevelConfig.HangCount)
            {
                return null;
            }

            return self.Diamonds[LieIndex, HangIndex];
        }
        public static void ScrollDiamond(this DiamondComponent self,C2M_PlayerScrollScreen message)
        {
            //todo 滑动钻石
            Log.Debug("screen diamond");
              int LieIndex = message.StartX;
            int HangIndex = message.StartY;
            Log.Debug("process scroll screen message");
            self.DomainScene().GetComponent<DiamondComponent>().ScrollDiamond(message);

            Diamond diamond = self.GetDiamond(LieIndex, HangIndex);
            Diamond nextDiamond = self.GetDiamondWithDir(diamond, message.DirType);
            if (diamond != null && nextDiamond != null)
            {
                M2C_SyncDiamondAction m2CSyncDiamondAction = new M2C_SyncDiamondAction();
                m2CSyncDiamondAction.DiamondActionItems.Add(self.SwapDiamondPos(diamond, nextDiamond));
                bool isCrash = true;
                bool isCrashSuccess = false;
                bool isMoveDown = false;
                bool isFirstAddSpecial = true;
                // bool isSpecial = false;
                Queue<Diamond> specialDiamonds = new Queue<Diamond>();
                while (isCrash || isMoveDown || specialDiamonds.Count > 0)
                {
                    isCrash = self.CheckCrash(m2CSyncDiamondAction.DiamondActionItems, diamond, nextDiamond, specialDiamonds, isFirstAddSpecial);
                    if (isFirstAddSpecial)
                    {
                        isFirstAddSpecial = false;
                    }
            
                    isMoveDown = self.MoveDownAllDiamond(m2CSyncDiamondAction.DiamondActionItems);
                    if (isCrashSuccess == false && isCrash)
                    {
                        isCrashSuccess = true;
                    }
            
                    if (specialDiamonds.Count > 0)
                    {
                        Diamond specialDiamond = specialDiamonds.Dequeue();
                        self.AutoCastSpecialDiamond(m2CSyncDiamondAction.DiamondActionItems, specialDiamond);
                    }
                }
            
                if (isCrashSuccess)
                {
                    self.ToggleTurnSeatIndex();
                    self.syncCurrentTurnIndex();
                }
                else
                {
                    //todo 交换失败。反向交换
                    m2CSyncDiamondAction.DiamondActionItems.Add(self.SwapDiamondPos(diamond, nextDiamond));
                }
            
                //todo 下发交换action 消息
                foreach (var unit in self.Units)
                {
                    MessageHelper.SendToClient(unit, m2CSyncDiamondAction);
                }
            }
            else
            {
                //todo 非法操作
            }
        }
    }
}