using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgSettingUISystem
    {
        public static void RegisterUIEvent(this DlgSettingUI self)
        {
            self.View.E_ShowMenuToggle.GetComponent<Toggle>().onValueChanged.AddListener((value) => { self.ShowMenu(value).Coroutine(); });
            self.View.E_MailButton.AddListenerAsync(self.MailButtonClick);
            self.View.E_FriendButton.AddListener(self.OnFriendButtonClick);
            self.View.E_ChangeShowHeroModeButton.AddListener(self.ShowAllHeroBag);
        }

        public static void SetChatInfos(this DlgSettingUI self, Dictionary<long, ChatInfo> chatInfos)
        {
            self.ChatInfosMap = chatInfos;
            self.ShowNewChatMark(self.ChatInfosMap.Count != 0);
        }

        public static void ReceiveChatInfo(this DlgSettingUI self, ChatInfo chatInfo)
        {
            if (self.ChatInfosMap.Keys.Contains(chatInfo.AccountInfo.Account))
            {
                self.ChatInfosMap.Remove(chatInfo.AccountInfo.Account);
            }

            self.ChatInfosMap.Add(chatInfo.AccountInfo.Account, chatInfo);

            self.ShowNewChatMark(true);

            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            var baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_FriendLayer);
            baseWindow.GetComponent<DlgFriendLayer>().SetChatInfoMap(self.ChatInfosMap);

            // self.View.ELoopFriendListLoopVerticalScrollRect.RefreshCells();
        }

        public static async void OnFriendButtonClick(this DlgSettingUI self)
        {
            self.ShowNewChatMark(false);
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_FriendLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_FriendLayer);
            if (baseWindow != null)
            {
                baseWindow.GetComponent<DlgFriendLayer>().SetChatInfoMap(self.ChatInfosMap);
            }
        }

        public static async void RequestNewMail(this DlgSettingUI self)
        {
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_GetAllMailRequest request = new C2M_GetAllMailRequest() { AccountId = account };
            M2C_GetAllMailResponse response = await session.Call(request) as M2C_GetAllMailResponse;
            Log.Debug($"request all mail {response.MailInfos.Count}");
            if (response.Error == ErrorCode.ERR_Success)
            {
                //取出未读的邮件数量
                List<MailInfo> mailInfos = response.MailInfos.FindAll(a => a.IsRead == false);
                self.View.E_RedDotImage.gameObject.SetActive(mailInfos.Count > 0);
            }
        }

        public static void SetMailInfos(this DlgSettingUI self, List<MailInfo> mailInfos)
        {
            List<MailInfo> infos = mailInfos.FindAll(a => a.IsRead == false);
            self.View.E_RedDotImage.gameObject.SetActive(infos.Count > 0);
        }

        public static void ShowRedDot(this DlgSettingUI self, bool value)
        {
            self.View.E_RedDotImage.gameObject.SetActive(value);
        }

        public static void ShowNewChatMark(this DlgSettingUI self, bool value)
        {
            self.View.E_NewChatMarkImage.gameObject.SetActive(value);
        }

        public static async ETTask MailButtonClick(this DlgSettingUI self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_MailLayer);
        }

        public static async ETTask ShowMenu(this DlgSettingUI self, bool isShow)
        {
            GameObject maskNode = GameObject.Find("DlgSettingUI/MaskNode");
            self.View.E_ShowImage.GetComponent<RectTransform>().localScale = new Vector3(1, isShow? 1 : -1, 1);
            Log.Debug($"mask Node {maskNode}");
            if (maskNode != null)
            {
                // Log.Debug("find obj");
                float time = 0;
                while (time < 0.2f)
                {
                    float heightRate = Mathf.Sin(Mathf.PI * 0.5f * (time / 0.2f));
                    if (!isShow)
                    {
                        heightRate = Mathf.Cos(Mathf.PI * 0.5f * (time / 0.2f));
                    }

                    float height = 220 + 500 * heightRate;
                    maskNode.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
                    time += Time.deltaTime;
                    self.View.E_ShowMenuToggle.GetComponent<RectTransform>().anchoredPosition = new Vector2(-113, -470 - heightRate * 500);

                    self.View.E_BGImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
                    // -1031
                    await TimerComponent.Instance.WaitFrameAsync();
                }

                maskNode.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, isShow? 870 : 270);
                self.View.E_ShowMenuToggle.GetComponent<RectTransform>().anchoredPosition = new Vector2(-113, !isShow? -470 : -969);
            }
        }

        public static void ShowWindow(this DlgSettingUI self, Entity contextData = null)
        {
            self.RequestNewMail();
            self.RequestHeroMode();
        }

        public static async void RequestHeroMode(this DlgSettingUI self)
        {
            // await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoadShowHeroMode, self.GetHashCode());
            //获取当前需要显示的英雄
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            var response = await session.Call(new C2M_GetCurrentShowHeroCardInfoRequest()) as M2C_GetCurrentShowHeroCardInfoResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                HeroCardInfo heroCardInfo = response.HeroCardInfo;

                // self.AddChild<HeroCard, int>(heroCardInfo.ConfigId);
                self.ShowHeroMode(heroCardInfo);
            }
            else
            {
                Log.Debug("no show hero mode");
            }
        }

        public static void ShowHeroMode(this DlgSettingUI self, HeroCardInfo heroCardInfo)
        {
            if (self.HeroMode != null)
            {
                GameObjectPoolHelper.ReturnObjectToPool(self.HeroMode);
            }

            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            GameObject go = GameObjectPoolHelper.GetObjectFromPool(config.HeroMode, true, 1);
            self.HeroMode = go;
            go.transform.SetParent(GlobalComponent.Instance.Global);
        }

        public static async void ShowAllHeroBag(this DlgSettingUI self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_AllHeroBagLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_AllHeroBagLayer);
            var allHeroBagLayer = baseWindow.GetComponent<DlgAllHeroBagLayer>();
            allHeroBagLayer.SetShowHeroType(HeroBagType.HeroAndMaterial);
            allHeroBagLayer.OnHeroItemInfoClick = (HeroCardInfo, ItemHeroCard, value) =>
            {
                uiComponent.HideWindow(WindowID.WindowID_AllHeroBagLayer);

                self.SetShowHeroMode(HeroCardInfo);
            };
        }

        public static void HideWindow(this DlgSettingUI self)
        {
            if (self.HeroMode != null)
            {
                GameObjectPoolHelper.ReturnObjectToPool(self.HeroMode);
            }
        }

        public static async void SetShowHeroMode(this DlgSettingUI self, HeroCardInfo heroCardInfo)
        {
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            var response =
                    await session.Call(new C2M_SetCurrentShowHeroCardInfoRequest() { HeroId = heroCardInfo.HeroId }) as
                            M2C_SetCurrentShowHeroCardInfoResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                self.ShowHeroMode(heroCardInfo);
            }
        }
    }
}