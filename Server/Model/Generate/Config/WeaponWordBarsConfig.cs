using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class WeaponWordBarsConfigCategory : ProtoObject
    {
        public static WeaponWordBarsConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, WeaponWordBarsConfig> dict = new Dictionary<int, WeaponWordBarsConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<WeaponWordBarsConfig> list = new List<WeaponWordBarsConfig>();
		
        public WeaponWordBarsConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (WeaponWordBarsConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public WeaponWordBarsConfig Get(int id)
        {
            this.dict.TryGetValue(id, out WeaponWordBarsConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (WeaponWordBarsConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, WeaponWordBarsConfig> GetAll()
        {
            return this.dict;
        }

        public WeaponWordBarsConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class WeaponWordBarsConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public int Star { get; set; }
		[ProtoMember(3)]
		public string Quality { get; set; }
		[ProtoMember(4)]
		public int MinValue { get; set; }
		[ProtoMember(5)]
		public int MaxValue { get; set; }
		[ProtoMember(6)]
		public string Name { get; set; }
		[ProtoMember(7)]
		public int WordBarType { get; set; }
		[ProtoMember(8)]
		public int NumberType { get; set; }
		[ProtoMember(9)]
		public int WeaponType { get; set; }
		[ProtoMember(10)]
		public string QualityIcon { get; set; }

	}
}
