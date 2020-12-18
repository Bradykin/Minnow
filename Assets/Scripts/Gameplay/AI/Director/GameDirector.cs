using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector
{
    public GameDirectorStatistics GameDirectorStatistics
    {
        get
        {
            if (m_gameDirectorStatistics == null)
            {
                m_gameDirectorStatistics = Files.ImportGameDirectorStatisticsData();
            }
            return m_gameDirectorStatistics;
        }
        set
        {
            m_gameDirectorStatistics = value;
        }
    }
    private GameDirectorStatistics m_gameDirectorStatistics;

    public void RecordCardChoice(in CardChoice cardChoice, in GameCard optionOne, in GameCard optionTwo, in GameCard optionThree)
    {

    }

    public void RecordRelicChoice(in RelicChoice relicChoice, in GameRelic optionOne, in GameRelic optionTwo)
    {

    }

    public void SaveGameDirectorData()
    {
        Files.ExportGameDirectorData(GameDirectorStatistics);
    }
}
