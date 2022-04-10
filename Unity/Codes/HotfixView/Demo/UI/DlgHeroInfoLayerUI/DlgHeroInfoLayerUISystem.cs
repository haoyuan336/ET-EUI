using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using ET.Account;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgHeroInfoLayerUISystem
    {
        public static void RegisterUIEvent(this DlgHeroInfoLayerUI self)
        {
            self.InitAllToggleEvent();
            self.View.E_LoopScrollListHeroLoopVerticalScrollRect.AddItemRefreshListener((Transform tr, int index) =>
            {
                self.OnLoopListItemRefreshHandler(tr, index);
            });
        }

        public static void OnLoopListItemRefreshHandler(this DlgHeroInfoLayerUI self, Transform tr, int index)
        {
            Scroll_ItemHeroCard scrollItemHeroCard = self.ItemHeroCards[index].BindTrans(tr);
            scrollItemHeroCard.SetHeroInfo(self.HeroCardInfos[index]);
        }

        public static void InitColorToggleEvent(this DlgHeroInfoLayerUI self, GameObject go, int index)
        {
            go.GetComponent<Toggle>().onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    // Log.Debug(go.name);
                    self.FilterColor(go, index);
                    go.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 160);
                }
                else
                {
                    go.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 140);
                }
            });
        }

        public static void FilterColor(this DlgHeroInfoLayerUI self, GameObject colorObj, int index)
        {
            // if (colorObj.name.Equals(self.View.E_RedImage.name))
            // {
            //     
            // }
            Log.Debug($"filter index{index}");
            Dictionary<int, List<HeroCardInfo>> map = new Dictionary<int, List<HeroCardInfo>>();
            foreach (var heroCardInfo in self.HeroCardInfos)
            {
                if (!map.ContainsKey(heroCardInfo.HeroColor))
                {
                    // map[heroCardInfo.HeroColor] = new List<HeroCardInfo>();
                    map.Add(heroCardInfo.HeroColor, new List<HeroCardInfo>());
                }

                map[heroCardInfo.HeroColor].Add(heroCardInfo);
            }

            List<HeroCardInfo> list = new List<HeroCardInfo>();
            list = map[index + 1];
            map.Remove(index + 1);
            foreach (var mapList in map.Values)
            {
                // list.Concat(mapList);
                // List<HeroCardInfo>
                foreach (var info in mapList)
                {
                    list.Add(info);
                }
            }

            self.HeroCardInfos = list;

            // self.HeroCardInfos.Sort((a, b) =>
            // {
            //     // return 1;
            //     // var aColor = a.HeroColor;
            //     // if (aColor == index + 1)
            //     // {
            //     //     aColor = 0;
            //     // }
            //     //
            //     // var bColor = b.HeroColor;
            //     // return aColor - bColor;
            //     if (a.HeroColor == index + 1)
            //     {
            //         return -1;
            //     }
            //
            //     return b.HeroColor - a.HeroColor;
            // });
            // self.HeroCardInfos.Clear();
            self.AddUIScrollItems(ref self.ItemHeroCards, self.HeroCardInfos.Count);
            self.View.E_LoopScrollListHeroLoopVerticalScrollRect.SetVisible(true, self.HeroCardInfos.Count);
        }

        public static void InitAllToggleEvent(this DlgHeroInfoLayerUI self)
        {
            List<GameObject> list = new List<GameObject>();
            list.Add(self.View.E_RedImage.gameObject);
            list.Add(self.View.E_YellowImage.gameObject);
            list.Add(self.View.E_GreenImage.gameObject);
            list.Add(self.View.E_BlueImage.gameObject);
            list.Add(self.View.E_PurpleImage.gameObject);
            var index = 0;
            foreach (var go in list)
            {
                self.InitColorToggleEvent(go, index);
                index++;
            }
        }

        public static async ETTask RequestHerosInfo(this DlgHeroInfoLayerUI self)
        {
            long Account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Log.Debug($"account = {Account}");
            M2C_GetAllHeroCardListResponse m2CGetAllHeroCardListResponse;
            try
            {
                m2CGetAllHeroCardListResponse = (M2C_GetAllHeroCardListResponse) await self.ZoneScene().GetComponent<SessionComponent>().Session
                        .Call(new C2M_GetAllHeroCardListRequest() { Account = Account });
                if (m2CGetAllHeroCardListResponse.Error == ErrorCode.ERR_Success)
                {
                    List<HeroCardInfo> heroCardInfos = m2CGetAllHeroCardListResponse.HeroCardInfos;
                    self.HeroCardInfos.Clear();
                    foreach (var heroCardInfo in heroCardInfos)
                    {
                        self.HeroCardInfos.Add(heroCardInfo);
                    }

                    self.AddUIScrollItems(ref self.ItemHeroCards, self.HeroCardInfos.Count);
                    self.View.E_LoopScrollListHeroLoopVerticalScrollRect.SetVisible(true, self.HeroCardInfos.Count);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static void ShowWindow(this DlgHeroInfoLayerUI self, Entity contextData = null)
        {
            self.RequestHerosInfo().Coroutine();
        }
    }
}