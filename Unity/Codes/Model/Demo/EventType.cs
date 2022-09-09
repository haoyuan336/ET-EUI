using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ET
{
    namespace EventType
    {
        public struct PlayAdditionalDamageAnimEvent
        {
            public HeroCardComponent HeroCardComponent;
            public HeroCardDataComponentInfo HeroCardDataComponentInfo;
        }
        public struct PlayHeroRecoveryAnimEvent
        {
            public HeroCardComponent HeroCardComponent;
            public HeroCardDataComponentInfo HeroCardDataComponentInfo;
        }
        public struct PlayBuffDamageAnim
        {
            public HeroCardComponent HeroCardComponent;
            public HeroCardDataComponentInfo HeroCardDataComponentInfo;
            public BuffInfo BuffInfo;
            public int DamageCount;

        }
        public struct InitObjectPool
        {
            public Scene Scene;
        }
        public struct PlayAudioEffect
        {
            public string AudioStr;
            public Scene ZoneScene;
        }

        public struct PlayGameAudioEffect
        {
            public string AudioStr;
            public Scene ZoneScene;
        }

        public struct HideCombo
        {
            public Scene Scene;
        }

        public struct ShowComobAnim
        {
            //显示comob动画
            public Scene Scene;
            public int ComboCount;
            public int CrashCount; //消除的个数
        }

        public struct ShowAttackMark
        {
            // public HeroCard HeroCard;
            public HeroCardComponent HeroCardComponent;

            public HeroCardDataComponentInfo HeroCardDataComponentInfo;

            //显示攻击标记
            public bool IsShow;
        }

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
            public int CurrentLevelNum;
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
            public DiamondActionType DiamondActionType;
        }

        public struct DestoryDiamondWithAnim
        {
            public Diamond Diamond;
            public int Index;
            public DiamondAction DiamondAction;
            public Scene Scene;
        }

        public struct DestoryDiamondView
        {
            public Diamond Diamond;
            public int Index;
            public DiamondAction DiamondAction;
            public Scene Scene;
            public DiamondActionItem DiamondActionItem;
        }

        public struct InitDiamondAndMoveDown
        {
            public Diamond Diamond;
        }

        public struct UnLockTouchLock
        {
            public Scene ZoneScene;
            public bool IsLockTouch;
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
            public HeroCardDataComponentInfo HeroCardDataComponentInfo;
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
            // public HeroCard AttackHeroCard;
            // public HeroCard BeAttackHeroCard;
            // public HeroCardDataComponentInfo AttackHeroCardDataComponentInfo;
            // public HeroCardDataComponentInfo BeAttackHeroCardDataComponentInfo;
            // public CrashCommonInfo CommonInfo;
            // public List<AttackActionItem> AttackActionItems;

            public AttackAction AttackAction;

            public HeroCardComponent HeroCardComponent;
            // public AttackAction AttackAction;
        }

        public struct PlayAddAttackViewAnim
        {
            public List<AddItemAction> AddItemActions;

            public Scene Scene;

            public HeroCard HeroCard;
            public Diamond Diamond;

            public AddItemAction AddItemAction;
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
            public List<AddItemAction> AddItemActions;

            public Scene Scene;

            public HeroCard HeroCard;

            // public Diamond Diamond;
            public Vector3 StartPos;
            public DiamondInfo DiamondInfo;

            public AddItemAction AddItemAction;
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

            public HeroCardComponent HeroCardComponent;
            public long HeroId;
            public bool IsShow;
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
            public Scene ZoneScene;
            public bool Value;
        }

        public struct PlayDiamondContentAnim
        {
            public bool IShow; //播放隐藏显示
        }

        public struct DestroyHeroCard
        {
            public HeroCard HeroCard; //销毁卡牌
        }

        public struct SetNewMails
        {
            public Scene Scene;
            public List<MailInfo> MailInfos; //邮件列表
        }

        public struct ReceiveChat
        {
            public ChatInfo ChatInfo;
            public Scene Scene;
        }

        public struct UpdateHeroAttackInfo
        {
            public HeroCardComponent HeroCardComponent;

            public ComboActionItem ComboActionItem;
            // public AddItemAction AddItemAction;
            // public HeroCard HeroCard;
        }

        public struct UpdateHeroInfoEvent
        {
            public HeroCardComponent HeroCardComponent;
            public HeroCardDataComponentInfo HeroCardDataComponentInfo;
        }

        public struct UpdateHeroBuffInfoEvent
        {
            public HeroCardComponent HeroCardComponent;
            public long HeroId;
            public List<BuffInfo> BuffInfos;
            public HeroCardDataComponentInfo HeroCardDataComponentInfo;
        }

        public struct UpdateHeroAngryInfoEvent
        {
            public HeroCardComponent HeroCardComponent;
            public HeroCardDataComponentInfo HeroCardDataComponentInfo;
        }

        public struct ShowMatchPVPRoomSuccessAnim
        {
            public Scene Scene;
            public long RoomId;
        }

        // public struct UpdateHeroBuffInfo
        // {
        //     public Scene Scene;
        //     public HeroCardComponent HeroCardComponent;
        //     public UpdateHeroBuffInfoItem UpdateHeroBuffInfoItem;
        // }
    }
}