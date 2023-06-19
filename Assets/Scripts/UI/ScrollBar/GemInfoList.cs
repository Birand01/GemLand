using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GemInfoList : MonoBehaviour
{
    public static event Action<int, string> OnSaveCollectedGem;
    [SerializeField] private List<GemSO> gemSOList = new List<GemSO>();
    [SerializeField] private List<GameObject> gemCardList=new List<GameObject>();
    [SerializeField] private GameObject gemCardPrefab;
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            gemSOList.Add(this.transform.GetChild(i).GetComponent<GemSO>());
        }
      
      
    }
    private void OnEnable()
    {
        GemInteraction.OnUpdateCollectedCountList += UpdateGemCount;
    }
    private void OnDisable()
    {
        GemInteraction.OnUpdateCollectedCountList -= UpdateGemCount;
    }
    private void Start()
    {
       
        InitializeGemUIList();
       
    }

  
    private void InitializeGemUIList()
    {
        for (int i = 0; i < gemSOList.Count; i++)
        {
            GameObject gemPrefab = Instantiate(gemCardPrefab);
            gemPrefab.transform.SetParent(this.transform);
            gemPrefab.transform.GetChild(0).GetComponent<Image>().sprite = gemSOList[i].gemIcon;
            gemPrefab.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = gemSOList[i].gemName;
            gemPrefab.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = gemSOList[i].collectedCount.ToString();
            gemPrefab.transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = gemSOList[i].initialPrice.ToString();
            gemCardList.Add(gemPrefab);

        }

    }
    private void UpdateGemCount(int collectedCount,string gemName)
    {
        for (int i = 0; i < gemCardList.Count; i++)
        {
            if (gemCardList[i].transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text==gemName.ToString())
            {
                gemCardList[i].transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = collectedCount.ToString();
               
            }
            OnSaveCollectedGem?.Invoke(collectedCount, gemName);
        }
    }
}
