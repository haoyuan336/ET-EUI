using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ET
{
    public class HeroModeObjectComponentDestroySystem: DestroySystem<HeroModeObjectCompoent>
    {
        public override void Destroy(HeroModeObjectCompoent self)
        {
            if (self.HeroMode != null)
            {
                GameObject.Destroy(self.HeroMode);
            }
        }
    }

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
            if (self.IsDisposed)
            {
                GameObject.Destroy(self.HeroMode);
                return;
            }

            // var distance = 1.5f;
            Vector3 pos = Vector3.zero;
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            if (heroCard.OwnerId.Equals(accountId))
            {
                pos = new Vector3(1.5f - heroCard.InTroopIndex * 1.5f, 0, 2.2f + 1);
            }
            else
            {
                pos = new Vector3(1.5f - heroCard.InTroopIndex * 1.5f, 0, -2.2f + 1);

                // var levelNum = self.ZoneScene().GetComponent<PlayerComponent>().CurrentLevelNum;
                // var levelConfig = LevelConfigCategory.Instance.Get(levelNum);
                // var heroIdStr = levelConfig.HeroId;
                // var count = heroIdStr.Split(',').Length;
                // var distance = 1.5f;
                // pos = new Vector3((count - 1) * distance * 0.5f - distance * heroCard.InTroopIndex, 0,
                //     -2.2f * (heroCard.CampIndex == 0? -1 : 1) + 1);
            }

            // Vector3 pos = new Vector3(-1.5f + heroCard.InTroopIndex * 1.5f, 0, -2.2f * (heroCard.CampIndex == 0? -1 : 1) + 1);
            self.HeroMode.transform.position = pos;
            self.HeroMode.transform.forward = heroCard.OwnerId.Equals(accountId)? Vector3.back : Vector3.forward;
            self.HeroModeInitPos = new Vector3(pos.x, pos.y, pos.z);

            // heroCard.AddComponent<HeroCardInfoObjectComponent>();
            await ETTask.CompletedTask;
        }
    }

    public class HeroModeObjectComponentUpdateSystem: UpdateSystem<HeroModeObjectCompoent>
    {
        public override void Update(HeroModeObjectCompoent self)
        {
            if (self.MoveActionItems.Count > 0)
            {
                var moveActionItem = self.MoveActionItems[0];
                moveActionItem.CurrentTime += Time.deltaTime * moveActionItem.Speed;

                var value = Mathf.Sin(moveActionItem.CurrentTime);
                var prePos = Vector3.Lerp(moveActionItem.CurrentPos, moveActionItem.EndPos, value);
                moveActionItem.GameObject.transform.position = prePos;
                if (moveActionItem.CurrentTime >= moveActionItem.Time)
                {
                    moveActionItem.Task.SetResult();
                    moveActionItem.GameObject.transform.position = moveActionItem.EndPos;
                    self.MoveActionItems.RemoveAt(0);
                }
            }

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
                    // Log.Debug($"hero card camp index{heroCard.CampIndex}");
                    long account = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
                    if (!heroCard.OwnerId.Equals(account))
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

        public static async ETTask PlayMoveToAnim(this HeroModeObjectCompoent self, Vector3 startPos, Vector3 targetPos)
        {
            await self.TurnTargetAnim(targetPos);
            // float time = 0;
            // float distance = Vector3.Distance(targetPos, self.HeroModeInitPos);
            self.HeroMode.GetComponent<Animator>().SetBool("Run", true);

            ETTask task = ETTask.Create();
            self.MoveActionItems.Add(new MoveActionItem()
            {
                CurrentPos = startPos,
                EndPos = targetPos,
                Time = Mathf.PI * 0.5f,
                Speed = 2,
                Task = task,
                GameObject = self.HeroMode
            });
            await task.GetAwaiter();
            self.HeroMode.GetComponent<Animator>().SetBool("Run", false);

            await ETTask.CompletedTask;
        }

        public static async ETTask TurnTargetAnim(this HeroModeObjectCompoent self, Vector3 targetPos)
        {
            var targetVector = targetPos - self.HeroMode.transform.position;
            Quaternion targetQuaternion = Quaternion.LookRotation(targetVector);
            float time = 0;
            while (time < 0.4f)
            {
                time += Time.deltaTime * 2;
                self.HeroMode.transform.rotation = Quaternion.Slerp(self.HeroMode.transform.rotation, targetQuaternion, time);
                await TimerComponent.Instance.WaitFrameAsync();
            }

            //转身到目标角度
            await ETTask.CompletedTask;
        }

        public static async ETTask PlayAttackAnimLogic(this HeroModeObjectCompoent self, EventType.PlayHeroCardAttackAnim message)
        {
            Log.Debug("play attack anim logic");
            // if (self.ChooseMark != null)
            // {
            //     GameObject.Destroy(self.ChooseMark);
            // }
            HeroCard beAttackHeroCards = message.BeAttackHeroCard;
            SkillInfo skill = message.AttackHeroCardDataComponentInfo.CurrentSkillInfo;
            Log.Debug($"Skill config id {skill.SkillConfigId}");
            SkillConfig skillConfig = SkillConfigCategory.Instance.Get(skill.SkillConfigId);
            await self.MoveToEnemyTarget(beAttackHeroCards, skillConfig);
            await self.PlayAttackAnim(message, skillConfig);
            await self.BackMoveToInitPos(skillConfig);
            // int campIndex = message.AttackHeroCard.CampIndex;

            bool isCamp = message.AttackHeroCard.OwnerId.Equals(self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId);
            await self.TurnTargetAnim((isCamp? Vector3.back : Vector3.forward) + self.HeroMode.transform.position);
            await ETTask.CompletedTask;
        }

        public static async ETTask BackMoveToInitPos(this HeroModeObjectCompoent self, SkillConfig skillConfig)
        {
            if (skillConfig.MoveType == (int)HeroMoveType.NoMove)
            {
                return;
            }

            await self.PlayMoveToAnim(self.HeroMode.transform.position, self.HeroModeInitPos);

            await ETTask.CompletedTask;
        }

        public static async ETTask MoveToEnemyTarget(this HeroModeObjectCompoent self, HeroCard beAttackHeroCard, SkillConfig skillConfig)
        {
            if (skillConfig.MoveType == (int)HeroMoveType.NoMove)
            {
                return;
            }

            HeroModeObjectCompoent beAttackHeroModeCom = beAttackHeroCard.GetComponent<HeroModeObjectCompoent>();
            var isCamp = self.GetParent<HeroCard>().OwnerId.Equals(self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId);

            Vector3 offsetPos = isCamp? Vector3.forward
                    : Vector3.back;

            var endPos = beAttackHeroModeCom.HeroMode.transform.position + offsetPos;
            switch (skillConfig.TargetPosType)
            {
                case (int)TargetPosMoveType.Face:
                    break;
                case (int)TargetPosMoveType.Middle:

                    endPos = (self.HeroMode.transform.position + beAttackHeroModeCom.HeroMode.transform.position) * 0.5f;
                    break;
            }

            await self.PlayMoveToAnim(self.HeroModeInitPos, endPos);
        }

        public static async ETTask PlayBeHitedEffect(this HeroModeObjectCompoent self, SkillConfig skillConfig)
        {
            if (skillConfig.BeAttackEffect == "")
            {
                return;
            }

            GameObject effect = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath(skillConfig.BeAttackEffect);
            effect.transform.SetParent(self.HeroMode.transform);
            Log.Debug($"be attack bone name {skillConfig.BeAttackBoneName}");
            if (skillConfig.BeAttackBoneName == "")
            {
                effect.transform.localPosition = Vector3.one * 0.1f;
            }
            else
            {
                // Transform bone = UIFindHelper.FindDeepChild(self.HeroMode, skillConfig.BeAttackBoneName);
                GameObject bone = GameObject.Find($"{self.HeroMode.name}/{skillConfig.BeAttackBoneName}");
                if (bone != null)
                {
                    Log.Debug("找到了 被攻击的骨骼位置");
                    effect.transform.localPosition = bone.transform.localPosition;
                }
                else
                {
                    effect.transform.localPosition = Vector3.one * 0.1f;
                    // effect.transform.position = Vector3.one * 0.1f;
                    Log.Debug("未找到被攻击骨骼");
                }
            }

            await TimerComponent.Instance.WaitAsync(5000);
            GameObject.Destroy(effect);
            // self.LoadPrefab(skillcon)
        }

        public static async ETTask PlayBeAttackAnim(this HeroModeObjectCompoent self, HeroCardDataComponentInfo componentInfo,
        SkillConfig skillConfig, CrashCommonInfo commonInfo)
        {
            int beAttackTime = skillConfig.BeAttackTime;
            await TimerComponent.Instance.WaitAsync(beAttackTime);
            self.HeroMode.GetComponent<Animator>().SetTrigger("BeAttack");
            // self.Parent.GetComponent<HeroCardObjectComponent>().UpdateHeroCardTextView(message.BeAttackHeroCardDataComponentInfo, message.CommonInfo);
            self.GetParent<HeroCard>().GetComponent<HeroCardInfoObjectComponent>().UpdateHPView(componentInfo);
            self.GetParent<HeroCard>().GetComponent<HeroCardInfoObjectComponent>().UpdateAngryView(componentInfo);
            self.GetParent<HeroCard>().GetComponent<HeroCardInfoObjectComponent>().ShowDamageViewAnim(componentInfo);
            self.PlayBeHitedEffect(skillConfig).Coroutine();
            if (componentInfo.HP <= 0)
            {
                self.HeroMode.GetComponent<Animator>().SetBool("Dead", true);
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask PlayAttackAnim(this HeroModeObjectCompoent self, EventType.PlayHeroCardAttackAnim message, SkillConfig skillConfig)
        {
            if (self.AttackMark != null)
            {
                self.AttackMark.SetActive(false);
            }

            HeroCard beAttackCard = message.BeAttackHeroCard;
            self.PlaySkillEffect(skillConfig);
            self.PlayFlyEffect(skillConfig, beAttackCard);
            beAttackCard.GetComponent<HeroModeObjectCompoent>()
                    .PlayBeAttackAnim(message.BeAttackHeroCardDataComponentInfo, skillConfig, message.CommonInfo).Coroutine();

            string skillAnimStr = skillConfig.SkillAnimName;
            // self.UpdateShowDataView(message.AttackHeroCardDataComponentInfo, message.CommonInfo);
            self.HeroMode.GetComponent<Animator>().SetTrigger(skillAnimStr);
            var skillTime = skillConfig.SkillTime;
            await TimerComponent.Instance.WaitAsync(skillTime);
            await self.Parent.GetComponent<HeroCardInfoObjectComponent>().InitAttackAdditionView(message.AttackHeroCardDataComponentInfo);
            self.Parent.GetComponent<HeroCardInfoObjectComponent>().UpdateAngryView(message.AttackHeroCardDataComponentInfo);
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
            Vector3 endPos = heroModeObject.HeroMode.transform.position + Vector3.up * 0.5f;
            effect.transform.position = startPos;
            effect.transform.forward = endPos - startPos;
            ETTask task = ETTask.Create();
            self.MoveActionItems.Add(new MoveActionItem()
            {
                Time = Mathf.PI * 0.5f,
                CurrentPos = startPos,
                EndPos = endPos,
                Speed = 2,
                GameObject = effect,
                Task = task
            });
            await task.GetAwaiter();

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

        public static async void ShowAttackMark(this HeroModeObjectCompoent self, bool isShow)
        {
            if (isShow)
            {
                if (self.AttackMark == null)
                {
                    self.AttackMark =
                            await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath(
                                "Assets/Bundles/Unit/HeroModePrefabs/AttackMark.prefab");
                    var height = self.HeroMode.GetComponentInChildren<SkinnedMeshRenderer>().bounds.size.y;
                    self.AttackMark.transform.position = self.HeroMode.transform.position + Vector3.up * height;
                }
                else
                {
                    self.AttackMark.SetActive(true);
                }
            }
            else
            {
                if (self.AttackMark != null)
                {
                    self.AttackMark.SetActive(false);
                }
            }

            await ETTask.CompletedTask;
        }
    }
}