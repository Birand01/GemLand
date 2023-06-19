using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellAreaInteraction : InteractionBase
{   
    
    public static event Action<Transform> OnPlayerStartDropGem;
    public static event Action OnPlayerStopDropGem;
    [SerializeField] private float totalGoldAmount;
    protected override void OnTriggerEnterAction(Collider collider)
    {
       
        OnPlayerStartDropGem?.Invoke(this.transform);
    }

    protected override void OnTriggerExitAction(Collider other)
    {
        OnPlayerStopDropGem?.Invoke();
    }
}
