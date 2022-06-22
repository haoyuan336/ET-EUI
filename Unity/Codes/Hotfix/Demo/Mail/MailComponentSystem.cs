using System.Collections.Generic;
using System.Linq;
using ET.Handler;

namespace ET
{
    public class MailComponentAwakeSystem: AwakeSystem<MailComponent>
    {
        public override void Awake(MailComponent self)
        {
        }
    }

    public static class MailComponentSystem
    {
#if SERVER
        public static async ETTask NewPlayerEnter(this MailComponent self, C2M_RegisterNewMailBoxMessage message, Unit unit)
        {
            long accountId = message.AccountId;
            List<Account> accounts = await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone())
                    .Query<Account>(a => a.Id.Equals(accountId) && a.AccountType != (int) AccountType.BlackList && a.IsRegisterMailBox == false);

            if (accounts.Count != 0)
            {
                Account account = accounts[0];
                account.IsRegisterMailBox = true;
                await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(account);
                M2C_SendMails send = new M2C_SendMails();
                Mail mail = new Mail()
                {
                    Id = IdGenerater.Instance.GenerateId(),
                    ReceiveId = accountId,
                    SendName = "系统",
                    SendTime = TimeHelper.ServerNow(),
                    MailTitle = "欢迎来到cog",
                    MailContent = "欢迎来到cog，邮件内容",
                };
                await DBManagerComponent.Instance.GetZoneDB(unit.DomainZone()).Save(mail);

                //创建奖励内容
                //取出奖励配置表
                AwardConfig config = AwardConfigCategory.Instance.Get(1000001);

                //创建奖励内容
                string heroListStr = config.HeroList;
                string weaponListStr = config.WeaponList;
                string itemListStr = config.ItemList;
                await self.CreateHeroAward(heroListStr, mail.Id);
                await self.CreateWeaponAward(weaponListStr, mail.Id);
                await self.CreateItemAward(itemListStr, mail.Id);
                // await ETTaskHelper.WaitAll(tasks);
                List<MailInfo> mails = new List<MailInfo>();
                mails.Add(mail.GetInfo());
                send.MailInfos = mails;
                MessageHelper.SendToClient(unit, send);
            }
        }

        public static async ETTask CreateHeroAward(this MailComponent self, string heroListStr, long mailId)
        {
            List<string> heroList = heroListStr.Split(',').ToList();

            List<ETTask> tasks = new List<ETTask>();

            foreach (var heroConfigId in heroList)
            {
                // HeroCard heroCard = new HeroCard();
                // heroCard.Id = IdGenerater.Instance.GenerateId();
                // heroCard.ConfigId = int.Parse(heroConfigId);
                // heroCard.MailId = mailId;
                // tasks.Add(DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(heroCard));
                HeroCard heroCard = new HeroCard();
                heroCard.Id = IdGenerater.Instance.GenerateId();
                heroCard.ConfigId = int.Parse(heroConfigId);
                heroCard.MailId = mailId;
                // self.AddChild(heroCard);
                tasks.Add(heroCard.Call(self.DomainZone(), mailId));
            }

            await ETTaskHelper.WaitAll(tasks);
        }

        public static async ETTask CreateWeaponAward(this MailComponent self, string weaponListStr, long mailId)
        {
            List<ETTask> tasks = new List<ETTask>();
            List<string> weaponList = weaponListStr.Split(',').ToList();
            foreach (var weaponConfigId in weaponList)
            {
                Weapon weapon = new Weapon();
                weapon.ConfigId = int.Parse(weaponConfigId);
                weapon.Id = IdGenerater.Instance.GenerateId();
                weapon.MailId = mailId;

                tasks.Add(DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(weapon));
            }

            await ETTaskHelper.WaitAll(tasks);
        }

        public static async ETTask CreateItemAward(this MailComponent self, string itemListStr, long mailId)
        {
            List<ETTask> tasks = new List<ETTask>();
            List<string> itemList = itemListStr.Split(',').ToList();
            foreach (var itemConfigId in itemList)
            {
                Item item = new Item();
                item.ConfigId = int.Parse(itemConfigId);
                item.Id = IdGenerater.Instance.GenerateId();
                item.MailId = mailId;
                tasks.Add(DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(item));
            }

            await ETTaskHelper.WaitAll(tasks);
        }

        public static async ETTask ReceiveAllHeroAward(this MailComponent self, long account, long ownerId)
        {
            List<HeroCard> heroCards = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone())
                    .Query<HeroCard>(a => a.MailId.Equals(ownerId) && a.State == (int) StateType.Active);
            List<ETTask> tasks = new List<ETTask>();
            foreach (var heroCard in heroCards)
            {
                heroCard.OwnerId = account;
                tasks.Add(DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(heroCard));
            }

            await ETTaskHelper.WaitAll(tasks);
        }

        public static async ETTask ReceiveAllWeaponAward(this MailComponent self, long account, long ownerId)
        {
            List<Weapon> weapons = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone())
                    .Query<Weapon>(a => a.MailId.Equals(ownerId) && a.State == (int) StateType.Active);
            List<ETTask> tasks = new List<ETTask>();
            foreach (var weapon in weapons)
            {
                weapon.OwnerId = account;
                tasks.Add(DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(weapon));
            }

            await ETTaskHelper.WaitAll(tasks);
        }

        public static async ETTask ReceiveAllItemWeaponAward(this MailComponent self, long account, long ownerId)
        {
            List<Item> items = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone())
                    .Query<Item>(a => a.MailId.Equals(ownerId) && a.State == (int) StateType.Active);
            List<ETTask> tasks = new List<ETTask>();
            foreach (var item in items)
            {
                item.OwnerId = account;
                tasks.Add(DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(item));
            }

            await ETTaskHelper.WaitAll(tasks);
        }

        public static async ETTask PlayerReceiveAward(this MailComponent self, long account, long ownerId)
        {
            //
            List<ETTask> tasks = new List<ETTask>();
            tasks.Add(self.ReceiveAllHeroAward(account, ownerId));
            tasks.Add(self.ReceiveAllWeaponAward(account, ownerId));
            tasks.Add(self.ReceiveAllItemWeaponAward(account, ownerId));
            await ETTaskHelper.WaitAll(tasks);
        }
#endif
    }
}