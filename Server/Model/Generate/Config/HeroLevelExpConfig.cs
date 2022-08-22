using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class HeroLevelExpConfigCategory : ProtoObject
    {
        public static HeroLevelExpConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, HeroLevelExpConfig> dict = new Dictionary<int, HeroLevelExpConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<HeroLevelExpConfig> list = new List<HeroLevelExpConfig>();
		
        public HeroLevelExpConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (HeroLevelExpConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public HeroLevelExpConfig Get(int id)
        {
            this.dict.TryGetValue(id, out HeroLevelExpConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (HeroLevelExpConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, HeroLevelExpConfig> GetAll()
        {
            return this.dict;
        }

        public HeroLevelExpConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class HeroLevelExpConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public int EXP { get; set; }
		[ProtoMember(3)]
		public int NeedRank { get; set; }
		[ProtoMember(4)]
		public int SkillLevel1 { get; set; }
		[ProtoMember(5)]
		public int SkillLevel2 { get; set; }
		[ProtoMember(6)]
		public int SkillLevel3 { get; set; }
		[ProtoMember(7)]
		public int SkillLevel4 { get; set; }

	}
}
