namespace ET
{
    public class PVERoomAwakeSystem: AwakeSystem<PVERoom>
    {
        public override void Awake(PVERoom self)
        {
            Log.Debug("pve room awake");
        }
    }

    public static class PVERoomSystem
    {
        public static void StartGame(this PVERoom self)
        {
            Log.Debug("start Game");
        }
    }
}