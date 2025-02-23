using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class ActionConfigCategory : ProtoObject
    {
        public static ActionConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, ActionConfig> dict = new Dictionary<int, ActionConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<ActionConfig> list = new List<ActionConfig>();
		
        public ActionConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (ActionConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public ActionConfig Get(int id)
        {
            this.dict.TryGetValue(id, out ActionConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (ActionConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, ActionConfig> GetAll()
        {
            return this.dict;
        }

        public ActionConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class ActionConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public string Des { get; set; }

	}
}
