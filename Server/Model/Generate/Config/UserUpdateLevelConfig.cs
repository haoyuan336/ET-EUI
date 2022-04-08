using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class UserUpdateLevelConfigCategory : ProtoObject
    {
        public static UserUpdateLevelConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, UserUpdateLevelConfig> dict = new Dictionary<int, UserUpdateLevelConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<UserUpdateLevelConfig> list = new List<UserUpdateLevelConfig>();
		
        public UserUpdateLevelConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (UserUpdateLevelConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public UserUpdateLevelConfig Get(int id)
        {
            this.dict.TryGetValue(id, out UserUpdateLevelConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (UserUpdateLevelConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, UserUpdateLevelConfig> GetAll()
        {
            return this.dict;
        }

        public UserUpdateLevelConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class UserUpdateLevelConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public int NeedExp { get; set; }

	}
}
