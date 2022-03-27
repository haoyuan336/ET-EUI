using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCardViewCtl: MonoBehaviour
{
    public TextMesh AttackTextMesh;
    public TextMesh AngryTextMesh;

    public void UpdateAttackView(float value)
    {
        this.AttackTextMesh.text = $"Attack:{value.ToString()}";
    }

    public void UpdateAngryView(float value)
    {
        this.AngryTextMesh.text = $"Angry:{value.ToString()}";
    }
}