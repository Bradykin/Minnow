using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIGameMenuButton : UIElementBase
        , IPointerClickHandler
{
    public GameObject m_gameMenu;

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        m_gameMenu.SetActive(!m_gameMenu.activeSelf);
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
