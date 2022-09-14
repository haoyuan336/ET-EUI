namespace ET
{
    public enum WeaponBagType
    {
        Weapon = 1,
        Materail = 2,
        WeaponAndMaterial = 3,
    }

    public enum WeaponType
    {
        Invalide = -1, //无
        Accessory = 4, //饰品
        Equip = 2, //护甲
        Ring = 3, //戒指
        Weapon = 1, //武器
    }

    public enum NumberType
    {
        Number = 1, //固定数值
        Percent = 2, //百分比
    }

    public enum WordBarType
    {
        Attack = 1,
        AttackAddition = 2,
        HP = 3,
        HPAddition = 4,
        Defecnce = 5,
        DefenceAddition = 6,
        CriticalHit = 7, //暴击
        CriticalHitDamage = 8, //暴击伤害
        Toughness = 9, //韧性
        DamageAddition = 10, //伤害加成
        DamageReduction = 11, //伤害减免
        // #词条类型1攻击2防御3生命4暴击5暴伤6暴抗7加伤8免伤
        // 1. 固定攻击
        // 2. 攻击%
        // 3. 固定生命
        // 4. 生命%
        // 5. 固定防御
        // 6. 防御%
        // 7. 暴击%
        // 8. 暴击伤害%
        // 9. 韧性%
        // 10. 伤害加成%
        // 11. 伤害减免%
    }
}