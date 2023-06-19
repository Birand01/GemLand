using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private TMP_Text goldText;
    private void OnEnable()
    {
        CountManager.OnTotalGoldAmount += PrintTotalGoldAmount;
    }
    
    private void OnDisable()
    {
        CountManager.OnTotalGoldAmount -= PrintTotalGoldAmount;

    }
    private void Awake()
    {
        goldText = GameObject.FindGameObjectWithTag("GoldText").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        goldText.text = Mathf.Round(CountManager.GetInstance().totalGoldAmount).ToString();
    }

    private void PrintTotalGoldAmount(float value)
    {
        goldText.text =Mathf.Round(value).ToString();

    }
    
}
