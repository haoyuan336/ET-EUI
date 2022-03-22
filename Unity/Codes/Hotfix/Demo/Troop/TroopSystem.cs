namespace ET
{
    public static class TroopSystem
    {
        public static TroopInfo GetTroopMessageInfo(this Troop self)
        {
            return new TroopInfo() { TroopId = self.Id };
        }

        public static void SetTroopMessageInfo(this Troop self, TroopInfo info)
        {
            self.Id = info.TroopId;
        }
    }
}