using System;

namespace ET
{
    public class DlgAddSubPlane: Entity, IAwake
    {
        public DlgAddSubPlaneViewComponent View
        {
            get => this.Parent.GetComponent<DlgAddSubPlaneViewComponent>();
        }

        public Action AddAction;
        public Action SubAction;
    }
}