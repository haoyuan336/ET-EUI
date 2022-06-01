using ET.Account;
using UnityEngine;

namespace ET
{
    public static class DlgHeroStrengthenLayerSystem
    {
        public static void RegisterUIEvent(this DlgHeroStrengthenLayer self)
        {
            self.View.E_BackButton.AddListener(() =>
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_HeroStrengthenLayer);
            });

            self.View.ETargetHeroContentLoopHorizontalScrollRect.AddItemRefreshListener(self.OnLoopTargetHeroItem);
            self.View.E_OKButton.AddListenerAsync(self.OnOkButtonClick);

            // self.View.uiTransform
        }

        public static async ETTask OnOkButtonClick(this DlgHeroStrengthenLayer self)
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
                self.View.ETargetHeroContentLoopHorizontalScrollRect.RefreshCells();
                UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
                UIBaseWindow baseWindow = uiComponent.AllWindowsDic[(int) WindowID.WindowID_AllHeroBagLayer];
                baseWindow.GetComponent<DlgAllHeroBagLayer>().ReferView();
                self.View.E_OKButton.gameObject.SetActive(false);

                UIBaseWindow showHeroInfoLayer = uiComponent.GetUIBaseWindow(WindowID.WindowID_ShowHeroInfoLayer);
                showHeroInfoLayer.GetComponent<DlgShowHeroInfoLayer>().ReferHeroCardView(response.HeroCardInfo);
                
            }

            await ETTask.CompletedTask;
        }
        public static void OnLoopTargetHeroItem(this DlgHeroStrengthenLayer self, Transform tr, int index)
        {
            Scroll_ItemHeroCard itemHeroCard = self.ItemHeroCards[index].BindTrans(tr);
            itemHeroCard.InitHeroCard(self.HeroCardInfo);
        }

        public static void SetCountInfo(this DlgHeroStrengthenLayer self, Scroll_ItemHeroCard itemHeroCard, HeroCardInfo heroCardInfo)
        {
            var configId = heroCardInfo.ConfigId;
            var config = HeroConfigCategory.Instance.Get(configId);
            // itemHeroCard.
            itemHeroCard.E_CountText.gameObject.SetActive(config.MaterialType == (int)HeroBagType.Materail);
            itemHeroCard.E_CountText.text = heroCardInfo.Count.ToString();
        }

        public static void HideWindow(this DlgHeroStrengthenLayer self)
        {
            self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_AllHeroBagLayer);
            self.AlChooseHeroCardInfo.Clear();
            self.View.E_OKButton.gameObject.SetActive(false);
        }

        public static async void ShowWindow(this DlgHeroStrengthenLayer self, Entity contextData = null)
        {
            //首先展示英雄背包层
            await ETTask.CompletedTask;
        }

        public static async void ShowAddSubPlane(this DlgHeroStrengthenLayer self, Scroll_ItemHeroCard itemHeroCard, HeroCardInfo heroCardInfo)
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

            // var isFull = self.CheckIsFull();
            dlgAddSubPlane.EnableAddButton(!self.CheckIsFull());
            dlgAddSubPlane.AddAction = () =>
            {
                var isFull = self.CheckIsFull();
                if (!isFull)
                {
                    HeroCardInfo findInfo = self.AlChooseHeroCardInfo.Find(a => a.HeroId.Equals(heroCardInfo.HeroId));
                    if (findInfo == null)
                    {
                        findInfo = new HeroCardInfo() { HeroId = heroCardInfo.HeroId, Count = 1, };
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

                    // itemHeroCard.E_ChooseCountText.gameObject.SetActive(true);
                    // itemHeroCard.E_ChooseCountText.text = findInfo.Count.ToString();
                    dlgAllHeroBagLayer.SetAllChooseHeroCardInfos(self.AlChooseHeroCardInfo);
                }

                isFull = self.CheckIsFull();
                self.View.E_OKButton.gameObject.SetActive(isFull);
                if (isFull)
                {
                    dlgAddSubPlane.EnableAddButton(false);
                    uiComponent.AllWindowsDic[(int) WindowID.WindowID_AllHeroBagLayer].GetComponent<DlgAllHeroBagLayer>()
                            .EnableItemWhitHeroInfos(self.AlChooseHeroCardInfo);
                }
            };
            dlgAddSubPlane.SubAction = () =>
            {
                var findInfo = self.AlChooseHeroCardInfo.Find(a => a.HeroId.Equals(heroCardInfo.HeroId));
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
            };
        }

        public static void OnHeroItemClick(this DlgHeroStrengthenLayer self, HeroCardInfo heroCardInfo, Scroll_ItemHeroCard itemHeroCard, bool value)
        {
            var config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            HeroCardInfo fingInfo = self.AlChooseHeroCardInfo.Find(a => a.HeroId.Equals(heroCardInfo.HeroId));
            var isFull = self.CheckIsFull();
            if (config.MaterialType == (int)HeroBagType.Materail)
            {
                Log.Debug($"materail type {config.MaterialType}");
                if (value)
                {
                    self.ShowAddSubPlane(itemHeroCard, heroCardInfo);
                }

                itemHeroCard.E_ChooseToggle.isOn = false;
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
            self.View.E_OKButton.gameObject.SetActive(self.CheckIsFull());
        }

        public static bool CheckIsFull(this DlgHeroStrengthenLayer self)
        {
            var count = 0;
            foreach (var info in self.AlChooseHeroCardInfo)
            {
                count += info.Count;
            }

            var full = count == 5;
            return full;
        }

        public static async void SetTargetInfo(this DlgHeroStrengthenLayer self, HeroCardInfo heroCardInfo)
        {
            self.HeroCardInfo = heroCardInfo;
            self.AddUIScrollItems(ref self.ItemHeroCards, 1);
            self.View.ETargetHeroContentLoopHorizontalScrollRect.SetVisible(true, 1);

            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_AllHeroBagLayer);
            UIBaseWindow uiBaseWindow = uiComponent.AllWindowsDic[(int) WindowID.WindowID_AllHeroBagLayer];
            uiBaseWindow.uiTransform.GetComponent<RectTransform>().offsetMax = new Vector2(0, -600);
            uiBaseWindow.uiTransform.GetComponent<RectTransform>().offsetMin = new Vector2(0, 300);
            uiBaseWindow.GetComponent<DlgAllHeroBagLayer>().OnHeroItemInfoClick = self.OnHeroItemClick;
            uiBaseWindow.GetComponent<DlgAllHeroBagLayer>().UnAbleHeroItemWhitHeroInfo(self.HeroCardInfo);
        }
    }
}