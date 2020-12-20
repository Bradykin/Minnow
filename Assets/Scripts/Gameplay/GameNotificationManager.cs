using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameNotificationManager
{
    public readonly static AnalyticsManager AnalyticsManager = new AnalyticsManager();
    public readonly static GameDirector GameDirector = new GameDirector();

    public static void RecordCardChoice(in GameCard cardChoice, in GameCard optionOne, in GameCard optionTwo, in GameCard optionThree)
    {
        AnalyticsManager.RecordCardChoice(cardChoice, optionOne, optionTwo, optionThree);
        GameDirector.RecordCardChoice(cardChoice, optionOne, optionTwo, optionThree);
    }

    public static void RecordRelicChoice(in GameRelic relicChoice, in GameRelic optionOne, in GameRelic optionTwo)
    {
        AnalyticsManager.RecordRelicChoice(relicChoice, optionOne, optionTwo);
        GameDirector.RecordRelicChoice(relicChoice, optionOne, optionTwo);
    }

    public static void SaveGameDirectorData()
    {
        GameDirector.SaveGameDirectorData();
    }
}
