using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgMailLayerSystem
    {
        public static void RegisterUIEvent(this DlgMailLayer self)
        {
            self.View.E_BackButton.AddListener(() => { self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_MailLayer); });
            self.AddUIScrollItems(ref self.ItemMails, 10);
            // self.View.ELoopScrollList_LoopVerticalScrollRect.SetVisible(true, 10);
            self.View.ELoopScrollList_LoopVerticalScrollRect.AddItemRefreshListener(self.OnLoopEventListener);
        }

        public static async void OnLoopEventListener(this DlgMailLayer self, Transform tr, int index)
        {
            var item = self.ItemMails[index].BindTrans(tr);

            item.E_ClickButton.onClick.RemoveAllListeners();
            item.E_ClickButton.onClick.AddListener(() => { self.OnMailItemClick(index); });
            self.InitMailItemInfo(item, self.MailInfos[index]);
            await ETTask.CompletedTask;
        }

        public static void InitMailItemInfo(this DlgMailLayer self, Scroll_ItemMail itemMail, MailInfo info)
        {
            itemMail.E_IsReadText.text = info.IsRead? "已读" : "未读";
            itemMail.E_timeText.text = info.SendTime.ToString();
            itemMail.E_TitleText.text = info.Title;
            itemMail.E_FromText.text = $"来自:{info.SendName}";
            itemMail.E_IsGetText.text = info.IsGet? "已领取" : "未领取";
        }

        public static async void OnMailItemClick(this DlgMailLayer self, int index)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_MailInfoLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_MailInfoLayer);
            baseWindow.GetComponent<DlgMailInfoLayer>().SetMailInfo(self.MailInfos[index]);

            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            List<long> mailIds = new List<long>();
            mailIds.Add(self.MailInfos[index].MailId);
            C2M_ReadMailsRequest request = new C2M_ReadMailsRequest() { AccountId = account, MailIds = mailIds };
            M2C_ReadMailsResponse response = await session.Call(request) as M2C_ReadMailsResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                Log.Debug("读取邮件消息发送完成");
                self.MailInfos = response.MailInfos;
                self.View.ELoopScrollList_LoopVerticalScrollRect.RefillCells();

                UIBaseWindow settingBaseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_SettingUI);
                settingBaseWindow.GetComponent<DlgSettingUI>().SetMailInfos(self.MailInfos);
            }
        }

        public static async void ShowWindow(this DlgMailLayer self, Entity contextData = null)
        {
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;

            C2M_GetAllMailRequest request = new C2M_GetAllMailRequest() { AccountId = account };
            M2C_GetAllMailResponse response = await session.Call(request) as M2C_GetAllMailResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                List<MailInfo> mailInfos = response.MailInfos;
                self.MailInfos = mailInfos;
                self.View.ELoopScrollList_LoopVerticalScrollRect.SetVisible(true, mailInfos.Count);
            }
        }

        public static void SetMailInfos(this DlgMailLayer self, List<MailInfo> mailInfos)
        {
        }

        public static void UpdateMailInfo(this DlgMailLayer self, MailInfo info)
        {
            //更新其中的一个邮件 
            self.MailInfos.RemoveAll(a => a.MailId.Equals(info.MailId));
            self.MailInfos.Add(info);
            self.View.ELoopScrollList_LoopVerticalScrollRect.RefillCells();
        }
    }
}