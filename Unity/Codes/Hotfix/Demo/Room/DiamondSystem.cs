using System.Linq;

namespace ET
{
    public class DiamondAwakeSystem: AwakeSystem<Diamond>
    {
        public override void Awake(Diamond self)
        {
#if !SERVER
            // Game.EventSystem.Publish(new EventType.CreateOneDiamondView() { Diamond = self });
            //             self.Id = diamondInfo.Id;
            //             self.LieIndex = diamondInfo.LieIndex;
            //             self.HangIndex = diamondInfo.HangIndex;
            //             self.DiamondType = diamondInfo.DiamondType;
            //             self.InitHangIndex = diamondInfo.InitHangIndex;
            //             self.InitLieIndex = diamondInfo.InitLieIndex;
            //             self.BoomType = diamondInfo.BoomType;
            // #if !SERVER
            //             Game.EventSystem.Publish(new EventType.UpdateDiamondData() { Diamond = self });
            //
            // #endif
#endif
        }
    }

    public class DiamondAwakeSystem2: AwakeSystem<Diamond, int, int>
    {
        public override void Awake(Diamond self, int colorType, int boomType)
        {
            // public DiamondTypeConfig Find<T>(string paramKeyStr, T value)
            // {
            //     var keyValuePair = this.dict.Values.ToList().Find(a =>
            //     {
            //         T target = (T) a.GetType().GetField(paramKeyStr).GetValue(a);
            //         if (target.Equals(value))
            //         {
            //             return true;
            //         }
            //
            //         return false;
            //     });
            //     return keyValuePair;
            // }

            var dict = DiamondTypeConfigCategory.Instance.GetAll();

            DiamondTypeConfig config = dict.Values.ToList().Find(a =>
            {
                if (a.ColorId == colorType && a.BoomType == boomType)
                {
                    return true;
                }

                return false;
            });
            if (config != null)
            {
                self.ConfigId = config.Id;
                self.BoomType = config.BoomType;
                self.DiamondType = config.ColorId;
            }
            else
            {
                Log.Error($"未找到相应的配置 diamondType{colorType} boomtype{boomType}");
            }

            // DiamondTypeConfig diamondTypeConfig = 
        }
    }

    public class DiamondAwakeSystem1: AwakeSystem<Diamond, DiamondInfo>
    {
        public override void Awake(Diamond self, DiamondInfo a)
        {
#if !SERVER
            self.Id = a.Id;
            self.LieIndex = a.LieIndex;
            self.HangIndex = a.HangIndex;
            self.DiamondType = a.DiamondType;
            self.InitHangIndex = a.InitHangIndex;
            self.InitLieIndex = a.InitLieIndex;
            self.BoomType = a.BoomType;
            self.ConfigId = a.ConfigId;
            Game.EventSystem.Publish(new EventType.CreateOneDiamondView() { Diamond = self, DiamondInfo = a });
#endif
        }
    }

    public static class DiamondSystem
    {
        public static void SetIndex(this Diamond self, int lieIndex, int hangIndex)
        {
            // Log.Debug($"set index{self.LieIndex}");
            // Log.Debug($"set index self");

            self.LieIndex = lieIndex;
            self.HangIndex = hangIndex;
        }

        public static DiamondInfo GetMessageInfo(this Diamond self)
        {
            return new DiamondInfo()
            {
                Id = self.Id,
                HangIndex = self.HangIndex,
                LieIndex = self.LieIndex,
                DiamondType = self.DiamondType,
                InitLieIndex = self.InitLieIndex,
                InitHangIndex = self.InitHangIndex,
                BoomType = self.BoomType,
                ConfigId = self.ConfigId
            };
        }

        public static bool EqualsIndex(this Diamond self, int lieIndex, int hangIndex)
        {
            if (self.LieIndex == lieIndex && self.HangIndex == hangIndex)
            {
                return true;
            }

            return false;
        }

        public static async ETTask Destroy(this Diamond self)
        {
#if !SERVER
            await Game.EventSystem.PublishAsync(new EventType.DestoryDiamondView() { Diamond = self });
#endif
            self.Dispose();
            await ETTask.CompletedTask;
        }

        //         public static void InitWithMessageInfo(this Diamond self, DiamondInfo diamondInfo)
        //         {
        //             self.Id = diamondInfo.Id;
        //             self.LieIndex = diamondInfo.LieIndex;
        //             self.HangIndex = diamondInfo.HangIndex;
        //             self.DiamondType = diamondInfo.DiamondType;
        //             self.InitHangIndex = diamondInfo.InitHangIndex;
        //             self.InitLieIndex = diamondInfo.InitLieIndex;
        //             self.BoomType = diamondInfo.BoomType;
        // #if !SERVER
        //             Game.EventSystem.Publish(new EventType.UpdateDiamondData() { Diamond = self });
        //
        // #endif
        //         }

        public static void UpdateIndex(this Diamond self, int LieIndex, int HangIndex)
        {
            self.LieIndex = LieIndex;
            self.HangIndex = HangIndex;
#if !SERVER
            Game.EventSystem.Publish(new EventType.UpdateDiamondIndex() { Diamond = self, LieIndex = LieIndex, HangIndex = HangIndex });

#endif
        }
    }
}