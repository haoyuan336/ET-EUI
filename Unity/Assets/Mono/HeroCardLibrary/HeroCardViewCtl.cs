using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using ET;
using UnityEditor;
using UnityEditor.AI;
using UnityEngine;

public class TimeTask
{
    public TimeTask(float time)
    {
        this.TotalTime = time;
        this.Task = ETTask.Create();
    }

    public ETTask Task;
    public float CurrentTime;
    public float TotalTime;

    public void Update()
    {
        this.CurrentTime += Time.deltaTime;
    }
}

public class HeroCardViewCtl: MonoBehaviour
{
    public TextMesh AttackTextMesh;
    public TextMesh AngryTextMesh;

    public SpriteRenderer HeroCardSpriteRenderer;

    public AllHeroCardLibrary AllHeroCardLibrary;

    private GameObject HeroMode;

    // private List<TimeTask> timeTasks = new List<TimeTask>();

    private ETTask waitTask;

    private float CurrentTime;
    private float TotalTime;

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
            GameObject go = Instantiate(heroCardLibrary.HeroModePrefab);
            go.transform.position = this.transform.position;
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

    public async ETTask PlayAttackLogic(GameObject target)
    {
        ETTask task = ETTask.Create();
        StartCoroutine(PlayMoveAction(task, target.transform.position + Vector3.up));
        await task;
        task = ETTask.Create();
        StartCoroutine(PlayAttackAnim(task));
        await task;
        Log.Debug("move end");
        task = ETTask.Create();
        StartCoroutine(PlayMoveBackAnim(task));
        await task;
    }

    IEnumerator PlayMoveAction(ETTask task, Vector3 targetPos)
    {
        float distance = 1;

        while (distance > 0.1f)
        {
            Vector3 prePos = Vector3.Lerp(this.HeroMode.transform.position, targetPos, 0.01f);
            this.HeroMode.transform.position = prePos;
            distance = Vector3.Distance(prePos, targetPos);
            yield return new WaitForSeconds(0);
        }

        task.SetResult();
    }

    IEnumerator PlayAttackAnim(ETTask task)
    {
        this.HeroMode.GetComponent<Animator>().SetTrigger("Attack");

        yield return new WaitForSeconds(1);
        task.SetResult();
    }

    IEnumerator PlayMoveBackAnim(ETTask task)
    {
        float distance = 1;

        Vector3 targetPos = this.transform.position;
        while (distance > 0.1f)
        {
            Vector3 prePos = Vector3.Lerp(this.HeroMode.transform.position, targetPos, 0.01f);
            this.HeroMode.transform.position = prePos;
            distance = Vector3.Distance(prePos, targetPos);
            yield return new WaitForSeconds(0);
        }

        task.SetResult();
    }
}