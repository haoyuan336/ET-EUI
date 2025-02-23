﻿using System;
using System.Collections.Generic;
using UnityEngine;

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
            //加载英雄模型
            HeroConfig heroConfig = HeroConfigCategory.Instance.Get(heroCard.ConfigId);
            var heroModeStr = heroConfig.HeroMode;
            GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>(heroModeStr);
            self.HeroMode = GameObject.Instantiate(prefab);
            if (self.IsDisposed)
            {
                GameObject.Destroy(self.HeroMode);
                return;
            }

            Vector3 pos = Vector3.zero;
            long accountId = self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId;
            if (heroCard.OwnerId.Equals(accountId))
            {
                pos = new Vector3(1.5f - heroCard.InTroopIndex * 1.5f, 0, 2.2f + 1);
            }
            else
            {
                pos = new Vector3(1.5f - heroCard.InTroopIndex * 1.5f, 0, -2.2f + 1);
            }

            self.HeroMode.transform.position = pos;
            self.HeroMode.transform.forward = heroCard.OwnerId.Equals(accountId)? Vector3.back : Vector3.forward;
            self.HeroModeInitPos = new Vector3(pos.x, pos.y, pos.z);
            await ETTask.CompletedTask;
        }
    }

    public class HeroModeObjectComponentUpdateSystem: UpdateSystem<HeroModeObjectCompoent>
    {
        public override void Update(HeroModeObjectCompoent self)
        {
            if (self.MoveActionItems.Count > 0)
            {
                // self.MoveActionItems.RemoveAll(a => a.StateType == StateType.Destroy);
                foreach (var moveActionItem in self.MoveActionItems)
                {
                    if (moveActionItem.StateType != StateType.Active)
                    {
                        continue;
                    }

                    switch (moveActionItem.MoveActionType)
                    {
                        case MoveActionType.Normal:
                            moveActionItem.CurrentTime += Time.deltaTime * moveActionItem.Speed;
                            var value = Mathf.Sin(moveActionItem.CurrentTime);
                            var prePos = Vector3.Lerp(moveActionItem.CurrentPos, moveActionItem.EndPos, value);
                            moveActionItem.GameObject.transform.position = prePos;
                            if (moveActionItem.CurrentTime >= moveActionItem.Time)
                            {
                                moveActionItem.Task.SetResult();
                                moveActionItem.GameObject.transform.position = moveActionItem.EndPos;
                                moveActionItem.StateType = StateType.Destroy;
                            }

                            break;
                        case MoveActionType.CircleToPoint:
                            moveActionItem.CurrentTime += Time.deltaTime * moveActionItem.Speed;
                            moveActionItem.GameObject.transform.rotation = Quaternion.Slerp(moveActionItem.CurrentQuat, moveActionItem.EndQuat,
                                moveActionItem.CurrentTime);
                            if (moveActionItem.CurrentTime >= moveActionItem.Time)
                            {
                                moveActionItem.Task.SetResult();
                                moveActionItem.GameObject.transform.rotation = moveActionItem.EndQuat;
                                moveActionItem.StateType = StateType.Destroy;
                            }

                            break;
                    }
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
        public static async ETTask PlayRebornAnimAsync(this HeroModeObjectCompoent self)
        {
            // self.HeroMode.GetComponent<Animator>()
            self.HeroMode.GetComponent<Animator>().SetTrigger("Reborn");
            await TimerComponent.Instance.WaitAsync(200);
            // PlayRebornAnimAsync
        }

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

        public static async ETTask PlayFlashToAnim(this HeroModeObjectCompoent self, Vector3 startPos, Vector3 targetPos)
        {
            await self.TurnTargetAnim(targetPos);

            self.HeroMode.transform.position = targetPos;

            await TimerComponent.Instance.WaitAsync(200);
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
                GameObject = self.HeroMode,
                MoveActionType = MoveActionType.Normal
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

            self.HeroMode.transform.rotation = targetQuaternion;
            //转身到目标角度
            await ETTask.CompletedTask;
        }

        public static async ETTask PlayBuffBeAttackEffect(this HeroModeObjectCompoent self, BuffInfo buffInfo)
        {
            BuffConfig buffConfig = BuffConfigCategory.Instance.Get(buffInfo.ConfigId);
            if (string.IsNullOrEmpty(buffConfig.BeAttackEffect))
            {
                return;
            }

            GameObject obj = GameObjectPoolHelper.GetObjectFromPool(buffConfig.BeAttackEffect, true, 1);
            obj.transform.SetParent(self.HeroMode.transform);
            obj.transform.localPosition = self.HeroMode.GetComponent<CapsuleCollider>().center;
            await TimerComponent.Instance.WaitAsync(1000);
            GameObjectPoolHelper.ReturnObjectToPool(obj);
            await ETTask.CompletedTask;
        }

        public static async ETTask PlayBuffDamageAnimator(this HeroModeObjectCompoent self, BuffInfo buffInfo,
        HeroCardDataComponentInfo heroCardDataComponentInfo)
        {
            BuffConfig buffConfig = BuffConfigCategory.Instance.Get(buffInfo.ConfigId);
            self.PlayBuffBeAttackEffect(buffInfo).Coroutine();
            if (string.IsNullOrEmpty(buffConfig.BeAttackAnim))
            {
                return;
            }

            Log.Debug($"play anim {buffConfig.BeAttackAnim}");
            self.HeroMode.GetComponent<Animator>().SetTrigger(buffConfig.BeAttackAnim);
            HeroCardInfoObjectComponent heroCardInfoObjectComponent = self.GetParent<HeroCard>().GetComponent<HeroCardInfoObjectComponent>();
            heroCardInfoObjectComponent.ShowDamageViewAnim(heroCardDataComponentInfo);
            heroCardInfoObjectComponent.UpdateHPView(heroCardDataComponentInfo);
            // await ETTask.CompletedTask;
            await TimerComponent.Instance.WaitAsync(1000);
            if (heroCardDataComponentInfo.HP <= 0)
            {
                self.HeroMode.GetComponent<Animator>().SetBool("Dead", true);
                self.SetDeadState();
            }
        }

        public static async ETTask PlayBuffDamageAnim(this HeroModeObjectCompoent self, HeroCardDataComponentInfo heroCardDataComponentInfo,
        int damageBuff, BuffInfo buffInfo)
        {
            Log.Debug($"damage buff count {damageBuff}");
            if (damageBuff <= 0)
            {
                return;
            }

            heroCardDataComponentInfo.Damage = damageBuff;

            self.PlayBuffBeAttackEffect(buffInfo).Coroutine();
            self.PlayBuffDamageAnimator(buffInfo, heroCardDataComponentInfo).Coroutine();

            await TimerComponent.Instance.WaitAsync(1000);
        }

        public static async ETTask PlayAttackAnimLogic(this HeroModeObjectCompoent self, HeroCardComponent heroCardComponent,
        AttackAction attackAction)
        {
            SkillInfo skillInfo = attackAction.AttackHeroCardDataComponentInfo.CurrentSkillInfo;
            Log.Debug($"skill info {skillInfo.SkillConfigId}");
            SkillConfig skillConfig = SkillConfigCategory.Instance.Get(skillInfo.SkillConfigId);
            var attackHeroCardDataComponerntInfo = attackAction.AttackHeroCardDataComponentInfo;
            var beHeroCardDataComponentInfos = attackAction.BeAttackHeroCardDataComponentInfos;
            var heroBuffInfos = attackAction.HeroBuffInfos;
            switch (skillConfig.RangeType)
            {
                case (int)SkillRangeType.EnemySingle:
                    await self.PlaySingleAttackAnim(heroCardComponent, attackAction, skillConfig);
                    break;
                case (int)SkillRangeType.EnemyGroup:
                    await self.PlayAttackGroupAnim(heroCardComponent, attackHeroCardDataComponerntInfo, beHeroCardDataComponentInfos, skillConfig,
                        heroBuffInfos);
                    break;
                case (int)SkillRangeType.FriendSingle:
                    break;
                case (int)SkillRangeType.FriendGroup:
                    Log.Debug("所有队友技能");
                    await self.PlayFriendGroupCareAnim(heroCardComponent, attackHeroCardDataComponerntInfo, beHeroCardDataComponentInfos,
                        skillConfig, heroBuffInfos);
                    break;
                case (int)SkillRangeType.SingleSelf:
                    await self.PlayAttackGroupAnim(heroCardComponent, attackHeroCardDataComponerntInfo, beHeroCardDataComponentInfos, skillConfig,
                        heroBuffInfos);
                    break;
                case (int)SkillRangeType.HealthLeast:
                    await self.PlaySingleAttackAnim(heroCardComponent, attackAction, skillConfig);
                    break;
            }
        }

        public static async ETTask PlayFriendGroupCareAnim(this HeroModeObjectCompoent self, HeroCardComponent heroCardComponent,
        HeroCardDataComponentInfo attackHeroCardDataComponentInfo, List<HeroCardDataComponentInfo> beAttaclHeroCardDataComponentInfos,
        SkillConfig skillConfig, List<HeroBuffInfo> heroBufferInfos)
        {
            if (self.AttackMark != null)
            {
                self.AttackMark.SetActive(false);
            }

            string skillAnimStr = skillConfig.SkillAnimName;
            self.HeroMode.GetComponent<Animator>().SetTrigger(skillAnimStr);
            self.PlaySkillEffectWithConfig(skillConfig).Coroutine();
            for (var i = 0; i < beAttaclHeroCardDataComponentInfos.Count; i++)
            {
                var info = beAttaclHeroCardDataComponentInfos[i];
                var heroCard = heroCardComponent.GetChild<HeroCard>(info.HeroId);
                heroCard.GetComponent<HeroModeObjectCompoent>()
                        .PlayBeCareAnim(info, skillConfig, heroBufferInfos?[i].BuffInfos);
            }

            var skillTime = skillConfig.SkillTime;
            await TimerComponent.Instance.WaitAsync(skillTime);
            self.Parent.GetComponent<HeroCardInfoObjectComponent>().InitAttackAdditionView(attackHeroCardDataComponentInfo);
            self.Parent.GetComponent<HeroCardInfoObjectComponent>().UpdateAngryView(attackHeroCardDataComponentInfo);
            await ETTask.CompletedTask;
        }

        public static async ETTask PlayCareSelfAnim(this HeroModeObjectCompoent self, HeroCardComponent heroCardComponent,
        HeroCardDataComponentInfo attHeroCardDataComponentInfo, List<HeroCardDataComponentInfo> beCardDataComponentInfos)
        {
            await ETTask.CompletedTask;
        }
        // public static async ETTask P

        // public static async ETTask PlayGroupA

        public static async ETTask PlaySingleAttackAnim(this HeroModeObjectCompoent self, HeroCardComponent heroCardComponent,
        AttackAction attackAction,
        SkillConfig skillConfig)
        {
            // HeroCardComponent heroCardComponent = message.HeroCardComponent;
            HeroCardDataComponentInfo beHeroCardDataComponentInfo = attackAction.BeAttackHeroCardDataComponentInfos[0];
            HeroCard beAttackHeroCard = heroCardComponent.GetChild<HeroCard>(beHeroCardDataComponentInfo.HeroId);
            HeroCardDataComponentInfo attackHeroCardDataComponerntInfo = attackAction.AttackHeroCardDataComponentInfo;
            await self.MoveToEnemyTarget(beAttackHeroCard, skillConfig);

            List<HeroBuffInfo> heroBufferInfo = attackAction.HeroBuffInfos;
            await self.PlayAttackAnim(beAttackHeroCard, beHeroCardDataComponentInfo, attackHeroCardDataComponerntInfo, skillConfig,
                heroBufferInfo[0]?.BuffInfos);
            await self.BackMoveToInitPos(skillConfig);
            // int campIndex = message.AttackHeroCard.CampIndex;
            bool isCamp = self.GetParent<HeroCard>().OwnerId.Equals(self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId);
            await self.TurnTargetAnim((isCamp? Vector3.back : Vector3.forward) + self.HeroMode.transform.position);
            await ETTask.CompletedTask;
        }

        public static async ETTask BackMoveToInitPos(this HeroModeObjectCompoent self, SkillConfig skillConfig)
        {
            if (skillConfig.MoveType == (int)HeroMoveType.NoMove)
            {
                return;
            }

            switch (skillConfig.MoveType)
            {
                case (int)HeroMoveType.Move:
                    await self.PlayMoveToAnim(self.HeroMode.transform.position, self.HeroModeInitPos);
                    break;
                case (int)HeroMoveType.NoMove:
                    break;
                case (int)HeroMoveType.Flash:
                    await self.PlayFlashToAnim(self.HeroMode.transform.position, self.HeroModeInitPos);
                    break;
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask MoveToEnemyTarget(this HeroModeObjectCompoent self, HeroCard beAttackHeroCard, SkillConfig skillConfig)
        {
            HeroModeObjectCompoent beAttackHeroModeCom = beAttackHeroCard.GetComponent<HeroModeObjectCompoent>();
            var isCamp = self.GetParent<HeroCard>().OwnerId.Equals(self.ZoneScene().GetComponent<AccountInfoComponent>().AccountId);

            Vector3 offsetPos = isCamp? Vector3.forward
                    : Vector3.back;

            var endPos = beAttackHeroModeCom.HeroMode.transform.position + offsetPos;

            switch (skillConfig.MoveType)
            {
                case (int)HeroMoveType.NoMove:
                    break;
                case (int)HeroMoveType.Move:
                    await self.PlayMoveToAnim(self.HeroModeInitPos, endPos);

                    break;
                case (int)HeroMoveType.Flash:
                    await self.PlayFlashToAnim(self.HeroModeInitPos, endPos);
                    break;
            }

            switch (skillConfig.TargetPosType)
            {
                case (int)TargetPosMoveType.Face:
                    break;
                case (int)TargetPosMoveType.Middle:

                    endPos = (self.HeroMode.transform.position + beAttackHeroModeCom.HeroMode.transform.position) * 0.5f;
                    break;
            }
        }

        public static async ETTask PlayBeHitedEffect(this HeroModeObjectCompoent self, SkillConfig skillConfig)
        {
            if (skillConfig.BeAttackEffect == "")
            {
                return;
            }

            var playTime = skillConfig.BeAttackEffectStartTime;
            await TimerComponent.Instance.WaitAsync(playTime);
            GameObject effect = GameObjectPoolHelper.GetObjectFromPool(skillConfig.BeAttackEffect, true, 3);
            // GameObject effect = await AddressableComponent.Instance.LoadGameObjectAndInstantiateByPath(skillConfig.BeAttackEffect);
            effect.transform.SetParent(self.HeroMode.transform);
            Log.Debug($"be attack bone name {skillConfig.BeAttackBoneName}");
            if (skillConfig.BeAttackBoneName == "")
            {
                effect.transform.localPosition = Vector3.one * 0.1f;
                effect.transform.forward = self.HeroMode.transform.forward * -1;
            }
            else
            {
                // Transform bone = UIFindHelper.FindDeepChild(self.HeroMode, skillConfig.BeAttackBoneName);
                Transform bone = UIFindHelper.FindDeepChild(self.HeroMode, skillConfig.BeAttackBoneName);
                // GameObject bone = GameObject.Find($"{self.HeroMode.name}/{skillConfig.BeAttackBoneName}");
                if (bone != null)
                {
                    Log.Debug("找到了 被攻击的骨骼位置");
                    effect.transform.position = bone.position;
                }
                else
                {
                    effect.transform.localPosition = Vector3.one * 0.1f;
                    // effect.transform.position = Vector3.one * 0.1f;
                    Log.Debug("未找到被攻击骨骼");
                }
            }

            await TimerComponent.Instance.WaitAsync(skillConfig.BeAttackEffectTime);
            // GameObject.Destroy(effect);
            // self.LoadPrefab(skillcon)
            GameObjectPoolHelper.ReturnObjectToPool(effect);
        }

        public static async void ShowBuffEffect(this HeroModeObjectCompoent self, List<BuffInfo> buffInfos,
        HeroCardDataComponentInfo heroCardDataComponentInfo)
        {
            //展示buff 特效

            foreach (var effect in self.BuffEffectList)
            {
                GameObjectPoolHelper.ReturnObjectToPool(effect);
            }

            self.BuffEffectList.Clear();
            self.ClearBuffState();
            if (heroCardDataComponentInfo.HP <= 0)
            {
                return;
            }

            if (buffInfos == null)
            {
                return;
            }

            foreach (var buffInfo in buffInfos)
            {
                Log.Debug($"buff info round count {buffInfo.RoundCount}");
                if (buffInfo.RoundCount == 0)
                {
                    continue;
                }

                BuffConfig config = BuffConfigCategory.Instance.Get(buffInfo.ConfigId);
                var effectPath = config.EffectPath;
                if (!string.IsNullOrEmpty(effectPath))
                {
                    GameObject gameObject = GameObjectPoolHelper.GetObjectFromPool(effectPath, true, 1);
                    gameObject.transform.SetParent(self.HeroMode.transform);
                    gameObject.transform.localPosition = self.HeroMode.GetComponent<CapsuleCollider>().center;
                    self.BuffEffectList.Add(gameObject);
                }

                self.PlayBuffState(buffInfo);
            }

            await ETTask.CompletedTask;
        }

        public static void ClearBuffState(this HeroModeObjectCompoent self)
        {
        }

        public static void PlayBuffState(this HeroModeObjectCompoent self, BuffInfo buffInfo)
        {
            BuffConfig config = BuffConfigCategory.Instance.Get(buffInfo.ConfigId);

            switch (config.AnimationState)
            {
                case "Frozen":
                    break;
            }
        }

        public static async void PlayBeCareAnim(this HeroModeObjectCompoent self, HeroCardDataComponentInfo componentInfo, SkillConfig skillConfig,
        List<BuffInfo> buffInfos = null)
        {
            await TimerComponent.Instance.WaitAsync(skillConfig.BeAttackAnimPlayTime);
            Log.Debug("播放治愈效果");
            // self.Parent.GetComponent<HeroCardObjectComponent>().UpdateHeroCardTextView(message.BeAttackHeroCardDataComponentInfo, message.CommonInfo);
            self.GetParent<HeroCard>().GetComponent<HeroCardInfoObjectComponent>().UpdateHPView(componentInfo);
            self.GetParent<HeroCard>().GetComponent<HeroCardInfoObjectComponent>().ShowCareHPViewAnim(componentInfo);
            self.GetParent<HeroCard>().GetComponent<HeroCardInfoObjectComponent>().ShowBuffViewInfo(buffInfos, componentInfo);
            self.ShowBuffEffect(buffInfos, componentInfo);
        }

        public static async ETTask PlayBeAttackAnim(this HeroModeObjectCompoent self)
        {
            self.HeroMode.GetComponent<Animator>().SetTrigger("BeAttack");
            await TimerComponent.Instance.WaitAsync(200);
        }

        public static async ETTask PlayBeAttackAnim(this HeroModeObjectCompoent self, HeroCardDataComponentInfo componentInfo,
        SkillConfig skillConfig, List<BuffInfo> buffInfos = null)
        {
            int beAttackTime = skillConfig.BeAttackAnimPlayTime;
            await TimerComponent.Instance.WaitAsync(beAttackTime);
            if (skillConfig.RangeType == (int)SkillRangeType.EnemySingle || skillConfig.RangeType == (int)SkillRangeType.EnemyGroup)
            {
                self.HeroMode.GetComponent<Animator>().SetTrigger("BeAttack");
            }

            // self.Parent.GetComponent<HeroCardObjectComponent>().UpdateHeroCardTextView(message.BeAttackHeroCardDataComponentInfo, message.CommonInfo);
            self.GetParent<HeroCard>().GetComponent<HeroCardInfoObjectComponent>().UpdateHPView(componentInfo);
            self.GetParent<HeroCard>().GetComponent<HeroCardInfoObjectComponent>().UpdateAngryView(componentInfo);
            self.GetParent<HeroCard>().GetComponent<HeroCardInfoObjectComponent>().ShowDamageViewAnim(componentInfo);
            self.GetParent<HeroCard>().GetComponent<HeroCardInfoObjectComponent>().ShowBuffViewInfo(buffInfos, componentInfo);
            self.ShowBuffEffect(buffInfos, componentInfo);

            if (componentInfo.HP <= 0)
            {
                self.HeroMode.GetComponent<Animator>().SetBool("Dead", true);
                self.SetDeadState();
                await TimerComponent.Instance.WaitAsync(2000);
            }

            await ETTask.CompletedTask;
        }

        public static void SetDeadState(this HeroModeObjectCompoent self)
        {
            // await TimerComponent.Instance.WaitAsync(1000);
            // foreach (var buff in self.BuffEffectList)
            // {
            //     GameObjectPoolHelper.ReturnObjectToPool(buff);
            // }
            //
            // self.Parent.GetComponent<HeroCardInfoObjectComponent>().SetDeadState();

            // self.BuffEffectList.Clear();
        }

        public static async ETTask PlayDeadAnim(this HeroModeObjectCompoent self, HeroCardDataComponentInfo heroCardDataComponentInfo)
        {
            Log.Warning($"play Dead anim {heroCardDataComponentInfo.HP}");

            if (heroCardDataComponentInfo.HP <= 0)
            {
                // if (componentInfo.HP <= 0)
                // {
                self.HeroMode.GetComponent<Animator>().SetBool("Dead", true);
                self.SetDeadState();
                await TimerComponent.Instance.WaitAsync(2000);
                // }
            }

            await ETTask.CompletedTask;
        }

        public static async ETTask PlayAttackAnim(this HeroModeObjectCompoent self, HeroCard beAttackHeroCard,
        HeroCardDataComponentInfo beAttackHeroCardDataComponentInfo, HeroCardDataComponentInfo attackHeroCardDataComponentInfo,
        SkillConfig skillConfig, List<BuffInfo> buffInfos = null)
        {
            if (self.AttackMark != null)
            {
                self.AttackMark.SetActive(false);
            }

            self.PlaySkillEffectWithConfig(skillConfig).Coroutine();
            self.PlayFlyEffect(skillConfig, beAttackHeroCard);
            string skillAnimStr = skillConfig.SkillAnimName;
            self.HeroMode.GetComponent<Animator>().SetTrigger(skillAnimStr);

            beAttackHeroCard.GetComponent<HeroModeObjectCompoent>()
                    .PlayBeAttackAnim(beAttackHeroCardDataComponentInfo, skillConfig, buffInfos).Coroutine();
            beAttackHeroCard.GetComponent<HeroModeObjectCompoent>().PlayBeHitedEffect(skillConfig).Coroutine();

            // self.UpdateShowDataView(message.AttackHeroCardDataComponentInfo, message.CommonInfo);
            var skillTime = skillConfig.SkillTime;
            await TimerComponent.Instance.WaitAsync(skillTime);

            // if (beAttackHeroCardDataComponentInfo.HP <= 0)
            // {
            //     await beAttackHeroCard.GetComponent<HeroModeObjectCompoent>().PlayDeadAnim(beAttackHeroCardDataComponentInfo);
            // }

            self.Parent.GetComponent<HeroCardInfoObjectComponent>().InitAttackAdditionView(attackHeroCardDataComponentInfo);
            self.Parent.GetComponent<HeroCardInfoObjectComponent>().UpdateAngryView(attackHeroCardDataComponentInfo);
        }

        public static async ETTask PlayAttackGroupAnim(this HeroModeObjectCompoent self, HeroCardComponent heroCardComponent,
        HeroCardDataComponentInfo attackHeroCardDataComponentInfo, List<HeroCardDataComponentInfo> beAttaclHeroCardDataComponentInfos,
        SkillConfig skillConfig, List<HeroBuffInfo> heroBufferInfos)
        {
            if (self.AttackMark != null)
            {
                self.AttackMark.SetActive(false);
            }

            string skillAnimStr = skillConfig.SkillAnimName;
            self.HeroMode.GetComponent<Animator>().SetTrigger(skillAnimStr);
            self.PlaySkillEffectWithConfig(skillConfig).Coroutine();
            for (var i = 0; i < beAttaclHeroCardDataComponentInfos.Count; i++)
            {
                var info = beAttaclHeroCardDataComponentInfos[i];
                var heroCard = heroCardComponent.GetChild<HeroCard>(info.HeroId);
                heroCard.GetComponent<HeroModeObjectCompoent>()
                        .PlayBeAttackAnim(info, skillConfig, heroBufferInfos?[i].BuffInfos).Coroutine();
                // heroCard.GetComponent<HeroModeObjectCompoent>().PlayBeHitedEffect(skillConfig).Coroutine();
            }

            self.PlayGroupBeAttackEffect(skillConfig);
            // self.UpdateShowDataView(message.AttackHeroCardDataComponentInfo, message.CommonInfo);
            var skillTime = skillConfig.SkillTime;
            await TimerComponent.Instance.WaitAsync(skillTime);

            // foreach (var beAttaclHeroCardDataComponentInfo in beAttaclHeroCardDataComponentInfos)
            // {
            //     if (beAttaclHeroCardDataComponentInfo.HP <=0)
            //     {
            //         // self.PlayDeadAnim()
            //     }
            // }            

            self.Parent.GetComponent<HeroCardInfoObjectComponent>().InitAttackAdditionView(attackHeroCardDataComponentInfo);
            self.Parent.GetComponent<HeroCardInfoObjectComponent>().UpdateAngryView(attackHeroCardDataComponentInfo);
            await ETTask.CompletedTask;
        }

        public static async void PlayGroupBeAttackEffect(this HeroModeObjectCompoent self, SkillConfig skillConfig)
        {
            if (skillConfig.BeAttackEffect == "")
            {
                return;
            }

            var playTime = skillConfig.BeAttackEffectStartTime;
            await TimerComponent.Instance.WaitAsync(playTime);
            GameObject effect = GameObjectPoolHelper.GetObjectFromPool(skillConfig.BeAttackEffect, true, 3);
            effect.transform.forward = self.HeroMode.transform.forward;
            effect.transform.position = new Vector3(0, 0, self.HeroMode.transform.position.z) + effect.transform.forward * 3;
            Log.Debug($"be attack bone name {skillConfig.BeAttackBoneName}");
            await TimerComponent.Instance.WaitAsync(skillConfig.BeAttackEffectTime);
            GameObjectPoolHelper.ReturnObjectToPool(effect);
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
                Task = task,
                MoveActionType = MoveActionType.Normal
            });
            await task.GetAwaiter();

            //
            GameObject.Destroy(effect);
        }

        public static async ETTask PlaySkillEffect(this HeroModeObjectCompoent self, string skillEffect, string bone, int startTime, int showTime)
        {
            GameObject obj = GameObjectPoolHelper.GetObjectFromPool(skillEffect, false, 1);
            await TimerComponent.Instance.WaitAsync(startTime);
            Log.Debug($"play skill effect {startTime}");
            obj.SetActive(true);
            obj.transform.forward = self.HeroMode.transform.forward;
            obj.transform.position = self.HeroMode.transform.position;
            if (!string.IsNullOrEmpty(bone))
            {
                var parent = UIFindHelper.FindDeepChild(self.HeroMode, bone);
                if (parent != null)
                {
                    obj.transform.SetParent(parent);
                    obj.transform.localPosition = Vector3.zero;
                }
                else
                {
                    Log.Warning($"not fount bone  {bone}");
                }
            }

            Log.Debug($"show time {showTime}");
            await TimerComponent.Instance.WaitAsync(showTime);
            GameObjectPoolHelper.ReturnObjectToPool(obj);
        }

        public static async ETTask PlaySkillEffectWithConfig(this HeroModeObjectCompoent self, SkillConfig config)
        {
            if (!string.IsNullOrEmpty(config.SkillEffect))
            {
                self.PlaySkillEffect(config.SkillEffect, config.SkillEffectBoneName, config.EffectStartTime, config.SkillTime).Coroutine();
            }

            if (!string.IsNullOrEmpty(config.SkillEffect2))
            {
                self.PlaySkillEffect(config.SkillEffect2, config.SkillEffectBioneName2, config.EffectStartTime, config.SkillTime).Coroutine();
            }

            await ETTask.CompletedTask;
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
                Log.Debug($"is show {isShow}");
                Log.Debug($"attack mark {self.AttackMark}");
                if (self.AttackMark != null)
                {
                    self.AttackMark.SetActive(false);
                }
            }

            await ETTask.CompletedTask;
        }
    }
}