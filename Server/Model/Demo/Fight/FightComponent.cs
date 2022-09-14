using System.Collections.Generic;

namespace ET
{
    public class FightComponent: Entity, IAwake
    {
        //战斗逻辑 组建
        public List<Unit> Units = new List<Unit>();
        public int CurrentTurnIndex = 0;
        public int HangCount = 0;
        public int LieCount = 0;
        // public DiamondComponent DiamondComponent = null;
        public LevelConfig LevelConfig;
        public HeroCard CurrentBeAttackHeroCard;
        // public HeroCard Current
        public List<DiamondActionItem> DiamondActionItems = new List<DiamondActionItem>();
    }
}