using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HeroCardTexture
{
    public int id;
    public Sprite Texture;
}
[CreateAssetMenu(fileName = "AllHeroCardTextureLibrary.asset",menuName = "HeroCard/Create All Hero Texture")]
public class AllHeroCardLibrary : ScriptableObject
{
    public List<HeroCardTexture> HeroCardTextures = new List<HeroCardTexture>();

    public Dictionary<int, Sprite> HeroCardTextureDict = new Dictionary<int, Sprite>();
    public void OnEnable()
    {
        foreach (var value in this.HeroCardTextures)
        {
            this.HeroCardTextureDict.Add(value.id, value.Texture);
        }
        
    }
}
