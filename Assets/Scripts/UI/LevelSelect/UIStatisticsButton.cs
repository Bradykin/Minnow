using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIStatisticsButton : UIElementBase
        , IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        //nmartino - Use this to implement statistics button
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
