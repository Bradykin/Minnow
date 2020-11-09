﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;

public class UIWinLossController : Singleton<UIWinLossController>
{
    public GameObject m_holder;

    public Text m_winLossText;
    public Text m_reputationText;

    public void Init(RunEndType runEndType, int reputationGained)
    {
        UIWorldElementNotificationController.Instance.ClearAllWorldElementNotifications();

        if (runEndType == RunEndType.Loss ||
            runEndType == RunEndType.Quit)
        {
            m_winLossText.text = "Defeat...";
        }
        else
        {
            m_winLossText.text = "Victory!";
        }

        m_reputationText.text = "Reputation Gained: " + reputationGained;

        m_holder.SetActive(true);
    }

    public void End()
    {
        GameHelper.ReturnToLevelSelectFromLevelScene();

        m_holder.SetActive(false);
    }

    public bool IsActive()
    {
        return m_holder.activeSelf;
    }
}
