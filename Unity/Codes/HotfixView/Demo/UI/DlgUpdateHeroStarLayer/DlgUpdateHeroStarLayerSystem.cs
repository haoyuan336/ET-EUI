using ILRuntime.Runtime;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = System.Numerics.Vector3;

namespace ET
{
    public static class DlgUpdateHeroStarLayerSystem
    {
        public static void RegisterUIEvent(this DlgUpdateHeroStarLayer self)
        {
            self.View.E_BackButton.AddListener(() =>
            {
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_UpdateHeroStarLayer);
            });
            self.View.E_OkButtonButton.AddListenerAsync(self.OkButtonClick);
            self.CurrentCommonHeroCard =
                    self.AddChildWithId<ESCommonHeroCard, Transform>(IdGenerater.Instance.GenerateId(),
                        self.View.E_CurrentHeroCardPosImage.transform);

            self.ChooseCommonHeroCard =
                    self.AddChildWithId<ESCommonHeroCard, Transform, bool>(IdGenerater.Instance.GenerateId(), self.View.E_ChooseImageImage.transform,
                        true);

            self.NextStarCommonHeroCard =
                    self.AddChildWithId<ESCommonHeroCard, Transform>(IdGenerater.Instance.GenerateId(), self.View.E_SuccessImageImage.transform);
            self.ChooseCommonHeroCard.OnButtonClick = self.OnChooseHeroButtonClick;
        }
        public static async ETTask<GameObject> CreateOneHeroCard(this DlgUpdateHeroStarLayer self, Transform parent)
        {
            GameObject gameObject =
                    await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath("Assets/Bundles/UI/Common/ESHeroCard.prefab");
            // obj.transform.SetParent();
            gameObject.transform.SetParent(parent);
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            gameObject.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            gameObject.GetComponent<RectTransform>().offsetMin = Vector2.zero;

            var toggleObj = UIFindHelper.FindDeepChild(gameObject, "E_Choose");
            toggleObj.GetComponent<Toggle>().interactable = false;
            return gameObject;
        }

