using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Globalization;
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
            scrollItemHeroCard.E_TextText.text = $"{self.HeroCardInfos[index].HeroName}";
            int colorId = self.HeroCardInfos[index].HeroColor;
            DiamondTypeConfig diamondTypeConfig = DiamondTypeConfigCategory.Instance.Get(colorId);
            string colorStr = diamondTypeConfig.ColorValue;
            Color color = self.HexToColor(colorStr);
            scrollItemHeroCard.E_ClickImage.color = color;
        }

        public static Color HexToColor(this DlgHeroInfoLayerUI self, string hex)
        {
            hex = hex.Replace("0x", string.Empty);
            hex = hex.Replace("#", string.Empty);
            byte a = byte.MaxValue;
            byte r = byte.Parse(hex.Substring(0, 2), NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), NumberStyles.HexNumber);
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(6, 2), NumberStyles.HexNumber);
            }

            return new Color32(r, g, b, a);
        }

        public static void InitColorToggleEvent(this DlgHeroInfoLayerUI self, GameObject go)
        {
            go.GetComponent<Toggle>().onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    // Log.Debug(go.name);
                    go.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 160);
                }
                else
                {
                    go.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 140);
                }
            });
        }

        public static void InitAllToggleEvent(this DlgHeroInfoLayerUI self)
        {
            List<GameObject> list = new List<GameObject>();
            list.Add(self.View.E_RedImage.gameObject);
            list.Add(self.View.E_YellowImage.gameObject);
            list.Add(self.View.E_GreenImage.gameObject);
            list.Add(self.View.E_BlueImage.gameObject);
            list.Add(self.View.E_PurpleImage.gameObject);
            foreach (var go in list)
            {
                self.InitColorToggleEvent(go);
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