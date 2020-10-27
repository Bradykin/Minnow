using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIGameMenuResumeButton : UIElementBase
        , IPointerClickHandler
{
    public GameObject m_gameMenu;

    public void OnPointerClick(PointerEventData eventData)
    {
        m_gameMenu.SetActive(false);
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
