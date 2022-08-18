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
		public string OwnerHero { get; set; }
		[ProtoMember(3)]
		public int SkillType { get; set; }
		[ProtoMember(4)]
		public string SkillName { get; set; }
		[ProtoMember(5)]
		public string SkillAnimName { get; set; }
		[ProtoMember(6)]
		public int SkillTime { get; set; }
		[ProtoMember(7)]
		public int BeAttackTime { get; set; }
		[ProtoMember(8)]
		public string SkillEffect { get; set; }
		[ProtoMember(9)]
		public int EffectStartTime { get; set; }
		[ProtoMember(10)]
		public string BeAttackEffect { get; set; }
		[ProtoMember(11)]
		public int FlyEffectStartTime { get; set; }
		[ProtoMember(12)]
		public string FlyEffect { get; set; }
		[ProtoMember(13)]
		public int MoveType { get; set; }
		[ProtoMember(14)]
		public int TargetPosType { get; set; }
		[ProtoMember(15)]
		public string BeAttackBoneName { get; set; }
		[ProtoMember(16)]
		public int RangeType { get; set; }
		[ProtoMember(17)]
		public int BuggConfigId { get; set; }
		[ProtoMember(18)]
		public int BuffRoundCount { get; set; }

	}
}
