using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class ComboConfigCategory : ProtoObject
    {
        public static ComboConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, ComboConfig> dict = new Dictionary<int, ComboConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<ComboConfig> list = new List<ComboConfig>();
		
        public ComboConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (ComboConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public ComboConfig Get(int id)
        {
            this.dict.TryGetValue(id, out ComboConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (ComboConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, ComboConfig> GetAll()
        {
            return this.dict;
        }

        public ComboConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class ComboConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public string AudioClip { get; set; }

	}
}
