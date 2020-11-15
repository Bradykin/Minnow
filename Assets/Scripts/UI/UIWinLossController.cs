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

    public void Init(RunEndType runEndType)
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

        GameController gameController = WorldController.Instance.m_gameController;

        bool isVictory = runEndType == RunEndType.Win;
        bool isFirstChaosClear = !PlayerDataManager.IsChaosLevelAchieved(gameController.GetCurMap().m_id, Globals.m_curChaos);

        m_reputationText.text = "Reputation Gained Total: " + gameController.GetRunExperienceNum(isVictory, isFirstChaosClear) + "\n" +
            "\tBase - " + gameController.GetBaseExpNum() + "\n" +
            "\tElite Kills - " + gameController.GetEliteExpNum() + "\n" +
            "\tEnemy Kills - " + gameController.GetKillExpNum() + "\n" +
            "\tEvent Discovery - " + gameController.GetEventExpNum();

        if (isVictory)
        {
            m_reputationText.text += "\n\tVictory - " + gameController.GetVictoryNum();
            
            if (isFirstChaosClear)
            {
                m_reputationText.text += "\n\tFirst " + gameController.GetCurMap().GetBaseName() + " Chaos " + Globals.m_curChaos + " Clear - " + gameController.GetFirstChaosNum();
            }
        }

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
