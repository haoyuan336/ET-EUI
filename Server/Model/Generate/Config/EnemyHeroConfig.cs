using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class EnemyHeroConfigCategory : ProtoObject
    {
        public static EnemyHeroConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, EnemyHeroConfig> dict = new Dictionary<int, EnemyHeroConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<EnemyHeroConfig> list = new List<EnemyHeroConfig>();
		
        public EnemyHeroConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (EnemyHeroConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public EnemyHeroConfig Get(int id)
        {
            this.dict.TryGetValue(id, out EnemyHeroConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (EnemyHeroConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, EnemyHeroConfig> GetAll()
        {
            return this.dict;
        }

        public EnemyHeroConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class EnemyHeroConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public string HeroName { get; set; }
		[ProtoMember(3)]
		public int Level { get; set; }
		[ProtoMember(4)]
		public int ConfigId { get; set; }
		[ProtoMember(5)]
		public int Star { get; set; }
		[ProtoMember(6)]
		public int Rank { get; set; }

	}
}
