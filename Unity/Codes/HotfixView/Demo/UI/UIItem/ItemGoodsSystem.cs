namespace ET
{
    public static class ItemGoodsSystem
    {
        public static void InitGoodsInfo(this Scroll_ItemGoods self, GoodsConfig config)
        {
            ItemConfig itemConfig = ItemConfigCategory.Instance.Get(config.ConfigId);
            self.E_DesText.text = itemConfig.Des;
        }
        
    }
}