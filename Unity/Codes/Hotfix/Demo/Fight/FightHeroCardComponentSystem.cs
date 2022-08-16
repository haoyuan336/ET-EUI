using System.Collections.Generic;

namespace ET
{
#if SERVER
    public class FightHeroCardComponentAwakeSystem: AwakeSystem<FightHeroCardCmponent>
    {
        public override void Awake(FightHeroCardCmponent self)
        {
            self.Awake();
        }
    }

    public static class FightHeroCardComponentSystem
    {
        public static async void Awake(this FightHeroCardCmponent self)
        {
            //取出玩家当前队伍的英雄

            Unit unit = self.GetParent<Unit>();
            if (unit.IsAI)
            {
            }
            else
            {
                TroopComponent troopComponent = self.Parent.GetComponent<TroopComponent>();
                List<HeroCard> heroCards = await troopComponent.GetCurrentTroopHeroCardsAsync();
                foreach (var heroCard in heroCards)
                {
                    self.AddChild(heroCard);
                    MessageHelper.SendToClient(self.GetParent<Unit>(), new M2C_CreateHeroModeMessage() { HeroCardInfo = heroCard.GetMessageInfo() });
                }
            }
        }
    }
#endif
}