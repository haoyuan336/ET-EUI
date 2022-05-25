using System.Collections.Generic;

namespace ET
{
    public class DiamondComponentAwakeSystem: AwakeSystem<DiamondComponent>
    {
        public override void Awake(DiamondComponent self)
        {
            // Log.Debug("diamond component awake system");
        }
    }

    public static class DiamondComponentSystem
    {
        public static List<DiamondInfo> InitDiamonds(this DiamondComponent self, int levelNum)
        {
            Log.Debug($"current level num {levelNum}");
            self.LevelConfig = LevelConfigCategory.Instance.Get(levelNum);
            Log.Debug($"level config {self.LevelConfig}");
            // self.Diamonds = new Diamond[self.LevelConfig.LieCount, self.LevelConfig.HangCount];
            // diamondComponent.CreateOneDiamond();
            // self.Diamonds = new DiamondStruct[self.LevelConfig.HangCount];
            // self.Diamonds = new Diamond[self.LevelConfig.HangCount * self.LevelConfig.LieCount];
            // for (int i = 0; i < self.LevelConfig.HangCount; i++)
            // {
            //     DiamondStruct diamondStruct = new DiamondStruct() { Diamonds = new Diamond[self.LevelConfig.LieCount] };
            //     self.Diamonds[i] = diamondStruct;
            // }

            self.Diamonds = new Diamond[ConstValue.HangCount * ConstValue.LieCount];
            List<DiamondInfo> diamondInfos = new List<DiamondInfo>();
            int[] map =
            {
                1, 2, 3, 4, 5, 4, 1, 1, 
                2, 3, 4, 5, 5, 1, 2, 3, 
                3, 2, 5, 4, 3, 2, 3, 2,
                4, 1, 3, 3, 2, 5, 2, 1, 
                5, 5, 1, 2, 1, 1, 5, 5, 
                4, 5, 2, 1, 4, 2, 4, 5,
                1, 4, 3, 4, 5, 1, 1, 4, 
                2, 3, 4, 5, 5, 1, 2, 3
            };
            for (var i = 0; i < ConstValue.HangCount; i++)
            {
                for (var j = 0; j < ConstValue.LieCount; j++)
                {
                    int colorType = map[i * ConstValue.LieCount + j];
                    Diamond diamond = self.CreateOneDiamond(colorType, 0);
                    diamond.SetIndex(j, i);
                    // diamond.DiamondType = map[];
                    // self.Diamonds[j, i] = diamond;
                    self.SetDiamondToList(j, i, diamond);
                    diamondInfos.Add(diamond.GetMessageInfo());
                }
            }

            return diamondInfos;
        }

        public static void SetDiamondToList(this DiamondComponent self, int lie, int hang, Diamond diamond)
        {
            // self.Diamonds[hang].Diamonds[lie] = diamond;
            var index = ConstValue.LieCount * hang + lie;
            if (diamond != null)
            {
                diamond.SetIndex(lie, hang);
            }

            self.Diamonds[index] = diamond;
        }

        public static async ETTask RemoveChild(this DiamondComponent self, Diamond diamond)
        {
            await diamond.Destroy();
        }

        // public static void RemoveAllChild(this DiamondComponent self)
        // {
        //     Log.Debug("remove all child");
        //     List<Diamond> diamonds = self.GetChilds<Diamond>();
        //     if (diamonds != null)
        //     {
        //         foreach (var diamond in diamonds)
        //         {
        //             diamond.Destroy().Coroutine();
        //         }
        //     }
        //     // foreach (var diamond in diamonds)
        //     // {
        //     //     // diamond.Destroy().Coroutine();
        //     // }
        // }

        public static Diamond CreateOneDiamond(this DiamondComponent self, int colorType, int boomType)
        {
            long id = IdGenerater.Instance.GenerateId();
            Diamond diamond = self.AddChildWithId<Diamond, int, int>(id, colorType, boomType);
            return diamond;
        }

