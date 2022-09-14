using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public class ESTroopHeroCards: Entity, ET.IAwake<UnityEngine.Transform>, IDestroy
    {
        public UnityEngine.UI.LoopVerticalScrollRect E_TroopCardContentLoopVerticalScrollRect
        {
            get
            {
                if (this.uiTransform == null)
                {
                    Log.Error("uiTransform is null.");
                    return null;
                }

                if (this.m_E_TroopCardContentLoopVerticalScrollRect == null)
                {
                    this.m_E_TroopCardContentLoopVerticalScrollRect =
                            UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject, "E_TroopCardContent");
                }

                return this.m_E_TroopCardContentLoopVerticalScrollRect;
            }
        }

        public void DestroyWidget()
        {
            this.m_E_TroopCardContentLoopVerticalScrollRect = null;
            this.uiTransform = null;
        }

        private UnityEngine.UI.LoopVerticalScrollRect m_E_TroopCardContentLoopVerticalScrollRect = null;
        public Transform uiTransform = null;
        public Dictionary<int, Scroll_ItemHeroCard> ItemTroopHeroCards = new Dictionary<int, Scroll_ItemHeroCard>();

        public List<HeroCardInfo> TroopHeroCardInfos = new List<HeroCardInfo>();

        public Action<HeroCardInfo, bool> ItemCardClickAction;
    }
}