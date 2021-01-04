using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector
{
    private const int tagWeightBaseValue = 10;

    private GameDirectorAccount GameDirectorAccount
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

    private GameDirectorRun GameDirectorRun
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
        if (Constants.GameDirectorTestPrints)
        {
            Debug.Log($"Game Director registering being offered one of {optionOne.GetBaseName()} {optionTwo.GetBaseName()} {optionThree.GetBaseName()}.");
            if (cardChoice == null)
            {
                Debug.Log("Game Director registering not taking any option");
            }
            else
            {
                Debug.Log($"Game Director registering taking {cardChoice.GetBaseName()}");
            }
        }
        GameDirectorAccount.RecordCardChoice(cardChoice, optionOne, optionTwo, optionThree);
        GameDirectorRun.RecordCardChoice(cardChoice, optionOne, optionTwo, optionThree);
    }
    
    public void RecordCardSingleChoice(in GameCard cardOption, bool taken)
    {
        if (Constants.GameDirectorTestPrints)
        {
            Debug.Log($"Game Director registering being offered {cardOption.GetBaseName()} and {taken} value for taking it.");
        }
        GameDirectorAccount.RecordCardSingleChoice(cardOption, taken);
        GameDirectorRun.RecordCardSingleChoice(cardOption, taken);
    }

    public void RecordCardDuplication(in GameCard cardDuplicated)
    {
        if (Constants.GameDirectorTestPrints)
        {
            Debug.Log($"Game Director registering duplicating {cardDuplicated.GetBaseName()}.");
        }
        GameDirectorAccount.RecordCardDuplication(cardDuplicated);
        GameDirectorRun.RecordCardDuplication(cardDuplicated);
    }

    public void RecordCardTransformation(in GameCard cardTransformed, in GameCard cardReceived)
    {
        if (Constants.GameDirectorTestPrints)
        {
            Debug.Log($"Game Director registering transforming {cardTransformed.GetBaseName()} into {cardReceived.GetBaseName()}.");
        }
        GameDirectorAccount.RecordCardTransformation(cardTransformed, cardReceived);
        GameDirectorRun.RecordCardTransformation(cardTransformed, cardReceived);
    }

    public void RecordCardRemoval(in GameCard cardRemoved)
    {
        if (Constants.GameDirectorTestPrints)
        {
            Debug.Log($"Game Director registering removing {cardRemoved.GetBaseName()}.");
        }
        GameDirectorAccount.RecordCardRemoval(cardRemoved);
        GameDirectorRun.RecordCardRemoval(cardRemoved);
    }

    public void RecordCardStarter(in GameCard cardStarter)
    {
        GameDirectorRun.RecordCardStarter(cardStarter);
    }

    public void RecordCardChaosGiven(in GameCard chaosCard)
    {
        GameDirectorAccount.RecordCardChaosGiven(chaosCard);
        GameDirectorRun.RecordCardChaosGiven(chaosCard);
    }

    public void RecordCardUnlock(in GameCard cardUnlocked)
    {
        if (Constants.GameDirectorTestPrints)
        {
            Debug.Log($"Game Director registering unlocking {cardUnlocked.GetBaseName()}.");
        }
        GameDirectorAccount.RecordCardUnlock(cardUnlocked);
    }

    public void RecordRelicChoice(in GameRelic relicChoice, in GameRelic optionOne, in GameRelic optionTwo)
    {
        if (Constants.GameDirectorTestPrints)
        {
            Debug.Log($"Game Director registering being offered one of {optionOne.GetBaseName()} {optionTwo.GetBaseName()}.");
            if (relicChoice == null)
            {
                Debug.Log("Game Director registering not taking any option");
            }
            else
            {
                Debug.Log($"Game Director registering taking {relicChoice.GetBaseName()}");
            }
        }
        GameDirectorAccount.RecordRelicChoice(relicChoice, optionOne, optionTwo);
        GameDirectorRun.RecordRelicChoice(relicChoice, optionOne, optionTwo);
    }

    public void RecordRelicSingleChoice(in GameRelic relicOption, bool taken)
    {
        if (Constants.GameDirectorTestPrints)
        {
            Debug.Log($"Game Director registering being offered {relicOption.GetBaseName()} and {taken} value for taking it.");
        }
        GameDirectorAccount.RecordRelicSingleChoice(relicOption, taken);
        GameDirectorRun.RecordRelicSingleChoice(relicOption, taken);
    }

    public void RecordRelicStarter(in GameRelic relicStarter)
    {
        GameDirectorRun.RecordRelicStarter(relicStarter);
    }

    public void RecordRelicUnlock(in GameRelic relicUnlocked)
    {
        if (Constants.GameDirectorTestPrints)
        {
            Debug.Log($"Game Director registering unlocking {relicUnlocked.GetBaseName()}.");
        }
        GameDirectorAccount.RecordRelicUnlock(relicUnlocked);
    }

    public int GetTagValueFor(GameElementBase checkElement)
    {
        return tagWeightBaseValue + GameDirectorAccount.GetTagValueFor(checkElement) + GameDirectorRun.GetTagValueFor(checkElement);
    }

    public void SaveGameDirectorData()
    {
        Files.ExportGameDirectorAccountData(GameDirectorAccount);
        Files.ExportGameDirectorRunData(GameDirectorRun);
    }
}
