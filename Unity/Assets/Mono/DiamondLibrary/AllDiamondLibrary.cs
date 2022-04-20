using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

namespace ET
{
    [CreateAssetMenu(fileName = "AllDiamondLibrary.asset", menuName = "Diamond/Create All Diamond Library")]
    public class AllDiamondLibrary : ScriptableObject
    {
        public List<DiamondLibrary> DiamondSprites = new List<DiamondLibrary>();
    
        public Dictionary<int, DiamondLibrary> DiamondSpriteMap = new Dictionary<int, DiamondLibrary>();
    
        public void OnEnable()
        {
            foreach (var diamondSprite in this.DiamondSprites)
            {
                if (!this.DiamondSpriteMap.ContainsKey(diamondSprite.DiamondType))
                {
                    this.DiamondSpriteMap.Add(diamondSprite.DiamondType, diamondSprite);
                }
            }
        }
    }
}

