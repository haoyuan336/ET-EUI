using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class LevelConfigCategory : ProtoObject
    {
        public static LevelConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, LevelConfig> dict = new Dictionary<int, LevelConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<LevelConfig> list = new List<LevelConfig>();
		
        public LevelConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (LevelConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public LevelConfig Get(int id)
        {
            this.dict.TryGetValue(id, out LevelConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (LevelConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, LevelConfig> GetAll()
        {
            return this.dict;
        }

        public LevelConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class LevelConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }

	}
}
