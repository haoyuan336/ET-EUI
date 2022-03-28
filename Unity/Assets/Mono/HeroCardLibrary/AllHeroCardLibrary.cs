using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HeroCardLibrary
{
    public int id;
    public Sprite Texture;
    public GameObject HeroModePrefab;
}
[CreateAssetMenu(fileName = "AllHeroCardTextureLibrary.asset",menuName = "HeroCard/Create All Hero Texture")]
public class AllHeroCardLibrary : ScriptableObject
{
    public List<HeroCardLibrary> HeroCardTextures = new List<HeroCardLibrary>();
    public Dictionary<int, HeroCardLibrary> HeroCardTextureDict = new Dictionary<int, HeroCardLibrary>();
    public void OnEnable()
    {
        foreach (var value in this.HeroCardTextures)
        {
            this.HeroCardTextureDict.Add(value.id, value);
        }
        
    }
}
