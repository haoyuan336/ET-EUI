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
            self.View.E_ShowImage.GetComponent<RectTransform>().localScale = new Vector3(1, isShow? 1 : -1, 1);
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

                    float height = 220 + 500 * heightRate;
                    maskNode.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
                    time += Time.deltaTime;
                    self.View.E_ShowMenuToggle.GetComponent<RectTransform>().anchoredPosition = new Vector2(-113, -470 - heightRate * 500);
                    
                    self.View.E_BGImage.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
                    // -1031
                    await TimerComponent.Instance.WaitFrameAsync();
                }

                maskNode.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, isShow? 870 : 270);
                self.View.E_ShowMenuToggle.GetComponent<RectTransform>().anchoredPosition = new Vector2(-113, !isShow? -470 : -969);
            }
        }

        public static void ShowWindow(this DlgSettingUI self, Entity contextData = null)
        {
        }
    }
}