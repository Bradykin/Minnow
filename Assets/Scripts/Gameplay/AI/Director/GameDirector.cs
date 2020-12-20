using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector
{
    public GameDirectorAccount GameDirectorAccount
    {
        get
        {
            if (m_gameDirectorAccount == null)
            {
                m_gameDirectorAccount = Files.ImportGameDirectorAccountData();
            }
            return m_gameDirectorAccount;
        }
        set
        {
            m_gameDirectorAccount = value;
        }
    }
    private GameDirectorAccount m_gameDirectorAccount;

    public GameDirectorRun GameDirectorRun
    {
        get
        {
            if (m_gameDirectorRun == null)
            {
                m_gameDirectorRun = Files.ImportGameDirectorRunData();
            }
            return m_gameDirectorRun;
        }
        set
        {
            m_gameDirectorRun = value;
        }
    }
    private GameDirectorRun m_gameDirectorRun;

    public void RecordCardChoice(in GameCard cardChoice, in GameCard optionOne, in GameCard optionTwo, in GameCard optionThree)
    {
        GameDirectorAccount.RecordCardChoice(cardChoice, optionOne, optionTwo, optionThree);
    }

    public void RecordRelicChoice(in GameRelic relicChoice, in GameRelic optionOne, in GameRelic optionTwo)
    {
        GameDirectorAccount.RecordRelicChoice(relicChoice, optionOne, optionTwo);
    }

    public void SaveGameDirectorData()
    {
        Files.ExportGameDirectorAccountData(GameDirectorAccount);
        Files.ExportGameDirectorRunData(GameDirectorRun);
    }
}
