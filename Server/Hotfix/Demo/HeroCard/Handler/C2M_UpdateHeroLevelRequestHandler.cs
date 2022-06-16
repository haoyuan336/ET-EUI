using System;
using System.Collections.Generic;

namespace ET
{
    public class C2M_UpdateHeroLevelRequestHandler: AMActorLocationRpcHandler<Unit, C2M_UpdateHeroLevelRequest, M2C_UpdateHeroLevelResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_UpdateHeroLevelRequest request, M2C_UpdateHeroLevelResponse response, Action reply)
        {
            long account = request.Account;
            long heroId = request.HeroId;

            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.UpdateHeroLevel, account.GetHashCode()))
            {
                List<HeroCard> heroCards = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                        .Query<HeroCard>(a => a.Id.Equals(heroId) && a.OwnerId.Equals(account) && a.State == (int) StateType.Active);
                if (heroCards.Count == 0)
                {
                    response.Error = ErrorCode.ERR_NotFoundHero;
                    reply();
                    return;
                }

                HeroCard heroCard = heroCards[0];
                //获取当前玩家拥有的经验值

                List<Item> items = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                        .Query<Item>(a => a.OwnerId.Equals(account) && a.State == (int) StateType.Active);
                var item = items.Find(a => a.ConfigId.Equals(1008));
                if (item == null)
                {
                    response.Error = ErrorCode.ERR_EXP_Not_Enough;
                    reply();
                    return;
                }

                //首先获取升级所需要的经验值
                var needExp = HeroHelper.GetNextLevelExp(heroCard.GetMessageInfo());
                if (needExp > item.Count)
                {
                    response.Error = ErrorCode.ERR_EXP_Not_Enough;
                    reply();
                    return;
                }

                heroCard.Level++;
                item.Count -= needExp;
                List<ETTask> tasks = new List<ETTask>();
                tasks.Add(DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(heroCard));
                tasks.Add(DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(item));
                await ETTaskHelper.WaitAll(tasks);
                response.HeroCardInfo = heroCard.GetMessageInfo();
                heroCard.Dispose();
                item.Dispose();
                response.Error = ErrorCode.ERR_Success;
                reply();
            }
        }
    }
}