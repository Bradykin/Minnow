using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UINewVersionNotificationResume : UIElementBase
        , IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        UINewVersionNotificationController.Instance.Close();
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
