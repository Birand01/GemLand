using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private void OnEnable()
    {
        GemInteraction.OnUpdateCollectedCountList += SaveCollectedGemCount;
        CountManager.OnTotalGoldSave += SaveTotalGoldCount;
    }

   
    private void OnDisable()
    {
        GemInteraction.OnUpdateCollectedCountList-= SaveCollectedGemCount;
        CountManager.OnTotalGoldSave -= SaveTotalGoldCount;

    }
    private void SaveCollectedGemCount(int gemCollectedCount, string gemName)
    {       
            PlayerPrefs.SetInt(gemName, gemCollectedCount);
    }


    private void SaveTotalGoldCount(string goldName, float gemCollectedCount)
    {
        if (PlayerPrefs.HasKey(goldName))
        {
            PlayerPrefs.SetFloat(goldName, gemCollectedCount );
           
        }
        else
        {
            PlayerPrefs.SetFloat(goldName, gemCollectedCount);
        }
    }
}


