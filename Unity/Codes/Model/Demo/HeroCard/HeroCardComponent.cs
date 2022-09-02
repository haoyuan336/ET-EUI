using System.Collections.Generic;

namespace ET
{
    public class HeroCardComponent: Entity, IAwake,ITransfer, IUpdate, IDestroy, IBeforeDestroy
    {
        // public List<HeroCard> HeroCards = new List<HeroCard>();
        // public List<HeroCard> CurrentTurnAttackList = new List<HeroCard>();
        public int CurrentTime = 0;

        public List<HeroCard> ChangeList = new List<HeroCard>();
    }
}