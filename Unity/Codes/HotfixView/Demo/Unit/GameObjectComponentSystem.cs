using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace ET
{
    public class GameObjectComponentAwake: AwakeSystem<GameObjectComponent>
    {
        public override void Awake(GameObjectComponent self)
        {
        }
    }

    [ObjectSystem]
    public class DestroySystem: DestroySystem<GameObjectComponent>
    {
        public override void Destroy(GameObjectComponent self)
        {
            // UnityEngine.Object.Destroy(self.GameObject);
            // PoolObject po = self.GameObject.GetComponent<PoolObject>();

            PoolObject po;
            if (!self.GameObject.TryGetComponent(out po))
            {
                UnityEngine.Object.Destroy(self.GameObject);
                Log.Debug("object 不是对象池对象，那么销毁掉");
            }
            else
            {
                if (!po.isPooled)
                {
                    GameObjectPoolHelper.ReturnObjectToPool(self.GameObject);
                }
            }

            Log.Debug($"destroy po {po} + name = {po.name}");
        }
    }

    [ObjectSystem]
    public class GameObjectComponentUpdateSysytem: UpdateSystem<GameObjectComponent>
    {
        public override void Update(GameObjectComponent self)
        {
            if (self.MoveActionItems.Count > 0)
            {
                MoveActionItem moveActionItem = self.MoveActionItems[0];
                float value = 0;
                Vector3 prePos = moveActionItem.CurrentPos;
                switch (moveActionItem.MoveActionType)
                {
                    case MoveActionType.Jump:
                        moveActionItem.Speed += 0.2f;
                        moveActionItem.CurrentTime += Time.deltaTime * moveActionItem.Speed;
                        value = Mathf.Abs(Mathf.Cos(moveActionItem.CurrentTime) * Mathf.Cos(moveActionItem.CurrentTime / 2));
                        prePos = Vector3.Lerp(moveActionItem.CurrentPos, moveActionItem.EndPos, (1 - value));
                        break;
                    case MoveActionType.Normal:
                        moveActionItem.CurrentTime += Time.deltaTime * moveActionItem.Speed;
                        value = Mathf.Abs(Mathf.Cos(moveActionItem.CurrentTime));
                        prePos = Vector3.Lerp(moveActionItem.CurrentPos, moveActionItem.EndPos, (1 - value));
                        break;
                    case MoveActionType.CircleToPoint:
                        moveActionItem.CurrentTime += Time.deltaTime * moveActionItem.Speed;

                        var x = moveActionItem.CurrentTime;
                        var y = Mathf.Cos(moveActionItem.CurrentTime);

                        x += moveActionItem.CurrentPos.x;
                        y += moveActionItem.EndPos.y;

                        prePos = new Vector3(x, moveActionItem.CurrentPos.y, y);
                        break;
                }

                self.GameObject.transform.position = prePos;

                if (moveActionItem.CurrentTime >= moveActionItem.Time)
                {
                    self.GameObject.transform.position = moveActionItem.EndPos;
                    moveActionItem.Task.SetResult();
                    self.MoveActionItems.RemoveAt(0);
                }
            }
        }
    }

    public static class GameObjectComponentSystem
    {
        public static async ETTask MoveDown(this GameObjectComponent self, Vector3 endPos)
        {
            ETTask task = ETTask.Create();
            self.MoveActionItems.Add(new MoveActionItem()
            {
                Time = Mathf.PI * 1,
                CurrentPos = self.GameObject.transform.position,
                EndPos = endPos,
                Task = task,
                Speed = 5,
                MoveActionType = MoveActionType.Jump
            });
            await task.GetAwaiter();
        }

        public static async ETTask MoveToPos(this GameObjectComponent self, Vector3 endPos)
        {
            ETTask task = ETTask.Create();
            self.MoveActionItems.Add(new MoveActionItem()
            {
                Time = Mathf.PI * 0.5f,
                CurrentPos = self.GameObject.transform.position,
                EndPos = endPos,
                Task = task,
                Speed = 5,
                MoveActionType = MoveActionType.Normal
            });
            await task.GetAwaiter();
        }
        // public static async ETTask 

        public static async ETTask RotateToVector(this GameObjectComponent self, Vector3 endVector3)
        {
            await ETTask.CompletedTask;
        }

        public static async void PlayerCircleActionToPos(this GameObjectComponent self, Vector3 endVector3, Action callBack)
        {
            ETTask task = ETTask.Create();
            self.MoveActionItems.Add(new MoveActionItem()
            {
                Time = Mathf.PI * 100,
                CurrentPos = self.GameObject.transform.position,
                EndPos = endVector3,
                Task = task,
                Speed = 1,
                MoveActionType = MoveActionType.CircleToPoint
            });
            await task.GetAwaiter();
            callBack();
        }
    }
}