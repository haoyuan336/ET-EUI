using System.Diagnostics.Eventing.Reader;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace ET
{
    public class SceneChangeComponentAwakeSystem: IAwake<SceneChangeComponent>
    {
    }

    public class SceneChangeComponentUpdateSystem: UpdateSystem<SceneChangeComponent>
    {
        public override void Update(SceneChangeComponent self)
        {
            if (!self.loadMapOperation.IsDone)
            {
                return;
            }

            if (self.tcs == null)
            {
                return;
            }

            ETTask tcs = self.tcs;
            self.tcs = null;
            tcs.SetResult();
        }
    }

    public class SceneChangeComponentDestroySystem: DestroySystem<SceneChangeComponent>
    {
        public override void Destroy(SceneChangeComponent self)
        {
            // self.loadMapOperation = null;
            if (self.loadMapOperation.IsValid() && self.loadMapOperation.IsDone)
            {
                // AddressableComponent.Instance.UnLoadSceneAsync(self.loadMapOperation);
            }

            self.tcs = null;
        }
    }

    public static class SceneChangeComponentSystem
    {
        public static async ETTask ChangeSceneAsync(this SceneChangeComponent self, AsyncOperationHandle<SceneInstance> instance)
        {
            self.tcs = ETTask.Create(true);
            self.loadMapOperation = instance;
            // 加载map
            // self.loadMapOperation = SceneManager.LoadSceneAsync(sceneName);
            //this.loadMapOperation.allowSceneActivation = false;
            await self.tcs;
        }

        public static int Process(this SceneChangeComponent self)
        {
            if (!self.loadMapOperation.IsValid())
            {
                return 0;
            }

            // return (int)(self.loadMapOperation.progress * 100);
            return (int) (self.loadMapOperation.GetDownloadStatus().Percent * 100);
        }
    }
}