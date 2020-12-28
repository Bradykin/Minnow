using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;
using UnityEngine.UI;
using TMPro;

public class UIWinLossController : Singleton<UIWinLossController>
{
    public GameObject m_holder;

    public TMP_Text m_winLossText;
    public TMP_Text m_reputationText;
    public TMP_Text m_hintText;

    public void Init(RunEndType runEndType)
    {
        UIWorldElementNotificationController.Instance.ClearAllWorldElementNotifications();

        if (runEndType == RunEndType.Loss ||
            runEndType == RunEndType.Quit)
        {
            m_winLossText.text = "Defeat...";
            m_hintText.text = UIHelper.GetHintText();
        }
        else
        {
            m_winLossText.text = "Victory!";
            m_hintText.text = "";
        }

        m_holder.SetActive(true);

        if (Globals.m_curChaos == 0 && runEndType != RunEndType.Win)
        {
            m_reputationText.text = "";
            return;
        }

        GameController gameController = WorldController.Instance.m_gameController;

        bool isVictory = runEndType == RunEndType.Win;
        bool isFirstChaosClear = !PlayerDataManager.IsChaosLevelAchieved(gameController.GetCurMap().m_id, Globals.m_curChaos);

        m_reputationText.text = "Reputation Gained Total: " + gameController.GetRunExperienceNum(isVictory, isFirstChaosClear) + "\n" +
            "\tElite Kills - " + gameController.GetEliteExpNum() + "\n" +
            "\tEnemy Kills - " + gameController.GetKillExpNum() + "\n" +
            "\tEvent Discovery - " + gameController.GetEventExpNum();

        if (isVictory)
        {
            m_reputationText.text += "\n\tVictory - " + gameController.GetVictoryNum();
            
            if (isFirstChaosClear && Globals.m_curChaos > 0)
            {
                m_reputationText.text += "\n\tFirst " + gameController.GetCurMap().GetBaseName() + " Chaos " + Globals.m_curChaos + " Clear - " + gameController.GetFirstChaosNum();
            }
            else if (isFirstChaosClear && Globals.m_curChaos == 0) //This is Lakeside bonus clear
            {
                m_reputationText.text += "\n\tFirst Map Clear " + gameController.GetFirstChaosNum();
            }
        }
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
