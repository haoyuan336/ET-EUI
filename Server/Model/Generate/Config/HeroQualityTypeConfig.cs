using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class HeroQualityTypeConfigCategory : ProtoObject
    {
        public static HeroQualityTypeConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, HeroQualityTypeConfig> dict = new Dictionary<int, HeroQualityTypeConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<HeroQualityTypeConfig> list = new List<HeroQualityTypeConfig>();
		
        public HeroQualityTypeConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (HeroQualityTypeConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public HeroQualityTypeConfig Get(int id)
        {
            this.dict.TryGetValue(id, out HeroQualityTypeConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (HeroQualityTypeConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, HeroQualityTypeConfig> GetAll()
        {
            return this.dict;
        }

        public HeroQualityTypeConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class HeroQualityTypeConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public string Des { get; set; }

	}
}
