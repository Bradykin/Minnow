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
    private const int weightDecreaseCardChaosGiven = 10;

    private const int weightDecreaseCardDuplicated = 8;
    private const int weightDecreaseCardTransformed = 10;
    private const int weightDecreaseCardTransformReceived = 8;
    private const int weightDecreaseCardRemoved = 10;

    private const int weightDecreaseAcceptRelic = 10;
    private const int weightDecreaseDeclineAllRelicOptions = 7;
    private const int weightDecreaseDeclineRelic = 5;
    private const int weightIncreaseNotOfferedRelic = 1;
    private const int weightDecreaseAcceptSingleRelicOption = 10;
    private const int weightDecreaseDeclineSingleRelicOption = 5;

    public List<GameDirectorCardWeight> cardWeights = new List<GameDirectorCardWeight>();
    public List<GameDirectorRelicWeight> relicWeights = new List<GameDirectorRelicWeight>();

    public void RecordCardChoice(in GameCard cardChoice, in GameCard optionOne, in GameCard optionTwo, in GameCard optionThree)
    {
        IReadOnlyList<GameCard> affectedCards = GameCardFactory.GetCardListOfTypeAtRarity(optionOne.m_rarity, optionOne is GameUnitCard);

        int maximumTagValue = -9999;
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
            maximumTagValue = Mathf.Max(maximumTagValue, cardWeight.curWeight);
            //Debug.Log($"{cardWeight.gameCard.GetBaseName()} adjusted from {curWeight} to {cardWeight.curWeight}");
        }

        if (maximumTagValue <= 0)
        {
            int increaseAmount = Mathf.Abs(maximumTagValue) + 3;
            for (int i = 0; i < affectedCards.Count; i++)
            {
                GameDirectorCardWeight cardWeight = GetCardWeight(affectedCards[i]);
                cardWeight.curWeight = Mathf.Clamp(cardWeight.curWeight + increaseAmount, -tagWeightMaximums, tagWeightMaximums);
            }
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

    public void RecordCardDuplication(in GameCard cardDuplicated)
    {
        GameDirectorCardWeight cardWeight = GetCardWeight(cardDuplicated);

        cardWeight.curWeight -= weightDecreaseCardDuplicated;

        cardWeight.curWeight = Mathf.Clamp(cardWeight.curWeight, -tagWeightMaximums, tagWeightMaximums);
    }

    public void RecordCardTransformation(in GameCard cardTransformed, in GameCard cardReceived)
    {
        GameDirectorCardWeight cardWeightTransformed = GetCardWeight(cardTransformed);
        cardWeightTransformed.curWeight -= weightDecreaseCardTransformed;
        cardWeightTransformed.curWeight = Mathf.Clamp(cardWeightTransformed.curWeight, -tagWeightMaximums, tagWeightMaximums);

        GameDirectorCardWeight cardWeightReceived = GetCardWeight(cardReceived);
        cardWeightReceived.curWeight -= weightDecreaseCardTransformReceived;
        cardWeightReceived.curWeight = Mathf.Clamp(cardWeightReceived.curWeight, -tagWeightMaximums, tagWeightMaximums);
    }

    public void RecordCardRemoval(in GameCard cardRemoved)
    {
        GameDirectorCardWeight cardWeight = GetCardWeight(cardRemoved);

        cardWeight.curWeight -= weightDecreaseCardRemoved;

        cardWeight.curWeight = Mathf.Clamp(cardWeight.curWeight, -tagWeightMaximums, tagWeightMaximums);
    }

    public void RecordCardUnlock(in GameCard cardUnlocked)
    {
        GameDirectorCardWeight cardWeight = GetCardWeight(cardUnlocked);

        cardWeight.curWeight = tagWeightMaximums / 2;
    }

    public void RecordCardChaosGiven(in GameCard chaosCard)
    {
        GameDirectorCardWeight cardWeight = GetCardWeight(chaosCard);

        cardWeight.curWeight -= weightDecreaseCardChaosGiven;

        cardWeight.curWeight = Mathf.Clamp(cardWeight.curWeight, -tagWeightMaximums, tagWeightMaximums);
    }

    public void RecordRelicChoice(in GameRelic relicChoice, in GameRelic optionOne, in GameRelic optionTwo)
    {
        IReadOnlyList<GameRelic> affectedRelics = GameRelicFactory.GetRelicListAtRarity(optionOne.m_rarity);

        int maximumTagValue = -9999;
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
            maximumTagValue = Mathf.Max(maximumTagValue, relicWeight.curWeight);
            //Debug.Log($"{relicWeight.gameRelic.GetBaseName()} adjusted from {curWeight} to {relicWeight.curWeight}");
        }

        if (maximumTagValue <= 0)
        {
            int increaseAmount = Mathf.Abs(maximumTagValue) + 3;
            for (int i = 0; i < affectedRelics.Count; i++)
            {
                GameDirectorRelicWeight relicWeight = GetRelicWeight(affectedRelics[i]);
                relicWeight.curWeight = Mathf.Clamp(relicWeight.curWeight + increaseAmount, -tagWeightMaximums, tagWeightMaximums);
            }
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

        relicWeight.curWeight = tagWeightMaximums / 2;
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
