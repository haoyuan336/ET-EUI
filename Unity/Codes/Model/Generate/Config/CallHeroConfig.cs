using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class CallHeroConfigCategory : ProtoObject
    {
        public static CallHeroConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, CallHeroConfig> dict = new Dictionary<int, CallHeroConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<CallHeroConfig> list = new List<CallHeroConfig>();
		
        public CallHeroConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (CallHeroConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public CallHeroConfig Get(int id)
        {
            this.dict.TryGetValue(id, out CallHeroConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (CallHeroConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, CallHeroConfig> GetAll()
        {
            return this.dict;
        }

        public CallHeroConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class CallHeroConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public int Count { get; set; }
		[ProtoMember(3)]
		public int ItemType { get; set; }
		[ProtoMember(4)]
		public string Des { get; set; }
		[ProtoMember(5)]
		public int ItemCount { get; set; }
		[ProtoMember(6)]
		public int HeroQuality { get; set; }

	}
}
