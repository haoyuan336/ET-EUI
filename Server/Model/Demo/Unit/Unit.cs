using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using UnityEngine;

namespace ET
{
    public sealed class Unit: Entity, IAwake<int>
    {

        public long CurrentRoomId;
        public long AccountId;
        public int ConfigId; //配置表id
        // public bool isMatching; //是否在匹配中
        // public long CurrentTroopId;
        
        public int InRoomIndex; //在房间里面的id

        public int SeatIndex;

        public int CurrentTurnAttackHeroSeatIndex = 0;   //todo 当前轮到攻击的英雄

        [BsonIgnore]
        public UnitType Type => (UnitType) this.Config.Type;

        [BsonIgnore]
        public UnitConfig Config => UnitConfigCategory.Instance.Get(this.ConfigId);

        private Vector3 position; //坐标
        // public List<HeroCard> HeroCards = new List<HeroCard>();

        // public List<long> HeroCardIDs = new List<long>();
        public long CurrentTroopId;  //当前选择的队伍id

        public bool IsAI => this.Config.IsAI == 1;

        public Vector3 Position
        {
            get => this.position;
            set
            {
                this.position = value;
                Game.EventSystem.Publish(new EventType.ChangePosition() { Unit = this });
            }
        }
        [BsonIgnore]
        public Vector3 Forward
        {
            get => this.Rotation * Vector3.forward;
            set => this.Rotation = Quaternion.LookRotation(value, Vector3.up);
        }
        
        private Quaternion rotation;
        
        public Quaternion Rotation
        {
            get => this.rotation;
            set
            {
                this.rotation = value;
                Game.EventSystem.Publish(new EventType.ChangeRotation() { Unit = this });
            }
        }
    }
}