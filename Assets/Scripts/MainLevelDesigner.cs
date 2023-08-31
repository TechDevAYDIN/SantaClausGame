using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainLevelDesigner : MonoBehaviour
{
    public Transform goalTransform;
    public List<GameObject> PlatformPrefabs = new List<GameObject>();
    [SerializeField] GameObject RingPlatform;
    private List<GameObject> spawnedLevels = new List<GameObject>();
    private float spawnPosZ = 30;
    // Start is called before the first frame update
    void Start()
    {

    }

    private Level[] GetLevels()
    {
        return Resources.LoadAll<Level>("Levels");
    }
    public void LoadLevel(int levelNum)
    {
        // Get the correct stage
        for (int i = levelNum; i > GetLevels().Length - 1; i -= 10)
        {
            Debug.Log("Level " + levelNum);
            levelNum -= 10;
        }        
        Level level = GetLevels()[Mathf.Clamp(levelNum, 0, GetLevels().Length - 1)];

        if (level == null)
        {
            Debug.LogError("No Level " + levelNum + " found in allSLevels list (MainLevelDesigner). All stages assigned in list?");
            return;
        }

        for (int i = 0; i < level.platforms.Count; i++)
        {
            GameObject platform;

            int RandPlat = Random.Range(Mathf.Clamp(level.platforms[i].minValue - 1, 0, PlatformPrefabs.Count - 1),
                                        Mathf.Clamp(level.platforms[i].maxValue, 0, PlatformPrefabs.Count - 1));

            platform = Instantiate(PlatformPrefabs[RandPlat], null);
            platform.transform.localPosition = new Vector3(0, -15, spawnPosZ);
            platform.transform.eulerAngles = new Vector3(0, 0, 0);
            spawnedLevels.Add(platform);
            spawnPosZ += 180;
            int RandRingPlat = Random.Range(0, 4);
            for (int x = 0; x < RandRingPlat; x++)
            {
                //AddRingPlatform();
            }
        }
        goalTransform.position = new Vector3(0, -15, spawnPosZ + 25);
        FindObjectOfType<MovementController>().zKalan = spawnPosZ;
        //GetComponent<NavMeshSurface>().BuildNavMesh();
        StartCoroutine(BuildNav());
        //Debug.Log(NavMeshLoader.LoadNavMeshAsync(GetComponent<NavMeshSurface>()));

        
    }

    IEnumerator BuildNav()
    {
        yield return new WaitForSeconds(.5f);
        AsyncOperation operation = GetComponent<NavMeshSurface>().UpdateNavMesh(GetComponent<NavMeshSurface>().navMeshData);
        //yield return new WaitUntil(()=> operation.isDone);
        while (!operation.isDone)
        {
            yield return new WaitForSeconds(.2f);
        }
        if (operation.isDone)
        {
            GameManager.singleton.canSpawnMan = true;
        }
    }
    void AddRingPlatform()
    {
        GameObject platform;

        

        platform = Instantiate(RingPlatform, null);
        platform.transform.localPosition = new Vector3(0, -15, spawnPosZ);
        platform.transform.eulerAngles = new Vector3(0, 0, 0);
        spawnedLevels.Add(platform);
        spawnPosZ += 50;
    }
}
