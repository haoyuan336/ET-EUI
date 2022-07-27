using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class SentenceConfigCategory : ProtoObject
    {
        public static SentenceConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, SentenceConfig> dict = new Dictionary<int, SentenceConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<SentenceConfig> list = new List<SentenceConfig>();
		
        public SentenceConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (SentenceConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public SentenceConfig Get(int id)
        {
            this.dict.TryGetValue(id, out SentenceConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (SentenceConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, SentenceConfig> GetAll()
        {
            return this.dict;
        }

        public SentenceConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class SentenceConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public string SentenceContent { get; set; }
		[ProtoMember(3)]
		public string HeadImage { get; set; }
		[ProtoMember(4)]
		public string AtlasImage { get; set; }

	}
}
