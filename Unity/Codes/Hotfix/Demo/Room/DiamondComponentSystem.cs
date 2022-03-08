using System.Linq;

namespace ET
{
    public static class DiamondComponentSystem
    {
        public static Diamond CreateOneDiamond(this DiamondComponent self)
        {
            long id = IdGenerater.Instance.GenerateId();
            Diamond diamond = self.AddChildWithId<Diamond>(id);

            int[] keys = DiamondTypeConfigCategory.Instance.GetAll().Keys.ToArray();
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