using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public enum CanvasType
{
    GameUI,
    ScrollBar
}
public class CanvasManager : MonoBehaviour
{
    List<CanvasController> canvasControllerList;
    CanvasController lastActiveCanvas;

    private void OnEnable()
    {
        CloseGemInfoPanel.OnCloseGemInfoPanel += SwitchCanvas;
        GemInfoButton.OnShowGemInfoPanel += SwitchCanvas;
    }
    private void OnDisable()
    {
        GemInfoButton.OnShowGemInfoPanel -= SwitchCanvas;
        CloseGemInfoPanel.OnCloseGemInfoPanel -= SwitchCanvas;

    }
    private void Awake()
    {
        canvasControllerList = GetComponentsInChildren<CanvasController>().ToList();
        canvasControllerList.ForEach(x => x.gameObject.SetActive(false));
        SwitchCanvas(CanvasType.GameUI, true);
    }

    private void SwitchCanvas(CanvasType canvasType, bool state)
    {
        if (lastActiveCanvas != null)
        {
            lastActiveCanvas.gameObject.SetActive(state);
        }
        CanvasController desiredCanvas = canvasControllerList.Find(x => x.canvasType == canvasType);
        if (desiredCanvas != null)
        {
            desiredCanvas.gameObject.SetActive(state);
            lastActiveCanvas = desiredCanvas;
        }
        else
        {
            Debug.LogWarning("The desired canvas was not found");
        }
    }
}
