using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    public List<GameObject> baseObjects;
    public List<int> pooledAmount;
    public int defaultBufferAmount = 20;

    private List<List<GameObject>> pooledObjects;

    void Awake()
    {
        if( instance == null )
        {
            instance = this;
        }
        else
        {
            Debug.Log("Duplicate instance detected, destroying gameObject");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        pooledObjects = new List<List<GameObject>>();

        for(int i = 0; i < baseObjects.Count; i++)
        {
            if(pooledAmount[i] <= 0)
                pooledAmount[i] = defaultBufferAmount;

            List<GameObject> list = new List<GameObject>();

            for(int j = 0; j < pooledAmount[i]; j++)
            {
                GameObject obj = Instantiate(baseObjects[i]);
                obj.SetActive(false);
                obj.transform.SetParent(this.transform);
                list.Add(obj);
            }

            pooledObjects.Add(list);
        }
    }

    public GameObject GetObject(string objectName, bool canExpand=false)
    {
        for(int i = 0; i < baseObjects.Count; i++)
        {
            if(baseObjects[i].name == objectName)
            {
                for(int j = 0; j < pooledObjects[i].Count; j++)
                {
                    if(!pooledObjects[i][j].activeInHierarchy)
                    {
                        pooledObjects[i][j].SetActive(true);
                        return pooledObjects[i][j];
                    }
                }

                if(canExpand)
                {
                    GameObject obj = Instantiate(baseObjects[i]);
                    pooledObjects[i].Add(obj);
                    return obj;
                }
            }
        }

        return null;
    }

    public void PoolObject(GameObject obj)
    {
        obj.transform.SetParent(this.transform);
        obj.SetActive(false);
    }

    public void ResetPool(string objectName)
    {
        for(int i = 0; i < baseObjects.Count; i++)
        {
            if(baseObjects[i].name == objectName)
            {
                for(int j = 0; j < pooledObjects[i].Count; j++)
                {
                    if(pooledObjects[i][j].activeInHierarchy)
                    {
                        pooledObjects[i][j].transform.SetParent(this.transform);
                        pooledObjects[i][j].SetActive(false);
                    }
                }

                break;
            }
        }
    }
}