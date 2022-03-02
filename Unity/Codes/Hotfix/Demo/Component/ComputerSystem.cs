namespace ET
{
    public  class ComputerAwakeSystem: AwakeSystem<Computer>
    {
        public override void Awake(Computer self)
        {
            Log.Debug("computer awake");
        }
    }

    public class ComputerUpdateSystem: UpdateSystem<Computer>
    {
        public override void Update(Computer self)
        {
            Log.Debug("Componter Update");
        }
    }

    public class ComputerDestroy: DestroySystem<Computer>
    {
        public override void Destroy(Computer self)
        {
            Log.Debug("Computer destroy");
        }
    }
    public static class ComputerSystem
    {
        public static void Start(this  Computer self)
        {
            Log.Debug("Component Start");
            self.GetComponent<PCCaseComponet>().StartPower();
            self.GetComponent<MonitorComponent>().Display();
        }
    }
}