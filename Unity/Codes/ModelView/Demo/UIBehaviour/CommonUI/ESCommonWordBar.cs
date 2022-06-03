using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public partial class ESCommonWordBar: Entity, ET.IAwake<UnityEngine.Transform>, IDestroy
    {
        public UnityEngine.UI.Image E_BgImage
        {
            get
            {
                if (this.uiTransform == null)
                {
                    Log.Error("uiTransform is null.");
                    return null;
                }

                if (this.m_E_BgImage == null)
                {
                    this.m_E_BgImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_Bg");
                }

                return this.m_E_BgImage;
            }
        }

        public UnityEngine.UI.Image E_WordBarTypeIconImage
        {
            get
            {
                if (this.uiTransform == null)
                {
                    Log.Error("uiTransform is null.");
                    return null;
                }

                if (this.m_E_WordBarTypeIconImage == null)
                {
                    this.m_E_WordBarTypeIconImage =
                            UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_WordBarTypeIcon");
                }

                return this.m_E_WordBarTypeIconImage;
            }
        }

        public UnityEngine.UI.Text E_WordBarTypeTextText
        {
            get
            {
                if (this.uiTransform == null)
                {
                    Log.Error("uiTransform is null.");
                    return null;
                }

                if (this.m_E_WordBarTypeTextText == null)
                {
                    this.m_E_WordBarTypeTextText =
                            UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "bg/E_WordBarTypeText");
                }

                return this.m_E_WordBarTypeTextText;
            }
        }

        public UnityEngine.UI.Text E_WordBarValueTextText
        {
            get
            {
                if (this.uiTransform == null)
                {
                    Log.Error("uiTransform is null.");
                    return null;
                }

                if (this.m_E_WordBarValueTextText == null)
                {
                    this.m_E_WordBarValueTextText =
                            UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "bg/E_WordBarValueText");
                }

                return this.m_E_WordBarValueTextText;
            }
        }

        public UnityEngine.UI.Image E_WordBarQualityIconImage
        {
            get
            {
                if (this.uiTransform == null)
                {
                    Log.Error("uiTransform is null.");
                    return null;
                }

                if (this.m_E_WordBarQualityIconImage == null)
                {
                    this.m_E_WordBarQualityIconImage =
                            UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject, "E_WordBarQualityIcon");
                }

                return this.m_E_WordBarQualityIconImage;
            }
        }

        public UnityEngine.UI.Text E_LockTextText
        {
            get
            {
                if (this.uiTransform == null)
                {
                    Log.Error("uiTransform is null.");
                    return null;
                }

                if (this.m_E_LockTextText == null)
                {
                    this.m_E_LockTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject, "E_LockText");
                }

                return this.m_E_LockTextText;
            }
        }

        public void DestroyWidget()
        {
            this.m_E_BgImage = null;
            this.m_E_WordBarTypeIconImage = null;
            this.m_E_WordBarTypeTextText = null;
            this.m_E_WordBarValueTextText = null;
            this.m_E_WordBarQualityIconImage = null;
            this.m_E_LockTextText = null;
            this.uiTransform = null;
        }

        private UnityEngine.UI.Image m_E_BgImage = null;
        private UnityEngine.UI.Image m_E_WordBarTypeIconImage = null;
        private UnityEngine.UI.Text m_E_WordBarTypeTextText = null;
        private UnityEngine.UI.Text m_E_WordBarValueTextText = null;
        private UnityEngine.UI.Image m_E_WordBarQualityIconImage = null;
        private UnityEngine.UI.Text m_E_LockTextText = null;
        public Transform uiTransform = null;
    }
}