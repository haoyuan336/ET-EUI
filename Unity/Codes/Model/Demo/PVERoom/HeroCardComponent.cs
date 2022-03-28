using System.Collections.Generic;

namespace ET
{
    public class HeroCardComponent: Entity, IAwake
    {
        public List<HeroCard> HeroCards = new List<HeroCard>();

        //当前局要发动的攻击的列表
        public List<HeroCard> CurrentTurnAttackList = new List<HeroCard>();
        
        
    }
}