        public static Diamond CreateOneDiamond(this DiamondComponent self)
        {
            // long id = IdGenerater.Instance.GenerateId();
            // Log.Debug($"create one diamond {id}");
            // Diamond diamond = self.AddChildWithId<Diamond>(id);

            // int[] keys = DiamondTypeConfigCategory.Instance.GetAll().Keys.ToArray();
            //todo test
            // keys = new[] { 1, 2, 3, 4, 5 };
            // var randomIndex = RandomHelper.RandomNumber(0, keys.Length);
            // int configIndex = keys[randomIndex];
            // diamond.DiamondType = configIndex;

            var keysStr = self.LevelConfig.DiamondTypes;

            var kerStrs = keysStr.Split(',');
            var randomIndex = RandomHelper.RandomNumber(0, kerStrs.Length);
            int key = int.Parse(kerStrs[randomIndex].ToString());
            return self.CreateOneDiamond(key, 0);
            // return diamond;
        }

        public static async ETTask CreateDiamoneWithMessage(this DiamondComponent self, DiamondInfo diamondInfo)
        {
            long id = diamondInfo.Id;
            ETTask tsk = ETTask.Create();
            self.AddChildWithId<Diamond, DiamondInfo, ETTask>(id, diamondInfo, tsk);
            // diamond.InitWithMessageInfo(diamondInfo);
            await tsk.GetAwaiter();
            // return diamond;
        }

        public static void InitMapWithMessage(this DiamondComponent self, M2C_InitMapData message)
        {
            foreach (var diamondInfo in message.DiamondInfo)
            {
                self.CreateDiamoneWithMessage(diamondInfo).Coroutine();
            }
        }

        // public static  Diamond Remove
        public static Diamond GetDiamond(this DiamondComponent self, int LieIndex, int HangIndex)
        {
            // Log.Debug($"level config {self.LevelConfig}");
            // return null;
            // Diamond diamond = null;
            if (LieIndex < 0 || HangIndex < 0 || LieIndex >= ConstValue.LieCount || HangIndex >= ConstValue.HangCount)
            {
                return null;
            }

            // return self.Diamonds[LieIndex, HangIndex];
            // return self.Diamonds[HangIndex].Diamonds[LieIndex];
            var index = ConstValue.LieCount * HangIndex + LieIndex;
            if (index >= self.Diamonds.Length)
            {
                return null;
            }

            return self.Diamonds[index];
        }

        public static Diamond GetDiamondWithDir(this DiamondComponent self, Diamond diamond, int type)
        {
            if (diamond == null)
            {
                return null;
            }
            int lieIndex = diamond.LieIndex;
            int hangIndex = diamond.HangIndex;
            switch (type)
            {
                case (int) ScrollDirType.Down:
                    hangIndex -= 1;
                    break;
                case (int) ScrollDirType.Left:
                    lieIndex -= 1;
                    break;
                case (int) ScrollDirType.Right:
                    lieIndex += 1;
                    break;
                case (int) ScrollDirType.Up:
                    hangIndex += 1;
                    break;
            }

            // if (hangIndex < 0 || hangIndex >= self.LevelConfig.HangCount || lieIndex < 0 || lieIndex >= self.LevelConfig.LieCount)
            // {
            // return null;
            // }

            // return self.Diamonds[lieIndex, hangIndex];
            // return self.Diamonds[hangIndex].Diamonds[lieIndex];
            return self.GetDiamond(lieIndex, hangIndex);
        }

        public static DiamondActionItem SwapDiamondPos(this DiamondComponent self, Diamond diamond1, Diamond diamond2)
        {
            DiamondActionItem diamondActionItem = new DiamondActionItem();
            int LieIndex1 = diamond1.LieIndex;
            int HangIndex1 = diamond1.HangIndex;

            int LieIndex2 = diamond2.LieIndex;
            int HangIndex2 = diamond2.HangIndex;
            // self.Diamonds[LieIndex1, HangIndex1] = diamond2;
            self.SetDiamondToList(LieIndex1, HangIndex1, diamond2);
            diamond2.SetIndex(LieIndex1, HangIndex1);
            diamondActionItem.DiamondActions.Add(new DiamondAction()
            {
                DiamondInfo = diamond2.GetMessageInfo(), ActionType = (int) DiamondActionType.Move
            });

            // self.Diamonds[LieIndex2, HangIndex2] = diamond1;
            self.SetDiamondToList(LieIndex2, HangIndex2, diamond1);
            diamond1.SetIndex(LieIndex2, HangIndex2);
            diamondActionItem.DiamondActions.Add(new DiamondAction()
            {
                DiamondInfo = diamond1.GetMessageInfo(), ActionType = (int) DiamondActionType.Move
            });
            return diamondActionItem;
        }

