using System;

namespace ET
{
    public interface IBeforeDestroy
    {
		
    }
	
    public interface IBeforeDestroySystem: ISystemType
    {
        void Run(object o);
    }

    [ObjectSystem]
    public abstract class BeforeDestroySystem<T> : IBeforeDestroySystem where T: IBeforeDestroy
    {
        public void Run(object o)
        {
            this.BeforeDestroy((T)o);
        }
		
        public Type SystemType()
        {
            return typeof(IBeforeDestroySystem);
        }

        public Type Type()
        {
            return typeof(T);
        }

        public abstract void BeforeDestroy(T self);
    }
}