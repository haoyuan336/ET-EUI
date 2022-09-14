using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class AwardConfigCategory : ProtoObject
    {
        public static AwardConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, AwardConfig> dict = new Dictionary<int, AwardConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<AwardConfig> list = new List<AwardConfig>();
		
        public AwardConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (AwardConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public AwardConfig Get(int id)
        {
            this.dict.TryGetValue(id, out AwardConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (AwardConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, AwardConfig> GetAll()
        {
            return this.dict;
        }

        public AwardConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class AwardConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public string Des { get; set; }
		[ProtoMember(3)]
		public string HeroList { get; set; }
		[ProtoMember(4)]
		public string WeaponList { get; set; }
		[ProtoMember(5)]
		public string ItemList { get; set; }

	}
}