        //获得列上的可以消除的列表
        public static List<List<Diamond>> GetLieCrashListList(this DiamondComponent self)
        {
            List<List<Diamond>> crashListList = new List<List<Diamond>>();
            for (int i = 0; i < ConstValue.HangCount; i++)
            {
                for (int j = 0; j < ConstValue.LieCount; j++)
                {
                    // Diamond diamond = self.Diamonds[j, i];
                    Diamond diamond = self.GetDiamond(j, i);
                    if (diamond == null)
                    {
                        continue;
                    }

                    if (self.CheckIsContainInListList(crashListList, diamond))
                    {
                        continue;
                    }

                    List<Diamond> sameLieList = self.CheckLieSameDiamond(diamond);
                    if (sameLieList.Count >= 3)
                    {
                        crashListList.Add(sameLieList);
                    }
                }
            }

            return crashListList;
        }

        public static List<Diamond> CheckLieSameDiamond(this DiamondComponent self, Diamond currentDiamond)
        {
            //todo 找到并保存相同类型的宝石
            List<Diamond> sameList = new List<Diamond>();
            //寻找上下方向上是否有相同类型的宝石
            Queue<Diamond> checkQueue = new Queue<Diamond>();
            checkQueue.Enqueue(currentDiamond);
            sameList.Add(currentDiamond);
            List<Diamond> alCheckList = new List<Diamond>();
            while (checkQueue.Count > 0)
            {
                Diamond diamond = checkQueue.Dequeue();
                if (alCheckList.Contains(diamond))
                {
                    continue;
                }

                alCheckList.Add(diamond);
                ScrollDirType[] scrollDirTypes = { ScrollDirType.Down, ScrollDirType.Up };
                for (var h = 0; h < scrollDirTypes.Length; h++)
                {
                    var type = scrollDirTypes[h];
                    Diamond findDiamond = self.GetDiamondWithDir(diamond, (int) type);

                    if (findDiamond != null)
                    {
                        if (alCheckList.Contains(findDiamond))
                        {
                            continue;
                        }

                        if (findDiamond.DiamondType == diamond.DiamondType)
                        {
                            checkQueue.Enqueue(findDiamond);
                            sameList.Add(findDiamond);
                        }
                    }
                }
            }

            return sameList;
        }

        public static bool CheckIsContainInListList<T>(this DiamondComponent self, List<List<T>> listlist, T target)
        {
            foreach (var list in listlist)
            {
                if (list.Contains(target))
                {
                    return true;
                }
            }

            return false;
        }

        public static List<List<Diamond>> GetHangCrashListList(this DiamondComponent self)
        {
            List<List<Diamond>> crashListList = new List<List<Diamond>>();
            for (int i = 0; i < ConstValue.HangCount; i++)
            {
                for (int j = 0; j < ConstValue.LieCount; j++)
                {
                    // Diamond diamond = self.Diamonds[j, i];
                    Diamond diamond = self.GetDiamond(j, i);
                    if (diamond == null)
                    {
                        continue;
                    }

                    if (self.CheckIsContainInListList(crashListList, diamond))
                    {
                        continue;
                    }

                    List<Diamond> sameHangList = self.CheckHangSameDiamond(diamond);
                    if (sameHangList.Count >= 3)
                    {
                        Log.Debug("same hang list =  " + sameHangList.Count);
                        crashListList.Add(sameHangList);
                    }
                }
            }

            return crashListList;
        }

        public static List<Diamond> CheckHangSameDiamond(this DiamondComponent self, Diamond currentDiamond)
        {
            //todo 找到并保存相同类型的宝石
            List<Diamond> sameList = new List<Diamond>();
            //寻找上下方向上是否有相同类型的宝石
            Queue<Diamond> checkQueue = new Queue<Diamond>();
            checkQueue.Enqueue(currentDiamond);
            sameList.Add(currentDiamond);

            List<Diamond> alCheckList = new List<Diamond>();
            while (checkQueue.Count > 0)
            {
                Diamond diamond = checkQueue.Dequeue();
                if (alCheckList.Contains(diamond))
                {
                    continue;
                }

                alCheckList.Add(diamond);

                ScrollDirType[] scrollDirTypes = new ScrollDirType[2] { ScrollDirType.Left, ScrollDirType.Right };
                for (var h = 0; h < scrollDirTypes.Length; h++)
                {
                    var type = scrollDirTypes[h];
                    Diamond findDiamond = self.GetDiamondWithDir(diamond, (int) type);

                    if (findDiamond != null)
                    {
                        if (alCheckList.Contains(findDiamond))
                        {
                            continue;
                        }

                        if (findDiamond.DiamondType == diamond.DiamondType)
                        {
                            checkQueue.Enqueue(findDiamond);
                            sameList.Add(findDiamond);
                        }
                    }
                }
            }

            return sameList;
        }

