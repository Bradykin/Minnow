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
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        m_settingsMenu.SetActive(!m_settingsMenu.activeSelf);
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
