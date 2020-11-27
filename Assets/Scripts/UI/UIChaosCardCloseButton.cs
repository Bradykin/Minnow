using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIChaosCardCloseButton : UIElementBase
    , IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        UIChaosCardPanelController.Instance.ClosePanel();
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