        public static bool CheckListIsOver<T>(this DiamondComponent self, List<T> list1, List<T> list2)
        {
            foreach (var target in list1)
            {
                if (list2.Contains(target))
                {
                    return true;
                }
            }

            //检查列表是否重合
            return false;
        }

        public static bool CheckCrash(this DiamondComponent self, List<DiamondActionItem> diamondActionItems, Diamond touchDiamond,
        Diamond swapDiamond,
        Queue<Diamond> specialDiamonds, bool isFirstAddSpecial)
        {
            List<List<Diamond>> lieCrashListList = self.GetLieCrashListList();
            List<List<Diamond>> hangCrashListList = self.GetHangCrashListList();
            List<List<Diamond>> endListList = new List<List<Diamond>>();
            // //行列 查重
            Log.Debug("查重");
            foreach (var lieList in lieCrashListList)
            {
                for (var i = 0; i < hangCrashListList.Count; i++)
                {
                    if (self.CheckListIsOver(lieList, hangCrashListList[i]))
                    {
                        var hangList = hangCrashListList[i];
                        hangCrashListList.RemoveAt(i);
                        i--;
                        foreach (var target in hangList)
                        {
                            if (!lieList.Contains(target))
                            {
                                lieList.Add(target);
                            }
                        }
                    }
                }
            }

            //
            foreach (var lieList in lieCrashListList)
            {
                endListList.Add(lieList);
            }

            //
            foreach (var hangList in hangCrashListList)
            {
                endListList.Add(hangList);
            }

            //
            Log.Debug("組裝action");
            DiamondActionItem diamondActionItem = new DiamondActionItem();
            foreach (var crashList in endListList)
            {
                foreach (var diamond in crashList)
                {
                    DiamondAction diamondAction = new DiamondAction();
                    diamondAction.ActionType = (int) DiamondActionType.Destory;
                    diamondAction.DiamondInfo = diamond.GetMessageInfo();
                    // self.Diamonds[diamond.LieIndex, diamond.HangIndex] = null;
                    self.SetDiamondToList(diamond.LieIndex, diamond.HangIndex, null);
                    diamond.Dispose();
                    diamondActionItem.DiamondActions.Add(diamondAction);
                }
            }

            DiamondActionItem specialActionItem = new DiamondActionItem();
            Log.Debug("特殊宝石");
            if (isFirstAddSpecial)
            {
                foreach (var list in endListList)
                {
                    //增加特殊钻石
                    DiamondAction diamondAction = self.AddSpecialDiamond(list, touchDiamond, swapDiamond, specialDiamonds);
                    if (diamondAction != null)
                    {
                        // diamondActionItem.DiamondActions.Add(diamondAction);
                        specialActionItem.DiamondActions.Add(diamondAction);
                        Log.Debug("增加特殊宝石");
                    }
                }
            }

            if (specialActionItem.DiamondActions.Count > 0)
            {
                diamondActionItems.Add(specialActionItem);
            }

            if (diamondActionItem.DiamondActions.Count > 0)
            {
                diamondActionItems.Add(diamondActionItem);
                return true;
            }

            return false;
        }

