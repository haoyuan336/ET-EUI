using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ET
{
    public class HeroModeObjectComponentAwakeSystem: AwakeSystem<HeroModeObjectCompoent, HeroCard>
    {
        public override async void Awake(HeroModeObjectCompoent self, HeroCard heroCard)
        {
            Log.Debug($"hero card config id {heroCard.ConfigId}");
            //加载英雄模型
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroCard.ConfigId);
            var heroModeStr = heroConfig.HeroMode;
            Log.Debug($"hero mode name {heroConfig.HeroMode}");

            GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>(heroModeStr);
            self.HeroMode = GameObject.Instantiate(prefab);
            Vector3 pos = new Vector3(-3 + heroCard.InTroopIndex * 3, 0, -7 * (heroCard.CampIndex == 0? 1 : -1));
            self.HeroMode.transform.position = pos;
            self.HeroMode.transform.forward = heroCard.CampIndex == 0? Vector3.forward : Vector3.back;
            self.HeroModeInitPos = new Vector3(pos.x, pos.y, pos.z);
            await ETTask.CompletedTask;
        }
    }

    public static class HeroModeObjectComponentSystem
    {
        public static async ETTask PlayMoveToAnim(this HeroModeObjectCompoent self, Vector3 startPos, Vector3 targetPos)
        {
            float time = 0;
            // float distance = Vector3.Distance(targetPos, self.HeroModeInitPos);
            while (time < Mathf.PI * 0.5f)
            {
                time += Time.deltaTime * 2;
                float value = Mathf.Sin(time);
                Vector3 prePos = Vector3.Lerp(startPos, targetPos, value);
                self.HeroMode.transform.position = prePos;
                // distance = Vector3.Distance(prePos, targetPos);
                await TimerComponent.Instance.WaitFrameAsync();
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask PlayAttackAnimLogic(this HeroModeObjectCompoent self, EventType.PlayHeroCardAttackAnim message)
        {
            HeroCard beAttackHeroCard = message.BeAttackHeroCard;

            HeroModeObjectCompoent beAttackHeroModeCom = beAttackHeroCard.GetComponent<HeroModeObjectCompoent>();

            Vector3 offsetPos = self.GetParent<HeroCard>().CampIndex == 0? Vector3.back
                    : Vector3.forward;
            await self.PlayMoveToAnim(self.HeroModeInitPos, beAttackHeroModeCom.HeroMode.transform.position + offsetPos);
            await self.PlayAttackAnim(message);
            await self.PlayMoveToAnim(self.HeroMode.transform.position, self.HeroModeInitPos);
            await ETTask.CompletedTask;
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
        }

        public static async ETTask PlayBeAttackAnim(this HeroModeObjectCompoent self)
        {
            self.HeroMode.GetComponent<Animator>().SetTrigger("BeAttack");
            await ETTask.CompletedTask;
        }
        public static async ETTask PlayAttackAnim(this HeroModeObjectCompoent self, EventType.PlayHeroCardAttackAnim message)
        {
            HeroCard heroCard = message.AttackHeroCard;
            HeroCard beAttackCard = message.BeAttackHeroCard;

            beAttackCard.GetComponent<HeroModeObjectCompoent>().PlayBeAttackAnim().Coroutine();

            long skillId = heroCard.CurrentSkillId;
            Log.Debug($"Skill id {skillId}");
            Skill skill = heroCard.GetChild<Skill>(skillId);
            SkillConfig skillConfig = SkillConfigCategory.Instance.Get(skill.ConfigId);
            string skillAnimStr = "";
            switch (skillConfig.SkillType)
            {
                case (int) SkillType.BigSkill:
                    skillAnimStr = "Attack";
                    break;
                case (int) SkillType.NormalSkill:
                    skillAnimStr = "Skill";
                    break;
            }

            Log.Debug("skill anim str = " + skillAnimStr);
            // GameObject selfGo = self.Parent.GetComponent<HeroModeObjectCompoent>().HeroMode;
            
            
            self.HeroMode.GetComponent<Animator>().SetTrigger(skillAnimStr);
            await TimerComponent.Instance.WaitAsync(1000);
        }
    }
}