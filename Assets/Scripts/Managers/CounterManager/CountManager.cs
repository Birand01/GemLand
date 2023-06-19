using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountManager : SingletonBase<CountManager> 
{
    public static event Action<string, float> OnTotalGoldSave;
    public static event Action<float> OnTotalGoldAmount;


    [SerializeField] internal float totalGoldAmount;
    internal string goldKey;


    private void OnEnable()
    {
        GemManager.OnCalculateGoldAmountOnDrop += CalculateTotalGoldAmount;
    }
    private void OnDisable()
    {
        GemManager.OnCalculateGoldAmountOnDrop -= CalculateTotalGoldAmount;

    }

    protected override void Awake()
    {
        base.Awake();
        goldKey = "TotalGoldAmount";
      
    }
    private void Start()
    {
        LoadCollectedoldCount();
    }

    private void LoadCollectedoldCount()
    {
        totalGoldAmount = PlayerPrefs.GetFloat(goldKey);
    } 

    private void CalculateTotalGoldAmount()
    {
        foreach (Transform gem in GemManager.GetInstance().gemList)
        {
           
            GemInteraction gemInteraction = gem.gameObject.GetComponent<GemInteraction>();
            if (gemInteraction != null)
            {
               
                totalGoldAmount += gemInteraction.CalculateGemPrice(gemInteraction.gemSO.initialPrice, gemInteraction.collectedGemScale);
                OnTotalGoldAmount?.Invoke(totalGoldAmount); // UI
                OnTotalGoldSave?.Invoke(goldKey, totalGoldAmount);
            }

        }
    }
}
