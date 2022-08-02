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
            UnityEngine.Object.Destroy(self.GameObject);
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
                moveActionItem.CurrentTime += Time.deltaTime * moveActionItem.Speed;

                var value = Mathf.Sin(moveActionItem.CurrentTime);
                Vector3 prePos = Vector3.Lerp(moveActionItem.CurrentPos, moveActionItem.EndPos, value);
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
        public static async ETTask MoveToPos(this GameObjectComponent self, Vector3 endPos)
        {
            ETTask task = ETTask.Create();
            self.MoveActionItems.Add(new MoveActionItem()
            {
                Time = Mathf.PI * 0.5f,
                CurrentPos = self.GameObject.transform.position,
                EndPos = endPos,
                Task = task,
                Speed = 5
            });
            await task.GetAwaiter();
        }
        // public static async ETTask 

        public static async ETTask RotateToVector(this GameObjectComponent self, Vector3 endVector3)
        {
            await ETTask.CompletedTask;
        }
    }
}