using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SerializeField]
public class GameDirectorAccount
{
    private const int tagWeightMaximums = 10;
    private const int weightDecreaseAcceptCard = 10;
    private const int weightDecreaseDeclineAllOptions = 7;
    private const int weightDecreaseDeclineCard = 3;
    private const int weightIncreaseNotOfferedCard = 1;
    
    public List<GameDirectorCardWeight> cardWeights = new List<GameDirectorCardWeight>();
    public List<GameDirectorRelicWeight> relicWeights = new List<GameDirectorRelicWeight>();

    public void RecordCardChoice(in GameCard cardChoice, in GameCard optionOne, in GameCard optionTwo, in GameCard optionThree)
    {
        IReadOnlyList<GameCard> affectedCards = GameCardFactory.GetCardListOfTypeAtRarity(optionOne.m_rarity, optionOne is GameUnitCard);

        for (int i = 0; i < affectedCards.Count; i++)
        {
            GameDirectorCardWeight cardWeight = GetCardWeight(affectedCards[i]);
            int curWeight = cardWeight.curWeight;
            if (cardChoice != null && cardWeight.gameCard.GetName() == cardChoice.GetName())
            {
                cardWeight.curWeight -= weightDecreaseAcceptCard;
            }
            else if (cardWeight.gameCard.GetName() == optionOne.GetName() || cardWeight.gameCard.GetName() == optionTwo.GetName() || cardWeight.gameCard.GetName() == optionThree.GetName())
            {
                if (cardChoice != null)
                {
                    cardWeight.curWeight -= weightDecreaseDeclineCard;
                }
                else
                {
                    cardWeight.curWeight -= weightDecreaseDeclineAllOptions;
                }
            }
            else
            {
                cardWeight.curWeight += weightIncreaseNotOfferedCard;
            }
            cardWeight.curWeight = Mathf.Clamp(cardWeight.curWeight, -tagWeightMaximums, tagWeightMaximums);
            //Debug.Log($"{cardWeight.gameCard.GetName()} adjusted from {curWeight} to {cardWeight.curWeight}");
        }
    }

    public void RecordRelicChoice(in GameRelic relicChoice, in GameRelic optionOne, in GameRelic optionTwo)
    {
        IReadOnlyList<GameRelic> affectedRelics = GameRelicFactory.GetRelicListAtRarity(optionOne.m_rarity);

        for (int i = 0; i < affectedRelics.Count; i++)
        {
            GameDirectorRelicWeight relicWeight = GetRelicWeight(affectedRelics[i]);
            int curWeight = relicWeight.curWeight;
            if (relicChoice != null && relicWeight.gameRelic.GetName() == relicChoice.GetName())
            {
                relicWeight.curWeight -= weightDecreaseAcceptCard;
            }
            else if (relicWeight.gameRelic.GetName() == optionOne.GetName() || relicWeight.gameRelic.GetName() == optionTwo.GetName())
            {
                if (relicChoice != null)
                {
                    relicWeight.curWeight -= weightDecreaseDeclineCard;
                }
                else
                {
                    relicWeight.curWeight -= weightDecreaseDeclineAllOptions;
                }
            }
            else
            {
                relicWeight.curWeight += weightIncreaseNotOfferedCard;
            }
            relicWeight.curWeight = Mathf.Clamp(relicWeight.curWeight, -tagWeightMaximums, tagWeightMaximums);
            //Debug.Log($"{relicWeight.gameRelic.GetName()} adjusted from {curWeight} to {relicWeight.curWeight}");
        }
    }

    public GameDirectorCardWeight GetCardWeight(GameCard gameCard)
    {
        if (cardWeights.Any(c => c.gameCard.GetName() == gameCard.GetName()))
        {
            return cardWeights.FirstOrDefault(c => c.gameCard.GetName() == gameCard.GetName());
        }

        GameDirectorCardWeight cardWeight = new GameDirectorCardWeight
        {
            gameCard = GameCardFactory.GetCardClone(gameCard)
        };
        cardWeights.Add(cardWeight);

        return cardWeight;
    }

    public GameDirectorRelicWeight GetRelicWeight(GameRelic gameRelic)
    {
        if (cardWeights.Any(c => c.gameCard.GetName() == gameRelic.GetName()))
        {
            return relicWeights.FirstOrDefault(c => c.gameRelic.GetName() == gameRelic.GetName());
        }

        GameDirectorRelicWeight relicWeight = new GameDirectorRelicWeight
        {
            gameRelic = GameRelicFactory.GetGameRelicClone(gameRelic)
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