        public static M2C_SyncDiamondAction ScrollDiamond(this DiamondComponent self, C2M_PlayerScrollScreen message)
        {
            //todo 滑动钻石
            Log.Debug("screen diamond");
            int LieIndex = message.StartX;
            int HangIndex = message.StartY;
            Log.Debug("process scroll screen message");
            // self.DomainScene().GetComponent<DiamondComponent>().ScrollDiamond(message);
            M2C_SyncDiamondAction m2CSyncDiamondAction = new M2C_SyncDiamondAction();
            Diamond diamond = self.GetDiamond(LieIndex, HangIndex);
            Diamond nextDiamond = self.GetDiamondWithDir(diamond, message.DirType);
            if (diamond != null && nextDiamond != null)
            {
                // M2C_SyncDiamondAction m2CSyncDiamondAction = new M2C_SyncDiamondAction();
                m2CSyncDiamondAction.DiamondActionItems.Add(self.SwapDiamondPos(diamond, nextDiamond));
                bool isCrash = true;
                bool isCrashSuccess = false;
                bool isMoveDown = false;
                bool isFirstAddSpecial = true;
                // bool isSpecial = false;
                Queue<Diamond> specialDiamonds = new Queue<Diamond>();
                while (isCrash || isMoveDown || specialDiamonds.Count > 0)
                {
                    isCrash = self.CheckCrash(m2CSyncDiamondAction.DiamondActionItems, diamond, nextDiamond, specialDiamonds, isFirstAddSpecial);
                    Log.Debug($"is crash {isCrash}");
                    if (isFirstAddSpecial)
                    {
                        isFirstAddSpecial = false;
                    }

                    Log.Debug("下落");
                    isMoveDown = self.MoveDownAllDiamond(m2CSyncDiamondAction.DiamondActionItems);
                    Log.Debug($"is move down {isMoveDown}");
                    if (isCrashSuccess == false && isCrash)
                    {
                        isCrashSuccess = true;
                    }

                    if (specialDiamonds.Count > 0)
                    {
                        Diamond specialDiamond = specialDiamonds.Dequeue();
                        Log.Debug("消除特殊宝石");
                        self.AutoCastSpecialDiamond(m2CSyncDiamondAction.DiamondActionItems, specialDiamond);
                    }

                    Log.Debug($"special diamond {specialDiamonds.Count}");
                }

                if (isCrashSuccess)
                {
                    // self.ToggleTurnSeatIndex();
                    // self.syncCurrentTurnIndex();
                }
                else
                {
                    //todo 交换失败。反向交换
                    m2CSyncDiamondAction.DiamondActionItems.Add(self.SwapDiamondPos(diamond, nextDiamond));
                }

                //todo 下发交换action 消息
                // foreach (var unit in self.Units)
                // {
                //     MessageHelper.SendToClient(unit, m2CSyncDiamondAction);
                // }
            }

            return m2CSyncDiamondAction;
        }

        public static bool MoveDownAllDiamond(this DiamondComponent self, List<DiamondActionItem> diamondActionItems)
        {
            bool isMoveDown = false;
            //todo 将宝石都向下移动
            DiamondActionItem moveActionItem = new DiamondActionItem();
            DiamondActionItem createActionItem = new DiamondActionItem();
            // DiamondComponent diamondComponent = self.DiamondComponent;
            //遍历每一列
            for (var i = 0; i < ConstValue.LieCount; i++)
            {
                List<Diamond> diamonds = new List<Diamond>();
                //找到这一列有多少是空的
                for (var j = 0; j < ConstValue.HangCount; j++)
                {
                    Diamond diamond = self.GetDiamond(i, j);
                    if (diamond != null)
                    {
                        diamonds.Add(diamond);
                    }
                }

                for (var j = 0; j < ConstValue.HangCount; j++)
                {
                    if (j < diamonds.Count)
                    {
                        Diamond diamond = diamonds[j];
                        if (!diamond.EqualsIndex(i, j))
                        {
                            // self.Diamonds[i, j] = diamond;
                            self.SetDiamondToList(i, j, diamond);
                            diamond.SetIndex(i, j);
                            DiamondAction action = new DiamondAction();
                            action.ActionType = (int) DiamondActionType.Move;
                            action.DiamondInfo = diamond.GetMessageInfo();
                            moveActionItem.DiamondActions.Add(action);
                        }
                    }
                    else
                    {
                        Diamond diamond = self.CreateOneDiamond();
                        // self.Diamonds[i, j] = diamond;
                        self.SetDiamondToList(i, j, diamond);
                        diamond.InitLieIndex = i;
                        diamond.InitHangIndex = ConstValue.HangCount + j;
                        diamond.SetIndex(i, j);
                        DiamondAction action = new DiamondAction();
                        action.ActionType = (int) DiamondActionType.Create;
                        action.DiamondInfo = diamond.GetMessageInfo();
                        createActionItem.DiamondActions.Add(action);
                    }
                }
            }

            if (moveActionItem.DiamondActions.Count > 0)
            {
                diamondActionItems.Add(moveActionItem);
                // isMoveDown = true;
            }

            if (createActionItem.DiamondActions.Count > 0)
            {
                diamondActionItems.Add(createActionItem);
                isMoveDown = true;
            }

            return isMoveDown;
        }

