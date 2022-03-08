namespace ET
{
    public class DiamondComponentAwakeSystem: AwakeSystem<DiamondComponent>
    {
        public override void Awake(DiamondComponent self)
        {
        }
    }

    public static class DiamondComponetSystem
    {
        public static Diamond CreateOneDiamond(this DiamondComponent self, int LieIndex, int HangIndex)
        {
            long id = IdGenerater.Instance.GenerateId();
            Diamond diamond = self.AddChildWithId<Diamond>(id);
            diamond.SetIndex(LieIndex, HangIndex);
            return diamond;
        }

        public static Diamond CreateDiamoneWithMessage(this DiamondComponent self, DiamondInfo diamondInfo)
        {
            Diamond diamond = self.AddChildWithId<Diamond>(diamondInfo.Id);
            diamond.SetIndex(diamondInfo.LieIndex, diamond.HangIndex);
            return diamond;
        }
    }
}