using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SerializeField]
public class GameDirectorAccount
{
    private const int tagWeightMaximums = 10;

    private const int weightDecreaseAcceptCard = 8;
    private const int weightDecreaseDeclineAllCardOptions = 7;
    private const int weightDecreaseDeclineCard = 5;
    private const int weightIncreaseNotOfferedCard = 1;
    private const int weightDecreaseAcceptSingleCardOption = 8;
    private const int weightDecreaseDeclineSingleCardOption = 5;

    private const int weightDecreaseAcceptRelic = 8;
    private const int weightDecreaseDeclineAllRelicOptions = 7;
    private const int weightDecreaseDeclineRelic = 6;
    private const int weightIncreaseNotOfferedRelic = 1;
    private const int weightDecreaseAcceptSingleRelicOption = 10;
    private const int weightDecreaseDeclineSingleRelicOption = 3;

    public List<GameDirectorCardWeight> cardWeights = new List<GameDirectorCardWeight>();
    public List<GameDirectorRelicWeight> relicWeights = new List<GameDirectorRelicWeight>();

    public void RecordCardChoice(in GameCard cardChoice, in GameCard optionOne, in GameCard optionTwo, in GameCard optionThree)
    {
        IReadOnlyList<GameCard> affectedCards = GameCardFactory.GetCardListOfTypeAtRarity(optionOne.m_rarity, optionOne is GameUnitCard);

        for (int i = 0; i < affectedCards.Count; i++)
        {
            GameDirectorCardWeight cardWeight = GetCardWeight(affectedCards[i]);
            int curWeight = cardWeight.curWeight;
            if (cardChoice != null && cardWeight.gameCardName == cardChoice.GetBaseName())
            {
                cardWeight.curWeight -= weightDecreaseAcceptCard;
            }
            else if (cardWeight.gameCardName == optionOne.GetBaseName() || cardWeight.gameCardName == optionTwo.GetBaseName() || cardWeight.gameCardName == optionThree.GetBaseName())
            {
                if (cardChoice != null)
                {
                    cardWeight.curWeight -= weightDecreaseDeclineCard;
                }
                else
                {
                    cardWeight.curWeight -= weightDecreaseDeclineAllCardOptions;
                }
            }
            else
            {
                cardWeight.curWeight += weightIncreaseNotOfferedCard;
            }
            cardWeight.curWeight = Mathf.Clamp(cardWeight.curWeight, -tagWeightMaximums, tagWeightMaximums);
            //Debug.Log($"{cardWeight.gameCard.GetBaseName()} adjusted from {curWeight} to {cardWeight.curWeight}");
        }
    }

    public void RecordCardSingleChoice(in GameCard cardOption, bool taken)
    {
        GameDirectorCardWeight cardWeight = GetCardWeight(cardOption);

        if (taken)
        {
            cardWeight.curWeight -= weightDecreaseAcceptSingleCardOption;
        }
        else
        {
            cardWeight.curWeight -= weightDecreaseDeclineSingleCardOption;
        }
        cardWeight.curWeight = Mathf.Clamp(cardWeight.curWeight, -tagWeightMaximums, tagWeightMaximums);
    }

    public void RecordCardUnlock(in GameCard cardUnlocked)
    {
        GameDirectorCardWeight cardWeight = GetCardWeight(cardUnlocked);

        cardWeight.curWeight = tagWeightMaximums;
    }

    public void RecordRelicChoice(in GameRelic relicChoice, in GameRelic optionOne, in GameRelic optionTwo)
    {
        IReadOnlyList<GameRelic> affectedRelics = GameRelicFactory.GetRelicListAtRarity(optionOne.m_rarity);

        for (int i = 0; i < affectedRelics.Count; i++)
        {
            GameDirectorRelicWeight relicWeight = GetRelicWeight(affectedRelics[i]);
            int curWeight = relicWeight.curWeight;
            if (relicChoice != null && relicWeight.gameRelicName == relicChoice.GetBaseName())
            {
                relicWeight.curWeight -= weightDecreaseAcceptRelic;
            }
            else if (relicWeight.gameRelicName == optionOne.GetBaseName() || relicWeight.gameRelicName == optionTwo.GetBaseName())
            {
                if (relicChoice != null)
                {
                    relicWeight.curWeight -= weightDecreaseDeclineRelic;
                }
                else
                {
                    relicWeight.curWeight -= weightDecreaseDeclineAllRelicOptions;
                }
            }
            else
            {
                relicWeight.curWeight += weightIncreaseNotOfferedRelic;
            }
            relicWeight.curWeight = Mathf.Clamp(relicWeight.curWeight, -tagWeightMaximums, tagWeightMaximums);
            //Debug.Log($"{relicWeight.gameRelic.GetBaseName()} adjusted from {curWeight} to {relicWeight.curWeight}");
        }
    }

    public void RecordRelicSingleChoice(in GameRelic relicOption, bool taken)
    {
        GameDirectorRelicWeight relicWeight = GetRelicWeight(relicOption);

        if (taken)
        {
            relicWeight.curWeight -= weightDecreaseAcceptSingleRelicOption;
        }
        else
        {
            relicWeight.curWeight -= weightDecreaseDeclineSingleRelicOption;
        }
        relicWeight.curWeight = Mathf.Clamp(relicWeight.curWeight, -tagWeightMaximums, tagWeightMaximums);
    }

    public void RecordRelicUnlock(in GameRelic relicUnlocked)
    {
        GameDirectorRelicWeight relicWeight = GetRelicWeight(relicUnlocked);

        relicWeight.curWeight = tagWeightMaximums;
    }

    public GameDirectorCardWeight GetCardWeight(GameCard gameCard)
    {
        if (cardWeights.Any(c => c.gameCardName == gameCard.GetBaseName()))
        {
            return cardWeights.FirstOrDefault(c => c.gameCardName == gameCard.GetBaseName());
        }

        GameDirectorCardWeight cardWeight = new GameDirectorCardWeight
        {
            gameCardName = gameCard.GetBaseName()
        };
        cardWeights.Add(cardWeight);

        return cardWeight;
    }

    public GameDirectorRelicWeight GetRelicWeight(GameRelic gameRelic)
    {
        if (relicWeights.Any(c => c.gameRelicName == gameRelic.GetBaseName()))
        {
            return relicWeights.FirstOrDefault(c => c.gameRelicName == gameRelic.GetBaseName());
        }

        GameDirectorRelicWeight relicWeight = new GameDirectorRelicWeight
        {
            gameRelicName = gameRelic.GetBaseName()
        };
        relicWeights.Add(relicWeight);

        return relicWeight;
    }

    public int GetTagValueFor(GameElementBase checkElement)
    {
        if (checkElement is GameCard gameCard)
        {
            return GetCardWeight(gameCard).curWeight;
        }
        else if (checkElement is GameRelic gameRelic)
        {
            return GetRelicWeight(gameRelic).curWeight;
        }

        Debug.LogError("GameDirectorAccount does not recognize GameElementBase");
        return 0;
    }
}
