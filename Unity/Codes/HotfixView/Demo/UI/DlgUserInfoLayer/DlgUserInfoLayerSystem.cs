using System.Collections.Generic;
using System.Linq;

namespace ET
{
    public static class DlgUserInfoLayerSystem
    {
        public static void RegisterUIEvent(this DlgUserInfoLayer self)
        {
            self.View.E_BackButton.AddListener(self.OnBackButtonClick);
            self.View.E_HeadButton.AddListenerAsync(self.OnHeadButtonClick);
            self.View.E_CopyButton.AddListener(self.OnCopyButtonClick);
        }

        public static void OnCopyButtonClick(this DlgUserInfoLayer self)
        {
            UnityEngine.GUIUtility.systemCopyBuffer = self.AccountInfo.Name;
            
        }

        public static async ETTask OnHeadButtonClick(this DlgUserInfoLayer self)
        {
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            if (accountId != self.AccountInfo.Account)
            {
                return;
            }

            //打开，切换头像层
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_ChooseHeadImageLayer);

            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_ChooseHeadImageLayer);
            baseWindow.GetComponent<DlgChooseHeadImageLayer>().SetAccountInfo(self.AccountInfo);

            baseWindow.GetComponent<DlgChooseHeadImageLayer>().ChooseHeadCallBackAction = null;
            baseWindow.GetComponent<DlgChooseHeadImageLayer>().ChooseHeadCallBackAction += self.OnChooseHeadCallBack;

            await ETTask.CompletedTask;
        }

        public static async void OnChooseHeadCallBack(this DlgUserInfoLayer self, HeadImageType type, int configId)
        {
            Log.Debug("ChangeHeadOrFrameRequest");
            //取出相关参数
            long accountId = self.AccountInfo.Account;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            var request = new C2M_ChangePlayerHeadOrFrameRequest() { AccountId = accountId, ConfigId = configId, HeadType = (int) type };
            var response = await session.Call(request) as M2C_ChangePlayerHeadOrFrameResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                self.AccountInfo = response.AccountInfo;
                // self.View..RefillCells();
                PlayerHeadImageResConfig config = PlayerHeadImageResConfigCategory.Instance.Get(self.AccountInfo.HeadImageConfigId);
                var spriteAtlas = config.SpriteAtlasRes;
                var sptiteRes = config.ImageRes;

                self.View.E_HeadIconImage.sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, sptiteRes);

                var headFrameConfig = PlayerHeadImageResConfigCategory.Instance.Get(self.AccountInfo.HeadFrameImageConfigId);

                var headFrameAtlas = headFrameConfig.SpriteAtlasRes;
                var headFrameImage = headFrameConfig.ImageRes;

                self.View.E_HeadFrameImage.sprite =
                        await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(headFrameAtlas, headFrameImage);
            }
        }

        public static void OnBackButtonClick(this DlgUserInfoLayer self)
        {
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            uiComponent.HideWindow(WindowID.WindowID_UserInfoLayer);
        }

        public static void ShowWindow(this DlgUserInfoLayer self, Entity contextData = null)
        {
        }

        public static async void SetUserInfo(this DlgUserInfoLayer self, AccountInfo accountInfo)
        {
            self.AccountInfo = accountInfo;
            Log.Debug($"set user info {accountInfo}");
            self.View.E_NameText.text = accountInfo.NickName;
            self.View.E_IDText.text = accountInfo.Name;
            var levelCount = LevelConfigCategory.Instance.GetAll().Count;
            Log.Debug($"current level number {accountInfo.PvELevelNumber}");
            self.View.E_ClearanceProgressText.text = $"{(float) accountInfo.PvELevelNumber / (float) levelCount * 100}%";

            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            // self.View.E_NameText
            self.View.E_EdItorButton.gameObject.SetActive(accountInfo.Account.Equals(accountId));
            List<HeroConfig> heroConfigs = HeroConfigCategory.Instance.GetAll().Values.ToList().FindAll(a =>
            {
                return a.MaterialType == (int) HeroBagType.Hero;
            });
            var heroCount = heroConfigs.Count;

            var request = new C2M_GetPlayerOwnHeroTypeCountRequest() { AccountId = accountInfo.Account };
            var session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            var response = await session.Call(request) as M2C_GetPlayerOwnHeroTypeCountResponse;
            if (response.Error == ErrorCode.ERR_Success)
            {
                var count = response.Count;
                Log.Debug($"拥有的英雄种类数量{count}");
                float percent = 100 * (float) count / heroCount;
                self.View.E_HeroProgressText.text = $"{percent}%";
            }

            //初始化头像

            PlayerHeadImageResConfig config = PlayerHeadImageResConfigCategory.Instance.Get(accountInfo.HeadImageConfigId);
            var spriteAtlas = config.SpriteAtlasRes;
            var sptiteRes = config.ImageRes;

            self.View.E_HeadIconImage.sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, sptiteRes);
            //查看英雄英雄多少种类的英雄
        }
    }
}