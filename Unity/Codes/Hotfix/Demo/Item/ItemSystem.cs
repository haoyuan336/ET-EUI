namespace ET
{
    public static class ItemSystem
    {
        public static ItemInfo GetInfo(this Item self)
        {
            return new ItemInfo() { ItemId = self.Id, ConfigId = self.ConfigId, Count = self.Count };
        }
    }
}