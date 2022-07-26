namespace ET
{
    public static class AccountSystem
    {
        public static AccountInfo GetInfo(this Account self)
        {
            return new AccountInfo()
            {
                Account = self.Id,
                Name = self.AccountName,
                NickName = self.NickName,
                CreateTime = self.CreateTime,
                LastLogonTime = self.LastLogonTime,
                PvELevelNumber = self.PVELevelNumber,
                HeadImageConfigId = self.HeadImageConfigId,
                HeadFrameImageConfigId = self.HeadFrameImageConfigId,
                CurrentTroopId = self.CurrentTroopId
            };
        }
    }
}