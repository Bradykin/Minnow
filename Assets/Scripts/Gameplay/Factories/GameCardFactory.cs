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

    public static void Init()
    {
        //Unit Cards
        m_cards.Add(new ContentConjuredImpCard());
        m_cards.Add(new ContentCyclopsCard());
        m_cards.Add(new ContentDemonSoldierCard());
        m_cards.Add(new ContentDevourerCard());
        m_cards.Add(new ContentDwarfArchitectCard());
        m_cards.Add(new ContentDwarfShivcasterCard());
        m_cards.Add(new ContentDwarvenSoldierCard());
        m_cards.Add(new ContentElvenRogueCard());
        m_cards.Add(new ContentElvenSentinelCard());
        m_cards.Add(new ContentElvenWizardCard());
        m_cards.Add(new ContentFishOracleCard());
        m_cards.Add(new ContentGladiatorCard());
        m_cards.Add(new ContentGoblinCard());
        m_cards.Add(new ContentGrasperCard());
        m_cards.Add(new ContentGroundskeeperCard());
        m_cards.Add(new ContentGuardCaptainCard());
        m_cards.Add(new ContentHeroCard());
        m_cards.Add(new ContentHomonculusCard());
        m_cards.Add(new ContentInjuredTrollCard());
        m_cards.Add(new ContentMageCard());
        m_cards.Add(new ContentMetalGolemCard());
        m_cards.Add(new ContentMinerCard());
        m_cards.Add(new ContentNaturalScoutCard());
        m_cards.Add(new ContentOverlordCard());
        m_cards.Add(new ContentRangerCard());
        m_cards.Add(new ContentRaptorCard());
        m_cards.Add(new ContentSabobotCard());
        m_cards.Add(new ContentShadowWarlockCard());
        m_cards.Add(new ContentSkeletonCard());
        m_cards.Add(new ContentStoneGolemCard());
        m_cards.Add(new ContentWandererCard());
        m_cards.Add(new ContentWildfolkCard());
        m_cards.Add(new ContentZombieCard());

        //Spell Cards
        m_cards.Add(new ContentAegisCard());
        m_cards.Add(new ContentAncientTextsCard());
        m_cards.Add(new ContentArcaneBoltCard());
        m_cards.Add(new ContentAssassinationContractCard());
        m_cards.Add(new ContentBatteryPackCard());
        m_cards.Add(new ContentBloodMoneyCard());
        m_cards.Add(new ContentBloodSacrificeCard());
        m_cards.Add(new ContentBullheadedCard());
        m_cards.Add(new ContentCosmicPactCard());
        m_cards.Add(new ContentCureWoundsCard());
        m_cards.Add(new ContentCurseOfInactionCard());
        m_cards.Add(new ContentDemonicAspectCard());
        m_cards.Add(new ContentDemoralizeCard());
        m_cards.Add(new ContentDreamCard());
        m_cards.Add(new ContentEncouragementCard());
        m_cards.Add(new ContentEnergizeCard());
        m_cards.Add(new ContentFireboltCard());
        m_cards.Add(new ContentFirestormCard());
        m_cards.Add(new ContentFletchingCard());
        m_cards.Add(new ContentFossilizeCard());
        m_cards.Add(new ContentFuryCard());
        m_cards.Add(new ContentGrowTalonsCard());
        m_cards.Add(new ContentImmolationCard());
        m_cards.Add(new ContentInsightCard());
        m_cards.Add(new ContentJoltCard());
        m_cards.Add(new ContentLegionOfBladesCard());
        m_cards.Add(new ContentLootingsCard());
        m_cards.Add(new ContentMarkedForDeathCard());
        m_cards.Add(new ContentMechanizeCard());
        m_cards.Add(new ContentMonsterProdCard());
        m_cards.Add(new ContentNecromanticTouchCard());
        m_cards.Add(new ContentNightWingsCard());
        m_cards.Add(new ContentOverchargeCard());
        m_cards.Add(new ContentPhalanxCard());
        m_cards.Add(new ContentPurgeCard());
        m_cards.Add(new ContentReforgingCard());
        m_cards.Add(new ContentRoarOfVictoryCard());
        m_cards.Add(new ContentRunicBladeCard());
        m_cards.Add(new ContentShivCard());
        m_cards.Add(new ContentSummoningCard());
        m_cards.Add(new ContentTonicOfFortitudeCard());
        m_cards.Add(new ContentTonicOfStrengthCard());
        m_cards.Add(new ContentTrollFormCard());
        m_cards.Add(new ContentWisdomOfThePastCard());
        m_cards.Add(new ContentDrainingBoltCard());
        m_cards.Add(new ContentWeakeningBoltCard());
        m_cards.Add(new ContentStaminaTrainingCard());
        m_cards.Add(new ContentOptimizeCard());

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
        int currentLevel = GameMetaProgression.GetCurLevel();

        if (exclusionList == null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GetPlayerUnlockLevel() > currentLevel)
                {
                    continue;
                }

                finalList.Add(list[i]);
            }
        }
        else
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GetPlayerUnlockLevel() > currentLevel)
                {
                    continue;
                }

                bool hasInExclusion = false;
                for (int c = 0; c < exclusionList.Count; c++)
                {
                    if (exclusionList[c].m_name == list[i].m_name)
                    {
                        hasInExclusion = true;
                        break;
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

    public static List<GameCard> GetTotalCardList()
    {
        return m_cards;
    }
}