        public static async void OnChooseHeroButtonClick(this DlgUpdateHeroStarLayer self)
        {
            //
            UIComponent uiComponent = self.DomainScene().GetComponent<UIComponent>();
            await uiComponent.ShowWindow(WindowID.WindowID_AllHeroBagLayer);
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_AllHeroBagLayer);
            baseWindow.GetComponent<DlgAllHeroBagLayer>().UnAbleHeroItemWhitHeroInfo(self.HeroCardInfo);
            baseWindow.GetComponent<DlgAllHeroBagLayer>().SetEnableSameStarCountHeroInfo(self.HeroCardInfo);
            RectTransform rectTransform = baseWindow.uiTransform.GetComponent<RectTransform>();
            baseWindow.GetComponent<DlgAllHeroBagLayer>().ReferView();
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
            baseWindow.GetComponent<DlgAllHeroBagLayer>().OnHeroItemInfoClick =
                    (HeroCardInfo cardInfo, Scroll_ItemHeroCard itemHeroCard, bool value) =>
                    {
                        if (value)
                        {
                            self.ChooseCardInfo = cardInfo;
                            uiComponent.HideWindow(WindowID.WindowID_AllHeroBagLayer);
                            uiComponent.HideWindow(WindowID.WindowID_BackButton);
                            self.PlayerChooseOneHeroCard(cardInfo);
                        }
                    };
        }

        public static void HideWindow(this DlgUpdateHeroStarLayer self)
        {
            self.ChooseCardInfo = null;
            self.ChooseCommonHeroCard.SetHeroCardInfo(null);
            self.NextStarCommonHeroCard.SetHeroCardInfo(null);
          
        }

        public static void PlayerChooseOneHeroCard(this DlgUpdateHeroStarLayer self, HeroCardInfo cardInfo)
        {
            //玩家选择了 一张英雄卡牌
            self.ChooseCardInfo = cardInfo;
            self.ChooseCommonHeroCard.SetHeroCardInfo(self.ChooseCardInfo);
            var heroCarInfo = new HeroCardInfo()
            {
                ConfigId = self.HeroCardInfo.ConfigId,
                Rank = self.HeroCardInfo.Rank,
                Star = self.HeroCardInfo.Star + 1,
                Level = self.HeroCardInfo.Level
            };
            self.NextStarCommonHeroCard.SetHeroCardInfo(heroCarInfo);
        }

        public static async ETTask OkButtonClick(this DlgUpdateHeroStarLayer self)
        {
            if (self.ChooseCardInfo == null)
            {
                return;
            }
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_UpdateHeroStarRequest request = new C2M_UpdateHeroStarRequest()
            {
                Account = accountId, HeroId = self.HeroCardInfo.HeroId, MaterialHeroId = self.ChooseCardInfo.HeroId
            };

            M2C_UpdateHeroStarResponse response = (M2C_UpdateHeroStarResponse) await session.Call(request);
            if (response.Error == ErrorCode.ERR_Success)
            {
                self.ChooseCardInfo = null;
                self.CurrentCommonHeroCard.SetHeroCardInfo(response.HeroCardInfo);
                self.ChooseCommonHeroCard.SetHeroCardInfo(null);
                self.NextStarCommonHeroCard.SetHeroCardInfo(null);

                self.SetTargetInfo(response.HeroCardInfo);
                UIBaseWindow baseWindow = self.DomainScene().GetComponent<UIComponent>().GetUIBaseWindow(WindowID.WindowID_ShowHeroInfoLayer);
                baseWindow.GetComponent<DlgShowHeroInfoLayer>().SetHeroInfo(response.HeroCardInfo);

                self.DomainScene().GetComponent<UIComponent>().GetUIBaseWindow(WindowID.WindowID_AllHeroBagLayer).GetComponent<DlgAllHeroBagLayer>()
                        .ReferView();
            }

            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgUpdateHeroStarLayer self, Entity contextData = null)
        {
        }

        public static async void InitStarView(this DlgUpdateHeroStarLayer self, HeroCardInfo heroCardInfo)
        {
            var starCount = heroCardInfo.Star;

            // var starGroup = UIFindHelper.FindDeepChild(self.View.E_StarGroupButton.gameObject, "StarGroup");

            var spriteAtlas = ConstValue.CommonUIAtlasPath;
            Transform tr = self.View.E_StarGroupButton.transform;
            for (int i = 0; i < 5; i++)
            {
                var child = tr.GetChild(i);
                if (i < starCount)
                {
                    var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, "star");
                    child.GetComponent<Image>().sprite = sprite;
                }
                else
                {
                    var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, "StarTexture");
                    child.GetComponent<Image>().sprite = sprite;
                }
            }
        }

        public static async void InitBaseInfo(this DlgUpdateHeroStarLayer self, HeroCardInfo heroCardInfo)
        {
            //初始化基础数据
            if (heroCardInfo.Star >= 5)
            {
                self.View.E_MaxText.gameObject.SetActive(true);
                self.View.E_BaseInfoButton.gameObject.SetActive(false);
                return;
            }

            self.View.E_MaxText.gameObject.SetActive(false);
            self.View.E_BaseInfoButton.gameObject.SetActive(true);
            self.View.E_CurrentAttackText.text = $"{HeroHelper.GetHeroBaseAttack(heroCardInfo)}";
            self.View.E_CurrentHPText.text = $"{HeroHelper.GetHeroBaseHP(heroCardInfo)}";
            self.View.E_CurrentDefenceText.text = $"{HeroHelper.GetHeroBaseDefence(heroCardInfo)}";

            var addAttack = HeroHelper.GetNextStarHeroBaseAttack(heroCardInfo) - HeroHelper.GetHeroBaseAttack(heroCardInfo);
            var addDefence = HeroHelper.GetNextStarHeroBaseDefence(heroCardInfo) - HeroHelper.GetHeroBaseDefence(heroCardInfo);
            var addHP = HeroHelper.GetNextStarHeroBaseHP(heroCardInfo) - HeroHelper.GetHeroBaseHP(heroCardInfo);

            self.View.E_NextAttackText.text = $"{HeroHelper.GetNextStarHeroBaseAttack(heroCardInfo)}(+{addAttack})";
            self.View.E_NextDefenceText.text = $"{HeroHelper.GetNextStarHeroBaseDefence(heroCardInfo)}(+{addDefence})";
            self.View.E_NextHPText.text = $"{HeroHelper.GetNextStarHeroBaseHP(heroCardInfo)}(+{addHP})";

            await ETTask.CompletedTask;
        }

        public static void SetTargetInfo(this DlgUpdateHeroStarLayer self, HeroCardInfo heroCardInfo)
        {
            self.HeroCardInfo = heroCardInfo;
            self.InitBaseInfo(heroCardInfo);
            self.InitStarView(heroCardInfo);
            self.CurrentCommonHeroCard.SetHeroCardInfo(self.HeroCardInfo);
        }
    }
}