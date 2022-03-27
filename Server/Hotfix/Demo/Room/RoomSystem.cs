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
    public class RoomAwakeSystem: AwakeSystem<Room>
    {
        public override void Awake(Room self)
        {
            Log.Debug("room awake" + self.Id);
            PvPLevelConfig pvPLevelConfig = PvPLevelConfigCategory.Instance.Get(1);
            self.HangCount = pvPLevelConfig.HangCount;
            self.LieCount = pvPLevelConfig.LieCount;

            // self.InitGameData();
        }
    }

    public static class RoomSystem
    {
        public static void AddUnits(this Room self, List<Unit> units)
        {
            self.Units = units;

            for (var i = 0; i < units.Count; i++)
            {
                units[i].InRoomIndex = i + 1;
            }

            foreach (var unit in self.Units)
            {
                Log.Debug("sync create room message");
                MessageHelper.SendToClient(unit, new M2C_SyncCreateRoomMessage() { InRoomIndex = unit.InRoomIndex });
            }

            // self.InitGameData();
            // self.SyncRoomInfo();
            // self.ToggleTurnSeatIndex();
            // self.syncCurrentTurnIndex();
        }

        // public static void ToggleTurnSeatIndex(this Room self)
        // {
        //     //切换轮流操作游戏的座位号
        //     if (self.CurrentTurnIndex == 0)
        //     {
        //         self.CurrentTurnIndex = 1;
        //     }
        //     else
        //     {
        //         self.CurrentTurnIndex++;
        //         if (self.CurrentTurnIndex > self.Units.Count)
        //         {
        //             self.CurrentTurnIndex = 1;
        //         }
        //     }
        // }
        //
        // public static void syncCurrentTurnIndex(this Room self)
        // {
        //     foreach (var unit in self.Units)
        //     {
        //         M2C_ChangeCurrentTurnSeatIndex m2CChangeCurrentTurnSeatIndex = new M2C_ChangeCurrentTurnSeatIndex();
        //         m2CChangeCurrentTurnSeatIndex.CurrentTurnIndex = self.CurrentTurnIndex;
        //         MessageHelper.SendToClient(unit, m2CChangeCurrentTurnSeatIndex);
        //     }
        // }
        //
        // //同步房间信息
        // public static void SyncRoomInfo(this Room self)
        // {
        //     foreach (var unit in self.Units)
        //     {
        //         M2C_SyncRoomInfo m2CSyncRoomInfo = new M2C_SyncRoomInfo();
        //         m2CSyncRoomInfo.RoomId = self.Id;
        //         m2CSyncRoomInfo.TurnIndex = self.CurrentTurnIndex;
        //         m2CSyncRoomInfo.MySeatIndex = unit.InRoomIndex;
        //         MessageHelper.SendToClient(unit, m2CSyncRoomInfo);
        //     }
        // }
        //
        // public static void InitGameData(this Room self)
        // {
        //     self.Diamonds = new Diamond[self.LieCount, self.HangCount];
        //     List<DiamondInfo> diamondInfos = new List<DiamondInfo>();
        //     DiamondComponent diamondComponent = self.DomainScene().GetComponent<DiamondComponent>();
        //     self.DiamondComponent = diamondComponent;
        //     // diamondComponent.CreateOneDiamond();
        //     int[,] map =
        //     {
        //         { 1, 2, 3, 4, 5, 6, 1, 1 }, { 2, 3, 4, 5, 6, 1, 2, 3 }, { 3, 2, 5, 4, 1, 2, 3, 2 }, { 4, 1, 6, 2, 2, 5, 2, 1 },
        //         { 5, 6, 1, 2, 1, 1, 5, 6 }, { 6, 5, 2, 1, 4, 2, 6, 5 }, { 1, 4, 3, 6, 5, 1, 1, 4 }, { 2, 3, 4, 5, 6, 1, 2, 3 }
        //     };
        //     for (var i = 0; i < self.HangCount; i++)
        //     {
        //         for (var j = 0; j < self.LieCount; j++)
        //         {
        //             Diamond diamond = diamondComponent.CreateOneDiamond();
        //             diamond.SetIndex(j, i);
        //             diamond.DiamondType = map[i, j];
        //             self.Diamonds[j, i] = diamond;
        //             diamondInfos.Add(diamond.GetMessageInfo());
        //         }
        //     }
        //
        //     foreach (var unit in self.Units)
        //     {
        //         MessageHelper.SendToClient(unit, new M2C_InitMapData() { DiamondInfo = diamondInfos });
        //     }
        // }
        //
        // public static void PlayerScrollScreen(this Room self, C2M_PlayerScrollScreen message)
        // {
        //     //交换两个位置的钻石
        //     //首先取出临时钻石
        //     int LieIndex = message.StartX;
        //     int HangIndex = message.StartY;
        //     Diamond diamond = self.GetDiamond(LieIndex, HangIndex);
        //     Diamond nextDiamond = self.GetDiamondWithDir(diamond, message.DirType);
        //     if (diamond != null && nextDiamond != null)
        //     {
        //         M2C_SyncDiamondAction m2CSyncDiamondAction = new M2C_SyncDiamondAction();
        //         m2CSyncDiamondAction.DiamondActionItems.Add(self.SwapDiamondPos(diamond, nextDiamond));
        //         bool isCrash = true;
        //         bool isCrashSuccess = false;
        //         bool isMoveDown = false;
        //         bool isFirstAddSpecial = true;
        //         // bool isSpecial = false;
        //         Queue<Diamond> specialDiamonds = new Queue<Diamond>();
        //         while (isCrash || isMoveDown || specialDiamonds.Count > 0)
        //         {
        //             isCrash = self.CheckCrash(m2CSyncDiamondAction.DiamondActionItems, diamond, nextDiamond, specialDiamonds, isFirstAddSpecial);
        //             if (isFirstAddSpecial)
        //             {
        //                 isFirstAddSpecial = false;
        //             }
        //             isMoveDown = self.MoveDownAllDiamond(m2CSyncDiamondAction.DiamondActionItems);
        //             if (isCrashSuccess == false && isCrash)
        //             {
        //                 isCrashSuccess = true;
        //             }
        //
        //             if (specialDiamonds.Count > 0)
        //             {
        //                 Diamond specialDiamond = specialDiamonds.Dequeue();
        //                 self.AutoCastSpecialDiamond(m2CSyncDiamondAction.DiamondActionItems, specialDiamond);
        //             }
        //         }
        //
        //         if (isCrashSuccess)
        //         {
        //             self.ToggleTurnSeatIndex();
        //             self.syncCurrentTurnIndex();
        //         }
        //         else
        //         {
        //             //todo 交换失败。反向交换
        //             m2CSyncDiamondAction.DiamondActionItems.Add(self.SwapDiamondPos(diamond, nextDiamond));
        //         }
        //
        //         //todo 下发交换action 消息
        //         foreach (var unit in self.Units)
        //         {
        //             MessageHelper.SendToClient(unit, m2CSyncDiamondAction);
        //         }
        //     }
        //     else
        //     {
        //         //todo 非法操作
        //     }
        // }
        //
        // //交换宝石的位置
        //
        //
        // public static Diamond GetDiamond(this Room self, int LieIndex, int HangIndex)
        // {
        //     // Diamond diamond = null;
        //     if (LieIndex < 0 || HangIndex < 0 || LieIndex >= self.LieCount || HangIndex >= self.HangCount)
        //     {
        //         return null;
        //     }
        //
        //     return self.Diamonds[LieIndex, HangIndex];
        // }
        //
        // //获得列上的可以消除的列表
        // public static List<List<Diamond>> GetLieCrashListList(this Room self)
        // {
        //     List<List<Diamond>> crashListList = new List<List<Diamond>>();
        //     for (int i = 0; i < self.HangCount; i++)
        //     {
        //         for (int j = 0; j < self.LieCount; j++)
        //         {
        //             Diamond diamond = self.Diamonds[j, i];
        //             if (diamond == null)
        //             {
        //                 continue;
        //             }
        //
        //             if (self.CheckIsContainInListList(crashListList, diamond))
        //             {
        //                 continue;
        //             }
        //
        //             List<Diamond> sameLieList = self.CheckLieSameDiamond(self.Diamonds[j, i]);
        //             if (sameLieList.Count >= 3)
        //             {
        //                 crashListList.Add(sameLieList);
        //             }
        //         }
        //     }
        //
        //     return crashListList;
        // }
        //
        // public static List<List<Diamond>> GetHangCrashListList(this Room self)
        // {
        //     List<List<Diamond>> crashListList = new List<List<Diamond>>();
        //     for (int i = 0; i < self.HangCount; i++)
        //     {
        //         for (int j = 0; j < self.LieCount; j++)
        //         {
        //             Diamond diamond = self.Diamonds[j, i];
        //             if (diamond == null)
        //             {
        //                 continue;
        //             }
        //
        //             if (self.CheckIsContainInListList(crashListList, diamond))
        //             {
        //                 continue;
        //             }
        //
        //             List<Diamond> sameHangList = self.CheckHangSameDiamond(self.Diamonds[j, i]);
        //             if (sameHangList.Count >= 3)
        //             {
        //                 Log.Debug("same hang list =  " + sameHangList.Count);
        //                 crashListList.Add(sameHangList);
        //             }
        //         }
        //     }
        //
        //     return crashListList;
        // }
        // public static List<Diamond> ConnectSameDiamondList(this Room self, List<Diamond> list1, List<Diamond> list2)
        // {
        //     List<Diamond> endList = new List<Diamond>();
        //     if (list1 != null && list1.Count > 2)
        //     {
        //         foreach (var diamond in list1)
        //         {
        //             endList.Add(diamond);
        //         }
        //     }
        //
        //     if (list2 != null && list2.Count > 2)
        //     {
        //         foreach (var diamond in list2)
        //         {
        //             endList.Add(diamond);
        //         }
        //     }
        //
        //     foreach (var diamond in endList)
        //     {
        //         Log.Debug($"{diamond.LieIndex} , {diamond.HangIndex}");
        //     }
        //
        //     Dictionary<Diamond, bool> dictionary = new Dictionary<Diamond, bool>();
        //     foreach (var diamond in endList)
        //     {
        //         if (!dictionary.Keys.Contains(diamond))
        //         {
        //             dictionary.Add(diamond, true);
        //         }
        //     }
        //
        //     return dictionary.Keys.ToList();
        // }
    }
}