        public static DirectionType GetListDirection(this DiamondComponent self, List<Diamond> list)
        {
            //获取当前列表的方向
            Dictionary<int, bool> lieMap = new Dictionary<int, bool>();
            Dictionary<int, bool> hangMap = new Dictionary<int, bool>();
            foreach (var diamond in list)
            {
                if (!lieMap.ContainsKey(diamond.LieIndex))
                {
                    lieMap.Add(diamond.LieIndex, true);
                }

                if (!hangMap.ContainsKey(diamond.HangIndex))
                {
                    hangMap.Add(diamond.HangIndex, true);
                }
            }

            Log.Debug("Lie map = " + lieMap.Count);
            Log.Debug("Hang map = " + hangMap.Count);
            if (lieMap.Count == list.Count)
            {
                return DirectionType.Horizontal;
            }

            if (hangMap.Count == list.Count)
            {
                return DirectionType.Vertical;
            }

            return DirectionType.Cross;
        }

        /*
        * 增加特殊钻石的逻辑
        **/
        public static DiamondAction AddSpecialDiamond(this DiamondComponent self, List<Diamond> crashList, Diamond touchDiamond, Diamond swapDiamond,
        Queue<Diamond> specialDiamonds)
        {
            DiamondAction diamondAction = null;

            if (crashList.Count >= 4)
            {
                // DiamondComponent diamondComponent = self.DomainScene().GetComponent<DiamondComponent>();
                // Diamond diamond = self.CreateOneDiamond();
                DirectionType directionType = self.GetListDirection(crashList);
                Log.Debug($"direction type {directionType}");
                Diamond targetDiamond = null;
                BoomType boomType = BoomType.Invalide;
                switch (directionType)
                {
                    case DirectionType.Horizontal:
                        targetDiamond = self.GetLastLeftDiamond(crashList);
                        if (crashList.Count >= 5)
                        {
                            boomType = BoomType.BlackHole;
                        }
                        else
                        {
                            boomType = BoomType.LazerH;
                        }

                        break;
                    case DirectionType.Vertical:
                        targetDiamond = self.GetLastDownDiamond(crashList);
                        if (crashList.Count >= 5)
                        {
                            boomType = BoomType.BlackHole;
                        }
                        else
                        {
                            boomType = BoomType.LazerV;
                        }

                        break;
                    case DirectionType.Cross:
                        if (crashList.Count >= 5)
                        {
                            Log.Debug("Cross");
                            boomType = BoomType.Boom;
                            targetDiamond = self.GetCrossDiamond(crashList);
                        }

                        break;
                }

                if (crashList.Contains(touchDiamond))
                {
                    targetDiamond = touchDiamond;
                }

                if (crashList.Contains(swapDiamond))
                {
                    targetDiamond = swapDiamond;
                }

                Diamond diamond = self.CreateOneSpecialDiamond(targetDiamond.DiamondType, boomType);
                if (!specialDiamonds.Contains(diamond))
                {
                    Log.Debug("create boom type" + boomType);
                    diamond.InitLieIndex = targetDiamond.LieIndex;
                    diamond.InitHangIndex = targetDiamond.HangIndex;
                    diamond.SetIndex(targetDiamond.LieIndex, targetDiamond.HangIndex);
                    // diamond.DiamondType = targetDiamond.DiamondType;
                    // diamond.BoomType = (int) boomType;
                    // self.Diamonds[diamond.LieIndex, diamond.HangIndex] = diamond;
                    self.SetDiamondToList(diamond.LieIndex, diamond.HangIndex, diamond);
                    diamondAction = new DiamondAction();
                    diamondAction.ActionType = (int) DiamondActionType.Create;
                    diamondAction.DiamondInfo = diamond.GetMessageInfo();
                    specialDiamonds.Enqueue(diamond);
                }
            }

            return diamondAction;
        }

        public static Diamond CreateOneSpecialDiamond(this DiamondComponent self, int diamondType, BoomType boomType)
        {
            return self.CreateOneDiamond(diamondType, (int) boomType);
        }

