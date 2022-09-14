using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class WeaponLevelExpConfigCategory : ProtoObject
    {
        public static WeaponLevelExpConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, WeaponLevelExpConfig> dict = new Dictionary<int, WeaponLevelExpConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<WeaponLevelExpConfig> list = new List<WeaponLevelExpConfig>();
		
        public WeaponLevelExpConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (WeaponLevelExpConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public WeaponLevelExpConfig Get(int id)
        {
            this.dict.TryGetValue(id, out WeaponLevelExpConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (WeaponLevelExpConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, WeaponLevelExpConfig> GetAll()
        {
            return this.dict;
        }

        public WeaponLevelExpConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class WeaponLevelExpConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public int EXP { get; set; }
		[ProtoMember(3)]
		public int TotalExp { get; set; }

	}
}
