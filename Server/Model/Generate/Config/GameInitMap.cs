using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class GameInitMapCategory : ProtoObject
    {
        public static GameInitMapCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, GameInitMap> dict = new Dictionary<int, GameInitMap>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<GameInitMap> list = new List<GameInitMap>();
		
        public GameInitMapCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (GameInitMap config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public GameInitMap Get(int id)
        {
            this.dict.TryGetValue(id, out GameInitMap item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (GameInitMap)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, GameInitMap> GetAll()
        {
            return this.dict;
        }

        public GameInitMap GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class GameInitMap: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public int HangCount { get; set; }
		[ProtoMember(3)]
		public int LieCount { get; set; }
		[ProtoMember(4)]
		public string Distance { get; set; }

	}
}
