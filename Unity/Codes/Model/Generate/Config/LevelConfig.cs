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
		[ProtoMember(2)]
		public string HeroId { get; set; }
		[ProtoMember(3)]
		public string DiamondTypes { get; set; }
		[ProtoMember(4)]
		public int BeHitedAngry { get; set; }
		[ProtoMember(5)]
		public int AngryCount { get; set; }
		[ProtoMember(6)]
		public int AttackAddition { get; set; }
		[ProtoMember(7)]
		public string StartAttackAddition { get; set; }
		[ProtoMember(8)]
		public string InitBoardData { get; set; }
		[ProtoMember(9)]
		public string LevelStoryBegin { get; set; }
		[ProtoMember(10)]
		public string LevelStoryWin { get; set; }
		[ProtoMember(11)]
		public string LevelStoryLose { get; set; }

	}
}
