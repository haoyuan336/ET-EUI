using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class TroopCountConfigCategory : ProtoObject
    {
        public static TroopCountConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, TroopCountConfig> dict = new Dictionary<int, TroopCountConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<TroopCountConfig> list = new List<TroopCountConfig>();
		
        public TroopCountConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (TroopCountConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public TroopCountConfig Get(int id)
        {
            this.dict.TryGetValue(id, out TroopCountConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (TroopCountConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, TroopCountConfig> GetAll()
        {
            return this.dict;
        }

        public TroopCountConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class TroopCountConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public int TroopCount { get; set; }

	}
}
