using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class CrashCommonAddConfigCategory : ProtoObject
    {
        public static CrashCommonAddConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, CrashCommonAddConfig> dict = new Dictionary<int, CrashCommonAddConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<CrashCommonAddConfig> list = new List<CrashCommonAddConfig>();
		
        public CrashCommonAddConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (CrashCommonAddConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public CrashCommonAddConfig Get(int id)
        {
            this.dict.TryGetValue(id, out CrashCommonAddConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (CrashCommonAddConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, CrashCommonAddConfig> GetAll()
        {
            return this.dict;
        }

        public CrashCommonAddConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class CrashCommonAddConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public int ConfigId { get; set; }
		[ProtoMember(3)]
		public int MoneyType { get; set; }
		[ProtoMember(4)]
		public int Price { get; set; }
		[ProtoMember(5)]
		public int GoodsType { get; set; }

	}
}
