﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIFeedbackButton : UIElementBase
        , IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);

        //nmartino - Use this to implement feedback button
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }
}
