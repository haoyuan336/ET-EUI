using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ET
{
    [Serializable]
    public class DiamondSprite
    {
        public Sprite Sprite;
        public int DiamondType;
    }

    [CreateAssetMenu(fileName = "DiamondSpriteLibrary.asset", menuName = "Diamond/Create Diamond Sprite")]
    public class DiamondLibrary: ScriptableObject
    {
        public List<DiamondSprite> DiamondSprites = new List<DiamondSprite>();

        public Dictionary<int, DiamondSprite> DiamondSpriteMap = new Dictionary<int, DiamondSprite>();

        public void OnEnable()
        {
            foreach (var diamondSprite in this.DiamondSprites)
            {
                this.DiamondSpriteMap.Add(diamondSprite.DiamondType, diamondSprite);
            }
        }
    }
}