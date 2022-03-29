using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using ET;
using UnityEngine;

public class HeroCardViewCtl: MonoBehaviour
{
    public TextMesh AttackTextMesh;
    public TextMesh AngryTextMesh;

    public SpriteRenderer HeroCardSpriteRenderer;

    public AllHeroCardLibrary AllHeroCardLibrary;

    private GameObject HeroMode;

    public void UpdateAttackView(float value)
    {
        this.AttackTextMesh.text = $"Attack:{value.ToString()}";
    }

    public void UpdateAngryView(float value)
    {
        this.AngryTextMesh.text = $"Angry:{value.ToString()}";
    }

    public void InitInfo(int configId)
    {
        Log.Debug($"hero color {configId}");
        HeroCardLibrary heroCardLibrary;
        if (this.AllHeroCardLibrary.HeroCardTextureDict.TryGetValue(configId, out heroCardLibrary))
        {
            this.HeroCardSpriteRenderer.sprite = heroCardLibrary.Texture;
            GameObject go = Instantiate(heroCardLibrary.HeroModePrefab, this.transform);
            // go.transform.forward = Vector3.back;
            go.transform.localScale = Vector3.one * 2;
            go.SetActive(false);
            this.HeroMode = go;
        }
    }

    public async void ChangeModeView()
    {
      

        this.HeroCardSpriteRenderer.gameObject.SetActive(false);
        this.HeroMode.SetActive(true);
    }

    public void ChangeCardView()
    {
    }

     public async  ETTask PlayAttackAnim(GameObject target)
     {
         Vector3 targetPos = target.transform.position;

         float distance = 1;

         while (distance > 0.1f)
         {
             Vector3 prePos = Vector3.Lerp(this.HeroMode.transform.position, targetPos, 0.01f);
             this.HeroMode.transform.position = prePos;
             distance = Vector3.Distance(prePos, targetPos);
             yield return 0;
         }
         
         
         
         await ETTask.CompletedTask;
     }
}