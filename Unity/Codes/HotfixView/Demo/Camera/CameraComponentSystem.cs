using System;
using UnityEngine;

namespace ET.Demo.Camera
{
    public class CameraComponentAwakeSystem: AwakeSystem<CameraComponent>
    {
        public override void Awake(CameraComponent self)
        {
        }
    }

    public static class CameraCamponentSystem
    {
        public static void ChangeFightCameraLook(this CameraComponent self, bool value)
        {
            if (self.CmProcessCamera == null)
            {
                self.CmProcessCamera = GameObject.Find("CM_Process");
            }

            if (self.CmProcessCamera != null)
            {
                self.CmProcessCamera.SetActive(!value);
            }
        }
    }
}