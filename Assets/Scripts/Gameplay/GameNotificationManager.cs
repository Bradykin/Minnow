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

    public static void RecordCardSingleChoice(in GameCard cardOption, bool taken)
    {
        AnalyticsManager.RecordCardSingleChoice(cardOption, taken);
        GameDirector.RecordCardSingleChoice(cardOption, taken);
    }

    public static void RecordCardDuplication(in GameCard cardDuplicated)
    {
        AnalyticsManager.RecordCardDuplication(cardDuplicated);
        GameDirector.RecordCardDuplication(cardDuplicated);
    }

    public static void RecordCardTransformation(in GameCard cardTransformed, in GameCard cardReceived)
    {
        AnalyticsManager.RecordCardTransformation(cardTransformed, cardReceived);
        GameDirector.RecordCardTransformation(cardTransformed, cardReceived);
    }

    public static void RecordCardRemoval(in GameCard cardRemoved)
    {
        AnalyticsManager.RecordCardRemoval(cardRemoved);
        GameDirector.RecordCardRemoval(cardRemoved);
    }

    public static void RecordCardUnlock(in GameCard cardUnlocked)
    {
        GameDirector.RecordCardUnlock(cardUnlocked);
    }

    public static void RecordRelicChoice(in GameRelic relicChoice, in GameRelic optionOne, in GameRelic optionTwo)
    {
        AnalyticsManager.RecordRelicChoice(relicChoice, optionOne, optionTwo);
        GameDirector.RecordRelicChoice(relicChoice, optionOne, optionTwo);
    }

    public static void RecordRelicSingleChoice(in GameRelic relicOption, bool taken)
    {
        AnalyticsManager.RecordRelicSingleChoice(relicOption, taken);
        GameDirector.RecordRelicSingleChoice(relicOption, taken);
    }

    public static void RecordRelicUnlock(in GameRelic relicUnlocked)
    {
        GameDirector.RecordRelicUnlock(relicUnlocked);
    }

    public static void SaveGameDirectorData()
    {
        GameDirector.SaveGameDirectorData();
    }
}
