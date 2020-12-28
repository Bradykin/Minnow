using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIGameMenuConcedeButton : UIElementBase
        , IPointerClickHandler
{
    public GameObject m_gameMenu;
    public GameObject m_holder;

    void Update()
    {
        m_holder.SetActive(GameHelper.IsInGame());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        m_gameMenu.SetActive(false);
        GameHelper.EndLevel(RunEndType.Quit);
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
