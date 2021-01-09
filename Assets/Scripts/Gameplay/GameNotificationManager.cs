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

    public static void RecordCardStarter(in GameCard cardStarter)
    {
        AnalyticsManager.RecordCardStarter(cardStarter);
        GameDirector.RecordCardStarter(cardStarter);
    }

    public static void RecordCardChaosGiven(in GameCard chaosCard)
    {
        AnalyticsManager.RecordCardChaosGiven(chaosCard);
        GameDirector.RecordCardChaosGiven(chaosCard);
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

    public static void EndLevel(in RunEndType endType)
    {
        AnalyticsManager.EndLevel(endType);
    }

    public static void RecordGainGold(in int goldValue)
    {
        AnalyticsManager.RecordGainGold(goldValue);
    }

    public static void RecordEliteKill()
    {
        AnalyticsManager.RecordEliteKill();
    }

    public static void RecordBuilding(GameBuildingBase building)
    {
        AnalyticsManager.RecordBuilding(building);
    }

    public static void RecordAction(GameActionIntermission action)
    {
        AnalyticsManager.RecordAction(action);
    }

    public static void RecordRelicStarter(in GameRelic relicStarter)
    {
        AnalyticsManager.RecordRelicStarter(relicStarter);
        GameDirector.RecordRelicStarter(relicStarter);
    }

    public static void RecordRelicUnlock(in GameRelic relicUnlocked)
    {
        GameDirector.RecordRelicUnlock(relicUnlocked);
    }

    public static void RecordEventInteracted(in GameEvent gameEvent, bool isFirstOption)
    {
        AnalyticsManager.RecordEventInteracted(gameEvent, isFirstOption);
    }

    public static void SaveGameDirectorData()
    {
        GameDirector.SaveGameDirectorData();
    }

    public static void ShowCardData()
    {
        AnalyticsManager.ShowCardData();
    }
}
