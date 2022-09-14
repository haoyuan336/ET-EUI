using System.Collections.Generic;
using System.ComponentModel;

namespace ET
{
    public class AnimationToolComponent: Entity, IAwake, IUpdate, IDestroy
    {
        public static AnimationToolComponent Instance = null;

        public List<MoveActionItem> MoveActionItems = new List<MoveActionItem>();
        public List<CircleActionItem> CircleActionItems = new List<CircleActionItem>();
        public List<ScaleActionItem> ScaleActionItems = new List<ScaleActionItem>();
    }
}