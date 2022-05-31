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

                // self.DomainScene().GetComponent<UIComponent>().HideWindow(wind);
            });
            self.View.E_OkButtonButton.AddListenerAsync(self.OkButtonClick);

            // self.View.E_ChooseImageButton.AddListener(self.OnChooseHeroButtonClick);
            self.InitHeroCard().Coroutine();
        }

        public static async ETTask InitHeroCard(this DlgUpdateHeroStarLayer self)
        {
            //首先初始化当前的英雄信息
            // Assets/Bundles/UI/Common/ESHeroCard.prefab
            if (self.CurrentHeroCardObject == null)
            {
                self.CurrentHeroCardObject = await self.CreateOneHeroCard(self.View.E_CurrentHeroCardPosImage.transform);
            }

            if (self.ChooseHeroCardObject == null)
            {
                self.ChooseHeroCardObject = await self.CreateOneHeroCard(self.View.E_ChooseImageImage.transform);
                self.RegisterChooseHeroEvent();

            }

            if (self.NextStarHeroCardObject == null)
            {
                self.NextStarHeroCardObject = await self.CreateOneHeroCard(self.View.E_SuccessImageImage.transform);
            }

            
            
            await ETTask.CompletedTask;
        }

        public static void RegisterChooseHeroEvent(this DlgUpdateHeroStarLayer self)
        {
            if (self.HeroCardInfo.Star >= 5)
            {
                return;
            }
            
            Log.Debug("RegisterChooseHeroEvent");
            
            var addObj = UIFindHelper.FindDeepChild(self.ChooseHeroCardObject, "E_AddText");
            addObj.gameObject.SetActive(true);
            var chooseHeroButton = UIFindHelper.FindDeepChild(self.ChooseHeroCardObject, "E_Choose");
            chooseHeroButton.GetComponent<Toggle>().interactable = true;
            chooseHeroButton.GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
            chooseHeroButton.GetComponent<Toggle>().onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    chooseHeroButton.GetComponent<Toggle>().isOn = false;
                    // Log.Debug("click");
                    // self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_AllHeroBagLayer);
                    self.OnChooseHeroButtonClick();
                }
                
            });

        }

        public static async void SetHeroCardInfo(this DlgUpdateHeroStarLayer self, GameObject heroCard, HeroCardInfo heroCardInfo)
        {
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            //首先设置头像
            var spriteAtlas = ConstValue.HeroCardAtlasPath;
            var headImage = UIFindHelper.FindDeepChild(heroCard, "E_Head");
            var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, config.HeroIconImage);
            headImage.GetComponent<Image>().sprite = sprite;

            var addObj = UIFindHelper.FindDeepChild(heroCard, "E_AddText");
            addObj.gameObject.SetActive(false);

            var elementIcon = UIFindHelper.FindDeepChild(heroCard, "E_Element");
            elementIcon.gameObject.SetActive(true);
            ElementConfig elementConfig = ElementConfigCategory.Instance.Get(config.HeroColor);
            elementIcon.GetComponent<Image>().sprite =
                    await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, elementConfig.IconImage);

            HeroQualityTypeConfig heroQualityTypeConfig = HeroQualityTypeConfigCategory.Instance.Get(config.HeroQuality);

            var qualityIcon = UIFindHelper.FindDeepChild(heroCard, "E_QualityIcon");
            qualityIcon.gameObject.SetActive(true);
            qualityIcon.GetComponent<Image>().sprite =
                    await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, heroQualityTypeConfig.Icon);

            var levelText = UIFindHelper.FindDeepChild(heroCard, "E_Level");
            levelText.gameObject.SetActive(true);
            levelText.GetComponent<Text>().text = $"Lv.{heroCardInfo.Level}";

            for (int i = 0; i < 5; i++)
            {
                var star = UIFindHelper.FindDeepChild(heroCard, $"Star_{i}");
                // star.gameObject.SetActive();
                star.gameObject.SetActive(i < heroCardInfo.Star);
            }

            await ETTask.CompletedTask;
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
            if (self.ChooseHeroCardObject != null)
            {
                GameObject.Destroy(self.ChooseHeroCardObject);
            }

            if (self.NextStarHeroCardObject != null)
            {
                GameObject.Destroy(self.NextStarHeroCardObject);
            }
        }

        public static void PlayerChooseOneHeroCard(this DlgUpdateHeroStarLayer self, HeroCardInfo cardInfo)
        {
            //玩家选择了 一张英雄卡牌
            // self.c
            // self.ChooseCardInfo
            self.ChooseCardInfo = cardInfo;
            self.SetHeroCardInfo(self.ChooseHeroCardObject, self.ChooseCardInfo);
            var heroCarInfo = new HeroCardInfo()
            {
                ConfigId = self.HeroCardInfo.ConfigId,
                Rank = self.HeroCardInfo.Rank,
                Star = self.HeroCardInfo.Star + 1,
                Level = self.HeroCardInfo.Level
            };
            self.SetHeroCardInfo(self.NextStarHeroCardObject, heroCarInfo);
        }


        public static async ETTask OkButtonClick(this DlgUpdateHeroStarLayer self)
        {
            if (self.ChooseCardInfo == null)
            {
                return;
            }
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            Session session = self.ZoneScene().GetComponent<SessionComponent>().Session;
            C2M_UpdateHeroStarRequest request = new C2M_UpdateHeroStarRequest() { Account = accountId, HeroId = self.HeroCardInfo.HeroId , MaterialHeroId = self.ChooseCardInfo.HeroId};

            M2C_UpdateHeroStarResponse response = (M2C_UpdateHeroStarResponse) await session.Call(request);
            if (response.Error == ErrorCode.ERR_Success)
            {
                self.ChooseCardInfo = null;
                if (self.ChooseHeroCardObject != null)
                {
                    GameObject.Destroy(self.ChooseHeroCardObject);
                    self.ChooseHeroCardObject = null;
                }

                if (self.NextStarHeroCardObject != null)
                {
                    GameObject.Destroy(self.NextStarHeroCardObject);
                    self.NextStarHeroCardObject = null;
                }
                self.SetTargetInfo(response.HeroCardInfo);
                UIBaseWindow baseWindow = self.DomainScene().GetComponent<UIComponent>().GetUIBaseWindow(WindowID.WindowID_ShowHeroInfoLayer);
                baseWindow.GetComponent<DlgShowHeroInfoLayer>().SetHeroInfo(response.HeroCardInfo);
                
                self.DomainScene().GetComponent<UIComponent>().GetUIBaseWindow(WindowID.WindowID_AllHeroBagLayer).GetComponent<DlgAllHeroBagLayer>().ReferView();
            }

            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgUpdateHeroStarLayer self, Entity contextData = null)
        {
        }

        public static async void InitHeadImage(this DlgUpdateHeroStarLayer self, HeroCardInfo heroCardInfo)
        {
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            var spriteAltas = ConstValue.HeroCardAtlasPath;
            Sprite sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAltas, config.HeroIconImage);
            // self.View.E.sprite = sprite;
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

            // TimerComponent
            // UIFindHelper.FindDeepChild()
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

        public static async void SetTargetInfo(this DlgUpdateHeroStarLayer self, HeroCardInfo heroCardInfo)
        {
            self.HeroCardInfo = heroCardInfo;
            self.InitHeadImage(heroCardInfo);
            self.InitBaseInfo(heroCardInfo);
            self.InitStarView(heroCardInfo);
            await self.InitHeroCard();
            self.SetHeroCardInfo(self.CurrentHeroCardObject, self.HeroCardInfo);
            
        }
    }
}