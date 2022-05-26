using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ET
{
    namespace EventType
    {
        public struct ShowHeroInfo
        {
            public HeroCardInfo HeroCardInfo;
        }
        public struct AppStart
        {
        }

        public struct SceneChangeStart
        {
            public Scene ZoneScene;
        }

        public struct SceneChangeFinish
        {
            public Scene ZoneScene;
            public Scene CurrentScene;
        }

        public struct ChangePosition
        {
            public Unit Unit;
            public Vector3 OldPos;
        }

        public struct ChangeRotation
        {
            public Unit Unit;
        }

        public struct PingChange
        {
            public Scene ZoneScene;
            public long Ping;
        }

        public struct AfterCreateZoneScene
        {
            public Scene ZoneScene;
        }

        public struct AfterCreateCurrentScene
        {
            public Scene CurrentScene;
        }

        public struct AfterCreateLoginScene
        {
            public Scene LoginScene;
        }

        public struct AppStartInitFinish
        {
            public Scene ZoneScene;
        }

        public struct LoginFinish
        {
            public Scene ZoneScene;
        }

        public struct LoadingBegin
        {
            public Scene Scene;
        }

        public struct LoadingFinish
        {
            public Scene Scene;
        }

        public struct EnterMapFinish
        {
            public Scene ZoneScene;
        }

        public struct AfterUnitCreate
        {
            public Unit Unit;
        }

        public struct MoveStart
        {
            public Unit Unit;
        }

        public struct MoveStop
        {
            public Unit Unit;
        }

        public struct UnitEnterSightRange
        {
        }

        public struct InstallComponent
        {
            public Computer Computer;
        }

        public struct ShowMatchButtonUIMessage
        {
            public Scene zoneScene;
        }

        public struct ReferCurrentMatchingCountText
        {
            public Scene zoneScene;
            public int Count;
        }

        public struct SyncCreateRoomMessage
        {
            public Scene zoneScene;
            public int InRoomIndex;
        }

        public struct CreateOneDiamondView
        {
            public Diamond Diamond;
            public DiamondInfo DiamondInfo;
        }

        public struct UpdateDiamondData
        {
            public Diamond Diamond;
        }

        public struct UpdateRoomInfo
        {
            public Scene zoneScene;
            public long RoomId;
            public int TurnIndex;
            public int MySeatIndex;
        }

        public struct UpdateCurrentTurnSeatIndex
        {
            public Scene zoneScene;
            public int TurnIndex;
        }

        public struct UpdateDiamondIndex
        {
            public Diamond Diamond;
            public int LieIndex;
            public int HangIndex;
        }

        public struct DestoryDiamondView
        {
            public Diamond Diamond;
        }

        public struct InitDiamondAndMoveDown
        {
            public Diamond Diamond;
        }

        public struct UnLockTouchLock
        {
            public Scene ZoneScene;
        }

        public struct ShowSceneUI
        {
            public Scene ZoneScene;
        }

        // public struct CreateHeroCardView
        // {
        //     public Dictionary<int, List<HeroCard>> HeroCardListMap;
        // }

        public struct CreateOneHeroCardView
        {
            public HeroCard HeroCard;
            public HeroCardInfo HeroCardInfo;
        }

        public struct CreateOneHeroModeView
        {
            public HeroCard HeroCard;
        }

        public struct UpdateAttackView
        {
            public HeroCard HeroCard;
        }

        public struct UpdateAngryView
        {
            public HeroCard HeroCard;
        }

        // public struct PlayAddHeroCardValueEffect
        // {
        //     public Diamond StartDiamond;
        //     public HeroCard EndHeroCard;
        // }

        public struct EnterAttackStateView
        {
            public HeroCard HeroCard;
        }

        public struct PlayHeroCardAttackAnim
        {
            public HeroCard AttackHeroCard;
            public HeroCard BeAttackHeroCard;
            public HeroCardInfo AttackHeroCardInfo;
            public HeroCardInfo BeAttackHeroCardInfo;
        }

        public struct PlayAddAttackViewAnim
        {
            public AddItemAction AddItemAction;

            public Scene Scene;
            // public HeroCard HeroCard;
            // public HeroCardInfo HeroCardInfo;
            // public Vector3 StartPos;
            // public Diamond Diamond;
            // public float AddAttack;
            // public float AddAngry;
            // public float EndAttack;
            // public float EndAngry;
        }

        public struct PlayAddAngryViewAnim
        {
            public AddItemAction AddItemAction;

            public Scene Scene;
            // public HeroCard HeroCard;
            //
            // // public Vector3 StartPos;
            // public Diamond Diamond;
            // public HeroCardInfo HeroCardInfo;
        }

        public struct ShowGameWinUI
        {
            public Scene ZondScene;
        }

        public struct ShowGameLoaseUI
        {
            public Scene ZoneScene;
        }

        /// <summary>
        /// 设置英雄为选择状态
        /// </summary>
        public struct SetHeroCardChooseState
        {
            public List<HeroCard> AllHeroCard;
            public HeroCard HeroCard;
            public bool Show;
        }

        /// <summary>
        /// 回合结束
        /// </summary>
        public struct GameAroundOver
        {
            public List<HeroCard> HeroCards;
        }
        /// <summary>
        /// 退出游戏场景
        /// </summary>
        public struct ExitGameScene
        {
        
        }

        public struct ChangeFightCameraLook
        {
            public bool Value;
        }

        public struct PlayDiamondContentAnim
        {
            public bool Value;  //播放隐藏显示
        }
    }
}