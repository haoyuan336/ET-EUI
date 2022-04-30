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
        // public static async ETTask PlayAddAttackEffect(this HeroCardView self, EventType.PlayAddAttackViewAnim message)
        // {
        //     Vector3 startPos = message.Diamond.GetComponent<GameObjectComponent>().GameObject.transform.position;
        //     // Diamond diamond = message.Diamond;
        //     // HeroCardViewCtl heroCardViewCtl = self.Parent.GetComponent<GameObjectComponent>().GameObject.GetComponent<HeroCardViewCtl>();
        //     await self.PlayAddEffectAnim(startPos, "DiamondAddAttackTrailEffect");
        //     // heroCardViewCtl.UpdateAttackView((message.HeroCard.Attack + message.HeroCard.DiamondAttack).ToString());
        //
        //     await ETTask.CompletedTask;
        // }

        // public static async ETTask PlayAddAngryEffect(this HeroCardView self, EventType.PlayAddAngryViewAnim message)
        // {
        //     Log.Debug("play add angry effect");
        //     Vector3 startPos = message.Diamond.GetComponent<GameObjectComponent>().GameObject.transform.position;
        //     await self.PlayAddEffectAnim(startPos, "DiamondAddAngryTrailEffect");
        //     self.Parent.GetComponent<HeroCardObjectComponent>().UpdateHeroCardTextView();
        // }

        // public static async ETTask PlayAddEffectAnim(this HeroCardView self, Vector3 startPos, string effectName)
        // {
        //     Log.Debug("play add effect anim");
        //     // Vector3 startPos = diamond.GetComponent<GameObjectComponent>().GameObject.transform.position + Vector3.back * 0.1f;
        //     Vector3 endPos = self.Parent.GetComponent<HeroModeObjectCompoent>().HeroMode.transform.position + Vector3.up;
        //     Log.Debug($"Load effect {effectName}");
        //     GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>(effectName);
        //     GameObject go = GameObject.Instantiate(prefab, GlobalComponent.Instance.Unit);
        //     go.transform.position = startPos;
        //     float distance = 1;
        //     while (distance > 0.1f)
        //     {
        //         Vector3 prePos = Vector3.Lerp(go.transform.position, endPos, 0.05f);
        //         go.transform.position = prePos;
        //         distance = Vector3.Distance(prePos, endPos);
        //         await TimerComponent.Instance.WaitFrameAsync();
        //     }
        // }

        // public static async ETTask PlayAttackAnimLogic(this HeroCardView self, EventType.PlayHeroCardAttackAnim message)
        // {
        //     Log.Debug("play attack logic");
        //     HeroCard beAttackHeroCard = message.BeAttackHeroCard;
        //     HeroModeObjectCompoent beAttackHeroModeCom = beAttackHeroCard.GetComponent<HeroModeObjectCompoent>();
        //     HeroModeObjectCompoent selfHeroModeCom = self.Parent.GetComponent<HeroModeObjectCompoent>();
        //     Vector3 initPos = selfHeroModeCom.HeroMode.transform.position;
        //     await self.PlayChangeModeAnim();
        //     await selfHeroModeCom.PlayMoveToAnim(beAttackHeroModeCom.HeroMode.transform.position);
        //     // await self.PlayMoveToAnim(targetGo.transform.position);
        //     await self.PlayAttackAnim(message);
        //     // await beAttackHeroCard.UpdateHPView();
        //     await self.ProcessBeAttackAnimLogic(message.BeAttackHeroCard);
        //     // await self.PlayMoveToBackAnim(startPos);
        //     await selfHeroModeCom.PlayMoveToAnim(initPos);
        //     await ETTask.CompletedTask;
        // }

        // public static async ETTask ProcessBeAttackAnimLogic(this HeroCardView self, HeroCard heroCard)
        // {
        //     float totalAngry = HeroConfigCategory.Instance.Get(heroCard.ConfigId).TotalAngry;
        //     heroCard.GetComponent<HeroCardObjectComponent>().HeroCard.GetComponent<HeroCardViewCtl>()
        //             .UpdateAngryView($"{heroCard.Angry.ToString()}/{totalAngry}");
        //     heroCard.GetComponent<HeroCardObjectComponent>().HeroCard.GetComponent<HeroCardViewCtl>().UpdateHPView(heroCard.HP);
        //     await ETTask.CompletedTask;
        // }

        // public static async ETTask PlayMoveToAnim(this HeroCardView self, Vector3 targetPos)
        // {
        //     GameObject heroMode = self.Parent.GetComponent<HeroModeObjectCompoent>().HeroMode;
        //     float distance = 100;
        //     while (distance > 3f)
        //     {
        //         Vector3 prePos = Vector3.Lerp(selfGo.transform.position, endPos, 0.01f);
        //         selfGo.transform.position = prePos;
        //         distance = Vector3.Distance(prePos, endPos);
        //         await TimerComponent.Instance.WaitFrameAsync();
        //     }
        // }

        // public static async ETTask PlayMoveToAnim(this HeroCardView self, Vector3 endPos)
        // {
        //     GameObject selfGo = self.Parent.GetComponent<HeroModeObjectCompoent>().HeroMode;
        //     // GameObject heroMode = selfGo;
        //     float distance = 100;
        //     while (distance > 3f)
        //     {
        //         Vector3 prePos = Vector3.Lerp(selfGo.transform.position, endPos, 0.01f);
        //         selfGo.transform.position = prePos;
        //         distance = Vector3.Distance(prePos, endPos);
        //         await TimerComponent.Instance.WaitFrameAsync();
        //     }
        // }
        //
        // public static async ETTask PlayMoveToBackAnim(this HeroCardView self, Vector3 endPos)
        // {
        //     // Vector3 endPos = self.Parent.GetComponent<HeroCardObjectComponent>().HeroCard.transform.position;
        //     // GameObject selfGo = self.Parent.GetComponent<HeroCardObjectComponent>().GameObject;
        //     GameObject heroMode = self.Parent.GetComponent<HeroModeObjectCompoent>().HeroMode;
        //     float distance = 100;
        //     while (distance > 0.1f)
        //     {
        //         Vector3 prePos = Vector3.Lerp(heroMode.transform.position, endPos, 0.01f);
        //         heroMode.transform.position = prePos;
        //         distance = Vector3.Distance(prePos, endPos);
        //         await TimerComponent.Instance.WaitFrameAsync();
        //     }
        // }

        // public static async ETTask PlayChangeModeAnim(this HeroCardView self)
        // {
        //     // GameObject selfGo = self.Parent.GetComponent<GameObjectComponent>().GameObject;
        //     // selfGo.GetComponent<HeroCardViewCtl>().ChangeModeView();
        //     await ETTask.CompletedTask;
        // }

        // public static async ETTask PlayAttackAnim(this HeroCardView self, EventType.PlayHeroCardAttackAnim message)
        // {
        //     HeroCard heroCard = message.AttackHeroCard;
        //     long skillId = heroCard.CurrentSkillId;
        //     Log.Debug($"Skill id {skillId}");
        //     Skill skill = heroCard.GetChild<Skill>(skillId);
        //     SkillConfig skillConfig = SkillConfigCategory.Instance.Get(skill.ConfigId);
        //     string skillAnimStr = "";
        //     switch (skillConfig.SkillType)
        //     {
        //         case (int) SkillType.BigSkill:
        //             skillAnimStr = "Attack";
        //             break;
        //         case (int) SkillType.NormalSkill:
        //             skillAnimStr = "Skill";
        //             break;
        //     }
        //
        //     Log.Debug("skill anim str = " + skillAnimStr);
        //     GameObject selfGo = self.Parent.GetComponent<HeroModeObjectCompoent>().HeroMode;
        //     selfGo.GetComponent<Animator>().SetTrigger(skillAnimStr);
        //     await TimerComponent.Instance.WaitAsync(1000);
        // }
    }
}