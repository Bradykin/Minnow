using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISettingsSaveAndCloseButton : UIElementBase
        , IPointerClickHandler
{
    public GameObject m_settingsMenu;

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        //TODO: alex - Save all settings data here
        m_settingsMenu.SetActive(false);
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
