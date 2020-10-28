﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UISettingsFollowEnemyButton : UIElementBase
        , IPointerClickHandler
{
    public Text m_followText;

    void Start()
    {
        SetFollowText();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GlobalSettings.m_followEnemy = !GlobalSettings.m_followEnemy;

        SetFollowText();

        AudioSFXController.Instance.PlaySFX(AudioHelper.UIClick);
    }

    public override void HandleTooltip()
    {
        //Left as stub
    }

    private void SetFollowText()
    {
        if (GlobalSettings.m_followEnemy)
        {
            m_followText.text = "Watch Enemy Turn";
        }
        else
        {
            m_followText.text = "Instant Enemy Turn";
        }
    }
}