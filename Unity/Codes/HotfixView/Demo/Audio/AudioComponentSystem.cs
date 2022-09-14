using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ET
{
    // using UnityEngine;
    // namespace ET
    // {
    public class AudioComponent: Entity, IAwake
    {
        public static AudioComponent Instance;

        // public static AddressableComponent Instance { get; set; }
        public GameObject GameObject;
    }
    // }

    public class AudioComponentAwakeSystem: AwakeSystem<AudioComponent>
    {
        public override void Awake(AudioComponent self)
        {
            AudioComponent.Instance = self;
            self.GameObject = GlobalComponent.Instance.EffectAudioSourceRoot.gameObject;
        }
    }

    public static class AudioComponentSystem
    {
        public static AudioComponent Instance;

        public static async void PlayMusicAudio(this AudioComponent self, string musicStr)
        {
            var audioClip = await AddressableComponent.Instance.LoadAssetByPathAsync<AudioClip>(musicStr);
            AudioSource audioSource = GlobalComponent.Instance.MusicAudioSourceRoot.GetComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }

        public static void StopMusicAudio(this AudioComponent self)
        {
            AudioSource audioSource = GlobalComponent.Instance.MusicAudioSourceRoot.GetComponent<AudioSource>();
            audioSource.Stop();
        }

        public static async void PlayAudioEffect(this AudioComponent self, string audioStr)
        {
            Log.Debug($"play audio effect {audioStr}");
            var audioClip = await AddressableComponent.Instance.LoadAssetByPathAsync<AudioClip>(audioStr);
            List<AudioSource> audioSources = self.GameObject.GetComponents<AudioSource>().ToList();
            foreach (var audioSource in audioSources)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = audioClip;
                    audioSource.Play();
                    break;
                }
            }
        }
    }
}