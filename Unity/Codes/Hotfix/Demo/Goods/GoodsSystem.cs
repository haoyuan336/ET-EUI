namespace ET
{
    public class GoodsAwakeSystem: AwakeSystem<Goods>
    {
        public override void Awake(Goods self)
        {
            
        }
    }

    public static class GoodsSystem
    {
        public static GoodsInfo GetGoodsInfo(this Goods self)
        {
            return new GoodsInfo() { GoodsId = self.Id, ConfigId = self.GoodsConfigId };
        }
    }
}