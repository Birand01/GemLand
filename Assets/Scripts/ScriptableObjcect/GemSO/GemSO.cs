using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "GemConfiguration", menuName = "ScriptableObjects/GemConfiguration", order = 0)]

public class GemSO : ScriptableObject
{
    public string gemName;
    public float initialPrice;
    public Sprite gemIcon;
    public GameObject gemPrefab;
    public int collectedCount;
    public Gem gemType;
   
}
public enum Gem
{
    Diamond,
    Cubie,
    Star,
    Sphere,
    Heart
}