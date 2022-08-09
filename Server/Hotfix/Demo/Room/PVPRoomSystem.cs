using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Numerics;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using NLog.Fluent;
using OfficeOpenXml.Export.ToDataTable;

namespace ET
{
    public class RoomAwakeSystem: AwakeSystem<PVPRoom>
    {
        public override void Awake(PVPRoom self)
        {
            Log.Debug("room awake" + self.Id);
            PvPLevelConfig pvPLevelConfig = PvPLevelConfigCategory.Instance.Get(1);
            self.HangCount = pvPLevelConfig.HangCount;
            self.LieCount = pvPLevelConfig.LieCount;

            // self.InitGameData();
        }
    }

    public static class PvPRoomSystem
    {
        public static void PlayerScrollScreen(this PVPRoom self)
        {
            
            
        }
    }
}