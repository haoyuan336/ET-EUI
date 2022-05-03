using System;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace ET
{
    [ObjectSystem]
    public class OperaComponentAwakeSystem: AwakeSystem<OperaComponent>
    {
        public override void Awake(OperaComponent self)
        {
            self.mapMask = LayerMask.GetMask("Map");
        }
    }

    [ObjectSystem]
    public class OperaComponentUpdateSystem: UpdateSystem<OperaComponent>
    {
        public override void Update(OperaComponent self)
        {
            self.Update();
        }
    }

    public static class OperaComponentSystem
    {
        public static void Update(this OperaComponent self)
        {
            if (Input.GetMouseButtonDown(0))
            {
                self.isTouching = true;
                self.ClickPoint = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            if (Input.GetMouseButtonUp(0))
            {
                self.isTouching = false;
            }

            if (self.isTouching)
            {
                if (Vector2.Distance(Input.mousePosition, self.ClickPoint) >= 50)
                {
                    self.isTouching = false;
                    Vector2 endDirVector2 = new Vector2(Input.mousePosition.x - self.ClickPoint.x, Input.mousePosition.y - self.ClickPoint.y);
                    var angle = Vector2.SignedAngle(endDirVector2, Vector2.up);
                    Log.Debug("Scroll Screen" + angle);
                    //得到滑动方向
                    ScrollDirType dir = ScrollDirType.Down;
                    if (angle > -45 && angle < 45)
                    {
                        dir = ScrollDirType.Up;
                    }

                    if (angle > 45 && angle < 135)
                    {
                        dir = ScrollDirType.Right;
                    }

                    if ((angle > 135 && angle < 180) || (angle > -180 && angle < -135))
                    {
                        dir = ScrollDirType.Down;
                    }

                    if (angle < -45 && angle > -135)
                    {
                        dir = ScrollDirType.Left;
                    }

                    Log.Debug($"scroll view = {dir.ToString()}");

                    PlayerComponent playerComponent = self.ZoneScene().GetComponent<PlayerComponent>();
                    // int mySeatIndex = playerComponent.MySeatIndex;
                    // int turnIndex = playerComponent.CurrentTurnIndex;
                    int hangCount = ConstValue.HangCount;
                    int lieCount = ConstValue.LieCount;
                    float distance = ConstValue.Distance;

                    // if (mySeatIndex == turnIndex)
                    // {
                    //只有自己的座位号 跟服务器下发的座位号一致的时候 才能操作游戏
                    Ray ray = Camera.main.ScreenPointToRay(self.ClickPoint);

                    RaycastHit raycastHit;
                    var maskCode = LayerMask.GetMask("Default");
                    bool isHited = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, maskCode);
                    if (isHited)
                    {
                        Log.Debug("hited" + raycastHit.transform.name);
                        UnityEngine.Vector3 pos = raycastHit.transform.position;
                        float x = pos.x;
                        float y = pos.z;
                        // a.Diamond.LieIndex - liecount * 0.5f + 0.5f) * distance,
                        float lieIndex = x / distance + lieCount * 0.5f - 0.5f;
                        float hangIndex = y / distance + hangCount * 0.5f - 0.5f;
                        C2M_PlayerScrollScreen c2MPlayerScrollScreen = new C2M_PlayerScrollScreen();
                        c2MPlayerScrollScreen.StartX = (int) lieIndex;
                        c2MPlayerScrollScreen.StartY = (int) hangIndex;
                        c2MPlayerScrollScreen.DirType = (int) dir;
                        c2MPlayerScrollScreen.RoomId = playerComponent.RoomId;
                        // c2MPlayerScrollScreen.RoomId = playerComponent.
                        if (self.TouchLock)
                        {
                            return;
                        }

                        self.TouchLock = true;
                        
                        self.ZoneScene().GetComponent<SessionComponent>().Session.Send(c2MPlayerScrollScreen);
                        
                    }
                }
            }

            // if (Input.GetMouseButtonDown(1))
            // {
            //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //     RaycastHit hit;
            //     if (Physics.Raycast(ray, out hit, 1000, self.mapMask))
            //     {
            //         self.ClickPoint = hit.point;
            //         self.frameClickMap.X = self.ClickPoint.x;
            //         self.frameClickMap.Y = self.ClickPoint.y;
            //         self.frameClickMap.Z = self.ClickPoint.z;
            //         self.ZoneScene().GetComponent<SessionComponent>().Session.Send(self.frameClickMap);
            //     }
            // }
            //
            // if (Input.GetKeyDown(KeyCode.R))
            // {
            //     CodeLoader.Instance.LoadLogic();
            //     Game.EventSystem.Add(CodeLoader.Instance.GetTypes());
            //     Game.EventSystem.Load();
            //     Log.Debug("hot reload success!");
            // }
            //
            // if (Input.GetKeyDown(KeyCode.T))
            // {
            //     C2M_TransferMap c2MTransferMap = new C2M_TransferMap();
            //     self.ZoneScene().GetComponent<SessionComponent>().Session.Call(c2MTransferMap).Coroutine();
            // }
        }
    }
}