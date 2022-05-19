using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(GameObject.Find("ObjectPooler").transform);
                objectPool.Enqueue(obj);

            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, Transform parent)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        //objectToSpawn.transform.parent = parent;
        objectToSpawn.transform.SetParent(parent);

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();

        

        return objectToSpawn;
    }
    public void PlaceInPool(GameObject objToDelete, string tag)
    {
        
        objToDelete.SetActive(false);
        objToDelete.transform.position = new Vector3(0, 0, 0);
        //objToDelete.transform.rotation = new Quaternion(0, 0, 0, 0);
        //objToDelete.transform.parent = null;
        objToDelete.transform.SetParent(GameObject.Find("ObjectPooler").transform);

        IPooledObject pooledObject = objToDelete.GetComponent<IPooledObject>();
        if (pooledObject != null)
        {
            pooledObject.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objToDelete);


    }

}
