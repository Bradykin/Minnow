using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIGameMenuSettingsButton : UIElementBase
        , IPointerClickHandler
{
    public GameObject m_settingsMenu;

    public void OnPointerClick(PointerEventData eventData)
    {
        m_settingsMenu.SetActive(true);
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
