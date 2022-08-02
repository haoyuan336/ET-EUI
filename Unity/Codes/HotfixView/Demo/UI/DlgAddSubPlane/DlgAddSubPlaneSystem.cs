namespace ET
{
    public static class DlgAddSubPlaneSystem
    {
        public static void RegisterUIEvent(this DlgAddSubPlane self)
        {
            self.View.E_BgButton.AddListener(() =>
            {
                Log.Debug("隐藏 加减层");
                self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_AddSubPlane);
            });
            // self.View.E_AddButton.AddListener();
            self.View.E_AddButton.AddListener(() =>
            {
                if (self.AddAction != null)
                {
                    self.AddAction();
                }
            });
            self.View.E_SubButton.AddListener(() =>
            {
                if (self.SubAction != null)
                {
                    self.SubAction();
                }
            });
        }

        public static void ShowWindow(this DlgAddSubPlane self, Entity contextData = null)
        {
            self.AddAction = null;
            self.SubAction = null;
            // self.View.E_countText
            // T itemWeapon = (T)contextData;
            // self.View.E_ContentImage.transform.position =
            // itemWeapon.uiTransform.position;
        }

        public static void HideWindow(this DlgAddSubPlane self)
        {
            self.AddAction = null;
            self.SubAction = null;
            self.View.E_AddButton.interactable = true;
            self.View.E_SubButton.interactable = true;
        }

        public static void SetInfo(this DlgAddSubPlane self, WeaponInfo info)
        {
        }

        public static void SetFull(this DlgAddSubPlane self, bool full)
        {
            self.View.E_AddButton.interactable = !full;
        }

        public static void EnableAddButton(this DlgAddSubPlane self, bool able)
        {
            self.View.E_AddButton.interactable = able;
        }

        public static void EnabelSubButton(this DlgAddSubPlane self, bool able)
        {
            self.View.E_SubButton.interactable = able;
        }
    }
}