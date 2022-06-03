namespace ET
{
    public static class WordBarSystem
    {
        public static WordBarInfo GetInfo(this WordBar self)
        {
            return new WordBarInfo()
            {
                WordBarId = self.Id,
                OnwerId = self.OwnerId,
                ConfigId = self.ConfigId,
                Value = self.Value,
                IsMain = self.IsMain
            };
        }
    }
}