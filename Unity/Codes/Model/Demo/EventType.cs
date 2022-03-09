using UnityEngine;

namespace ET
{
    namespace EventType
    {
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
    }
}