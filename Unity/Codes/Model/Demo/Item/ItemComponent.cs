using System.Collections.Generic;

namespace ET
{
    public class ItemComponent: Entity, IAwake,IUpdate,ITransfer, IDestroy, IBeforeDestroy
    {
        public float CurrentTime = 0;
        public List<Item> ChangeData = new List<Item>();
    }
}