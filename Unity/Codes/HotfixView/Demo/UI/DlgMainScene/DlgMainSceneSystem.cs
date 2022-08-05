using System.Collections;
using System.Collections.Generic;
using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using ET.Account;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgMainSceneSystem
    {
        public static void RegisterUIEvent(this DlgMainScene self)
        {
            self.RegisterNewMailMessage();
            self.PlayBgMusic();

            self.LoadAllAudio();
        }

        public static async void LoadAllAudio(this DlgMainScene self)
        {
            List<AudioClip> audioClips = await AddressableComponent.Instance.LoadAssetsByLabelAsync<AudioClip>("MainSceneAudio", (result) => { });
            Log.Debug("all audio load success");
            foreach (var clip in audioClips)
            {
                Log.Debug($"load audio name {clip.name}");
            }

            await ETTask.CompletedTask;
        }

        public static async void PlayBgMusic(this DlgMainScene self)
        {
            //首先家在音频
            var audioClip = await AddressableComponent.Instance.LoadAssetByPathAsync<AudioClip>("Assets/Res/Audios/Main_1.mp3");
            AudioSource audioSource = self.View.uiTransform.GetComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }

        public static void RegisterNewMailMessage(this DlgMainScene self)
        {
            long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            C2M_RegisterNewMailBoxMessage message = new C2M_RegisterNewMailBoxMessage() { AccountId = account };
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            session.Send(message);
        }

        public static async ETTask<long> GetTroopIdAsync(this DlgMainScene self)
        {
            long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_GetAllTroopInfosResponse m2CGetAllTroopInfosResponse;
            m2CGetAllTroopInfosResponse = (M2C_GetAllTroopInfosResponse)await session
                    .Call(new C2M_GetAllTroopInfosRequest() { Account = AccountId });

            if (m2CGetAllTroopInfosResponse.Error == ErrorCode.ERR_Success)
            {
                // self.TroopInfos = m2CGetAllTroopInfosResponse.TroopInfos;
                // self.ShowTroopItemList();
                List<TroopInfo> troopInfos = m2CGetAllTroopInfosResponse.TroopInfos;
                if (troopInfos.Count > 0)
                {
                    var troopId = troopInfos[0].TroopId;
                    return troopId;
                }
            }

            return 0;
        }

        public static async ETTask<HeroCardInfo> GetFirstHeroCardInfoAsync(this DlgMainScene self, long troopId)
        {
            // long AccountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            M2C_GetHeroInfosWithTroopIdResponse m2CGetHeroInfosWithTroopIdResponse;
            m2CGetHeroInfosWithTroopIdResponse =
                    (M2C_GetHeroInfosWithTroopIdResponse)await session.Call(new C2M_GetHeroInfosWithTroopIdRequest() { TroopId = troopId });
            HeroCardInfo target = null;
            if (m2CGetHeroInfosWithTroopIdResponse.Error == ErrorCode.ERR_Success)
            {
                Log.Debug("获取队伍英雄成功");

                List<HeroCardInfo> heroCardInfos = m2CGetHeroInfosWithTroopIdResponse.HeroCardInfos;
                Log.Debug($"hero card infos{heroCardInfos.Count}");
                if (heroCardInfos.Count > 0)
                {
                    target = heroCardInfos[0];
                    if (heroCardInfos.Count > 1)
                    {
                        // heroCardInfos.Sort((a, b) => { return a.InTroopIndex - b.InTroopIndex; }); 
                        int index = 10000;
                        foreach (var heroCardInfo in heroCardInfos)
                        {
                            if (heroCardInfo.InTroopIndex < index)
                            {
                                index = heroCardInfo.InTroopIndex;
                                target = heroCardInfo;
                            }
                        }
                    }
                }
            }

            return target;
        }

        public static async void ShowWindow(this DlgMainScene self, Entity contextData = null)
        {
            Log.Debug("dlg main scene show window");
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_GoldInfoUI);
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_AccountInfo);
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MessageTaskActiveInfo);
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_SettingUI);
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_FormationUI);
            await self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainSceneMenu);
            long troopId = await self.GetTroopIdAsync();
            if (troopId != 0)
            {
                HeroCardInfo heroCardInfo = await self.GetFirstHeroCardInfoAsync(troopId);
                if (heroCardInfo != null)
                {
                    self.ShowHeroMode(heroCardInfo);
                }
            }
        }

        public static async void ShowHeroMode(this DlgMainScene self, HeroCardInfo heroCardInfo)
        {
            Log.Debug("show hero Mode");
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            if (self.HeroMode != null)
            {
                GameObject.Destroy(self.HeroMode);
                self.HeroMode = null;
            }

            self.HeroMode = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath(config.HeroMode);
            Transform shadow = UIFindHelper.FindDeepChild(GlobalComponent.Instance.Global.gameObject, "Shadow");
            shadow.gameObject.SetActive(true);
        }

        public static void HideWindow(this DlgMainScene self)
        {
            GameObject.Destroy(self.HeroMode);
            self.HeroMode = null;
            Transform shadow = UIFindHelper.FindDeepChild(GlobalComponent.Instance.Global.gameObject, "Shadow");
            shadow.gameObject.SetActive(false);
        }
    }
}