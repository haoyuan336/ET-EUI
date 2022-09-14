using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class DiamondTypeConfigCategory : ProtoObject
    {
        public static DiamondTypeConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, DiamondTypeConfig> dict = new Dictionary<int, DiamondTypeConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<DiamondTypeConfig> list = new List<DiamondTypeConfig>();
		
        public DiamondTypeConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (DiamondTypeConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public DiamondTypeConfig Get(int id)
        {
            this.dict.TryGetValue(id, out DiamondTypeConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (DiamondTypeConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, DiamondTypeConfig> GetAll()
        {
            return this.dict;
        }

        public DiamondTypeConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class DiamondTypeConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public int ColorId { get; set; }
		[ProtoMember(3)]
		public string ColorValue { get; set; }
		[ProtoMember(4)]
		public string CrashAudio { get; set; }
		[ProtoMember(5)]
		public string DestoryEffectRes { get; set; }
		[ProtoMember(6)]
		public int DestoryEffectTime { get; set; }
		[ProtoMember(7)]
		public string AddAttack { get; set; }
		[ProtoMember(8)]
		public string AddAngry { get; set; }
		[ProtoMember(10)]
		public string TextureName { get; set; }
		[ProtoMember(11)]
		public int BoomType { get; set; }
		[ProtoMember(13)]
		public string PrefabRes { get; set; }
		[ProtoMember(14)]
		public string ElementDes { get; set; }
		[ProtoMember(15)]
		public string FlyEffectAttackRes { get; set; }
		[ProtoMember(16)]
		public string FlyEffectAngryRes { get; set; }

	}
}
