using System.Collections;
using ET;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
    public TextMesh HPTextMesh;
    public TextMesh DiamondAttackTextMesh; //宝石攻击

    public SpriteRenderer HeroCardSpriteRenderer;

    // public AllHeroCardLibrary AllHeroCardLibrary;

    // public GameObject HeroMode;

    // private List<TimeTask> timeTasks = new List<TimeTask>();

    private ETTask waitTask;

    private float CurrentTime;
    private float TotalTime;

    public void UpdateAttackView(string value)
    {
        this.AttackTextMesh.text = $"A:{value.ToString()}";
    }

    public void UpdateDiamondAttackView(string value)
    {
        this.DiamondAttackTextMesh.text = $"DA:{value.ToString()}";
    }

    public void UpdateAngryView(string value)
    {
        // Log.Debug($"Update angry view {value}");
        this.AngryTextMesh.text = $"N:{value.ToString()}";
    }

    public void UpdateHPView(float value)
    {
        this.HPTextMesh.text = $"H:{value.ToString()}";
    }

    public void InitInfo(int configId, int CampIndex)
    {
        Log.Debug($"hero color {configId}");
        // this.HeroCardSpriteRenderer.sprite = heroCardLibrary.Texture;
        // GameObject go = Instantiate(heroCardLibrary.HeroModePrefab);
        
        // GameObject go = Addressables.LoadAssetAsync<GameObject>().WaitForCompletion();
        // go.transform.position = this.transform.position;
        // // go.transform.forward = Vector3.back;
        // go.transform.localScale = Vector3.one * 2;
        // // go.SetActive(false);
        // go.transform.forward = CampIndex == 0? Vector3.forward : Vector3.back;
        // this.HeroMode = go;
    }

    // public GameObject ChangeModeView()
    // {
    //     this.HeroCardSpriteRenderer.gameObject.SetActive(false);
    //     this.HeroMode.SetActive(true);
    //     return this.HeroMode;
    // }

    public void ChangeCardView()
    {
    }

    // public GameObject GetHeroMode()
    // {
    //     return this.HeroMode;
    // }

    IEnumerator PlayMoveAction(ETTask task, Vector3 targetPos)
    {
        float distance = 1;

        while (distance > 0.1f)
        {
            // Vector3 prePos = Vector3.Lerp(this.HeroMode.transform.position, targetPos, 0.01f);
            // this.HeroMode.transform.position = prePos;
            // distance = Vector3.Distance(prePos, targetPos);
            yield return new WaitForSeconds(0);
        }

        task.SetResult();
    }

    IEnumerator PlayAttackAnim(ETTask task)
    {
        // this.HeroMode.GetComponent<Animator>().SetTrigger("Attack");

        yield return new WaitForSeconds(1);
        task.SetResult();
    }

    IEnumerator PlayMoveBackAnim(ETTask task)
    {
        float distance = 1;

        Vector3 targetPos = this.transform.position;
        while (distance > 0.1f)
        {
            // Vector3 prePos = Vector3.Lerp(this.HeroMode.transform.position, targetPos, 0.01f);
            // this.HeroMode.transform.position = prePos;
            // distance = Vector3.Distance(prePos, targetPos);
            yield return new WaitForSeconds(0);
        }

        task.SetResult();
    }
}