        public static Diamond GetLastLeftDiamond(this DiamondComponent self, List<Diamond> list)
        {
            Diamond target = null;
            var maxLeftIndex = 1000;
            foreach (var diamond in list)
            {
                if (diamond.LieIndex < maxLeftIndex)
                {
                    maxLeftIndex = diamond.LieIndex;
                    target = diamond;
                }
            }

            return target;
        }

        public static Diamond GetLastRightDiamond(this DiamondComponent self, List<Diamond> list)
        {
            Diamond target = null;
            var maxRightIndex = -1000;
            foreach (var diamond in list)
            {
                if (diamond.LieIndex > maxRightIndex)
                {
                    maxRightIndex = diamond.LieIndex;
                    target = diamond;
                }
            }

            return target;
        }

        public static Diamond GetCrossDiamond(this DiamondComponent self, List<Diamond> list)
        {
            List<Diamond> HList = new List<Diamond>();
            List<Diamond> VList = new List<Diamond>();
            Dictionary<int, int> Lmap = new Dictionary<int, int>();
            Dictionary<int, int> HMap = new Dictionary<int, int>();
            foreach (var diamond in list)
            {
                if (!Lmap.ContainsKey(diamond.LieIndex))
                {
                    Lmap.Add(diamond.LieIndex, 1);
                }
                else
                {
                    Lmap[diamond.LieIndex]++;
                }

                if (!HMap.ContainsKey(diamond.HangIndex))
                {
                    HMap.Add(diamond.HangIndex, 1);
                }
                else
                {
                    HMap[diamond.HangIndex]++;
                }
            }

            var max = 0;
            var LieIndex = 0;
            foreach (var key in Lmap.Keys)
            {
                if (Lmap[key] > max)
                {
                    max = Lmap[key];
                    LieIndex = key;
                }
            }

            max = 0;
            var HangIndex = 0;
            foreach (var key in HMap.Keys)
            {
                if (HMap[key] > max)
                {
                    max = HMap[key];
                    HangIndex = key;
                }
            }

            Log.Debug("Lie index = " + LieIndex);
            Log.Debug("Hang index" + HangIndex);
            foreach (var diamond in list)
            {
                if (diamond.LieIndex == LieIndex)
                {
                    VList.Add(diamond);
                }
            }

            Log.Debug("v list count" + VList.Count);
            foreach (var diamond in list)
            {
                if (diamond.HangIndex == HangIndex)
                {
                    HList.Add(diamond);
                }
            }

            Log.Debug("h list count" + HList.Count);

            foreach (var diamond in HList)
            {
                if (VList.Contains(diamond))
                {
                    return diamond;
                }
            }

            return null;
        }

        public static bool AutoCastSpecialDiamond(this DiamondComponent self, List<DiamondActionItem> diamondActionItems, Diamond diamond)
        {
            bool isSpecial = false;
            DiamondActionItem diamondActionItem = new DiamondActionItem();
            DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(diamond.ConfigId);
            if (diamond != null && config.BoomType != (int) BoomType.Invalide)
            {
                Log.Debug("存在特殊diamond");
                List<Diamond> crashList = new List<Diamond>();
                switch (config.BoomType)
                {
                    case (int) BoomType.Boom:
                        crashList = self.GetCrossLineDiamond(diamond);
                        break;
                    case (int) BoomType.BlackHole:
                        crashList = self.GetAroundDiamond(diamond, 5);
                        break;
                    case (int) BoomType.LazerH:
                        crashList = self.GetLineDiamond(diamond, DirectionType.Horizontal);
                        break;
                    case (int) BoomType.LazerV:
                        crashList = self.GetLineDiamond(diamond, DirectionType.Vertical);
                        break;
                }

                isSpecial = true;
                // crashListList.Add(crashList);
                foreach (var crashDiamond in crashList)
                {
                    Diamond getDiamond = self.GetDiamond(crashDiamond.LieIndex, crashDiamond.HangIndex);
                    if (crashDiamond != null && !crashDiamond.IsDisposed && getDiamond != null)
                    {
                        DiamondAction diamondAction = new DiamondAction();
                        diamondAction.ActionType = (int) DiamondActionType.Destory;
                        diamondAction.DiamondInfo = crashDiamond.GetMessageInfo();
                        // self.Diamonds[crashDiamond.LieIndex, crashDiamond.HangIndex] = null;
                        self.SetDiamondToList(crashDiamond.LieIndex, crashDiamond.HangIndex, null);
                        crashDiamond.Dispose();
                        diamondActionItem.DiamondActions.Add(diamondAction);
                    }
                }
            }

            if (diamondActionItem.DiamondActions.Count > 0)
            {
                diamondActionItems.Add(diamondActionItem);
            }

            return isSpecial;
        }

