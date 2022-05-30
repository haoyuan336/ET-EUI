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

            self.UpdateHPView(self.Parent.GetComponent<HeroCardDataComponent>().GetInfo());
            // self.UpdateAngryView();

            // self.HpBarImage.GetComponent<Image>().fillAmount = 1;
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
        public static async void ShowDamageViewAnim(this HeroCardInfoObjectComponent self, HeroCardDataComponentInfo component)
        {
            var gameObject = new GameObject();
            Text text = gameObject.AddComponent<Text>();
            // GlobalComponent.Instance.NormalRoot
            text.transform.SetParent(GlobalComponent.Instance.NormalRoot);

            Font obj = AddressableComponent.Instance.LoadAssetByPath<Font>("Assets/Res/font/SVM-font/SVN-Aaron Script.otf");
            text.font = obj;
            text.text = $"{component.NormalDamage}";
            text.fontSize = 50;
            text.fontStyle = FontStyle.Bold;
            Vector2 startPos = self.GameObject.transform.position;
            // text.GetComponent<RectTransform>().anchoredPosition = self.GameObject.GetComponent<RectTransform>().anchoredPosition;
            text.color = Color.red;
            float time = 0;
            while (time < 1)
            {
                var vec = Vector2.Lerp(startPos, startPos + new Vector2(0,100), time);
                text.transform.position = vec;
                time += Time.deltaTime;
                await TimerComponent.Instance.WaitFrameAsync();
            }
            GameObject.Destroy(text);
        }

        public static async void UpdateHPView(this HeroCardInfoObjectComponent self, HeroCardDataComponentInfo component)
        {
            var totalHp = self.GetParent<HeroCard>().GetComponent<HeroCardDataComponent>().GetHeroBaseHP();
            // var totalHp
            Log.Debug($"damage component {component.NormalDamage}");
            Log.Debug($"total hp {totalHp}");
            Log.Debug($"hp {component.HP}");
            float percent = (float) component.HP / totalHp;
            Log.Debug($"percent {percent}");
            self.HpBarImage.GetComponent<Image>().fillAmount = percent;
            await ETTask.CompletedTask;
        }

        public static async ETTask UpdateAngryView(this HeroCardInfoObjectComponent self, HeroCardDataComponentInfo info)
        {
            self.AngryBarImage.GetComponent<Image>().fillAmount = (float) info.Angry / self.HeroConfig.TotalAngry;
            await ETTask.CompletedTask;
        }

        public static async ETTask InitAttackAdditionView(this HeroCardInfoObjectComponent self, HeroCardDataComponentInfo info)
        {
            self.CommonText.GetComponent<Text>().text = "";
            self.AttackBarImage.GetComponent<Image>().fillAmount = (float) info.DiamondAttackAddition / 200;
            await ETTask.CompletedTask;
        }

        //todo 更新显示增加的攻击力加成
        public static async ETTask UpdateAttackAdditionView(this HeroCardInfoObjectComponent self, AddItemAction addItemAction)
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