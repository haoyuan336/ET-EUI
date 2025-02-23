﻿using System.Collections;
using System.Collections.Generic;
using System;
using ET.Account;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgGoldInfoUISystem
    {
        public static void RegisterUIEvent(this DlgGoldInfoUI self)
        {
            self.View.E_AddPowerButton.AddListenerAsync(self.RequestAddPowerAsync);
            self.View.E_AddGoldButton.AddListenerAsync(self.RequestAddGoldAsync);
            self.View.E_AddDiamondButton.AddListenerAsync(self.RequestAddDiamondAsync);
            self.View.E_AddExpButton.AddListenerAsync(self.RequestAddExpAsync);
        }

        public static void ShowWidgetWithType(this DlgGoldInfoUI self, GoldInfoUIType type)
        {
            self.View.E_PowerGroupImage.gameObject.SetActive(false);
            self.View.E_ExpGroupImage.gameObject.SetActive(false);
            self.View.E_WeaponChipGroupImage.gameObject.SetActive(false);
            switch (type)
            {
                case GoldInfoUIType.HeroInfo:
                    self.View.E_ExpGroupImage.gameObject.SetActive(true);
                    break;
                case GoldInfoUIType.MainScene:
                    self.View.E_PowerGroupImage.gameObject.SetActive(true);
                    break;
                case GoldInfoUIType.WeaponInfo:
                    self.View.E_WeaponChipGroupImage.gameObject.SetActive(true);
                    break;
            }
        }

        public static async ETTask RequestAddExpAsync(this DlgGoldInfoUI self)
        {
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_AddItemResponse response =
                    (M2C_AddItemResponse)await session.Call(new C2M_AddItemRequest() { AccountId = AccountId, Count = 10000, ConfigId = 1008 });
            if (response.Error == ErrorCode.ERR_Success)
            {
                // Log.Debug("增加体力值成功");
                self.View.E_ExpText.text = $"{response.ItemInfo.Count}";
                if (self.DataChangeAction != null)
                {
                    self.DataChangeAction();
                }
            }
        }

        public static async ETTask RequestAddDiamondAsync(this DlgGoldInfoUI self)
        {
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_AddItemResponse response =
                    (M2C_AddItemResponse)await session.Call(new C2M_AddItemRequest() { AccountId = AccountId, Count = 1000, ConfigId = 1002 });
            if (response.Error == ErrorCode.ERR_Success)
            {
                // Log.Debug("增加体力值成功");
                self.View.E_DiamondText.text = $"{response.ItemInfo.Count}";
            }
        }

        public static async ETTask RequestAddPowerAsync(this DlgGoldInfoUI self)
        {
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_AddItemResponse response =
                    (M2C_AddItemResponse)await session.Call(new C2M_AddItemRequest() { AccountId = AccountId, Count = 10, ConfigId = 1003 });
            if (response.Error == ErrorCode.ERR_Success)
            {
                // Log.Debug("增加体力值成功");
                self.View.E_PowerText.text = $"{response.ItemInfo.Count}";
            }
        }

        public static async ETTask RequestAddGoldAsync(this DlgGoldInfoUI self)
        {
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_AddItemResponse response =
                    (M2C_AddItemResponse)await session.Call(new C2M_AddItemRequest() { AccountId = AccountId, Count = 10, ConfigId = 1001 });
            if (response.Error == ErrorCode.ERR_Success)
            {
                // Log.Debug("增加体力值成功");
                self.View.E_GoldText.text = $"{response.ItemInfo.Count}";
            }
        }

        public static void ShowWindow(this DlgGoldInfoUI self, Entity contextData = null)
        {
            self.ReferGoldInfo();
        }

        public static async void ReferGoldInfo(this DlgGoldInfoUI self)
        {
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            // M2C_GetGoldInfoResponse response = (M2C_GetGoldInfoResponse) await session.Call(new C2M_GetGoldInfoRequest() { AccountId = AccountId });
            M2C_GetAllItemResponse response = (M2C_GetAllItemResponse)await session.Call(new C2M_GetAllItemRequest());
            if (response.Error == ErrorCode.ERR_Success)
            {
                var goldItem = response.ItemInfos.Find((a) => { return a.ConfigId == 1001; });
                if (goldItem != null)
                {
                    self.View.E_GoldText.text = goldItem.Count.ToString();
                }

                var powerItem = response.ItemInfos.Find((a) => { return a.ConfigId == 1003; });
                if (powerItem != null)
                {
                    self.View.E_PowerText.text = powerItem.Count.ToString();
                }

                var diamondItem = response.ItemInfos.Find((a) => { return a.ConfigId == 1002; });
                if (diamondItem != null)
                {
                    self.View.E_DiamondText.text = diamondItem.Count.ToString();
                }

                var exp = response.ItemInfos.Find(a => a.ConfigId == 1008);
                if (exp != null)
                {
                    self.View.E_ExpText.text = exp.Count.ToString();
                }
            }
        }
    }
}