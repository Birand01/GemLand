using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemInfoButton : ButtonBase
{
    public static event Action<CanvasType, bool> OnShowGemInfoPanel;
    protected override void OnButtonClickEvent()
    {
       OnShowGemInfoPanel?.Invoke(CanvasType.ScrollBar, true);
    }
}
