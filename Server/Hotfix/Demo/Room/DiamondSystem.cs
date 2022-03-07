namespace ET.Room
{
    public static class DiamondSystem
    {
        public static void SetIndex(this Diamond self, int lieIndex, int hangIndex)
        {
            self.LieIndex = lieIndex;
            self.HangIndex = hangIndex;
        }

        public static DiamondInfo GetMessageInfo(this Diamond self)
        {
            return new DiamondInfo() { Id = self.Id, HangIndex = self.HangIndex, LieIndex = self.LieIndex };
        }
    }
}