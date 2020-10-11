using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameCardFactory
{
    private static List<GameCard> m_cards = new List<GameCard>();
    private static List<GameCard> m_unitCards = new List<GameCard>();
    private static List<GameCard> m_enemyCards = new List<GameCard>();
    private static List<GameCard> m_standardCards = new List<GameCard>();
    private static List<GameCard> m_standardSpellCards = new List<GameCard>();
    private static List<GameCard> m_standardUnitCards = new List<GameCard>();

    private static List<GameCard> m_rareCards = new List<GameCard>();
    private static List<GameCard> m_uncommonCards = new List<GameCard>();
    private static List<GameCard> m_commonCards = new List<GameCard>();

    private static List<GameCard> m_rareUnitCards = new List<GameCard>();
    private static List<GameCard> m_uncommonUnitCards = new List<GameCard>();
    private static List<GameCard> m_commonUnitCards = new List<GameCard>();

    private static List<GameCard> m_rareSpellCards = new List<GameCard>();
    private static List<GameCard> m_uncommonSpellCards = new List<GameCard>();
    private static List<GameCard> m_commonSpellCards = new List<GameCard>();

    public static void Init(List<GameCard> cards)
    {
        m_cards = cards;

        //Unit Cards
        m_cards.Add(new ContentZombieCard());

        //Enemy Cards
        m_enemyCards.Add(new ContentAngryBirdEnemyCard());
        m_enemyCards.Add(new ContentDarkWarriorEnemyCard());
        m_enemyCards.Add(new ContentLichEnemyCard());
        m_enemyCards.Add(new ContentLizardmanEnemyCard());
        m_enemyCards.Add(new ContentMobolaEnemyCard());
        m_enemyCards.Add(new ContentOrcEnemyCard());
        m_enemyCards.Add(new ContentOrcShamanEnemyCard());
        m_enemyCards.Add(new ContentSiegebreakerCard());
        m_enemyCards.Add(new ContentShadeEnemyCard());
        m_enemyCards.Add(new ContentSlimeEnemyCard());
        m_enemyCards.Add(new ContentSnakeEnemyCard());
        m_enemyCards.Add(new ContentSpinnerEnemyCard());
        m_enemyCards.Add(new ContentToadEnemyCard());
        m_enemyCards.Add(new ContentWerewolfEnemyCard());
        m_enemyCards.Add(new ContentYetiEnemyCard());
        m_enemyCards.Add(new ContentZombieEnemyCard());


        for (int i = 0; i < m_cards.Count; i++)
        {
            bool isUnit = m_cards[i] is GameUnitCard;
            if (isUnit)
            {
                m_unitCards.Add(m_cards[i]);
            }

            if (m_cards[i].m_rarity == GameElementBase.GameRarity.Common 
                || m_cards[i].m_rarity == GameElementBase.GameRarity.Uncommon 
                || m_cards[i].m_rarity == GameElementBase.GameRarity.Rare)
            {
                m_standardCards.Add(m_cards[i]);

                if (isUnit)
                {
                    m_standardUnitCards.Add(m_cards[i]);
                    if (m_cards[i].m_rarity == GameElementBase.GameRarity.Common)
                    {
                        m_commonUnitCards.Add(m_cards[i]);
                    }
                    else if (m_cards[i].m_rarity == GameElementBase.GameRarity.Uncommon)
                    {
                        m_uncommonUnitCards.Add(m_cards[i]);
                    }
                    else if (m_cards[i].m_rarity == GameElementBase.GameRarity.Rare)
                    {
                        m_rareUnitCards.Add(m_cards[i]);
                    }
                }
                else
                {
                    m_standardSpellCards.Add(m_cards[i]);

                    if (m_cards[i].m_rarity == GameElementBase.GameRarity.Common)
                    {
                        m_commonSpellCards.Add(m_cards[i]);
                    }
                    else if (m_cards[i].m_rarity == GameElementBase.GameRarity.Uncommon)
                    {
                        m_uncommonSpellCards.Add(m_cards[i]);
                    }
                    else if (m_cards[i].m_rarity == GameElementBase.GameRarity.Rare)
                    {
                        m_rareSpellCards.Add(m_cards[i]);
                    }
                }
            }

            if (m_cards[i].m_rarity == GameElementBase.GameRarity.Common)
            {
                m_commonCards.Add(m_cards[i]);
            }
            else if (m_cards[i].m_rarity == GameElementBase.GameRarity.Uncommon)
            {
                m_uncommonCards.Add(m_cards[i]);
            }
            else if (m_cards[i].m_rarity == GameElementBase.GameRarity.Rare)
            {
                m_rareCards.Add(m_cards[i]);
            }
        }
    }

    public static GameCard GetRandomStandardCard(List<GameCard> exclusionList = null)
    {
        List<GameCard> checkList = GetCheckList(m_commonCards, m_uncommonCards, m_rareCards);

        return GetCardFromList(checkList, exclusionList);
    }

    public static GameCard GetRandomStandardUnitCard(List<GameCard> exclusionList = null)
    {
        List<GameCard> checkList = GetCheckList(m_commonUnitCards, m_uncommonUnitCards, m_rareUnitCards);

        return GetCardFromList(checkList, exclusionList);
    }

    public static GameCard GetRandomStandardSpellCard(List<GameCard> exclusionList = null)
    {
        List<GameCard> checkList = GetCheckList(m_commonSpellCards, m_uncommonSpellCards, m_rareSpellCards);

        return GetCardFromList(checkList, exclusionList);
    }

    private static List<GameCard> GetCheckList(List<GameCard> commonCards, List<GameCard> uncommonCards, List<GameCard> rareCards)
    {
        int chanceForUncommon = Constants.PercentChanceForUncommonCard;
        int chanceForRare = Constants.PercentChanceForRareCard;

        if (GameHelper.PercentChanceRoll(chanceForUncommon))
        {
            return uncommonCards;
        }
        else if (GameHelper.PercentChanceRoll(chanceForRare))
        {
            return rareCards;
        }
        else
        {
            return commonCards;
        }
    }


    public static GameCard GetCardClone(GameCard toClone)
    {
        return (GameCard)Activator.CreateInstance(toClone.GetType());
    }

    public static GameCard GetCardDup(GameCard toClone)
    {
        GameCard clone = (GameCard)Activator.CreateInstance(toClone.GetType());

        if (toClone is GameUnitCard && clone is GameUnitCard)
        {
            GameUnit toCloneUnit = ((GameUnitCard)toClone).GetUnit();
            GameUnit cloneUnit = ((GameUnitCard)clone).GetUnit();

            cloneUnit.CopyOff(toCloneUnit);
        }

        return clone;
    }

    private static GameCard GetCardFromList(List<GameCard> list, List<GameCard> exclusionList)
    {
        //Fill the list by removing anything that was excluded.
        List<GameCard> finalList = new List<GameCard>();
        if (exclusionList == null)
        {
            finalList = list;
        }
        else
        {
            for (int i = 0; i < list.Count; i++)
            {
                bool hasInExclusion = false;
                for (int c = 0; c < exclusionList.Count; c++)
                {
                    if (exclusionList[c].m_name == list[i].m_name)
                    {
                        hasInExclusion = true;
                    }
                }

                if (!hasInExclusion)
                {
                    finalList.Add(list[i]);
                }
            }
        }

        //Use the tag weights + randomization to get the correct card here.
        int totalWeight = 0;
        for (int i = 0; i < finalList.Count; i++)
        {
            int tagWeight = GameTag.GetTagValueFor(finalList[i]);
            if (tagWeight > 0)
            {
                finalList[i].m_storedTagWeight = tagWeight + totalWeight;
                totalWeight += tagWeight;
            }
            else
            {
                finalList.RemoveAt(i);
                i--;
            }
        }

        int r = UnityEngine.Random.Range(0, totalWeight);

        for (int i = 0; i < finalList.Count; i++)
        {
            if (r <= finalList[i].m_storedTagWeight)
            {
                return GetCardClone(finalList[i]);
            }
        }

        Debug.LogError("Failed to find any card when trying get one (likely caused by tag weighting issues).");
        return null;
    }

    public static GameUnitCard GetCardFromUnit(GameUnit unit)
    {
        if (unit.GetTeam() == Team.Player)
        {
            for (int i = 0; i < m_unitCards.Count; i++)
            {
                GameUnitCard unitCard = (GameUnitCard)m_unitCards[i];

                if (unitCard.GetUnit().m_name == unit.m_name)
                {
                    GameUnitCard cardClone = (GameUnitCard)GetCardClone(m_unitCards[i]);
                    cardClone.SetUnit(unit);

                    return cardClone;
                }
            }
        }
        else
        {
            for (int i = 0; i < m_enemyCards.Count; i++)
            {
                GameUnitCard unitCard = (GameUnitCard)m_enemyCards[i];

                if (unitCard.GetUnit().m_name == unit.m_name)
                {
                    GameUnitCard cardClone = (GameUnitCard)GetCardClone(m_enemyCards[i]);
                    cardClone.SetUnit(unit);

                    return cardClone;
                }
            }
        }

        return null;
    }
}

