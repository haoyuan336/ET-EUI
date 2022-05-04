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
            Vector3 pos = new Vector3(-2.5f + heroCard.InTroopIndex * 2.5f, 0, -5 * (heroCard.CampIndex == 0? 1 : -1));
            self.HeroMode.transform.position = pos;
            self.HeroMode.transform.forward = heroCard.CampIndex == 0? Vector3.forward : Vector3.back;
            self.HeroModeInitPos = new Vector3(pos.x, pos.y, pos.z);


            self.AddComponent<HeroCardInfoObjectComponent>();
            await ETTask.CompletedTask;
        }
    }

   
    public static class HeroModeObjectComponentSystem
    {
        public static void UpdateShowDataView(this HeroModeObjectCompoent self, HeroCardInfo heroCardInfo)
        {
            //todo 更新显示的英雄数据
            HeroCardInfoObjectComponent component = self.GetComponent<HeroCardInfoObjectComponent>();
            component.UpdateView(heroCardInfo);
        }
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
        }

        public static async ETTask PlayBeAttackAnim(this HeroModeObjectCompoent self, EventType.PlayHeroCardAttackAnim message)
        {
            self.HeroMode.GetComponent<Animator>().SetTrigger("BeAttack");
            self.Parent.GetComponent<HeroCardObjectComponent>().UpdateHeroCardTextView(message.BeAttackHeroCardInfo);
            self.GetComponent<HeroCardInfoObjectComponent>().UpdateView(message.BeAttackHeroCardInfo);
            await TimerComponent.Instance.WaitAsync(1000);
            if (message.BeAttackHeroCardInfo.HP <= 0)
            {
                self.HeroMode.GetComponent<Animator>().SetBool("Dead", true);
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask PlayAttackAnim(this HeroModeObjectCompoent self, EventType.PlayHeroCardAttackAnim message)
        {
            HeroCard heroCard = message.AttackHeroCard;
            HeroCard beAttackCard = message.BeAttackHeroCard;

            beAttackCard.GetComponent<HeroModeObjectCompoent>().PlayBeAttackAnim(message).Coroutine();

            long skillId = heroCard.CurrentSkillId;
            Log.Debug($"Skill id {skillId}");
            Skill skill = heroCard.GetChild<Skill>(skillId);
            SkillConfig skillConfig = SkillConfigCategory.Instance.Get(skill.ConfigId);
            string skillAnimStr = skillConfig.SkillAnimName;
            self.UpdateShowDataView(message.AttackHeroCardInfo);
            // switch (skillConfig.SkillType)
            // {
            //     case (int) SkillType.BigSkill:
            //         skillAnimStr = "Attack";
            //         break;
            //     case (int) SkillType.Skill1:
            //         skillAnimStr = "Skill1";
            //         break;
            // }

            // Log.Debug("skill anim str = " + skillAnimStr);
            // GameObject selfGo = self.Parent.GetComponent<HeroModeObjectCompoent>().HeroMode;

            self.HeroMode.GetComponent<Animator>().SetTrigger(skillAnimStr);
            await TimerComponent.Instance.WaitAsync(1000);
        }

        // public static async ETTask PlayAddAngryEffect(this HeroModeObjectCompoent self, EventType.PlayAddAngryViewAnim message)
        // {
        //     // Log.Debug("play add angry effect");
        //     Vector3 startPos = message.Diamond.GetComponent<GameObjectComponent>().GameObject.transform.position;
        //     await self.PlayAddEffectAnim(startPos, "DiamondAddAngryTrailEffect");
        //     // self.Parent.GetComponent<HeroCardObjectComponent>().UpdateHeroCardTextView();
        // }
        //
        // public static async ETTask PlayAddAttackEffect(this HeroModeObjectCompoent self, EventType.PlayAddAttackViewAnim message)
        // {
        //     Vector3 startPos = message.Diamond.GetComponent<GameObjectComponent>().GameObject.transform.position;
        //     // Diamond diamond = message.Diamond;
        //     // HeroCardViewCtl heroCardViewCtl = self.Parent.GetComponent<GameObjectComponent>().GameObject.GetComponent<HeroCardViewCtl>();
        //     await self.PlayAddEffectAnim(startPos, "DiamondAddAttackTrailEffect");
        //     // heroCardViewCtl.UpdateAttackView((message.HeroCard.Attack + message.HeroCard.DiamondAttack).ToString());
        //
        //     await ETTask.CompletedTask;
        // }

        public static async ETTask PlayAddEffectAnim(this HeroModeObjectCompoent self, Vector3 startPos, string effectName)
        {
            Log.Debug("play add effect anim");
            // Vector3 startPos = diamond.GetComponent<GameObjectComponent>().GameObject.transform.position + Vector3.back * 0.1f;
            // Vector3 endPos = self.Parent.GetComponent<HeroModeObjectCompoent>().HeroMode.transform.position + Vector3.up;
            Vector3 endPos = self.HeroMode.transform.position + Vector3.up;
            Log.Debug($"Load effect {effectName}");
            GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>(effectName);
            GameObject go = GameObject.Instantiate(prefab, GlobalComponent.Instance.Unit);
            go.transform.position = startPos;
            float time = 0;
            while (time < Mathf.PI * 0.5f)
            {
                // Vector3 prePos = Vector3.Lerp(go.transform.position, endPos, 0.05f);
                // go.transform.position = prePos;
                // distance = Vector3.Distance(prePos, endPos);
                var value = Mathf.Sin(time);
                Vector3 prePos = Vector3.Lerp(startPos, endPos, value);
                time += Time.deltaTime * 5;
                go.transform.position = prePos;
                await TimerComponent.Instance.WaitFrameAsync();
            }

            GameObject.Destroy(go);
        }
    }
}