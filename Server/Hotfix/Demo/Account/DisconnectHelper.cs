﻿namespace ET
{
    public static class DisconnectHelper
    {
        public static async ETTask Disconnect(this Session self)
        {
            if (self == null || self.IsDisposed)
            {
                return;
            }

            long instanceId = self.InstanceId;
            await TimerComponent.Instance.WaitAsync(1000);
            if (instanceId != self.InstanceId)
            {
                return;
            }
            self.Dispose();
        }
    }
}