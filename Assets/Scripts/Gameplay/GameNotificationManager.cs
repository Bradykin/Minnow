using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardChoice : int
{
    First,
    Second,
    Third,
    None // = 3
}

public enum RelicChoice : int
{
    First,
    Second,
    None // = 3
}


public static class GameNotificationManager
{
    public readonly static AnalyticsManager AnalyticsManager = new AnalyticsManager();
    public readonly static GameDirector GameDirector = new GameDirector();

    public static void RecordCardChoice(in CardChoice cardChoice, in GameCard optionOne, in GameCard optionTwo, in GameCard optionThree)
    {
        AnalyticsManager.RecordCardChoice(cardChoice, optionOne, optionTwo, optionThree);
        GameDirector.RecordCardChoice(cardChoice, optionOne, optionTwo, optionThree);
    }

    public static void RecordRelicChoice(in RelicChoice relicChoice, in GameRelic optionOne, in GameRelic optionTwo)
    {
        AnalyticsManager.RecordRelicChoice(relicChoice, optionOne, optionTwo);
        GameDirector.RecordRelicChoice(relicChoice, optionOne, optionTwo);
    }

    public static void SaveGameDirectorData()
    {
        GameDirector.SaveGameDirectorData();
    }
}
