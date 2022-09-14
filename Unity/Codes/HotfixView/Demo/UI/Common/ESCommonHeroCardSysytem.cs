using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public class ESCommonHeroCardAwakeSystem2: AwakeSystem<ESCommonHeroCard, Transform, bool>
    {
        public override async void Awake(ESCommonHeroCard self, Transform a, bool b)
        {
            await self.Awake(a);
            self.IsCanClick = b;
            self.E_Toggle.interactable = a;
            Log.Debug($"awake b{b}");
            self.E_Add.gameObject.SetActive(b);
            if (b)
            {
                self.E_Toggle.onValueChanged.RemoveAllListeners();
                // self.E_Toggle.isOn = false;
                self.E_Toggle.AddListener((value) =>
                {
                    if (value)
                    {
                        self.E_Toggle.isOn = false;
                        if (self.OnButtonClick != null)
                        {
                            self.OnButtonClick();
                        }
                    }
                });
            }
        }
    }

    public class ESCommonHeroCardAwakeSystem: AwakeSystem<ESCommonHeroCard, Transform>
    {
        public override async void Awake(ESCommonHeroCard self, Transform parent)
        {
            await self.Awake(parent);

            await ETTask.CompletedTask;
        }
    }

    public static class ESHeroCardSysytem
    {
        public static async ETTask Awake(this ESCommonHeroCard self, Transform parent)
        {
            var gameObject = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath("Assets/Bundles/UI/Common/ESHeroCard.prefab");
            gameObject.transform.SetParent(parent);
            gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            gameObject.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            gameObject.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            gameObject.GetComponent<RectTransform>().localScale = Vector3.one;

            self.GameObject = gameObject;
            self.E_HeadImage = UIFindHelper.FindDeepChild(gameObject, "E_Head").GetComponent<Image>();
            self.E_Add = UIFindHelper.FindDeepChild(gameObject, "E_AddText").gameObject;
            self.E_ElemanetIconImage = UIFindHelper.FindDeepChild(gameObject, "E_Element").GetComponent<Image>();
            self.E_QaulityIconImage = UIFindHelper.FindDeepChild(gameObject, "E_QualityIcon").GetComponent<Image>();
            self.E_LevelText = UIFindHelper.FindDeepChild(gameObject, "E_Level").GetComponent<Text>();
            self.E_Toggle = UIFindHelper.FindDeepChild(gameObject, "E_Choose").GetComponent<Toggle>();
            for (int i = 0; i < 5; i++)
            {
                var star = UIFindHelper.FindDeepChild(self.GameObject, $"Star_{i}");
                self.StarList.Add(star.gameObject);
            }

            self.ReferView();
        }

        public static async void ReferView(this ESCommonHeroCard self)
        {
            if (self.GameObject == null)
            {
                return;
            }

            if (self.HeroCardInfo != null)
            {
                HeroConfig heroConfig = HeroConfigCategory.Instance.Get(self.HeroCardInfo.ConfigId);
                ElementConfig elementConfig = ElementConfigCategory.Instance.Get(heroConfig.HeroColor);
                HeroQualityTypeConfig qualityTypeConfig = HeroQualityTypeConfigCategory.Instance.Get(heroConfig.HeroQuality);
                var spriteAtlas = ConstValue.HeroCardAtlasPath;

                List<ETTask<Sprite>> tasks = new List<ETTask<Sprite>>();
                tasks.Add(AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, heroConfig.HeroIconImage));
                tasks.Add(AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, elementConfig.IconImage));
                tasks.Add(AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, qualityTypeConfig.Icon));
                await ETTaskHelper.WaitAll(tasks);
                // var sprite = ;
                self.E_ElemanetIconImage.gameObject.SetActive(true);
                self.E_QaulityIconImage.gameObject.SetActive(true);
                self.E_LevelText.gameObject.SetActive(true);
                self.E_HeadImage.sprite = tasks[0].GetResult();
                self.E_ElemanetIconImage.sprite = tasks[1].GetResult();
                self.E_QaulityIconImage.sprite = tasks[2].GetResult();

                for (int i = 0; i < self.StarList.Count; i++)
                {
                    GameObject star = self.StarList[i];
                    star.SetActive(i < self.HeroCardInfo.Star);
                }

                self.E_Add.SetActive(false);
                self.E_LevelText.text = $"LV.{self.HeroCardInfo.Level}";
            }
            else
            {
                foreach (var star in self.StarList)
                {
                    star.SetActive(false);
                }
                self.E_Add.gameObject.SetActive(self.IsCanClick);
                self.E_ElemanetIconImage.gameObject.SetActive(false);
                self.E_QaulityIconImage.gameObject.SetActive(false);
                self.E_LevelText.gameObject.SetActive(false);

                var spriteAtlas = ConstValue.CommonUIAtlasPath;
                var bgPath = ConstValue.FrameBgPath;
                var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(spriteAtlas, bgPath);
                self.E_HeadImage.sprite = sprite;
            }
        }

        public static void SetHeroCardInfo(this ESCommonHeroCard self, HeroCardInfo heroCardInfo)
        {
            self.HeroCardInfo = heroCardInfo;
            self.ReferView();
        }
    }
}