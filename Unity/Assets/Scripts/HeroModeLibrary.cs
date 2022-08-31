using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum MoveType
{
    Move = 1,
    NoMove = 2,
}

public enum TargetPosType
{
    Face = 1,
    Middle = 2,
}

[Serializable]
public class SkillConfig
{
    public string SkillName;
    public float SkillTime;
    public float ShouJiTime;
    public GameObject effectPrefab;
    public string skill1BoneName;
    public string skill2BoneName;
    public GameObject effectPrefab1;
    public float EffectStartTime;
    public float EffectStartTime2;
    public GameObject BeAttacckEffectPrefab;
    public float BeAttackEffectPlayTime;
    public float FlyEffectStartTime;
    public GameObject FlyEffectPrefab;
    public MoveType MoveType = MoveType.Move;
    public TargetPosType TargetPosType = TargetPosType.Face;
    public string BeHitedBoneName = "";
}

[CreateAssetMenu(menuName = "create hero mode liabray", fileName = "heromode.asset")]
public class HeroModeLibrary: ScriptableObject
{
    public GameObject HeroMode;
    public string HeroName;
    // public MoveType MoveType;
    public List<SkillConfig> skillConfigs;
    // Lazy<>
}