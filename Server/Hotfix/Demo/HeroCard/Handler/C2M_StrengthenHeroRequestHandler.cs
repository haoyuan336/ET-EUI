﻿// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
//
// namespace ET
// {
//     public class C2M_StrengthenHeroRequestHandler: AMActorLocationRpcHandler<Unit, C2M_StrenthenHeroRequest, M2C_StrenthenHeroResponse>
//     {
//         protected override async ETTask Run(Unit unit, C2M_StrenthenHeroRequest request, M2C_StrenthenHeroResponse response, Action reply)
//         {
//             long accountId = request.AccountId;
//             HeroCardInfo targetHeroCardInfo = request.TargetHeroCardInfo;
//             List<HeroCardInfo> heroCardInfos = request.ChooseHeroCardInfos;
//             bool isCon = heroCardInfos.Exists(a => a.HeroId.Equals(targetHeroCardInfo.HeroId));
//             if (isCon)
//             {
//                 //材料里面包含英雄，错误
//                 response.Error = ErrorCode.ERR_NotFoundHero;
//                 reply();
//                 return;
//             }
//
//             List<HeroCard> allHeroCard = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
//                     .Query<HeroCard>(a => a.OwnerId.Equals(accountId) && a.State == (int) StateType.Active);
//             HeroCard targetHeroCard = allHeroCard.Find(a => a.Id.Equals(targetHeroCardInfo.HeroId));
//             if (targetHeroCard == null)
//             {
//                 response.Error = ErrorCode.ERR_NotFoundHero;
//                 reply();
//                 return;
//             }
//
//             // Dictionary<long, HeroCard> materialHeroCards =  allHeroCard.ToDictionary(hero);
//             List<HeroCard> materialHeroCards = new List<HeroCard>();
//             Dictionary<long, HeroCard> allHeroCardMap = allHeroCard.ToDictionary(a => a.Id, a => a);
//
//             foreach (var heroCardInfo in heroCardInfos)
//             {
//                 HeroCard card = allHeroCardMap[heroCardInfo.HeroId];
//
//                 if (heroCardInfo.Count > card.Count)
//                 {
//                     response.Error = ErrorCode.ERR_MaterialNotEnough;
//                     reply();
//                     return;
//                 }
//                 materialHeroCards.Add(card);
//             }
//            
//
//             // var needExp = HeroHelper.GetNextLevelExp(targetHeroCard.GetMessageInfo());
//             var sumExp = targetHeroCard.CurrentExp;
//
//             foreach (var materialHeroCard in materialHeroCards)
//             {
//                 sumExp += HeroHelper.GetHeroAllLevelExp(materialHeroCard.GetMessageInfo());
//             }
//
//             // if (sumExp < needExp)
//             // {
//             //     response.Error = ErrorCode.ERR_EXP_Not_Enough;
//             //     reply();
//             //     return;
//             // }
//
//             var endLevel = HeroHelper.GetHeroLevelInfoWithExp(targetHeroCard.GetMessageInfo(), sumExp);
//             var lastExp = HeroHelper.GetHeroLevelLastExp(targetHeroCard.GetMessageInfo(), sumExp);
//             targetHeroCard.Level = endLevel;
//             targetHeroCard.CurrentExp = lastExp;
//             await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(targetHeroCard);
//             //将材料储存一下
//             List<ETTask> tasks2 = new List<ETTask>();
//             foreach (var card in materialHeroCards)
//             {
//                 {
//                     var config = HeroConfigCategory.Instance.Get(card.ConfigId);
//                     if (config.MaterialType == (int) HeroBagType.Materail)
//                     {
//                         HeroCardInfo info = heroCardInfos.Find(a => a.HeroId.Equals(card.Id));
//                         if (info != null)
//                         {
//                             card.Count -= info.Count;
//                             tasks2.Add(DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(card));
//                         }
//                     }
//                     else
//                     {
//                         card.State = (int) HeroCardState.Destroy;
//                         tasks2.Add(DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(card));
//                     }
//                 }
//             }
//
//             await ETTaskHelper.WaitAll(tasks2);
//             response.Error = ErrorCode.ERR_Success;
//             response.HeroCardInfo = targetHeroCard.GetMessageInfo();
//             reply();
//
//             await ETTask.CompletedTask;
//         }
//     }
// }