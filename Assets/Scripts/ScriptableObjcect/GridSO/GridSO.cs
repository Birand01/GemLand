using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridConfiguration", menuName = "ScriptableObjects/GridConfiguration", order = 2)]
public class GridSO : ScriptableObject
{
    public Transform gridPrefab;
    public int row, column;
    [Range(0, 10)] public float gridSpaceOffset;
    public Vector3 gridOrigin;
}
