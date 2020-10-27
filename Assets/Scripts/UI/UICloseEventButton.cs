using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICloseEventButton : UIElementBase
    , IPointerClickHandler
{
    public override void HandleTooltip()
    {
        //Left as stub.
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIHUDNotificationController.Instance.CloseCurrentEvent();
    }
}
