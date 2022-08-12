using System;
using System.Collections.Generic;
using System.Linq;
using ET.EventType;
using UnityEngine;

namespace ET
{
    public class PlayGameAudioEffect: AEvent<EventType.PlayGameAudioEffect>
    {
        protected override async ETTask Run(EventType.PlayGameAudioEffect a)
        {
            if (string.IsNullOrEmpty(a.AudioStr))
            {
                return;
            }
            UIComponent uiComponent = a.ZoneScene.GetComponent<UIComponent>();
            UIBaseWindow baseWindow = uiComponent.GetUIBaseWindow(WindowID.WindowID_GameUI);
            baseWindow.GetComponent<DlgGameUI>().PlayEffectAudio(a.AudioStr);

            await ETTask.CompletedTask;
        }
    }

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
            baseWindow.GetComponent<DlgGameUI>().ShowComboAnim(a);

            await ETTask.CompletedTask;
        }
    }

    public static class DlgGameUISystem
    {
        public static async void PlayEffectAudio(this DlgGameUI self, string audioStr, bool isLoop = false)
        {
            //加载资源
            var audioClip = await AddressableComponent.Instance.LoadAssetByPathAsync<AudioClip>(audioStr);

            List<AudioSource> audioSources = self.View.uiTransform.GetComponents<AudioSource>().ToList();
            foreach (var audioSource in audioSources)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.loop = isLoop;
                    audioSource.playOnAwake = false;
                    audioSource.clip = audioClip;
                    audioSource.Play();
                    break;
                }
            }
        }

        public static async void RegisterUIEvent(this DlgGameUI self)
        {
            self.View.E_BackButton.AddListenerAsync(self.ExitGameButtonClick,ConstValue.BackButtonAudioStr);

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
            // self.PlayEffectAudio("Assets/Res/Audios/12_dungeon_pvp_bgm_loop.mp3", true);
        }

        public static void HideWindow(this DlgGameUI self)
        {
            // self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.);
            self.IsShow = false;
        }

        public static async void ShowComboAnim(this DlgGameUI self, ShowComobAnim a)
        {
            // if (a.ComboCount == 0)
            // {
            //     return;
            // }

            self.ItemGameCombo.uiTransform.gameObject.SetActive(true);
            self.ItemGameCombo.E_ComboText.text = $"COMBOX{a.ComboCount}";

            // ComboConfig config = ComboConfigCategory.Instance.Get(count);
            // AudioSource audioSource = self.View.uiTransform.GetComponent<AudioSource>();
            // var audioClip = await AddressableComponent.Instance.LoadAssetByPathAsync<AudioClip>(config.AudioClip);
            // audioSource.clip = audioClip;
            // audioSource.Play();
            self.PlayCrashDiamondAudio(a);
            var time = 0.0f;
            while (time < Math.PI)
            {
                time += Time.deltaTime * 10;
                var scale = Mathf.Sin(time);
                self.ItemGameCombo.E_ComboText.GetComponent<RectTransform>().localScale = new Vector2(scale + 1, scale + 1);
                await TimerComponent.Instance.WaitFrameAsync();
            }
        }

        public static async void PlayCrashDiamondAudio(this DlgGameUI self, ShowComobAnim a)
        {
            // List<AudioSource> audioSources = self.View.uiTransform.GetComponents<AudioSource>().ToList();
            //首先加载audioclip 
            var config = ComboConfigCategory.Instance.Get(a.ComboCount);
            Log.Debug($"config  name {config.AudioClip}");
            self.PlayEffectAudio(config.AudioClip);
            // var audioClip = await AddressableComponent.Instance.LoadAssetByPathAsync<AudioClip>(config.AudioClip);
            // foreach (var audioSource in audioSources)
            // {
            //     if (!audioSource.isPlaying)
            //     {
            //         // audioSource.clip
            //         audioSource.clip = audioClip;
            //         audioSource.Play();
            //         break;
            //     }
            // }

            await ETTask.CompletedTask;
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