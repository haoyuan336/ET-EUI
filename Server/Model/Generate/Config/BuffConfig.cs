using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class BuffConfigCategory : ProtoObject
    {
        public static BuffConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, BuffConfig> dict = new Dictionary<int, BuffConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<BuffConfig> list = new List<BuffConfig>();
		
        public BuffConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (BuffConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public BuffConfig Get(int id)
        {
            this.dict.TryGetValue(id, out BuffConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (BuffConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, BuffConfig> GetAll()
        {
            return this.dict;
        }

        public BuffConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class BuffConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public string buffName { get; set; }
		[ProtoMember(3)]
		public int AddType { get; set; }
		[ProtoMember(4)]
		public string Des { get; set; }
		[ProtoMember(5)]
		public int value { get; set; }
		[ProtoMember(6)]
		public int IsCanAttack { get; set; }
		[ProtoMember(7)]
		public string ImageStr { get; set; }
		[ProtoMember(8)]
		public string SpriteAtlas { get; set; }
		[ProtoMember(9)]
		public int RoundOverActiveBuff { get; set; }
		[ProtoMember(10)]
		public int MaxOverlabCount { get; set; }
		[ProtoMember(11)]
		public string EffectPath { get; set; }

	}
}
