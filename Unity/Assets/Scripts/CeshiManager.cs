using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class CeshiManager: MonoBehaviour
{
    public List<HeroModeLibrary> heroModeLibraries;
    public GameObject TargetHeroMode;
    private GameObject currentShowMode;
    private int index = 0;

    public void Start()
    {
        this.ShowMode(this.index);
    }

    public void OnButtonClick(string custom)
    {
        switch (custom)
        {
            case "left":
                this.ShowBefor();
                return;
                break;
            case "right":
                this.ShowNext();
                return;
                break;
        }

        StartCoroutine(this.PlayAnim(custom));
    }

    Vector3 GetTargetPos(SkillConfig config)
    {
        var type = config.TargetPosType;
        if (type == TargetPosType.Middle)
        {
            return this.TargetHeroMode.transform.position * 0.5f;
        }

        return this.TargetHeroMode.transform.position - Vector3.forward;
    }

    IEnumerator PlayMove(Vector3 startPos, Vector3 targetPos, HeroModeLibrary library, SkillConfig skillConfig)
    {
        if (skillConfig.MoveType == MoveType.NoMove)
        {
            yield break;
        }

        this.currentShowMode.GetComponent<Animator>().SetBool("Run", true);
        float time = 0;
        this.currentShowMode.transform.forward = targetPos - startPos;
        while (time < 0.5f)
        {
            time += Time.deltaTime;

            var pos = Vector3.Lerp(startPos, targetPos, time * 2);
            this.currentShowMode.transform.position = pos;
            yield return 0;
        }

        this.currentShowMode.GetComponent<Animator>().SetBool("Run", false);
    }

    IEnumerator PlayAnim(string custom)
    {
        Dictionary<string, string> map = new Dictionary<string, string>();
        map.Add("pugong", "Attack");
        map.Add("jineng1", "Skill1");
        map.Add("jineng2", "Skill2");
        map.Add("dazhao", "BigSkill");
        if (!map.Keys.Contains(custom))
        {
            yield break;
        }

        HeroModeLibrary library = this.heroModeLibraries[this.index];
        List<SkillConfig> skillConfigs = library.skillConfigs;
        SkillConfig config = skillConfigs.Find(a => { return a.SkillName.Equals(map[custom]); });

        var taregtPos = this.GetTargetPos(config);
        yield return PlayMove(Vector3.zero, taregtPos, library, config);
        StartCoroutine(this.PlayFlySkillEffect(config));
        StartCoroutine(PlaySkillEffect(config));
        StartCoroutine(this.PlayEnemyBeAttackAnim(config));
        StartCoroutine(this.PlayEnemyBeAttackEffect(config));
        this.currentShowMode.GetComponent<Animator>().SetTrigger(map[custom]);
        // float time = 
        yield return new WaitForSeconds(config.SkillTime);
        // this.currentShowMode.GetComponent<Animator>().SetTrigger("Idle");
        yield return this.PlayMove(taregtPos, Vector3.zero, library, config);
        this.currentShowMode.transform.forward = Vector3.forward;
    }

    // IEnumerator PlayBeAttackEffect(SkillConfig config)
    // {
    //     yield break;
    // }

    IEnumerator PlaySkillEffect(SkillConfig config)
    {
        yield return new WaitForSeconds(config.EffectStartTime);
        if (config.effectPrefab != null)
        {
            GameObject skillPrefab = config.effectPrefab;
            var skill = Instantiate(skillPrefab);
            skill.transform.position = this.currentShowMode.transform.position;
            yield return new WaitForSeconds(5);
            Destroy(skill);
        }
    }

    IEnumerator PlayFlySkillEffect(SkillConfig skillConfig)
    {
        if (skillConfig.FlyEffectPrefab == null)
        {
            yield break;
        }

        float startTime = skillConfig.FlyEffectStartTime;
        yield return new WaitForSeconds(startTime);
        var obj = Instantiate(skillConfig.FlyEffectPrefab);
        obj.transform.position = this.currentShowMode.transform.position;

        float time = 0;
        while (time < 0.5f)
        {
            var pos = Vector3.Lerp(this.currentShowMode.transform.position, this.TargetHeroMode.transform.position, time * 2);
            obj.transform.position = pos;

            time += Time.deltaTime;
            yield return 0;
        }

        GameObject.Destroy(obj);

        yield break;
    }

    IEnumerator PlayEnemyBeAttackAnim(SkillConfig config)
    {
        yield return new WaitForSeconds(config.ShouJiTime);

        this.TargetHeroMode.GetComponent<Animator>().SetTrigger("BeAttack");
       
    }

    IEnumerator PlayEnemyBeAttackEffect(SkillConfig config)
    {
        yield return new WaitForSeconds(config.BeAttackEffectPlayTime);
        if (config.BeAttacckEffectPrefab != null)
        {
            GameObject skillPrefab = config.BeAttacckEffectPrefab;
            var skill = Instantiate(skillPrefab);
            // var bound = this.TargetHeroMode.GetComponentInChildren<SkinnedMeshRenderer>().bounds.center;
            skill.transform.position = this.TargetHeroMode.transform.position;
            if (config.BeHitedBoneName != "")
            {
                GameObject obj = GameObject.Find($"TargetHero001/{config.BeHitedBoneName}");
                skill.transform.position = obj.transform.position;
            }

            yield return new WaitForSeconds(2);
            GameObject.Destroy(skill);
        }
    }

    public void ShowMode(int index)
    {
        var mode = this.heroModeLibraries[index];
        if (this.currentShowMode != null)
        {
            Destroy(this.currentShowMode);
        }

        this.currentShowMode = Instantiate(mode.HeroMode);
    }

    public void ShowNext()
    {
        this.index++;
        if (this.index >= this.heroModeLibraries.Count)
        {
            this.index = 0;
        }

        this.ShowMode(this.index);
    }

    public void ShowBefor()
    {
        this.index--;
        if (this.index < 0)
        {
            this.index = this.heroModeLibraries.Count - 1;
        }

        ShowMode(this.index);
    }
}