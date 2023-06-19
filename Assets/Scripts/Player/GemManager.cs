using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Unity.VisualScripting;

public class GemManager : SingletonBase<GemManager>
{

    public static event Action<AudioType, bool> OnGemDropSound;
    public static event Action OnCalculateGoldAmountOnDrop;
    public static event Action<GameObject> OnGameobjectEnqueueHandler;

    [SerializeField] internal List<Transform> gemList = new List<Transform>();
  
  
    private Transform gemHolder;
    private bool state = false;

    private void OnEnable()
    {
        SellAreaInteraction.OnPlayerStartDropGem += DropGem;
        SellAreaInteraction.OnPlayerStopDropGem += OnDropExitHandler;
        GemInteraction.OnAddGemToStack += StackGem;
    }
    protected override void Awake()
    {
       
        gemHolder = GameObject.FindGameObjectWithTag("GemParent").transform;
        gemList.Add(gemHolder);
        base.Awake();
    }

  

    private void StackGem(Transform _itemToAdd)
    {
        _itemToAdd.DOKill(true);
       
        _itemToAdd.DOJump(gemHolder.position +
        new Vector3(0,  gemList.Count, 0), 0.5f, 1, 0.1f).OnComplete(
         () =>
         {

             _itemToAdd.SetParent(gemHolder, true);
             _itemToAdd.localPosition = new Vector3(0, 1f * gemList.Count, 0);
             _itemToAdd.localRotation = Quaternion.Euler(-90f, 0f, 0f);
             gemList.Add(_itemToAdd.transform);
         }
         );


    }
    private void OnDropExitHandler()
    {
        state = false;
       
    }
    private void DropGem(Transform dropPosition)
    {
        OnCalculateGoldAmountOnDrop?.Invoke();
        StartCoroutine(DropGemCorotuine(dropPosition));
    }
  
    private IEnumerator DropGemCorotuine(Transform dropPosition)
    {
        state = true;
        int counter = gemList.Count - 1;
        for (int i = counter; i >= 1; i--)
        {

            OnGemDropSound?.Invoke(AudioType.DropSound, state);
            gemList.ElementAt(i).parent = dropPosition;
            gemList[i].DOLocalJump(new Vector3(dropPosition.localPosition.x, dropPosition.localPosition.y - 0.3f,
            dropPosition.localPosition.z), 2, 1, 0.1f).SetEase(Ease.InOutBounce);
            StartCoroutine(DisableGem(gemList[i]));
            gemList[i].gameObject.GetComponent<BoxCollider>().isTrigger = false;
            gemList.RemoveAt(i);
         
            if (!state)
            {
                break;
            }

            yield return new WaitForSeconds(0.1f);
          

        }

    } 

   
    private IEnumerator DisableGem(Transform list)
    {
        yield return new WaitForSeconds(0.1f);
        list.gameObject.transform.DOScale(Vector3.zero,0.1f);
        OnGameobjectEnqueueHandler?.Invoke(list.gameObject);
        OnGemDropSound?.Invoke(AudioType.DropSound, false);

    }

    private void OnDisable()
    {
        GemInteraction.OnAddGemToStack -= StackGem;
        SellAreaInteraction.OnPlayerStartDropGem -= DropGem;
        SellAreaInteraction.OnPlayerStopDropGem -= OnDropExitHandler;
    }
}
