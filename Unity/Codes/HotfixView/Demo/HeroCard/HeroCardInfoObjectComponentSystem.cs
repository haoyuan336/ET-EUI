using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    // public class HeroCardInfoObjectComponentDestroySystem: DestroySystem<HeroCardInfoObjectComponent>
    // {
    //     
    // }
    public class HeroCardInfoObjectComponentAwakeSystem: AwakeSystem<HeroCardInfoObjectComponent, int>
    {
        public override async void Awake(HeroCardInfoObjectComponent self, int configId)
        {
            GameObject prefab =
                    await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>("Assets/Bundles/UI/CustomUI/HeroCardInfoUI.prefab");
            self.GameObject = GameObject.Instantiate(prefab, GlobalComponent.Instance.NormalRoot.transform);
            // self.HeroMode = self.GetParent<HeroCard>().GetComponent<HeroModeObjectCompoent>().HeroMode;
            if (self.IsDisposed)
            {
                GameObject.Destroy(self.GameObject);
                return;
            }
            // self.GameObject.name = self.Id.ToString();
            self.AngryBarImage = UIFindHelper.FindDeepChild(self.GameObject, "AngryBar").gameObject;
            self.HeroElementIcon = UIFindHelper.FindDeepChild(self.GameObject, "HeroElementIcon").gameObject;
            self.HpBarImage = UIFindHelper.FindDeepChild(self.GameObject, "HpBar").gameObject;
            self.AttackBarImage = UIFindHelper.FindDeepChild(self.GameObject, "AttackBar").gameObject;
            self.CommonText = UIFindHelper.FindDeepChild(self.GameObject, "CommonText").gameObject;
            // HeroCard heroCard = self.GetParent<HeroModeObjectCompoent>().GetParent<HeroCard>();
            // var configId = heroCard.ConfigId;

            var config = HeroConfigCategory.Instance.Get(configId);
            self.HeroConfig = config;
            var elemengConfig = ElementConfigCategory.Instance.Get(config.HeroColor);
            var elemengImageStr = elemengConfig.IconImage;
            var sprite = await AddressableComponent.Instance.LoadSpriteAtlasByPathNameAsync(ConstValue.HeroCardAtlasPath, elemengImageStr);
            self.HeroElementIcon.GetComponent<Image>().sprite = sprite;

            self.HpBarImage.GetComponent<Image>().fillAmount = 1;
            self.AngryBarImage.GetComponent<Image>().fillAmount = 0;
            self.AttackBarImage.GetComponent<Image>().fillAmount = 0;
            self.CommonText.GetComponent<Text>().text = "";
            self.HeroHeight = 0;
        }
    }

    public class HeroCardInfoObjectComponentDestroySystem: DestroySystem<HeroCardInfoObjectComponent>
    {
        public override void Destroy(HeroCardInfoObjectComponent self)
        {
            Log.Debug("销毁卡牌info ");
            GameObject.Destroy(self.GameObject);
        }
    }
    public class HeroCardInfoObjectComponentUpdateSystem: UpdateSystem<HeroCardInfoObjectComponent>
    {
        public override void Update(HeroCardInfoObjectComponent self)
        {
            if (self.HeroMode == null)
            {
                HeroModeObjectCompoent heroModeObjectCompoent = self.Parent.GetComponent<HeroModeObjectCompoent>();
                if (heroModeObjectCompoent != null)
                {
                    self.HeroMode = heroModeObjectCompoent.HeroMode;
                }
            }
            if (self.GameObject != null && self.HeroMode != null)
            {
                Vector3 pos = Camera.main.WorldToScreenPoint(self.HeroMode.transform.position + Vector3.up * 1.8f);
                self.GameObject.transform.position = pos;
            }
        }
    }

    public static class HeroCardInfoObjectComponentSystem
    {

        public static async ETTask UpdateAngryView(this HeroCardInfoObjectComponent self, AddItemAction addItemAction)
        {
            self.AngryBarImage.GetComponent<Image>().fillAmount = (float) addItemAction.HeroCardDataComponentInfo.Angry / self.HeroConfig.TotalAngry;
            await ETTask.CompletedTask;
        }
        //todo 更新显示增加的攻击力加成
        public static async  ETTask UpdateAttackAdditionView(this HeroCardInfoObjectComponent self, AddItemAction addItemAction)
        {
            // HeroConfig heroConfig = HeroConfigCategory.Instance.Get(addItemAction.HeroCardDataComponentInfo.ConfigId);
            var addition = addItemAction.HeroCardDataComponentInfo.DiamondAttackAddition;
            var common = addItemAction.CrashCommonInfo;
            self.CommonText.GetComponent<Text>().text = $"CommonX{common.CommonCount}";
            self.AttackBarImage.GetComponent<Image>().fillAmount = (float) addition / 200;
            await ETTask.CompletedTask;
        }
    }
}