        public static List<Diamond> GetLineDiamond(this DiamondComponent self, Diamond diamond, DirectionType directionType)
        {
            List<Diamond> list = new List<Diamond>();
            var count = directionType == DirectionType.Horizontal? ConstValue.LieCount : ConstValue.HangCount;
            for (int i = 0; i < count; i++)
            {
                // Diamond target = self.Diamonds[directionType == DirectionType.Horizontal? i : diamond.LieIndex,
                //     directionType == DirectionType.Horizontal? diamond.HangIndex : i];
                Diamond target = self.GetDiamond(directionType == DirectionType.Horizontal? i : diamond.LieIndex,
                    directionType == DirectionType.Horizontal? diamond.HangIndex : i);

                DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(target.ConfigId);
                if (!target.Equals(diamond) && config.BoomType != (int) BoomType.Invalide)
                {
                    //todo 如果被消除的对象是一个特殊猪，并且它不与原来猪是同一颗
                }
                else
                {
                    list.Add(target);
                }
            }

            return list;
        }

        public static List<Diamond> GetCrossLineDiamond(this DiamondComponent self, Diamond diamond)
        {
            List<Diamond> list = new List<Diamond>();
            //获取交叉点上所有钻石
            for (int i = 0; i < ConstValue.HangCount; i++)
            {
                // Diamond target = self.Diamonds[diamond.LieIndex, i];
                Diamond target = self.GetDiamond(diamond.LieIndex, i);
                DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(target.ConfigId);
                if (!target.Equals(diamond) && config.BoomType != (int) BoomType.Invalide)
                {
                    //todo 如果被消除的对象是一个特殊猪，并且它不与原来猪是同一颗
                }
                else
                {
                    list.Add(target);
                }
            }

            for (int i = 0; i < ConstValue.LieCount; i++)
            {
                // Diamond target = self.Diamonds[i, diamond.HangIndex];
                Diamond target = self.GetDiamond(i, diamond.HangIndex);
                DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(target.ConfigId);
                if (!target.Equals(diamond) && config.BoomType != (int) BoomType.Invalide)
                {
                    //todo 如果被消除的对象是一个特殊猪，并且它不与原来猪是同一颗
                }
                else
                {
                    if (!list.Contains(target))
                    {
                        list.Add(target);
                    }
                }
            }

            return list;
        }

        public static List<Diamond> GetAroundDiamond(this DiamondComponent self, Diamond diamond, int count)
        {
            var startLieIndex = diamond.LieIndex - (count - 1) / 2;
            var endLieIndex = startLieIndex + count;
            var startHangIndex = diamond.HangIndex - (count - 1) / 2;
            var endHangIndex = startHangIndex + count;
            List<Diamond> list = new List<Diamond>();
            for (int i = startHangIndex; i < endHangIndex; i++)
            {
                for (int j = startLieIndex; j < endLieIndex; j++)
                {
                    Diamond targetDiamond = self.GetDiamond(j, i);
                    if (targetDiamond != null)
                    {
                        DiamondTypeConfig config = DiamondTypeConfigCategory.Instance.Get(targetDiamond.ConfigId);
                        if (!targetDiamond.Equals(diamond) && config.BoomType != (int) BoomType.Invalide)
                        {
                            //todo 如果被消除的对象是一个特殊猪，并且它不与原来猪是同一颗
                        }
                        else
                        {
                            list.Add(targetDiamond);
                        }
                    }
                }
            }

            return list;
        }

        public static Diamond GetLastDownDiamond(this DiamondComponent self, List<Diamond> list)
        {
            Diamond target = null;
            var minHangIndex = 1000;
            foreach (var diamond in list)
            {
                if (diamond.HangIndex < minHangIndex)
                {
                    minHangIndex = diamond.HangIndex;
                    target = diamond;
                }
            }

            return target;
        }
    }
}