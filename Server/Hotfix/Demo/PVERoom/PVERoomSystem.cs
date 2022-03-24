using System.Collections.Generic;

namespace ET
{
    public class PVERoomAwakeSystem: AwakeSystem<PVERoom>
    {
        public override void Awake(PVERoom self)
        {
            Log.Debug("pve room awake");
        }
    }

    public static class PVERoomSystem
    {
        public static async void PlayerGameReady(this PVERoom self, Unit unit, long AccountId)
        {
            // Log.Debug("player game read");            
            // unit.GetComponent<LoginAccountRecordComponentSystem>()
            //取出当前玩家 玩到的关卡数
            List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Account>((a) => a.Id.Equals(AccountId));
            if (accounts.Count > 0)
            {
                int levelNum = accounts[0].PVELevelNumber;
                Log.Debug($"level num {levelNum}");
            }
        }
    }
}