using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolBase : MonoBehaviour
{
    
    protected Queue<GameObject> objectPool;
    [SerializeField] private Transform spawnedObjectsParent;
    internal int poolSize;
    private void OnEnable()
    {
        GemManager.OnGameobjectEnqueueHandler += EnQueueGameObject;
    }
    private void OnDisable()
    {
        GemManager.OnGameobjectEnqueueHandler -= EnQueueGameObject;

    }
    private void Awake()
    {

        objectPool = new Queue<GameObject>();
    }

   
    public GameObject CreateObject(GameObject gemPrefab)
    {
        GameObject spawnedObject = gemPrefab;
        if (objectPool.Count < poolSize)
        {
            spawnedObject = Instantiate(gemPrefab, transform.position, Quaternion.identity);
            spawnedObject.name = transform.root.name + "_" + gemPrefab.name + "_" + objectPool.Count;
            spawnedObject.transform.parent = spawnedObjectsParent;
            EnQueueGameObject(spawnedObject);
        }
       
      
        return spawnedObject;
    }

    public bool IsQeueuEmpty()
    {
        if (objectPool.Count <= 0)
        {
            return true;
        }
        return false;
    }

    public GameObject DeQueueGameObject()
    {
        if(objectPool.Count<=0)
        {
            return null;
        }
        return objectPool.Dequeue();
    }

    public void EnQueueGameObject(GameObject gameObject)
    {
        gameObject.transform.parent = spawnedObjectsParent;
        objectPool.Enqueue(gameObject);
    }
}
