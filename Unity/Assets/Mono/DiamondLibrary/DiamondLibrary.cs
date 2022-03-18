using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ET
{
    // [Serializable]
    // public class DiamondSprite
    // {
    //     public Sprite Sprite;
    //     public int DiamondType;
    // }

    [CreateAssetMenu(fileName = "DiamondLibrary.asset", menuName = "Diamond/Create Diamond Library")]
    public class DiamondLibrary: ScriptableObject
    {
        public int DiamondType;
        public Sprite normalTexture;
        public Sprite BoomTexture;
        public Sprite LazerVTexture;
        public Sprite LazerHTexture;
        public Sprite BlackHoleTexture;
    }
}