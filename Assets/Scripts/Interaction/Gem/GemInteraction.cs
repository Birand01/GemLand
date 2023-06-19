using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GemInteraction : InteractionBase
{
    public static event Action<Transform> OnAddGemToStack;
    public static event Action<AudioType, bool> OnPlayerCollectGemSound;
    public static event Action<int,GameObject> OnGemTouchIndex;
    public static event Action<int,string> OnUpdateCollectedCountList;

    [SerializeField] internal GemSO gemSO;
    private float collectableScaleValue;
    internal float collectedGemScale;
   


    protected override void Awake()
    {
        collectableScaleValue = 0.25f;     
        base.Awake();
    }

    protected override void OnTriggerEnterAction(Collider collider)
    {
        
        if (CheckScale())
        {
            gemSO.collectedCount++;
            OnUpdateCollectedCountList?.Invoke(gemSO.collectedCount,gemSO.gemName);
            collectedGemScale = transform.localScale.x;
           
            CalculateGemPrice(gemSO.initialPrice, collectedGemScale);
             
            OnAddGemToStack?.Invoke(this.transform);
            OnGemTouchIndex?.Invoke(this.transform.parent.gameObject.GetComponent<CollectType>().order,
                this.gameObject.transform.parent.gameObject);
            OnPlayerCollectGemSound?.Invoke(AudioType.CollectSound, true);
          
        }
        else
        {
            Debug.Log("Gems are not collectable due to scale factor");
        }
       
    }
  
    internal float CalculateGemPrice(float initialPrice,float scaleFactor)
    {
      return (initialPrice + (scaleFactor * 100f));
    }
   
    private bool CheckScale()
    {
        if(this.transform.localScale.x>collectableScaleValue || this.transform.localScale.y > collectableScaleValue
            || this.transform.localScale.z > collectableScaleValue)
        {
            return true;
        }
        else
        { return false; }
    }
}
