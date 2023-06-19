using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GemType
{
    Star,
    Cubie,
    Diamond,
    SphereGem
}

[RequireComponent(typeof(ObjectPoolBase))]
public class GemSpawner : MonoBehaviour
{
    [SerializeField] private GemSpawnerSO gemSpawnerSO;  
    [SerializeField] private List<GameObject> gridGeneratorPrefabs = new List<GameObject>();
    private ObjectPoolBase objectPoolBase;
    private int randomGemNumber;
    private int totalGridCount;
    private void OnEnable()
    {
        GemInteraction.OnGemTouchIndex += InstantiatePooledGem;
    }
    private void OnDisable()
    {
        GemInteraction.OnGemTouchIndex -= InstantiatePooledGem;

    }
 
    private void Awake()
    {
        objectPoolBase=GetComponent<ObjectPoolBase>();
        LoadCollectedGemCount();
    }
  
  
    private void Start()
    {
        SetPoolSize();
        StartInitialInstantiation();
        InitializePool();

    }
    private void LoadCollectedGemCount()
    {
        for (int i = 0; i < gemSpawnerSO.gemList.Count; i++)
        {
            gemSpawnerSO.gemList[i].collectedCount = PlayerPrefs.GetInt(gemSpawnerSO.gemList[i].gemName);

        }
    }

    private void StartInitialInstantiation()
    {
        for (int i = 0; i < gridGeneratorPrefabs.Count; i++)
        {
            StartCoroutine(SpawnInitialGem(gemSpawnerSO.spawnDelay, gridGeneratorPrefabs[i]));
        }
    }
    private void SetPoolSize()
    {
        for (int i = 0; i < gridGeneratorPrefabs.Count; i++)
        {
            for (int j = 0; j < gridGeneratorPrefabs[i].transform.childCount; j++)
            {
                totalGridCount++;

            }
        }
        objectPoolBase.poolSize = totalGridCount;

    }

    private void InitializePool()
    {
        for (int i = 0; i < objectPoolBase.poolSize; i++)
        {
            randomGemNumber = UnityEngine.Random.Range(0, gemSpawnerSO.gemList.Count);
            objectPoolBase.CreateObject(gemSpawnerSO.gemList[randomGemNumber].gemPrefab);
        }
    }
    private IEnumerator SpawnInitialGem(float delay,GameObject gridGo)
    {
       int index = 0;
       while (index < gridGo.GetComponent<GridGenerator>().gemSpawnPoints.Count)
        {
           
            InstantiateGem(index, gridGo.transform.GetChild(index).gameObject);
            yield return new WaitForSeconds(1f);
            index++;
        }
      
    }
    private void InstantiateGem(int index,GameObject go)
    {
       
        randomGemNumber = UnityEngine.Random.Range(0, gemSpawnerSO.gemList.Count);

        GameObject gem = Instantiate(gemSpawnerSO.gemList[randomGemNumber].gemPrefab);
        PropertyOfGameObject(gem, go.transform);
    }


    private void InstantiatePooledGem(int index, GameObject go)
    {

        randomGemNumber = UnityEngine.Random.Range(0, gemSpawnerSO.gemList.Count);
        GameObject gem = objectPoolBase.DeQueueGameObject();
        PropertyOfGameObject(gem, go.transform);     
        if(objectPoolBase.IsQeueuEmpty())
        {
            InitializePool();
        }
    }

    private void PropertyOfGameObject(GameObject gameObject,Transform parent)
    {
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.transform.SetParent(parent.transform); // child of  gridGenerator
        gameObject.transform.localPosition = new Vector3(0, 3f, 0);
        gameObject.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
        gameObject.transform.DOScale(gemSpawnerSO.finalScaleSize, gemSpawnerSO.scaleDelay).SetEase(Ease.Linear);
    }

  
}
