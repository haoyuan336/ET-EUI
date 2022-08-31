using UnityEngine;
using System.Collections;

public class SM_effectGenerator : MonoBehaviour
{
    #region 变量
    private GameObject[] createThis;  // list of possible prefabs
    private int rndNr; // this is for just a random number holder when we need it
    public int thisManyTimes = 3;
    public float overThisTime = 1.0f;

    public float xWidth;  // define the square where prefabs will be generated
    public float yWidth;
    public float zWidth;

    public float xRotMax;  // define maximum rotation of each prefab
    public float yRotMax = 180;
    public float zRotMax;

    public bool allUseSameRotation = false;
    private bool allRotationDecided = false;

    public bool detachToWorld = true;

    private float x_cur;  // these are used in the random palcement process
    private float y_cur;
    private float z_cur;

    private float xRotCur;  // these are used in the random protation process
    private float yRotCur;
    private float zRotCur;

    private float timeCounter;  // counts the time :p
    private float effectCounter;  // you will guess ti

    private float trigger;  // trigger: at which interwals should we generate a particle
    public float duration = 1;
    public float startDelay = 0;
    #endregion

    private void OnEnable()
    {
        effectCounter = 0;
        int childCount = transform.childCount;
        createThis = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            createThis[i] = transform.GetChild(i).gameObject;
            createThis[i].SetActive(false);
        }
        if (thisManyTimes < 1) //hack to avoid division with zero and negative numbers
            thisManyTimes = 1;
        trigger = overThisTime / thisManyTimes;  //define the intervals of time of the prefab generation.
        StartCoroutine(CreatePrefab());
    }

    private void OnDisable()
    {
        StopCoroutine(CreatePrefab());
    }

    IEnumerator CreatePrefab()
    {
        while (startDelay > 0)
        {
            startDelay -= Time.deltaTime;
            yield return null;
        }
        while (effectCounter <= thisManyTimes)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter > trigger && effectCounter <= thisManyTimes)
            {
                rndNr = Mathf.RoundToInt(Random.value * createThis.Length);  //decide which prefab to create
                if (rndNr >= createThis.Length)
                    rndNr = createThis.Length - 1;

                x_cur = transform.position.x + (Random.value * xWidth) - (xWidth * 0.5f);  // decide an actual place
                y_cur = transform.position.y + (Random.value * yWidth) - (yWidth * 0.5f);
                z_cur = transform.position.z + (Random.value * zWidth) - (zWidth * 0.5f);

                // basically this plays only once if allRotationDecided=true, otherwise it plays all the time
                if (allUseSameRotation == false || allRotationDecided == false)
                {
                    xRotCur = transform.rotation.x + (Random.value * xRotMax * 2) - (xRotMax);  // decide rotation
                    yRotCur = transform.rotation.y + (Random.value * yRotMax * 2) - (yRotMax);
                    zRotCur = transform.rotation.z + (Random.value * zRotMax * 2) - (zRotMax);
                    allRotationDecided = true;
                }

                GameObject justCreated = Instantiate(createThis[rndNr], new Vector3(x_cur, y_cur, z_cur), transform.rotation) as GameObject;  //create the prefab
                justCreated.transform.Rotate(xRotCur, yRotCur, zRotCur);
                justCreated.SetActive(true);
                if (detachToWorld == false)  // if needed we attach the freshly generated prefab to the object that is holding this script
                    justCreated.transform.parent = transform;
                timeCounter -= trigger;  //administration :p
                effectCounter += 1;
                GameObject.DestroyObject(justCreated, duration);
            }
            yield return null;
        }
    }
}
