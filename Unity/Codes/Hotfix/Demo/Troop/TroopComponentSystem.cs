using System.Collections.Generic;

namespace ET
{
    public class TroopComponentAwakeSystem: AwakeSystem<TroopComponent>
    {
        public override void Awake(TroopComponent self)
        {
        }
    }

    public class TroopComponentBeforeDestroy: BeforeDestroySystem<TroopComponent>
    {
        public override void BeforeDestroy(TroopComponent self)
        {
#if SERVER
            self.SaveData();
#endif
        }
    }

    public static class TroopComponentSystem
    {
#if SERVER

        public static Troop GetTroopIdWithIndex(this TroopComponent self, int index)
        {
            List<Troop> troops = self.GetChilds<Troop>();
            return troops.Find(a => a.Index.Equals(index));
        }

        public static async ETTask<List<HeroCardInfo>> GetHeroCardInfosByIndexAsync(this TroopComponent self)
        {
            var index = await self.GetCurrentTroopIndexAsync();
            //找到herocardcomponent 

            var troop = self.GetTroopIdWithIndex(index);

            HeroCardComponent heroCardComponent = self.Parent.GetComponent<HeroCardComponent>();

            List<HeroCard> heroCards = await heroCardComponent.GetHeroCardsWithTroopIdAsync(troop.Id);
            List<HeroCardInfo> heroCardInfos = new List<HeroCardInfo>();
            foreach (var heroCard in heroCards)
            {
                heroCardInfos.Add(heroCard.GetMessageInfo());
            }

            return heroCardInfos;
        }

        public static async void PlayerChooseTroopIndex(this TroopComponent self, int index)
        {
            List<Troop> troops = await self.GetAllTroopAyncs();
            foreach (var tr in troops)
            {
                tr.IsOn = false;
            }

            Troop troop = troops.Find(a => a.Index.Equals(index));
            if (troop == null)
            {
                // self.AddChildWithId<Troop>(IdGenerater.Instance.GenerateId());
                long accountId = self.GetParent<Unit>().AccountId;
                troop = new Troop() { Id = IdGenerater.Instance.GenerateId(), Index = index, IsOn = true, OnwerId = accountId };
                self.AddChild(troop);
            }

            troop.IsOn = true;
        }

        public static async ETTask<int> GetCurrentTroopIndexAsync(this TroopComponent self)
        {
            List<Troop> troops = await self.GetAllTroopAyncs();
            foreach (var troop in troops)
            {
                if (troop.IsOn)
                {
                    return troop.Index;
                }
            }

            return 1;
        }

        public static async ETTask<List<Troop>> GetAllTroopAyncs(this TroopComponent self)
        {
            List<Troop> troops = self.GetChilds<Troop>();
            if (troops == null)
            {
                long account = self.GetParent<Unit>().AccountId;
                troops = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone())
                        .Query<Troop>(a => a.OnwerId.Equals(account) && a.State == (int)StateType.Active);
                //取出数据之后，保存一下
                foreach (var troop in troops)
                {
                    self.AddChild(troop);
                }
            }

            List<Troop> newTroops = new List<Troop>();
            foreach (var troop in troops)
            {
                if (troop.State == (int)StateType.Active)
                {
                    newTroops.Add(troop);
                }
            }

            return newTroops;
        }

        public static void SaveData(this TroopComponent self)
        {
            List<Troop> troops = self.GetChilds<Troop>();
            if (troops != null)
            {
                foreach (var troop in troops)
                {
                    DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(troop).Coroutine();
                }
            }
        }
#endif
    }
}