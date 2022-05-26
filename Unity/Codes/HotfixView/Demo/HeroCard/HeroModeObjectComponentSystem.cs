using System;
using System.IO.Pipes;
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
            Vector3 pos = new Vector3(-1.5f + heroCard.InTroopIndex * 1.5f, 0, -3 * (heroCard.CampIndex == 0? 1 : -1));
            self.HeroMode.transform.position = pos;
            self.HeroMode.transform.forward = heroCard.CampIndex == 0? Vector3.forward : Vector3.back;
            self.HeroModeInitPos = new Vector3(pos.x, pos.y, pos.z);

            self.AddComponent<HeroCardInfoObjectComponent>();
            await ETTask.CompletedTask;
        }
    }

    public class HeroModeObjectComponentUpdateSystem: UpdateSystem<HeroModeObjectCompoent>
    {
        public override void Update(HeroModeObjectCompoent self)
        {
            if (Input.GetMouseButtonDown(0))
            {
                self.IsTouching = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                self.IsTouching = false;
            }

            if (self.IsTouching)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit raycastHit;
                var maskCode = LayerMask.GetMask("Default");
                var isHided = Physics.Raycast(ray, out raycastHit, 10000, maskCode);
                if (isHided && raycastHit.transform.gameObject.Equals(self.HeroMode))
                {
                    self.IsTouching = false;
                    Log.Debug($"is hitde obj {self.HeroMode.name}");
                    HeroCard heroCard = self.GetParent<HeroCard>();
                    Log.Debug($"hero card camp index{heroCard.CampIndex}");
                    if (heroCard.CampIndex != 0)
                    {
                        //点击了 非本阵营的英雄，开启集火模式
                        // heroCard.GetComponent<>()

                        // zoneScene.GetComponent<OperaComponent>().TouchLock = false;
                        var touchLock = heroCard.DomainScene().GetComponent<OperaComponent>().TouchLock;
                        if (touchLock)
                        {
                            Log.Debug("不可操作阶段");
                            return;
                        }

                        var roomId = self.ZoneScene().GetComponent<PlayerComponent>().RoomId;
                        C2M_PlayerClickHeroMode clickHeroMode = new C2M_PlayerClickHeroMode() { HeroId = heroCard.Id, RoomId = roomId };
                        heroCard.ZoneScene().GetComponent<SessionComponent>().Session.Send(clickHeroMode);
                    }
                }
            }
        }
    }

    public static class HeroModeObjectComponentSystem
    {
        public static async void ShowChooseMark(this HeroModeObjectCompoent self, bool isShow)
        {
            if (self.ChooseMark != null)
            {
                GameObject.Destroy(self.ChooseMark);
                self.ChooseMark = null;
            }

            if (isShow)
            {
                var path = "Assets/Bundles/Unit/HeroModePrefabs/HeroChooseMark.prefab";
                GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>(path);
                self.ChooseMark = GameObject.Instantiate(prefab);
                var height = self.HeroMode.GetComponentInChildren<SkinnedMeshRenderer>().bounds.size.y;

                self.ChooseMark.transform.position = self.HeroMode.transform.position + Vector3.up * height;
            }
        }

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
            self.HeroMode.GetComponent<Animator>().SetBool("Run", true);
            while (time < Mathf.PI * 0.5f)
            {
                time += Time.deltaTime * 2;
                float value = Mathf.Sin(time);
                Vector3 prePos = Vector3.Lerp(startPos, targetPos, value);
                self.HeroMode.transform.position = prePos;
                // distance = Vector3.Distance(prePos, targetPos);
                await TimerComponent.Instance.WaitFrameAsync();
            }

            self.HeroMode.GetComponent<Animator>().SetBool("Run", false);

            await ETTask.CompletedTask;
        }

        public static async ETTask PlayAttackAnimLogic(this HeroModeObjectCompoent self, EventType.PlayHeroCardAttackAnim message)
        {
            // if (self.ChooseMark != null)
            // {
            //     GameObject.Destroy(self.ChooseMark);
            // }
            HeroCard beAttackHeroCard = message.BeAttackHeroCard;
            HeroCard heroCard = message.AttackHeroCard;

            long skillId = heroCard.CurrentSkillId;
            Skill skill = heroCard.GetChild<Skill>(skillId);
            // skill.ConfigId = 1000009;
            SkillConfig skillConfig = SkillConfigCategory.Instance.Get(skill.ConfigId);
            await self.MoveToEnemyTarget(beAttackHeroCard, skillConfig);
            await self.PlayAttackAnim(message, skillConfig);
            await self.BackMoveToInitPos(skillConfig);
            // await self.PlayMoveToAnim(self.HeroMode.transform.position, self.HeroModeInitPos);
            await ETTask.CompletedTask;
        }

        public static async ETTask BackMoveToInitPos(this HeroModeObjectCompoent self, SkillConfig skillConfig)
        {
            if (skillConfig.MoveType == (int) HeroMoveType.NoMove)
            {
                return;
            }

            await self.PlayMoveToAnim(self.HeroMode.transform.position, self.HeroModeInitPos);
        }

        public static async ETTask MoveToEnemyTarget(this HeroModeObjectCompoent self, HeroCard beAttackHeroCard, SkillConfig skillConfig)
        {
            if (skillConfig.MoveType == (int) HeroMoveType.NoMove)
            {
                return;
            }

            HeroModeObjectCompoent beAttackHeroModeCom = beAttackHeroCard.GetComponent<HeroModeObjectCompoent>();
            Vector3 offsetPos = self.GetParent<HeroCard>().CampIndex == 0? Vector3.back
                    : Vector3.forward;

            var endPos = beAttackHeroModeCom.HeroMode.transform.position + offsetPos;
            switch (skillConfig.TargetPosType)
            {
                case (int) TargetPosMoveType.Face:
                    break;
                case (int) TargetPosMoveType.Middle:

                    endPos = (self.HeroMode.transform.position + beAttackHeroModeCom.HeroMode.transform.position) * 0.5f;
                    break;
            }

            await self.PlayMoveToAnim(self.HeroModeInitPos, endPos);
        }
        // public static async ETTask LoadPrefab(this HeroModeObjectCompoent self, string path)
        // {
        //     Log.Debug($"加载技能特效预制件 {path}");
        //     GameObject effect = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath(path);
        //
        //     TimerComponent.Instance.WaitAsync(5000);
        //     GameObject.Destroy(effect);
        // }

        public static async ETTask PlayBeHitedEffect(this HeroModeObjectCompoent self, SkillConfig skillConfig)
        {
            if (skillConfig.BeAttackEffect == "")
            {
                return;
            }

            GameObject effect = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath(skillConfig.BeAttackEffect);
            if (skillConfig.BeAttackBoneName == "")
            {
                effect.transform.position = self.HeroMode.transform.position;
            }
            else
            {
                Transform bone = UIFindHelper.FindDeepChild(self.HeroMode, skillConfig.BeAttackBoneName);
                if (bone != null)
                {
                    Log.Debug("找到了 被攻击的骨骼位置");
                    effect.transform.position = bone.transform.position;
                }
                else
                {
                    Log.Debug("未找到被攻击骨骼");
                }
            }

            await TimerComponent.Instance.WaitAsync(5000);
            GameObject.Destroy(effect);
            // self.LoadPrefab(skillcon)
        }

        public static async ETTask PlayBeAttackAnim(this HeroModeObjectCompoent self, EventType.PlayHeroCardAttackAnim message,
        SkillConfig skillConfig)
        {
            int beAttackTime = skillConfig.BeAttackTime;
            await TimerComponent.Instance.WaitAsync(beAttackTime);
            self.HeroMode.GetComponent<Animator>().SetTrigger("BeAttack");
            self.Parent.GetComponent<HeroCardObjectComponent>().UpdateHeroCardTextView(message.BeAttackHeroCardInfo);
            self.GetComponent<HeroCardInfoObjectComponent>().UpdateView(message.BeAttackHeroCardInfo);
            self.PlayBeHitedEffect(skillConfig).Coroutine();
            if (message.BeAttackHeroCardInfo.HP <= 0)
            {
                self.HeroMode.GetComponent<Animator>().SetBool("Dead", true);
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask PlayAttackAnim(this HeroModeObjectCompoent self, EventType.PlayHeroCardAttackAnim message, SkillConfig skillConfig)
        {
            HeroCard beAttackCard = message.BeAttackHeroCard;
            self.PlaySkillEffect(skillConfig);
            self.PlayFlyEffect(skillConfig, beAttackCard);
            beAttackCard.GetComponent<HeroModeObjectCompoent>().PlayBeAttackAnim(message, skillConfig).Coroutine();
            string skillAnimStr = skillConfig.SkillAnimName;
            self.UpdateShowDataView(message.AttackHeroCardInfo);
            self.HeroMode.GetComponent<Animator>().SetTrigger(skillAnimStr);
            var skillTime = skillConfig.SkillTime;
            await TimerComponent.Instance.WaitAsync(skillTime);
        }

        public static async void PlayFlyEffect(this HeroModeObjectCompoent self, SkillConfig config, HeroCard beAttackHeroCard)
        {
            if (config.FlyEffect == "")
            {
                return;
            }

            await TimerComponent.Instance.WaitAsync(config.FlyEffectStartTime);
            GameObject effect = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath(config.FlyEffect);
            HeroModeObjectCompoent heroModeObject = beAttackHeroCard.GetComponent<HeroModeObjectCompoent>();
            Vector3 startPos = self.HeroMode.transform.position + Vector3.up;
            Vector3 endPos = heroModeObject.HeroMode.transform.position + Vector3.up;
            effect.transform.position = startPos;
            float time = 0;
            effect.transform.forward = endPos - startPos;
            while (time < 0.5f)
            {
                Vector3 pos = Vector3.Lerp(startPos, endPos, time * 2);
                effect.transform.position = pos;
                time += Time.deltaTime;
                Log.Debug($"time {time}");
                await TimerComponent.Instance.WaitFrameAsync();
            }

            //
            GameObject.Destroy(effect);
        }

        public static async void PlaySkillEffect(this HeroModeObjectCompoent self, SkillConfig config)
        {
            if (config.SkillEffect != "")
            {
                GameObject pre = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>(config.SkillEffect);
                await TimerComponent.Instance.WaitAsync(config.EffectStartTime);
                GameObject obj = GameObject.Instantiate(pre);
                obj.transform.forward = self.HeroMode.transform.forward;
                obj.transform.position = self.HeroMode.transform.position;
                await TimerComponent.Instance.WaitAsync(5000);
                GameObject.Destroy(obj);
            }
        }

        public static async ETTask PlayAddEffectAnim(this HeroModeObjectCompoent self, Vector3 startPos, string effectName)
        {
            Log.Debug("play add effect anim");
            Vector3 endPos = self.HeroMode.transform.position + Vector3.up;
            Log.Debug($"Load effect {effectName}");
            GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>(effectName);
            GameObject go = GameObject.Instantiate(prefab, GlobalComponent.Instance.Unit);
            go.transform.position = startPos;
            float time = 0;
            while (time < Mathf.PI * 0.5f)
            {
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