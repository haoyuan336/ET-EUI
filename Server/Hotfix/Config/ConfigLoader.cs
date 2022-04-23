using System.Collections.Generic;
using System.IO;

namespace ET
{
    public class ConfigLoader: IConfigLoader
    {
        public async ETTask GetAllConfigBytes(Dictionary<string, byte[]> output)
        {
            foreach (string file in Directory.GetFiles($"../Config", "*.bytes"))
            {
                string key = Path.GetFileNameWithoutExtension(file);
                output[key] = File.ReadAllBytes(file);
            }

            await ETTask.CompletedTask;
        }

        public byte[] GetOneConfigBytes(string configName)
        {
            byte[] configBytes = File.ReadAllBytes($"../Config/{configName}.bytes");
            return configBytes;
        }
    }
}