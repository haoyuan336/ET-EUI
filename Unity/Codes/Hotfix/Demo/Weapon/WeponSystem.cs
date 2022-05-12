namespace ET
{
    public class WeponSystemAwake: AwakeSystem<Weapon>
    {
        public override void Awake(Weapon self)
        {
        }
    }

    public static class WeaponSystem
    {
        public static WeaponInfo GetInfo(this Weapon self)
        {
            return new WeaponInfo() { WeaponId = self.Id, ConfigId = self.ConfigId, Count = self.Count, };
        }
    }
}