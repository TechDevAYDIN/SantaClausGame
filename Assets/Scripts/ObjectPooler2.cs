using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler2 : MonoBehaviour
{
    #region Instance
    public static ObjectPooler2 SharedInstance
    {
        get
        {
            if (sharedInstance == null)
                sharedInstance = FindObjectOfType(typeof(ObjectPooler2)) as ObjectPooler2;

            return sharedInstance;
        }
        set
        {
            sharedInstance = value;
        }
    }
    private static ObjectPooler2 sharedInstance;
    #endregion

    public List<GameObject> pooledObjects;

    public List<GameObject> itemsToPool;

    // Start is called before the first frame update
    void Start()
    {
        CreateObject();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateObject()
    {
        if (itemsToPool == null) return;
        for (int i = 0; i < itemsToPool.Count * 5; i++)
        {
            CreatePooledObject(itemsToPool[0]);
        }
    }
    public GameObject CreatePooledObject(GameObject item)
    {
        GameObject obj = Instantiate(item);
        obj.transform.SetParent(transform);
        //obj.transform.localScale = item.transform.localScale;
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
    public GameObject GetPooledObject()
    {
        pooledObjects.RemoveAll(i => i == null);
        GameObject obj = null;
        while (true)
        {
            int index = Random.Range(0,pooledObjects.Count);

            if (pooledObjects[index] == null || pooledObjects[index].activeSelf) continue;
            if (!pooledObjects[index].gameObject.activeSelf)
            {
                obj = pooledObjects[index];
                if (obj) break;
            }
        }
        if (obj != null)
        {
            obj.gameObject.SetActive(true);
            return obj.gameObject;
        }
        return null;
    }
    public void PutBack(GameObject obj)
    {
        obj.SetActive(false);
    }
}
