using System;

namespace ET
{
    public class DlgBackButton: Entity, IAwake
    {
        public DlgBackButtonViewComponent View
        {
            get => this.Parent.GetComponent<DlgBackButtonViewComponent>();
        }

        public Action BackButtonClickAction;
    }
}