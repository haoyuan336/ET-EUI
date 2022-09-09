using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class SkillConfigCategory : ProtoObject
    {
        public static SkillConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, SkillConfig> dict = new Dictionary<int, SkillConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<SkillConfig> list = new List<SkillConfig>();
		
        public SkillConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (SkillConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public SkillConfig Get(int id)
        {
            this.dict.TryGetValue(id, out SkillConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (SkillConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, SkillConfig> GetAll()
        {
            return this.dict;
        }

        public SkillConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class SkillConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public int TargetPosType { get; set; }
		[ProtoMember(3)]
		public int MoveType { get; set; }
		[ProtoMember(4)]
		public int RangeType { get; set; }
		[ProtoMember(5)]
		public int MaxAttackCount { get; set; }
		[ProtoMember(6)]
		public string OwnerHeroName { get; set; }
		[ProtoMember(7)]
		public int IncreaseAngry { get; set; }
		[ProtoMember(8)]
		public int IncreaseAngrySelfBuffCondition { get; set; }
		[ProtoMember(9)]
		public int[] IncreaseDamageRate { get; set; }
		[ProtoMember(10)]
		public int IncreaseDamageTargetBuffCondition { get; set; }
		[ProtoMember(11)]
		public int[] ReduceAngry { get; set; }
		[ProtoMember(12)]
		public int ReduceAngrySelfBuffCondition { get; set; }
		[ProtoMember(13)]
		public int IsPurify { get; set; }
		[ProtoMember(14)]
		public int[] AdditionalHealthDamageRates { get; set; }
		[ProtoMember(15)]
		public int IsAdditionalDamage { get; set; }
		[ProtoMember(16)]
		public int AdditionalDamageEnemyBuffCondition { get; set; }
		[ProtoMember(17)]
		public int IsKill { get; set; }
		[ProtoMember(18)]
		public int[] KillConditions { get; set; }
		[ProtoMember(19)]
		public int KillSuccessActiveSelfBuff { get; set; }
		[ProtoMember(20)]
		public int[] SelfAttackAdditionHealths { get; set; }
		[ProtoMember(21)]
		public int[] CareHealths { get; set; }
		[ProtoMember(22)]
		public int[] LevelDamages { get; set; }
		[ProtoMember(23)]
		public int[] LevelBuffRoundCounts { get; set; }
		[ProtoMember(24)]
		public int ActiveBuffCondition { get; set; }
		[ProtoMember(25)]
		public int[] BuffConfigIds { get; set; }
		[ProtoMember(26)]
		public int BuffOverCount { get; set; }
		[ProtoMember(27)]
		public int[] HealthShieldAdditions { get; set; }
		[ProtoMember(28)]
		public int[] BuffDamageAdditions { get; set; }
		[ProtoMember(29)]
		public int BuffDamageAdditionCondition { get; set; }
		[ProtoMember(32)]
		public int SkillType { get; set; }
		[ProtoMember(33)]
		public string SkillName { get; set; }
		[ProtoMember(34)]
		public string SkillAnimName { get; set; }
		[ProtoMember(35)]
		public int SkillTime { get; set; }
		[ProtoMember(36)]
		public int BeAttackAnimPlayTime { get; set; }
		[ProtoMember(37)]
		public string SkillEffect { get; set; }
		[ProtoMember(38)]
		public string SkillEffectBoneName { get; set; }
		[ProtoMember(39)]
		public string SkillEffect2 { get; set; }
		[ProtoMember(40)]
		public string SkillEffectBioneName2 { get; set; }
		[ProtoMember(41)]
		public int SkillEffectTime { get; set; }
		[ProtoMember(42)]
		public int EffectStartTime { get; set; }
		[ProtoMember(43)]
		public string BeAttackEffect { get; set; }
		[ProtoMember(44)]
		public int BeAttackEffectStartTime { get; set; }
		[ProtoMember(45)]
		public int BeAttackEffectTime { get; set; }
		[ProtoMember(46)]
		public int FlyEffectStartTime { get; set; }
		[ProtoMember(47)]
		public string FlyEffect { get; set; }
		[ProtoMember(48)]
		public string BeAttackBoneName { get; set; }
		[ProtoMember(49)]
		public int AttackCount { get; set; }

	}
}
