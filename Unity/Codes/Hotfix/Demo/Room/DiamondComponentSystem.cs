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
    }
}