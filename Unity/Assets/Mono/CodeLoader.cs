using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using System.Linq;
using UnityEngine.AddressableAssets;

namespace ET
{
    public class CodeLoader: IDisposable
    {
        public static CodeLoader Instance = new CodeLoader();

        public Action Update;
        public Action LateUpdate;
        public Action OnApplicationQuit;

        private Assembly assembly;

        private ILRuntime.Runtime.Enviorment.AppDomain appDomain;

        private Type[] allTypes;

        public CodeMode CodeMode { get; set; }

        private CodeLoader()
        {
        }

        public void Dispose()
        {
            this.appDomain?.Dispose();
        }

        public void Start()
        {
            Debug.Log($"code mode {this.CodeMode}");
            switch (this.CodeMode)
            {
                case CodeMode.Mono:
                {
                    
                    // Dictionary<string, UnityEngine.Object> dictionary = AssetsBundleHelper.LoadBundle("code.unity3d");
                    // byte[] assBytes = ((TextAsset) dictionary["Code.dll"]).bytes;
                    // byte[] pdbBytes = ((TextAsset) dictionary["Code.pdb"]).bytes;

                    TextAsset codedll = Addressables.LoadAssetAsync<TextAsset>("Assets/Bundles/Code/Code.dll.bytes").WaitForCompletion();
                    TextAsset codepdb = Addressables.LoadAssetAsync<TextAsset>("Assets/Bundles/Code/Code.pdb.bytes").WaitForCompletion();
                    byte[] assBytes = codedll.bytes;
                    byte[] pdbBytes = codepdb.bytes;
                    assembly = Assembly.Load(assBytes, pdbBytes);
                    this.allTypes = assembly.GetTypes();
                    IStaticMethod start = new MonoStaticMethod(assembly, "ET.Entry", "Start");
                    start.Run();
                    break;
                }
                case CodeMode.ILRuntime:
                {
                    Debug.Log("code move il runtime");
                    // Dictionary<string, UnityEngine.Object> dictionary = AssetsBundleHelper.LoadBundle("code.unity3d");
                    // byte[] assBytes = ((TextAsset) dictionary["Code.dll"]).bytes;
                    // byte[] pdbBytes = ((TextAsset) dictionary["Code.pdb"]).bytes;

                    //byte[] assBytes = File.ReadAllBytes(Path.Combine("../Unity/", Define.BuildOutputDir, "Code.dll"));
                    //byte[] pdbBytes = File.ReadAllBytes(Path.Combine("../Unity/", Define.BuildOutputDir, "Code.pdb"));
                    Debug.Log("load code dll");

                    TextAsset codedll = Addressables.LoadAssetAsync<TextAsset>("Code.dll").WaitForCompletion();
                    Debug.Log("load code dll success");

                    TextAsset codepdb = Addressables.LoadAssetAsync<TextAsset>("Code.pdb").WaitForCompletion();
                    Debug.Log("load code pdb success");

                    byte[] assBytes = codedll.bytes;
                    byte[] pdbBytes = codepdb.bytes;
                    Debug.Log($"ass bytes {assBytes.Length}");
                    Debug.Log($"pdb bytes {pdbBytes.Length}");
                    appDomain = new ILRuntime.Runtime.Enviorment.AppDomain();
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
                    Debug.Log($"System.Threading.Thread.CurrentThread.ManagedThreadId");
                    this.appDomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
                    MemoryStream assStream = new MemoryStream(assBytes);
                    MemoryStream pdbStream = new MemoryStream(pdbBytes);
                    Debug.Log($"System.Threading.Thread.CurrentThread.ManagedThreadId");

                    appDomain.LoadAssembly(assStream, pdbStream, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
                    Debug.Log($"ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider");
                    Debug.Log("InitILRuntime");
                    ILHelper.InitILRuntime(appDomain);
                    Debug.Log("appDomain.LoadedTypes.Values.Select(x => x.ReflectionType).ToArray();");
                    this.allTypes = appDomain.LoadedTypes.Values.Select(x => x.ReflectionType).ToArray();
                    Debug.Log(" IStaticMethod start = new ILStaticMethod");
                    IStaticMethod start = new ILStaticMethod(appDomain, "ET.Entry", "Start", 0);
                    Debug.Log("start run");
                    start.Run();
                    break;
                }
                case CodeMode.Reload:
                {
                    Debug.Log("reload");
                    byte[] assBytes = File.ReadAllBytes(Path.Combine(Define.BuildOutputDir, "Data.dll"));
                    byte[] pdbBytes = File.ReadAllBytes(Path.Combine(Define.BuildOutputDir, "Data.pdb"));

                    assembly = Assembly.Load(assBytes, pdbBytes);
                    this.LoadLogic();
                    IStaticMethod start = new MonoStaticMethod(assembly, "ET.Entry", "Start");
                    start.Run();
                    break;
                }
            }
        }

        // 热重载调用下面三个方法
        // CodeLoader.Instance.LoadLogic();
        // Game.EventSystem.Add(CodeLoader.Instance.GetTypes());
        // Game.EventSystem.Load();
        public void LoadLogic()
        {
            if (this.CodeMode != CodeMode.Reload)
            {
                throw new Exception("CodeMode != Reload!");
            }

            // 傻屌Unity在这里搞了个傻逼优化，认为同一个路径的dll，返回的程序集就一样。所以这里每次编译都要随机名字
            string[] logicFiles = Directory.GetFiles(Define.BuildOutputDir, "Logic_*.dll");
            if (logicFiles.Length != 1)
            {
                throw new Exception("Logic dll count != 1");
            }

            string logicName = Path.GetFileNameWithoutExtension(logicFiles[0]);
            byte[] assBytes = File.ReadAllBytes(Path.Combine(Define.BuildOutputDir, $"{logicName}.dll"));
            byte[] pdbBytes = File.ReadAllBytes(Path.Combine(Define.BuildOutputDir, $"{logicName}.pdb"));

            Assembly hotfixAssembly = Assembly.Load(assBytes, pdbBytes);

            List<Type> listType = new List<Type>();
            listType.AddRange(this.assembly.GetTypes());
            listType.AddRange(hotfixAssembly.GetTypes());
            this.allTypes = listType.ToArray();
        }

        public Type[] GetTypes()
        {
            return this.allTypes;
        }
    }
}