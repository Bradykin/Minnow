using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICloseDeckViewButton : WorldElementBase
    , IPointerClickHandler
{
    public override void HandleTooltip()
    {
        //Left as stub.
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Globals.m_inDeckView = false;
    }
}
