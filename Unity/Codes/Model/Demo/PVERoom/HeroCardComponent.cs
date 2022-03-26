using System.Collections.Generic;

namespace ET
{
    public class HeroCardComponent: Entity, IAwake
    {
        public List<HeroCard> HeroCards = new List<HeroCard>();
    }
}