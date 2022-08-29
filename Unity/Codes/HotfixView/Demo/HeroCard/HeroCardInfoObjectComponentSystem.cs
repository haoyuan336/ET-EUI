using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public class HeroCardInfoObjectComponentAwakeSystem: AwakeSystem<HeroCardInfoObjectComponent, HeroCardInfo, HeroCardDataComponentInfo>
    {
        public override async void Awake(HeroCardInfoObjectComponent self, HeroCardInfo heroCardInfo,
        HeroCardDataComponentInfo heroCardDataComponentInfo)
        {
            GameObject prefab =
                    await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>("Assets/Bundles/UI/CustomUI/HeroCardInfoUI.prefab");
            self.GameObject = UnityEngine.Object.Instantiate(prefab, GlobalComponent.Instance.GameUIRoot);
            if (self.IsDisposed)
            {
                UnityEngine.Object.Destroy(self.GameObject);
                return;
            }

            self.HeroConfig = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            self.ESHeroCardInfoUI = self.AddChildWithId<ESHeroCardInfoUI, Transform>(IdGenerater.Instance.GenerateId(), self.GameObject.transform);
            self.ESHeroCardInfoUI.SetInfo(heroCardInfo, heroCardDataComponentInfo);

            self.ESHeroCardInfoUI.E_AttackBarImage.fillAmount = heroCardDataComponentInfo.DiamondAttackAddition;
            self.UpdateAngryView(heroCardDataComponentInfo);
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
        public static async void ShowBuffViewInfo(this HeroCardInfoObjectComponent self, List<BuffInfo> buffInfos)
        {
            self.ESHeroCardInfoUI.SetBuffInfos(buffInfos);

            await ETTask.CompletedTask;
        }

        public static async void ShowDamageViewAnim(this HeroCardInfoObjectComponent self, HeroCardDataComponentInfo component)
        {
            Log.Debug($"damage {component.Damage}");
            var gameObject = new GameObject();
            Text text = gameObject.AddComponent<Text>();
            text.transform.SetParent(GlobalComponent.Instance.NormalRoot);
            Font obj = AddressableComponent.Instance.LoadAssetByPath<Font>("Assets/Res/font/SVM-font/SVN-Aaron Script.otf");
            text.font = obj;
            text.text = $"{component.Damage}";
            text.fontSize = component.IsCritical? 50 : 40;
            text.fontStyle = FontStyle.Bold;
            Vector2 startPos = self.GameObject.transform.position;
            text.color = component.IsCritical? Color.red : Color.green;
            float time = 0;
            while (time < 1)
            {
                var vec = Vector2.Lerp(startPos, startPos + new Vector2(0, 100), time);
                text.transform.position = vec;
                text.transform.localScale = Vector3.one + Vector3.one * Mathf.Sin(Mathf.PI * (time + 0.01f));
                time += Time.deltaTime;
                await TimerComponent.Instance.WaitFrameAsync();
            }

            GameObject.Destroy(text);
        }

        public static async void UpdateHPView(this HeroCardInfoObjectComponent self, HeroCardDataComponentInfo componentInfo)
        {
            Log.Debug($"update hp view {componentInfo.HP}");
            Log.Debug($"total hp {componentInfo.TotalHP}");
            float percent = (float)componentInfo.HP / componentInfo.TotalHP;
            self.ESHeroCardInfoUI.E_HpBarImage.GetComponent<Image>().fillAmount = percent;
            await ETTask.CompletedTask;
        }

        public static void UpdateAngryView(this HeroCardInfoObjectComponent self, HeroCardDataComponentInfo info)
        {
            self.ESHeroCardInfoUI.E_AngryBarImage.GetComponent<Image>().fillAmount = (float)info.Angry / self.HeroConfig.TotalAngry;
        }

        public static async ETTask InitAttackAdditionView(this HeroCardInfoObjectComponent self, HeroCardDataComponentInfo info)
        {
            // self.ESHeroCardInfoUI.E_CommonText.GetComponent<Text>().text = "";
            self.ESHeroCardInfoUI.E_AttackBarImage.GetComponent<Image>().fillAmount = (float)info.DiamondAttackAddition / 200;
            await ETTask.CompletedTask;
        }

        //todo 更新显示增加的攻击力加成
        public static void UpdateAttackAdditionView(this HeroCardInfoObjectComponent self, HeroCardDataComponentInfo heroCardDataComponentInfo)
        {
            var addition = heroCardDataComponentInfo.DiamondAttackAddition;
            // var common = addItemAction.CrashCommonInfo;
            // self.ESHeroCardInfoUI.E_CommonText.GetComponent<Text>().text = $"CommonX{common.CommonCount}";
            self.ESHeroCardInfoUI.E_AttackBarImage.GetComponent<Image>().fillAmount = (float)addition / 100;
        }

        // public static void UpdateAngryView(this HeroCardInfoObjectComponent self, HeroCardDataComponent)
        // {
        //     
        // }
    }
}