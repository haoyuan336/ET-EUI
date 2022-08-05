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
            self.GameObject = GlobalComponent.Instance.AudioResourceRoot.gameObject;
        }
    }

    public static class AudioComponentSystem
    {
        public static AudioComponent Instance;

        public static async void PlayAudioEffect(this  AudioComponent self, string audioStr)
        {
            Log.Debug($"play audio effect {audioStr}");
            await ETTask.CompletedTask;
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