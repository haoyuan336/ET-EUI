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

            //
            self.HeroConfig = HeroConfigCategory.Instance.Get(heroCardInfo.ConfigId);
            self.ESHeroCardInfoUI = self.AddChildWithId<ESHeroCardInfoUI, Transform>(IdGenerater.Instance.GenerateId(), self.GameObject.transform);
            self.ESHeroCardInfoUI.SetInfo(heroCardInfo, heroCardDataComponentInfo);
            //
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
        public static void SetDeadState(this HeroCardInfoObjectComponent self)
        {
            self.ESHeroCardInfoUI.SetBuffInfos(null);
        }

        public static async void ShowBuffViewInfo(this HeroCardInfoObjectComponent self, List<BuffInfo> buffInfos,
        HeroCardDataComponentInfo heroCardDataComponentInfo)
        {
            self.ESHeroCardInfoUI.SetBuffInfos(buffInfos);
            self.ESHeroCardInfoUI.E_HpShieldBarImage.fillAmount = 0;
            if (buffInfos == null)
            {
                return;
            }

            foreach (var buffInfo in buffInfos)
            {
                if (buffInfo.RoundCount == 0)
                {
                    continue;
                }

                switch (buffInfo.ConfigId)
                {
                    case 110:
                        self.ESHeroCardInfoUI.E_HpShieldBarImage.fillAmount = (float)buffInfo.HealthShield / heroCardDataComponentInfo.TotalHP;
                        break;
                }
            }

            await ETTask.CompletedTask;
        }

        public static async void ShowCareHPViewAnim(this HeroCardInfoObjectComponent self, HeroCardDataComponentInfo componentInfo)
        {
            Log.Debug($"add hp = {componentInfo.AddHP}");
            var gameObject = new GameObject();
            Text text = gameObject.AddComponent<Text>();
            text.transform.SetParent(GlobalComponent.Instance.NormalRoot);
            Font obj = AddressableComponent.Instance.LoadAssetByPath<Font>("Assets/Res/font/SVM-font/SVN-Aaron Script.otf");
            text.font = obj;
            text.text = $"+{componentInfo.AddHP}";
            text.fontSize = componentInfo.IsCritical? 50 : 40;
            text.fontStyle = FontStyle.Bold;
            Vector2 startPos = self.GameObject.transform.position;
            text.color = Color.green;
            ETTask task = ETTask.Create();
            AnimationToolComponent.Instance.MoveAction(new MoveActionItem()
            {
                Time = 1,
                CurrentPos = startPos,
                EndPos = startPos + new Vector2(0, 100),
                Task = task,
                Speed = 1,
                GameObject = text.gameObject,
            });
            AnimationToolComponent.Instance.ScaleAction(new ScaleActionItem()
            {
                Time = 1,
                CurrentScale = Vector3.one,
                EndScale = Vector3.one * 2,
                Speed = 1,
                GameObject = text.gameObject
            });
            await task.GetAwaiter();
            GameObject.Destroy(text);
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
            // float time = 0;
            // while (time < 1)
            // {
            //     var vec = Vector2.Lerp(startPos, startPos + new Vector2(0, 100), time);
            //     text.transform.position = vec;
            //     text.transform.localScale = Vector3.one + Vector3.one * Mathf.Sin(Mathf.PI * (time + 0.01f));
            //     time += Time.deltaTime;
            //     await TimerComponent.Instance.WaitFrameAsync();
            // }

            ETTask task = ETTask.Create();

            AnimationToolComponent.Instance.MoveAction(new MoveActionItem()
            {
                Time = 1,
                CurrentPos = startPos,
                EndPos = startPos + new Vector2(0, 100),
                Task = task,
                Speed = 1,
                GameObject = text.gameObject,
            });

            AnimationToolComponent.Instance.ScaleAction(new ScaleActionItem()
            {
                Time = 1,
                CurrentScale = Vector3.one,
                EndScale = Vector3.one * 2,
                Speed = 1,
                GameObject = text.gameObject
            });

            // self.MoveActionItems.Add(new MoveActionItem()
            // {
            //     Time = 1,
            //     CurrentPos = startPos,
            //     EndPos = startPos + new Vector2(0, 100),
            //     Task = task,
            //     Speed = 1,
            //     GameObject = text.gameObject,
            //     MoveActionType = MoveActionType.Normal
            //     // public float CurrentTime = 0;
            //     // public float Time;
            //     // public Vector3 CurrentPos;
            //     // public Quaternion CurrentQuat;
            //     // public Quaternion EndQuat;
            //     // public Vector3 EndPos;
            //     // public ETTask Task;
            //     // public float Speed;
            //     // public GameObject GameObject;
            //     // public MoveActionType MoveActionType = MoveActionType.Invalide;
            //     // public StateType StateType = StateType.Active;
            // });
            await task.GetAwaiter();

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

        public static async void UpdateAngryView(this HeroCardInfoObjectComponent self, HeroCardDataComponentInfo info)
        {
            self.ESHeroCardInfoUI.E_AngryBarImage.GetComponent<Image>().fillAmount = (float)info.Angry / self.HeroConfig.TotalAngry;
            var gameObject = new GameObject();
            Text text = gameObject.AddComponent<Text>();
            text.transform.SetParent(GlobalComponent.Instance.NormalRoot);
            Font obj = await AddressableComponent.Instance.LoadAssetByPathAsync<Font>("Assets/Res/font/SVM-font/SVN-Aaron Script.otf");
            text.font = obj;
            text.text = $"+{info.AddAngry}";
            text.fontStyle = FontStyle.Bold;
            text.fontSize = 60;
            Vector2 startPos = self.GameObject.transform.position;
            text.color = Color.yellow;
            ETTask task = ETTask.Create();
            AnimationToolComponent.Instance.MoveAction(new MoveActionItem()
            {
                Time = 1,
                CurrentPos = startPos,
                EndPos = startPos + new Vector2(startPos.x, 100),
                Task = task,
                Speed = 1,
                GameObject = text.gameObject,
            });

            AnimationToolComponent.Instance.ScaleAction(new ScaleActionItem()
            {
                Time = 1,
                CurrentScale = Vector3.one,
                EndScale = Vector3.one * 2,
                Speed = 1,
                GameObject = text.gameObject
            });
            await task.GetAwaiter();
            GameObject.Destroy(text);
        }

        public static void InitAttackAdditionView(this HeroCardInfoObjectComponent self, HeroCardDataComponentInfo info)
        {
            self.ESHeroCardInfoUI.E_AttackBarImage.GetComponent<Image>().fillAmount = (float)info.DiamondAttackAddition / 200;
        }

        //todo 更新显示增加的攻击力加成
        public static void UpdateAttackAdditionView(this HeroCardInfoObjectComponent self, HeroCardDataComponentInfo heroCardDataComponentInfo)
        {
            var addition = heroCardDataComponentInfo.DiamondAttackAddition;
            self.ESHeroCardInfoUI.E_AttackBarImage.GetComponent<Image>().fillAmount = (float)addition / 100;
        }
    }
}