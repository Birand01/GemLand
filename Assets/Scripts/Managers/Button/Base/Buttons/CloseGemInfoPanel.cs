using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGemInfoPanel : ButtonBase
{
    public static event Action<CanvasType, bool> OnCloseGemInfoPanel;
    protected override void OnButtonClickEvent()
    {
       OnCloseGemInfoPanel?.Invoke(CanvasType.ScrollBar, false);
    }

    
}
