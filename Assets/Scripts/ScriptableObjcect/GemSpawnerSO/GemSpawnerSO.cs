using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GemSpawnerConfiguration", menuName = "ScriptableObjects/GemSpawnerConfiguration", order = 3)]

public class GemSpawnerSO : ScriptableObject
{
    public float spawnDelay, scaleDelay, finalScaleSize;
    public Ease spawningEaseType;
    public List<GemSO> gemList;
}
