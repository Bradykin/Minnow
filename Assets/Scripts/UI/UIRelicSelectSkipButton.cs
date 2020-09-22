using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIRelicSelectSkipButton : WorldElementBase
    , IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        UIRelicSelectController.Instance.SkipSelection();
        m_tintImage.color = UIHelper.GetDefaultTintColor();
        Globals.m_selectedCard = null;
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
