using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public class HeroCardInfoObjectComponentAwakeSystem: AwakeSystem<HeroCardInfoObjectComponent>
    {
        public override async void Awake(HeroCardInfoObjectComponent self)
        {
            GameObject prefab =
                    await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>("Assets/Bundles/UI/CustomUI/HeroCardInfoUI.prefab");
            self.GameObject = GameObject.Instantiate(prefab, GlobalComponent.Instance.NormalRoot.transform);
            self.HeroMode = self.GetParent<HeroModeObjectCompoent>().HeroMode;

            self.GameObject.name = self.Id.ToString();
            self.AngryBarImage = UIFindHelper.FindDeepChild(self.GameObject, "AngryBar").gameObject;
            self.HeroElementIcon = UIFindHelper.FindDeepChild(self.GameObject, "HeroElementIcon").gameObject;
            self.HpBarImage = UIFindHelper.FindDeepChild(self.GameObject, "HpBar").gameObject;

            HeroCard heroCard = self.GetParent<HeroModeObjectCompoent>().GetParent<HeroCard>();

            var configId = heroCard.ConfigId;
            var config = HeroConfigCategory.Instance.Get(configId);
            var elemengConfig = ElementConfigCategory.Instance.Get(config.HeroColor);
            var elemengImageStr = elemengConfig.IconImage;
            var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.HeroCardAtlasPath, elemengImageStr);
            self.HeroElementIcon.GetComponent<Image>().sprite = sprite;
            
            self.HpBarImage.GetComponent<Image>().fillAmount = 1;
            self.AngryBarImage.GetComponent<Image>().fillAmount = 0;
            self.HeroHeight = 0;
        }
    }

    public class HeroCardInfoObjectComponentDestroySystem: DestroySystem<HeroCardInfoObjectComponent>
    {
        public override void Destroy(HeroCardInfoObjectComponent self)
        {
            // Destroy(self.GameObject);
            GameObject.Destroy(self.GameObject);
        }
    }

    public class HeroCardInfoObjectComponentUpdateSystem: UpdateSystem<HeroCardInfoObjectComponent>
    {
        public override void Update(HeroCardInfoObjectComponent self)
        {
            if (self.GameObject != null && self.HeroMode != null)
            {
                Vector3 pos = Camera.main.WorldToScreenPoint(self.HeroMode.transform.position);
                self.GameObject.transform.position = pos;
            }
        }
    }

    public static class HeroCardInfoObjectComponentSystem
    {
        public static void UpdateView(this HeroCardInfoObjectComponent self, HeroCardInfo heroCardInfo)
        {
            HeroConfig config = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            var totalAngry = config.TotalAngry;
            var currentAngry = heroCardInfo.Angry;
            var percent = currentAngry / totalAngry;
            self.AngryBarImage.GetComponent<Image>().fillAmount = percent;

            var totalHP = heroCardInfo.TotalHP;
            var currentHP = heroCardInfo.HP;
            var hpPercent = currentHP / totalHP;

            self.HpBarImage.GetComponent<Image>().fillAmount = hpPercent;
        }
    }
}