using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class WeaponsConfigCategory : ProtoObject
    {
        public static WeaponsConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, WeaponsConfig> dict = new Dictionary<int, WeaponsConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<WeaponsConfig> list = new List<WeaponsConfig>();
		
        public WeaponsConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (WeaponsConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public WeaponsConfig Get(int id)
        {
            this.dict.TryGetValue(id, out WeaponsConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (WeaponsConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, WeaponsConfig> GetAll()
        {
            return this.dict;
        }

        public WeaponsConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class WeaponsConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public int Star { get; set; }
		[ProtoMember(3)]
		public string IconResName { get; set; }
		[ProtoMember(4)]
		public int BaseDefence { get; set; }
		[ProtoMember(5)]
		public int Price { get; set; }
		[ProtoMember(6)]
		public int MoneyType { get; set; }
		[ProtoMember(7)]
		public string Name { get; set; }
		[ProtoMember(8)]
		public int WeaponType { get; set; }
		[ProtoMember(9)]
		public int MaterialType { get; set; }
		[ProtoMember(10)]
		public string WeaponTypeName { get; set; }
		[ProtoMember(11)]
		public string WeaponDes { get; set; }

	}
}
