using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ET
{
    // 1 mono模式 2 ILRuntime模式 3 mono热重载模式
    public enum CodeMode
    {
        Mono = 1,
        ILRuntime = 2,
        Reload = 3,
    }

    public class Init: MonoBehaviour
    {
        public CodeMode CodeMode = CodeMode.Mono;
        public LoadProgressBar LoadProgressBar;

        private void Awake()
        {
            // #if ENABLE_IL2CPP
            // 			this.CodeMode = CodeMode.ILRuntime;
            // #endif
            // 			
            // 			System.AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            // 			{
            // 				Log.Error(e.ExceptionObject.ToString());
            // 			};
            // 			
            // 			SynchronizationContext.SetSynchronizationContext(ThreadSynchronizationContext.Instance);
            // 			
            // 			DontDestroyOnLoad(gameObject);
            //
            // 			ETTask.ExceptionHandler += Log.Error;
            //
            // 			Log.ILog = new UnityLogger();
            //
            // 			Options.Instance = new Options();
            //
            // 			CodeLoader.Instance.CodeMode = this.CodeMode;
        }

        IEnumerator Start()
        {
            this.LoadProgressBar.gameObject.SetActive(true);

            var handler = Addressables.DownloadDependenciesAsync("All");
            while (!handler.IsDone)
            {
                var percent = handler.GetDownloadStatus().Percent;
                if (percent < 1)
                {
                    LoadProgressBar.Progress = percent;
                }

                Debug.LogWarning($"percent {percent}");
                yield return null;
            }

            if (handler.IsDone)
            {
                Debug.Log("更新资源完成");
                Destroy(this.LoadProgressBar.gameObject);
                this.InitCode();
            }
        }

        void InitCode()
        {
#if ENABLE_IL2CPP
            			this.CodeMode = CodeMode.ILRuntime;
#endif

            System.AppDomain.CurrentDomain.UnhandledException += (sender, e) => { Log.Error(e.ExceptionObject.ToString()); };

            SynchronizationContext.SetSynchronizationContext(ThreadSynchronizationContext.Instance);

            DontDestroyOnLoad(gameObject);

            ETTask.ExceptionHandler += Log.Error;

            Log.ILog = new UnityLogger();

            Options.Instance = new Options();

            CodeLoader.Instance.CodeMode = this.CodeMode;
            CodeLoader.Instance.Start();
        }

        // private void Start()
        // {
        // 	CodeLoader.Instance.Start();
        // }
        //
        private void Update()
        {
            if (CodeLoader.Instance.Update != null)
            {
                CodeLoader.Instance.Update();
            }
        }

        //
        // //
        private void LateUpdate()
        {
            if (CodeLoader.Instance.LateUpdate != null)
            {
                CodeLoader.Instance.LateUpdate();
            }
        }

        //
        private void OnApplicationQuit()
        {
            if (CodeLoader.Instance.OnApplicationQuit != null)
            {
                CodeLoader.Instance.OnApplicationQuit();
            }

            CodeLoader.Instance.Dispose();
        }
    }
}