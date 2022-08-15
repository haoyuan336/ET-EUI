namespace ET
{
    public class ItemComponent: Entity, IAwake,IUpdate,ITransfer, IDestroy, IBeforeDestroy
    {
        public float CurrentTime = 0;
    }
}