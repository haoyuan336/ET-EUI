using System;
using ET.EventType;
using UnityEngine;

namespace ET
{
    public class HideComboEvent: AEvent<HideCombo>
    {
        protected override async ETTask Run(HideCombo a)
        {
            Scene scene = a.Scene;
            var uiComponent = scene.GetComponent<UIComponent>();
            var baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_GameUI);
            baseWindow.GetComponent<DlgGameUI>().HideCombo();
            await ETTask.CompletedTask;
        }
    }

    public class ShowComboEvent: AEvent<EventType.ShowComobAnim>
    {
        protected override async ETTask Run(ShowComobAnim a)
        {
            Scene scene = a.Scene;
            int count = a.ComboCount;

            var uiComponent = scene.GetComponent<UIComponent>();
            //
            var baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_GameUI);
            baseWindow.GetComponent<DlgGameUI>().ShowComboAnim(count);

            await ETTask.CompletedTask;
        }
    }

    public static class DlgGameUISystem
    {
        public static async void RegisterUIEvent(this DlgGameUI self)
        {
            self.View.E_BackButton.AddListenerAsync(self.ExitGameButtonClick);

            GameObject prefab = await AddressableComponent.Instance.LoadAssetByPathAsync<GameObject>("ItemGameCombo");

            GameObject go = GameObject.Instantiate(prefab, self.View.uiTransform);
            //
            var itemGameCombo = self.AddChild<Scroll_ItemGameCombo>();
            itemGameCombo.uiTransform = go.transform;
            itemGameCombo.uiTransform.gameObject.SetActive(false);
            self.ItemGameCombo = itemGameCombo;
        }

        public static async ETTask ExitGameButtonClick(this DlgGameUI self)
        {
            self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_ExitGameAlert).Coroutine();

            await ETTask.CompletedTask;
        }

        public static void ShowWindow(this DlgGameUI self, Entity contextData = null)
        {
            // while (self.IsShow)
            // {
            //     await TimerComponent.Instance.WaitFrameAsync();
            //
            //     if (self.CountDownTime > 0)
            //     {
            //         self.CountDownTime -= Time.deltaTime;
            //         if (self.CountDownTime <= 0)
            //         {
            //             self.ItemGameCombo.uiTransform.gameObject.SetActive(false);
            //         }
            //     }
            // }
        }

        public static void HideWindow(this DlgGameUI self)
        {
            // self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.);
            self.IsShow = false;
        }

        public static async void ShowComboAnim(this DlgGameUI self, int count)
        {
            if (count == 0)
            {
                return;
            }

            self.ItemGameCombo.uiTransform.gameObject.SetActive(true);
            self.ItemGameCombo.E_ComboText.text = $"COMBOX{count}";
            
            ComboConfig config = ComboConfigCategory.Instance.Get(count);
            AudioSource audioSource = self.View.uiTransform.GetComponent<AudioSource>();
            var audioClip = await AddressableComponent.Instance.LoadAssetByPathAsync<AudioClip>(config.AudioClip);
            audioSource.clip = audioClip;
            audioSource.Play();
            
            var time = 0.0f;
            while (time < Math.PI)
            {
                time += Time.deltaTime * 10;

                var scale = Mathf.Sin(time);

                self.ItemGameCombo.E_ComboText.GetComponent<RectTransform>().localScale = new Vector2(scale + 1, scale + 1);
                await TimerComponent.Instance.WaitFrameAsync();
            }

        
        }

        public static async void HideCombo(this DlgGameUI self)
        {
            var time = 0.0f;
            while (time < Math.PI * 0.5f)
            {
                time += Time.deltaTime * 10;

                var scale = Mathf.Sin(time);

                self.ItemGameCombo.E_ComboText.GetComponent<RectTransform>().localScale = new Vector2(1 - scale, 1 - scale);
                await TimerComponent.Instance.WaitFrameAsync();
            }
            self.ItemGameCombo.uiTransform.gameObject.SetActive(false);
            self.ItemGameCombo.E_ComboText.GetComponent<RectTransform>().localScale = new Vector2(1, 1);

        }
    }
}