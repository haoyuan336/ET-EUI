using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace ET
{
    public class HeroCardViewAwakeSystem: AwakeSystem<HeroCardView>
    {
        public override void Awake(HeroCardView self)
        {
        }
    }

    public static class HeroCardViewSystem
    {
        public static async ETTask PlayAddAttackEffect(this HeroCardView self, EventType.PlayAddAttackAngryViewAnim message)
        {
            if (message.AddAttack > 0)
            {
                Diamond diamond = message.Diamond;
                HeroCardViewCtl heroCardViewCtl = self.Parent.GetComponent<GameObjectComponent>().GameObject.GetComponent<HeroCardViewCtl>();
                await self.PlayAddEffectAnim(diamond, "DiamondAddAttackTrailEffect");
                heroCardViewCtl.UpdateAttackView(message.EndAttack.ToString());
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask PlayAddAngryEffect(this HeroCardView self, EventType.PlayAddAttackAngryViewAnim message)
        {
            if (message.AddAngry > 0)
            {
                Log.Debug($"end angry {message.EndAngry}");
                Log.Debug($"add angry {message.AddAngry}");
                Diamond diamond = message.Diamond;
                HeroCardViewCtl heroCardViewCtl = self.Parent.GetComponent<GameObjectComponent>().GameObject.GetComponent<HeroCardViewCtl>();
                await self.PlayAddEffectAnim(diamond, "DiamondAddAngryTrailEffect");
                float TotalAngry = HeroConfigCategory.Instance.Get(message.HeroCard.ConfigId).TotalAngry;
                heroCardViewCtl.UpdateAngryView($"{message.EndAngry.ToString()}/{TotalAngry}");
            }
        }

        public static async ETTask PlayAddEffectAnim(this HeroCardView self, Diamond diamond, string effectName)
        {
            Vector3 startPos = diamond.GetComponent<GameObjectComponent>().GameObject.transform.position + Vector3.back * 0.1f;
            Vector3 endPos = self.Parent.GetComponent<GameObjectComponent>().GameObject.transform.position + Vector3.back * 0.1f;
            GameObject bundleGameObject = (GameObject) ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit");
            GameObject prefab = bundleGameObject.Get<GameObject>(effectName);
            GameObject go = GameObject.Instantiate(prefab, GlobalComponent.Instance.Unit);
            go.transform.position = startPos;
            float distance = 1;
            while (distance > 0.1f)
            {
                Vector3 prePos = Vector3.Lerp(go.transform.position, endPos, 0.05f);
                go.transform.position = prePos;
                distance = Vector3.Distance(prePos, endPos);
                await TimerComponent.Instance.WaitFrameAsync();
            }
        }

        public static async ETTask PlayAttackAnimLogic(this HeroCardView self, EventType.PlayHeroCardAttackAnim message)
        {
            Log.Debug("play attack logic");
            HeroCard beAttackHeroCard = message.BeAttackHeroCard;
            GameObject targetGo = beAttackHeroCard.GetComponent<GameObjectComponent>().GameObject;
            await self.PlayChangeModeAnim();
            await self.PlayMoveToAnim(targetGo.transform.position);
            await self.PlayAttackAnim(message);
            // await beAttackHeroCard.UpdateHPView();
            await self.ProcessBeAttackAnimLogic(message.BeAttackHeroCard);
            await self.PlayMoveToAnim(self.Parent.GetComponent<GameObjectComponent>().GameObject.transform.position);
            await ETTask.CompletedTask;
        }

        public static async ETTask ProcessBeAttackAnimLogic(this HeroCardView self, HeroCard heroCard)
        {
            heroCard.GetComponent<GameObjectComponent>().GameObject.GetComponent<HeroCardViewCtl>().UpdateHPView(heroCard.HP);
            await ETTask.CompletedTask;
        }

        public static async ETTask PlayMoveToAnim(this HeroCardView self, Vector3 endPos)
        {
            GameObject selfGo = self.Parent.GetComponent<GameObjectComponent>().GameObject;
            GameObject heroMode = selfGo.GetComponent<HeroCardViewCtl>().GetHeroMode();
            float distance = 100;
            while (distance > 1)
            {
                Vector3 prePos = Vector3.Lerp(heroMode.transform.position, endPos, 0.01f);
                heroMode.transform.position = prePos;
                distance = Vector3.Distance(prePos, endPos);
                await TimerComponent.Instance.WaitFrameAsync();
            }
        }

        public static async ETTask PlayChangeModeAnim(this HeroCardView self)
        {
            GameObject selfGo = self.Parent.GetComponent<GameObjectComponent>().GameObject;
            selfGo.GetComponent<HeroCardViewCtl>().ChangeModeView();
            await ETTask.CompletedTask;
        }

        public static async ETTask PlayAttackAnim(this HeroCardView self, EventType.PlayHeroCardAttackAnim message)
        {
            HeroCard heroCard = message.AttackHeroCard;
            long skillId = heroCard.CurrentSkillId;
            Log.Debug($"Skill id {skillId}");
            Skill skill = heroCard.GetChild<Skill>(skillId);
            GameObject selfGo = self.Parent.GetComponent<GameObjectComponent>().GameObject;
            GameObject heroMode = selfGo.GetComponent<HeroCardViewCtl>().GetHeroMode();
            string skillAnimStr = "";
            switch (skill.SkillType)
            {
                case (int) SkillType.BigSkill:
                    skillAnimStr = "Attack";
                    break;
                case (int) SkillType.NormalSkill:
                    skillAnimStr = "Skill";
                    break;
            }

            Log.Debug("skill anim str = " + skillAnimStr);

            heroMode.GetComponent<Animator>().SetTrigger(skillAnimStr);
            await TimerComponent.Instance.WaitAsync(1000);
        }
    }
}