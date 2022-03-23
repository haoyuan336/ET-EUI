using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class HeroConfigCategory : ProtoObject
    {
        public static HeroConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, HeroConfig> dict = new Dictionary<int, HeroConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<HeroConfig> list = new List<HeroConfig>();
		
        public HeroConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (HeroConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public HeroConfig Get(int id)
        {
            this.dict.TryGetValue(id, out HeroConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (HeroConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, HeroConfig> GetAll()
        {
            return this.dict;
        }

        public HeroConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class HeroConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public string HeroName { get; set; }
		[ProtoMember(3)]
		public int HeroColor { get; set; }
		[ProtoMember(4)]
		public int HeroHP { get; set; }

	}
}
