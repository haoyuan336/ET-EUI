using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class ElementConfigCategory : ProtoObject
    {
        public static ElementConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, ElementConfig> dict = new Dictionary<int, ElementConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<ElementConfig> list = new List<ElementConfig>();
		
        public ElementConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (ElementConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public ElementConfig Get(int id)
        {
            this.dict.TryGetValue(id, out ElementConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (ElementConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, ElementConfig> GetAll()
        {
            return this.dict;
        }

        public ElementConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class ElementConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public string IconImage { get; set; }

	}
}
