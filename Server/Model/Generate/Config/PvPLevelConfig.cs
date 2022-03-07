using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class PvPLevelConfigCategory : ProtoObject
    {
        public static PvPLevelConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, PvPLevelConfig> dict = new Dictionary<int, PvPLevelConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<PvPLevelConfig> list = new List<PvPLevelConfig>();
		
        public PvPLevelConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (PvPLevelConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public PvPLevelConfig Get(int id)
        {
            this.dict.TryGetValue(id, out PvPLevelConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (PvPLevelConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, PvPLevelConfig> GetAll()
        {
            return this.dict;
        }

        public PvPLevelConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class PvPLevelConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public int HangCount { get; set; }
		[ProtoMember(3)]
		public int LieCount { get; set; }

	}
}
