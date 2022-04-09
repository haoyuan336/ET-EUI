using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgSettingUISystem
    {
        public static void RegisterUIEvent(this DlgSettingUI self)
        {
            self.View.E_ShowMenuToggle.GetComponent<Toggle>().onValueChanged.AddListener((value) => { self.ShowMenu(value).Coroutine(); });
        }

        public static async ETTask ShowMenu(this DlgSettingUI self, bool isShow)
        {
            GameObject maskNode = GameObject.Find("DlgSettingUI/MaskNode");
            Log.Debug($"mask Node {maskNode}");
            if (maskNode != null)
            {
                // Log.Debug("find obj");
                float time = 0;
                while (time < 0.2f)
                {
                    float heightRate = Mathf.Sin(Mathf.PI * 0.5f * (time / 0.2f));
                    if (!isShow)
                    {
                        heightRate = Mathf.Cos(Mathf.PI * 0.5f * (time / 0.2f));
                    }

                    float height = 270 + 600 * heightRate;
                    maskNode.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
                    time += Time.deltaTime;
                    self.View.E_ShowMenuToggle.GetComponent<RectTransform>().anchoredPosition = new Vector2(-113, -441 - heightRate * 600);
                    // -1031
                    await TimerComponent.Instance.WaitFrameAsync();
                }

                maskNode.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, isShow? 870 : 270);
                self.View.E_ShowMenuToggle.GetComponent<RectTransform>().anchoredPosition = new Vector2(-113, !isShow? -440 : -1040);
            }
        }

        public static void ShowWindow(this DlgSettingUI self, Entity contextData = null)
        {
        }
    }
}