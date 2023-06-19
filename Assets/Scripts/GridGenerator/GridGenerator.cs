using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class GridGenerator : MonoBehaviour
{
    [SerializeField] private GridSO gridSO;
    [SerializeField] internal List<Transform> gemSpawnPoints = new List<Transform>();

    private void OnEnable()
    {
        SpawnGrid();
        InitializeSpawnPlaces();
    }
    private void Awake()
    {
            
       
       

    }
    

    private void InitializeSpawnPlaces()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            gemSpawnPoints.Add(transform.GetChild(i));

        }
    }

    private void SpawnGrid()
    {
        for (int i = 0; i < gridSO.row; i++)
        {
            for (int j=0; j < gridSO.column; j++)
            {
                Vector3 spawnPosition=new Vector3(i*gridSO.gridSpaceOffset,0f,j*gridSO.gridSpaceOffset)+gridSO.gridOrigin;
                SpawnGridLayout(spawnPosition, Quaternion.identity,j+i*gridSO.column);
            }
        }
    }

    private void SpawnGridLayout(Vector3 positionToSpawn,Quaternion rotation,int initialIndex)
    {
        Transform gridPrefab=Instantiate(gridSO.gridPrefab,positionToSpawn,rotation);
       
        gridPrefab.GetComponent<CollectType>().order=initialIndex;     
        gridPrefab.transform.SetParent(this.transform);
       
    }

   


}





