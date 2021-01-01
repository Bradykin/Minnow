using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIConfirmationCancel : UIElementBase
    , IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        UIConfirmationController.Instance.Cancel();
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
