using System;
using System.Collections.Generic;

namespace ET.Handler
{
    public class C2M_GetAllAwardInfoRequestHandler: AMActorLocationRpcHandler<Unit, C2M_GetAllAwardInfoRequest, M2C_GetAllAwardInfoResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_GetAllAwardInfoRequest request, M2C_GetAllAwardInfoResponse response, Action reply)
        {
            long account = request.AccountId;
            long ownerId = request.OwnerId;
            //取出邮件来
            List<Mail> mails = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Query<Mail>(a =>
                    a.Id.Equals(ownerId) && a.ReceiveId.Equals(account) && a.State == (int) StateType.Active);
            if (mails.Count == 0)
            {
                response.Error = ErrorCode.ERR_Not_Found_Mail;
                reply();
                return;
            }

            Mail mail = mails[0];

            //找到此邮件包含的奖励内容
            List<HeroCard> heroCards = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<HeroCard>(a => a.MailId.Equals(mail.Id) && a.State == (int) StateType.Active);
            List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
            foreach (var heroCard in heroCards)
            {
                heroCardInfos.Add(heroCard.GetMessageInfo());
                heroCard.Dispose();
            }
            //找到武器
            List<Weapon> weapons = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Weapon>(a => a.MailId.Equals(mail.Id) && a.State == (int) StateType.Active);
            List<WeaponInfo> weaponInfos = new List<WeaponInfo>();
            foreach (var weapon in weapons)
            {
                weaponInfos.Add(weapon.GetInfo());
                weapon.Dispose();
            }
            List<Item> items = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Item>(a => a.MailId.Equals(mail.Id) && a.State == (int) StateType.Active);
            List<ItemInfo> itemInfos = new List<ItemInfo>();
            foreach (var item in items)
            {
                itemInfos.Add(item.GetInfo());
                item.Dispose();
            }
            response.HeroCardInfos = heroCardInfos;
            response.WeaponInfos = weaponInfos;
            response.ItemInfos = itemInfos;
            response.Error = ErrorCode.ERR_Success;
            reply();
        }
    }
}