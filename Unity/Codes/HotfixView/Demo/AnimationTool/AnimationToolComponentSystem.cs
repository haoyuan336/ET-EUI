using UnityEngine;

namespace ET
{
    public class AnimationToolComponentAwakeSystem: AwakeSystem<AnimationToolComponent>
    {
        public override void Awake(AnimationToolComponent self)
        {
            AnimationToolComponent.Instance = self;
        }
    }

    public class AnimationToolComponentUpdateSystem: UpdateSystem<AnimationToolComponent>
    {
        public override void Update(AnimationToolComponent self)
        {
            if (self.ScaleActionItems.Count > 0)
            {
                foreach (var scaleActionItem in self.ScaleActionItems)
                {
                    if (scaleActionItem.StateType != StateType.Active)
                    {
                        continue;
                    }

                    scaleActionItem.CurrentTime += Time.deltaTime * scaleActionItem.Speed;
                    var value = Mathf.Sin(scaleActionItem.CurrentTime);
                    var prePos = Vector3.Lerp(scaleActionItem.CurrentScale, scaleActionItem.EndScale, value);
                    scaleActionItem.GameObject.transform.localScale = prePos;
                    if (scaleActionItem.CurrentTime >= scaleActionItem.Time)
                    {
                        if (scaleActionItem.Task != null)
                        {
                            scaleActionItem.Task.SetResult();
                        }

                        scaleActionItem.GameObject.transform.localPosition = scaleActionItem.EndScale;
                        scaleActionItem.StateType = StateType.Destroy;
                    }
                }
            }

            if (self.MoveActionItems.Count > 0)
            {
                foreach (var moveActionItem in self.MoveActionItems)
                {
                    if (moveActionItem.StateType != StateType.Active)
                    {
                        continue;
                    }

                    moveActionItem.CurrentTime += Time.deltaTime * moveActionItem.Speed;
                    var value = Mathf.Sin(moveActionItem.CurrentTime);
                    var prePos = Vector3.Lerp(moveActionItem.CurrentPos, moveActionItem.EndPos, value);
                    moveActionItem.GameObject.transform.position = prePos;
                    if (moveActionItem.CurrentTime >= moveActionItem.Time)
                    {
                        if (moveActionItem.Task != null)
                        {
                            moveActionItem.Task.SetResult();
                        }

                        moveActionItem.GameObject.transform.position = moveActionItem.EndPos;
                        moveActionItem.StateType = StateType.Destroy;
                    }
                    // break;
                    // case MoveActionType.CircleToPoint:
                    //     moveActionItem.CurrentTime += Time.deltaTime * moveActionItem.Speed;
                    //     moveActionItem.GameObject.transform.rotation = Quaternion.Slerp(moveActionItem.CurrentQuat, moveActionItem.EndQuat,
                    //         moveActionItem.CurrentTime);
                    //     if (moveActionItem.CurrentTime >= moveActionItem.Time)
                    //     {
                    //         moveActionItem.Task.SetResult();
                    //         moveActionItem.GameObject.transform.rotation = moveActionItem.EndQuat;
                    //         moveActionItem.StateType = StateType.Destroy;
                    //     }
                    //
                    //     break;
                    // }
                }
            }
        }
    }

    public static class AnimationToolComponentSystem
    {
        public static void MoveAction(this AnimationToolComponent self, MoveActionItem moveActionItem)
        {
            self.MoveActionItems.Add(moveActionItem);
        }

        public static void ScaleAction(this AnimationToolComponent self, ScaleActionItem scaleActionItem)
        {
            self.ScaleActionItems.Add(scaleActionItem);
        }
    }
}