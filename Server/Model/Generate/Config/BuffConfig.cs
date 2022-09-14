using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class BuffConfigCategory : ProtoObject
    {
        public static BuffConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, BuffConfig> dict = new Dictionary<int, BuffConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<BuffConfig> list = new List<BuffConfig>();
		
        public BuffConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (BuffConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public BuffConfig Get(int id)
        {
            this.dict.TryGetValue(id, out BuffConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (BuffConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, BuffConfig> GetAll()
        {
            return this.dict;
        }

        public BuffConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class BuffConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public string buffName { get; set; }
		[ProtoMember(3)]
		public int AddType { get; set; }
		[ProtoMember(4)]
		public string Des { get; set; }
		[ProtoMember(5)]
		public int IsDazzling { get; set; }
		[ProtoMember(6)]
		public int IsInvisible { get; set; }
		[ProtoMember(7)]
		public int RecoveryHealthAddition { get; set; }
		[ProtoMember(8)]
		public int IsRecovery { get; set; }
		[ProtoMember(9)]
		public int IsInvincible { get; set; }
		[ProtoMember(10)]
		public int IsImmune { get; set; }
		[ProtoMember(11)]
		public int IsAvoidDeath { get; set; }
		[ProtoMember(12)]
		public int AvoidDeathHealth { get; set; }
		[ProtoMember(13)]
		public int Provocation { get; set; }
		[ProtoMember(14)]
		public int IsCanCover { get; set; }
		[ProtoMember(15)]
		public int IsToDeath { get; set; }
		[ProtoMember(16)]
		public int GetAngryReduceRate { get; set; }
		[ProtoMember(17)]
		public int DefenceMultRate { get; set; }
		[ProtoMember(18)]
		public int DefenceReduceRate { get; set; }
		[ProtoMember(19)]
		public int AttackMultRate { get; set; }
		[ProtoMember(20)]
		public int AttackReduceRate { get; set; }
		[ProtoMember(21)]
		public int ResurrectionHealthRate { get; set; }
		[ProtoMember(22)]
		public int EndDamageReduceRate { get; set; }
		[ProtoMember(23)]
		public int AllDamageMultRate { get; set; }
		[ProtoMember(24)]
		public int CareHealthReduceRate { get; set; }
		[ProtoMember(25)]
		public int DeductionSelfTotalHealthRate { get; set; }
		[ProtoMember(26)]
		public int DeductionAttackAttackHealthRate { get; set; }
		[ProtoMember(27)]
		public int DamageAddition { get; set; }
		[ProtoMember(28)]
		public int IsCanAttack { get; set; }
		[ProtoMember(29)]
		public int IsFrozen { get; set; }
		[ProtoMember(30)]
		public string ImageStr { get; set; }
		[ProtoMember(31)]
		public string SpriteAtlas { get; set; }
		[ProtoMember(32)]
		public int MaxOverlabCount { get; set; }
		[ProtoMember(33)]
		public string EffectPath { get; set; }
		[ProtoMember(34)]
		public string AnimationState { get; set; }
		[ProtoMember(35)]
		public string BeAttackEffect { get; set; }
		[ProtoMember(36)]
		public string BeAttackAnim { get; set; }

	}
}
