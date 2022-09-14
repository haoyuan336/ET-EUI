using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class PlayerHeadImageResConfigCategory : ProtoObject
    {
        public static PlayerHeadImageResConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, PlayerHeadImageResConfig> dict = new Dictionary<int, PlayerHeadImageResConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<PlayerHeadImageResConfig> list = new List<PlayerHeadImageResConfig>();
		
        public PlayerHeadImageResConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (PlayerHeadImageResConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public PlayerHeadImageResConfig Get(int id)
        {
            this.dict.TryGetValue(id, out PlayerHeadImageResConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (PlayerHeadImageResConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, PlayerHeadImageResConfig> GetAll()
        {
            return this.dict;
        }

        public PlayerHeadImageResConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class PlayerHeadImageResConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public string ImageRes { get; set; }
		[ProtoMember(3)]
		public string SpriteAtlasRes { get; set; }
		[ProtoMember(4)]
		public int Type { get; set; }

	}
}
