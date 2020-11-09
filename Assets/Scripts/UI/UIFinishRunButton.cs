using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIFinishRunButton : UIElementBase
        , IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        UIWinLossController.Instance.End();
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
