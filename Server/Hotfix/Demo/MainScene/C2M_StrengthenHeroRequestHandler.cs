using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ET
{
    public class C2M_StrengthenHeroRequestHandler: AMActorLocationRpcHandler<Unit, C2M_StrenthenHeroRequest, M2C_StrenthenHeroResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_StrenthenHeroRequest request, M2C_StrenthenHeroResponse response, Action reply)
        {
            long accountId = request.AccountId;
            HeroCardInfo targetHeroCardInfo = request.TargetHeroCardInfo;
            List<HeroCardInfo> heroCardInfos = request.ChooseHeroCardInfos;
            List<HeroCard> allCards = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<HeroCard>(a => a.OwnerId.Equals(accountId) && a.State == (int) HeroCardState.Active);
            if (allCards.Count == 0)
            {
                response.Error = ErrorCode.ERR_NotFoundHero;
                Log.Warning("此玩家拥有此英雄");
            }
            else
            {
                List<HeroCard> heroCards = new List<HeroCard>();
                HeroCard targetHeroCard = allCards.Find(a => a.Id.Equals(targetHeroCardInfo.HeroId));
                if (targetHeroCard != null)
                {
                    foreach (var info in heroCardInfos)
                    {
                        HeroCard card = allCards.Find(a => a.Id.Equals(info.HeroId));
                        if (card != null)
                        {
                            heroCards.Add(card);
                        }
                        else
                        {
                            var config = HeroConfigCategory.Instance.Get(card.ConfigId);
                            if (config.MaterialType == (int) HeroBagType.Materail)
                            {
                                if (info.Count > card.Count)
                                {
                                    response.Error = ErrorCode.ERR_MaterialNotEnough; //材料不足
                                    break;
                                }
                            }
                            else
                            {
                            }

                            response.Error = ErrorCode.ERR_NotFoundHero;
                            break;
                        }
                    }

                    //找到了所有材料以及目标英雄，那么开始合成， 目标英雄等级数加1，材料状态都设置为销毁状态
                    targetHeroCard.Level++;

                    //然后保存数据

                    List<ETTask> tasks = new List<ETTask>();

                    tasks.Add(DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(targetHeroCard));
                    // tasks.Add(DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()));
                    foreach (var card in heroCards)
                    {
                        var config = HeroConfigCategory.Instance.Get(card.ConfigId);
                        if (config.MaterialType == (int) HeroBagType.Materail)
                        {
                            HeroCardInfo info = heroCardInfos.Find(a => a.HeroId.Equals(card.Id));
                            if (info != null)
                            {
                                card.Count -= info.Count;
                                tasks.Add(DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(card));
                            }
                        }
                        else
                        {
                            card.State = (int) HeroCardState.Destroy;
                            tasks.Add(DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(card));
                        }
                    }

                    await ETTaskHelper.WaitAll(tasks);
                    response.HeroCardInfo = targetHeroCard.GetMessageInfo();
                }
                else
                {
                    response.Error = ErrorCode.ERR_NotFoundHero;
                }
            }

            reply();

            await ETTask.CompletedTask;
        }
    }
}