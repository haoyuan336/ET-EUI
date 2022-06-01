using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public static class DlgHeroStrengthenPreviewLayerSystem
    {
        public static void RegisterUIEvent(this DlgHeroStrengthenPreviewLayer self)
        {
            self.CurrentCommonHeroCard =
                    self.AddChildWithId<ESCommonHeroCard, Transform>(IdGenerater.Instance.GenerateId(), self.View.E_CurrentImage.transform);

            self.View.E_BackButton.AddListener(() =>
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_HeroStrengthenPreviewLayer);
            });

            for (int i = 0; i < 5; i++)
            {
                ESCommonHeroCard heroCard = self.AddChildWithId<ESCommonHeroCard, Transform, bool>(IdGenerater.Instance.GenerateId(),
                    self.View.E_ChooseCardGroupImage.transform, true);
                self.ESCommonHeroCards.Add(heroCard);
                heroCard.OnButtonClick = self.ShowChooseHeroPlane;
            }

            self.View.E_OkButtonButton.AddListenerAsync(self.OnOkButtonClick);
        }

        public static async ETTask OnOkButtonClick(this DlgHeroStrengthenPreviewLayer self)
        {
            Log.Debug("强化");
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;

            M2C_StrenthenHeroResponse response = (M2C_StrenthenHeroResponse) await session.Call(new C2M_StrenthenHeroRequest()
            {
                AccountId = accountId, TargetHeroCardInfo = self.HeroCardInfo, ChooseHeroCardInfos = self.AlChooseHeroCardInfo
            });
            if (response.Error == ErrorCode.ERR_Success)
            {
                Log.Debug("强化成功");
                self.AlChooseHeroCardInfo.Clear();
                self.HeroCardInfo = response.HeroCardInfo;
                self.SetTargetInfo(self.HeroCardInfo);
                // self.View.ETargetHeroContentLoopHorizontalScrollRect.RefreshCells();
                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                UIBaseWindow baseWindow = uiComponent.AllWindowsDic[(int) WindowID.WindowID_AllHeroBagLayer];
                baseWindow.GetComponent<DlgAllHeroBagLayer>().ReferView();
                // self.View.E_OKButton.gameObject.SetActive(false);
                // UIBaseWindow showHeroInfoLayer = uiComponent.GetUIBaseWindow(WindowID.WindowID_ShowHeroInfoLayer);
                // showHeroInfoLayer.GetComponent<DlgShowHeroInfoLayer>().ReferHeroCardView(response.HeroCardInfo);
            }

            await ETTask.CompletedTask;
        }

        public static void HideWindow(this DlgHeroStrengthenPreviewLayer self)
        {
            self.AlChooseHeroCardInfo.Clear();
            foreach (var heroCard in self.ESCommonHeroCards)
            {
                heroCard.SetHeroCardInfo(null);
            }
        }

        public static async void ShowChooseHeroPlane(this DlgHeroStrengthenPreviewLayer self)
        {
            //显示选择英雄面板
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_AllHeroBagLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_AllHeroBagLayer);
            baseWindow.GetComponent<DlgAllHeroBagLayer>().UnAbleHeroItemWhitHeroInfo(self.HeroCardInfo);
            baseWindow.GetComponent<DlgAllHeroBagLayer>().SetAllChooseHeroCardInfos(self.AlChooseHeroCardInfo);
            if (self.CheckIsFull())
            {
                baseWindow.GetComponent<DlgAllHeroBagLayer>().EnableItemWhitHeroInfos(self.AlChooseHeroCardInfo);
            }

            // baseWindow.GetComponent<DlgAllHeroBagLayer>().SetEnableSameStarCountHeroInfo(self.HeroCardInfo);
            RectTransform rectTransform = baseWindow.uiTransform.GetComponent<RectTransform>();
            // baseWindow.GetComponent<DlgAllHeroBagLayer>().ReferView();
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, -300);
            rectTransform.offsetMin = new Vector2(0, 0);

            await uiComponent.ShowWindow(WindowID.WindowID_BackButton);
            UIBaseWindow backBaseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_BackButton);
            backBaseWindow.uiTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(166, -205);
            backBaseWindow.GetComponent<DlgBackButton>().BackButtonClickAction = () =>
            {
                uiComponent.HideWindow(WindowID.WindowID_AllHeroBagLayer);
                uiComponent.HideWindow(WindowID.WindowID_BackButton);
            };
            baseWindow.GetComponent<DlgAllHeroBagLayer>().OnHeroItemInfoClick = null;
            baseWindow.GetComponent<DlgAllHeroBagLayer>().OnHeroItemInfoClick = self.OnHeroItemClick;
            // await TimerComponent.Instance.WaitAsync(1000);
            // baseWindow.GetComponent<DlgAllHeroBagLayer>().SetAllChooseHeroCardInfos(self.AlChooseHeroCardInfo);
        }

        public static bool CheckIsFull(this DlgHeroStrengthenPreviewLayer self)
        {
            var count = 0;
            foreach (var info in self.AlChooseHeroCardInfo)
            {
                count += info.Count;
            }

            var full = count == 5;
            return full;
        }

        public static void OnHeroItemClick(this DlgHeroStrengthenPreviewLayer self, HeroCardInfo heroCardInfo, Scroll_ItemHeroCard itemHeroCard,
        bool value)
        {
            var config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            HeroCardInfo fingInfo = self.AlChooseHeroCardInfo.Find(a => a.HeroId.Equals(heroCardInfo.HeroId));
            var isFull = self.CheckIsFull();
            if (config.MaterialType == (int) HeroBagType.Materail)
            {
                Log.Debug($"materail type {config.MaterialType}");
                if (value)
                {
                    self.ShowAddSubPlane(itemHeroCard, heroCardInfo);
                }

                itemHeroCard.E_ChooseToggle.isOn = false;
                return;
            }
            else
            {
                if (fingInfo == null)
                {
                    if (isFull)
                    {
                        itemHeroCard.E_ChooseToggle.isOn = false;
                        return;
                    }

                    if (value && !isFull)
                    {
                        self.AlChooseHeroCardInfo.Add(heroCardInfo);
                    }
                }
                else
                {
                    if (!value)
                    {
                        self.AlChooseHeroCardInfo.Remove(fingInfo);
                    }
                }
            }

            UIBaseWindow baseWindow = self.DomainScene().GetComponent<UIComponent>().AllWindowsDic[(int) WindowID.WindowID_AllHeroBagLayer];
            isFull = self.CheckIsFull();
            if (isFull)
            {
                //如果选择满了。那么需要禁用其他按钮
                //首先找到所有英雄的实力
                baseWindow.GetComponent<DlgAllHeroBagLayer>().EnableItemWhitHeroInfos(self.AlChooseHeroCardInfo);
            }
            else
            {
                baseWindow.GetComponent<DlgAllHeroBagLayer>().EnableItemWhitHeroInfos(null);
            }

            baseWindow.GetComponent<DlgAllHeroBagLayer>().SetAllChooseHeroCardInfos(self.AlChooseHeroCardInfo);
            self.ReferChooseHeroCardView();
        }

        public static void ShowWindow(this DlgHeroStrengthenPreviewLayer self, Entity contextData = null)
        {
        }

        public static void SetTargetInfo(this DlgHeroStrengthenPreviewLayer self, HeroCardInfo heroCardInfo)
        {
            self.HeroCardInfo = heroCardInfo;
            self.CurrentCommonHeroCard.SetHeroCardInfo(self.HeroCardInfo);
            self.View.E_CurrentLevelText.text = $"{self.HeroCardInfo.Level}";
            self.View.E_NextLevelText.text = "-(-)";
            self.View.E_CurrentHPText.text = HeroHelper.GetHeroBaseHP(self.HeroCardInfo).ToString();
            self.View.E_CurrentDefenceText.text = HeroHelper.GetHeroBaseDefence(self.HeroCardInfo).ToString();
            self.View.E_CurrentAttackText.text = HeroHelper.GetHeroBaseAttack(self.HeroCardInfo).ToString();
            self.View.E_NextAttackText.text = "-(-)";
            self.View.E_NextDefenceText.text = "-(-)";
            self.View.E_NextHPText.text = "-(-)";
            self.View.E_CurLevelText.text = $"LV.{self.HeroCardInfo.Level}";
            var currentExp = heroCardInfo.CurrentExp;
            Log.Debug($"current exp {currentExp}");
            var needExp = HeroHelper.GetNextLevelExp(self.HeroCardInfo);

            self.View.E_ExpBarImage.fillAmount = (float) currentExp / needExp;
            self.View.E_LastExpText.text = $"距离升级:{needExp - currentExp}EXP";
            self.View.E_TotalExpText.text = "累计经验0EXP";
            self.View.E_EndLevelText.text = "LV{-}";

            self.View.E_OkButtonButton.interactable = false;

            foreach (var heroCard in self.ESCommonHeroCards)
            {
                heroCard.SetHeroCardInfo(null);
            }
        }

        public static async void ShowAddSubPlane(this DlgHeroStrengthenPreviewLayer self, Scroll_ItemHeroCard itemHeroCard, HeroCardInfo heroCardInfo)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_AddSubPlane, WindowID.WindowID_Invaild,
                new ShowWindowData() { contextData = itemHeroCard });

            UIBaseWindow baseWindow = uiComponent.AllWindowsDic[(int) WindowID.WindowID_AddSubPlane];
            // baseWindow.uiTransform.position = itemHeroCard.uiTransform.position;
            // itemWeapon.uiTransform.position;
            DlgAddSubPlane dlgAddSubPlane = baseWindow.GetComponent<DlgAddSubPlane>();
            dlgAddSubPlane.View.E_ContentImage.transform.position = itemHeroCard.E_ChooseToggle.transform.position;

            DlgAllHeroBagLayer dlgAllHeroBagLayer =
                    uiComponent.AllWindowsDic[(int) WindowID.WindowID_AllHeroBagLayer].GetComponent<DlgAllHeroBagLayer>();

            dlgAddSubPlane.EnableAddButton(!self.CheckIsFull());

            HeroCardInfo findInfo = self.AlChooseHeroCardInfo.Find(a => a.HeroId.Equals(heroCardInfo.HeroId));
            if (findInfo != null)
            {
                if (findInfo.Count == heroCardInfo.Count)
                {
                    dlgAddSubPlane.EnableAddButton(false);
                }
            }

            // var isFull = self.CheckIsFull();
            dlgAddSubPlane.AddAction = null;
            dlgAddSubPlane.AddAction = () =>
            {
                var isFull = self.CheckIsFull();
                if (!isFull)
                {
                    if (findInfo == null)
                    {
                        Log.Debug($"choose hero level{heroCardInfo.Level}");
                        findInfo = new HeroCardInfo()
                        {
                            HeroId = heroCardInfo.HeroId,
                            Count = 1,
                            ConfigId = heroCardInfo.ConfigId,
                            Level = heroCardInfo.Level,
                            Star = heroCardInfo.Star,
                            Rank = heroCardInfo.Rank
                        };
                        self.AlChooseHeroCardInfo.Add(findInfo);
                    }
                    else
                    {
                        findInfo.Count++;
                    }

                    dlgAddSubPlane.EnabelSubButton(true);

                    if (findInfo.Count == heroCardInfo.Count)
                    {
                        dlgAddSubPlane.EnableAddButton(false);
                    }

                    dlgAllHeroBagLayer.SetAllChooseHeroCardInfos(self.AlChooseHeroCardInfo);
                    self.ReferChooseHeroCardView();
                }

                isFull = self.CheckIsFull();
                // self.View.E_OKButton.gameObject.SetActive(isFull);
                if (isFull)
                {
                    dlgAddSubPlane.EnableAddButton(false);
                    uiComponent.AllWindowsDic[(int) WindowID.WindowID_AllHeroBagLayer].GetComponent<DlgAllHeroBagLayer>()
                            .EnableItemWhitHeroInfos(self.AlChooseHeroCardInfo);
                }
            };
            dlgAddSubPlane.SubAction = null;
            dlgAddSubPlane.SubAction = () =>
            {
                // var findInfo = self.AlChooseHeroCardInfo.Find(a => a.HeroId.Equals(heroCardInfo.HeroId));
                if (findInfo != null && findInfo.Count > 0)
                {
                    findInfo.Count--;
                    itemHeroCard.E_ChooseCountText.text = findInfo.Count.ToString();

                    if (findInfo.Count == 0)
                    {
                        self.AlChooseHeroCardInfo.Remove(findInfo);
                        dlgAddSubPlane.EnabelSubButton(false);
                        itemHeroCard.E_ChooseCountText.gameObject.SetActive(false);
                    }

                    dlgAddSubPlane.EnableAddButton(true);
                    uiComponent.AllWindowsDic[(int) WindowID.WindowID_AllHeroBagLayer].GetComponent<DlgAllHeroBagLayer>()
                            .EnableItemWhitHeroInfos(null);
                }

                self.ReferChooseHeroCardView();
            };
        }

        public static void ReferChooseHeroCardView(this DlgHeroStrengthenPreviewLayer self)
        {
            List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
            foreach (var heroCardInfo in self.AlChooseHeroCardInfo)
            {
                Log.Debug($"heroc ardinfo {heroCardInfo.ConfigId}");
                for (int i = 0; i < heroCardInfo.Count; i++)
                {
                    var info = new HeroCardInfo()
                    {
                        HeroId = heroCardInfo.HeroId,
                        Level = heroCardInfo.Level,
                        Star = heroCardInfo.Star,
                        Rank = heroCardInfo.Rank,
                        ConfigId = heroCardInfo.ConfigId
                    };
                    heroCardInfos.Add(info);
                }
            }

            Log.Debug($"hero card infos {heroCardInfos.Count}");
            for (int i = 0; i < self.ESCommonHeroCards.Count; i++)
            {
                var commonHeroCard = self.ESCommonHeroCards[i];
                if (i < heroCardInfos.Count)
                {
                    var info = heroCardInfos[i];
                    Log.Debug($"info {info.ConfigId}");
                    commonHeroCard.SetHeroCardInfo(info);
                }
                else
                {
                    commonHeroCard.SetHeroCardInfo(null);
                }
            }

            self.ReferBaseInfo();
        }

        public static void ReferBaseInfo(this DlgHeroStrengthenPreviewLayer self)
        {
            //首先计算一共有拥有多少经验值
            var expSum = 0;
            foreach (var heroCardInfo in self.AlChooseHeroCardInfo)
            {
                expSum += HeroHelper.GetHeroAllLevelExp(heroCardInfo);
            }

            self.View.E_TotalExpText.text = $"累计经验:{expSum}EXP";
            var needExp = HeroHelper.GetNextLevelExp(self.HeroCardInfo);
            self.View.E_ExpBarImage.fillAmount = (float) (expSum + self.HeroCardInfo.CurrentExp) / needExp;

            var heroInfo = new HeroCardInfo()
            {
                Level = self.HeroCardInfo.Level,
                Star = self.HeroCardInfo.Star,
                Rank = self.HeroCardInfo.Rank,
                ConfigId = self.HeroCardInfo.ConfigId
            };

            int endLevel = HeroHelper.GetHeroLevelInfoWithExp(heroInfo, expSum + self.HeroCardInfo.CurrentExp);
            heroInfo.Level = endLevel;
            
            Log.Debug($"end level {endLevel}");
            Log.Debug($"current hero card level {self.HeroCardInfo.Level}");
            self.View.E_OkButtonButton.interactable = heroInfo.Level > self.HeroCardInfo.Level;
            Log.Debug($"end level {heroInfo.Level}");
            // var endLevel = 
            self.View.E_NextLevelText.text = heroInfo.Level > self.HeroCardInfo.Level? $"{heroInfo.Level}" : "-";
            self.View.E_EndLevelText.text = heroInfo.Level > self.HeroCardInfo.Level? $"LV.{heroInfo.Level}" : "LV.-";
            // self.View.E_NextHPText.text = heroInfo.Level> self.HeroCardInfo.Level?$"{heroInfo.}"
            var addExp = HeroHelper.GetHeroLevelLastExp(self.HeroCardInfo, expSum + self.HeroCardInfo.CurrentExp);
            self.View.E_AddExpText.text = addExp > 0? $"+{addExp}EXP" : "+0EXP";

            // self.View.E_NextHPText.text
            self.View.E_NextAttackText.text =
                    $"{HeroHelper.GetHeroBaseAttack(heroInfo)}(+{HeroHelper.GetHeroBaseAttack(heroInfo) - HeroHelper.GetHeroBaseAttack(self.HeroCardInfo)})";
            self.View.E_NextDefenceText.text =
                    $"{HeroHelper.GetHeroBaseDefence(heroInfo)}(+{HeroHelper.GetHeroBaseDefence(heroInfo) - HeroHelper.GetHeroBaseDefence(self.HeroCardInfo)})";
            self.View.E_NextHPText.text =
                    $"{HeroHelper.GetHeroBaseHP(heroInfo)}(+{HeroHelper.GetHeroBaseHP(heroInfo) - HeroHelper.GetHeroBaseHP(self.HeroCardInfo)})";
        }
    }
}