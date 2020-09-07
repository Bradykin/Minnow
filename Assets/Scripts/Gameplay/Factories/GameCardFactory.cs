using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameCardFactory
{
    private static List<GameCard> m_cards = new List<GameCard>();
    private static List<GameCard> m_standardCards = new List<GameCard>();
    private static List<GameCard> m_standardSpellCards = new List<GameCard>();
    private static List<GameCard> m_standardEntityCards = new List<GameCard>();

    private static List<GameCard> m_rareCards = new List<GameCard>();
    private static List<GameCard> m_uncommonCards = new List<GameCard>();
    private static List<GameCard> m_commonCards = new List<GameCard>();

    private static List<GameCard> m_rareEntityCards = new List<GameCard>();
    private static List<GameCard> m_uncommonEntityCards = new List<GameCard>();
    private static List<GameCard> m_commonEntityCards = new List<GameCard>();

    private static List<GameCard> m_rareSpellCards = new List<GameCard>();
    private static List<GameCard> m_uncommonSpellCards = new List<GameCard>();
    private static List<GameCard> m_commonSpellCards = new List<GameCard>();

    private static bool m_hasInit = false;

    public static void Init()
    {
        m_cards.Add(new ContentCureWoundsCard());
        m_cards.Add(new ContentDevourerCard());
        m_cards.Add(new ContentDrainCard());
        m_cards.Add(new ContentDwarvenSoldierCard());
        m_cards.Add(new ContentElvenRogueCard());
        m_cards.Add(new ContentElvenSentinelCard());
        m_cards.Add(new ContentElvenWizardCard());
        m_cards.Add(new ContentEnergizeCard());
        m_cards.Add(new ContentFireboltCard());
        m_cards.Add(new ContentFishOracleCard());
        m_cards.Add(new ContentGoblinCard());
        m_cards.Add(new ContentGroundskeeperCard());
        m_cards.Add(new ContentInjuredTrollCard());
        m_cards.Add(new ContentNaturalScoutCard());
        m_cards.Add(new ContentSabobotCard());
        m_cards.Add(new ContentCaveDragonCard());
        m_cards.Add(new ContentGrowthCard());
        m_cards.Add(new ContentClearcutCard());
        m_cards.Add(new ContentInsightCard());
        m_cards.Add(new ContentDreamCard());
        m_cards.Add(new ContentGrowTalonsCard());
        m_cards.Add(new ContentTrollFormCard());
        m_cards.Add(new ContentDemoncAspectCard());
        m_cards.Add(new ContentBloodSacrificeCard());
        m_cards.Add(new ContentJoltCard());
        m_cards.Add(new ContentDemonSoldierCard());
        m_cards.Add(new ContentDwarfArchitectCard());
        m_cards.Add(new ContentDwarfHealerCard());
        m_cards.Add(new ContentGladiatorCard());
        m_cards.Add(new ContentMageCard());
        m_cards.Add(new ContentShadowWarlockCard());
        m_cards.Add(new ContentMinerCard());
        m_cards.Add(new ContentRangerCard());
        m_cards.Add(new ContentWandererCard());
        m_cards.Add(new ContentHeroCard());
        m_cards.Add(new ContentGuardCaptainCard());
        m_cards.Add(new ContentWildfolkCard());
        m_cards.Add(new ContentConjuredImpCard());
        m_cards.Add(new ContentMetalGolemCard());
        m_cards.Add(new ContentHomonculusCard());
        m_cards.Add(new ContentOverlordCard());
        m_cards.Add(new ContentRaptorCard());
        m_cards.Add(new ContentSkeletonCard());
        m_cards.Add(new ContentGrasperCard());
        m_cards.Add(new ContentArcaneBoltCard());
        m_cards.Add(new ContentEntangleCard());
        m_cards.Add(new ContentNecromanticTouchCard());
        m_cards.Add(new ContentNightWingsCard());
        m_cards.Add(new ContentSkypierceCard());
        m_cards.Add(new ContentPurpleBeamCard());


        for (int i = 0; i < m_cards.Count; i++)
        {
            if (m_cards[i].m_rarity == GameElementBase.GameRarity.Common 
                || m_cards[i].m_rarity == GameElementBase.GameRarity.Uncommon 
                || m_cards[i].m_rarity == GameElementBase.GameRarity.Rare)
            {
                m_standardCards.Add(m_cards[i]);

                if (m_cards[i] is GameCardEntityBase)
                {
                    m_standardEntityCards.Add(m_cards[i]);
                    if (m_cards[i].m_rarity == GameElementBase.GameRarity.Common)
                    {
                        m_commonEntityCards.Add(m_cards[i]);
                    }
                    else if (m_cards[i].m_rarity == GameElementBase.GameRarity.Uncommon)
                    {
                        m_uncommonEntityCards.Add(m_cards[i]);
                    }
                    else if (m_cards[i].m_rarity == GameElementBase.GameRarity.Rare)
                    {
                        m_rareEntityCards.Add(m_cards[i]);
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

        //Debug.Log("Common Cards: " + m_commonCards.Count);
        //Debug.Log("Uncommon Cards: " + m_uncommonCards.Count);
        //Debug.Log("Rare Cards: " + m_rareCards.Count);

        m_hasInit = true;
    }

    public static GameCard GetRandomStandardCard()
    {
        if (!m_hasInit)
        {
            Init();
        }

        List<GameCard> checkList;

        if (GameHelper.PercentChanceRoll(Constants.PercentChanceForUncommonCard))
        {
            checkList = m_uncommonCards;
        }
        else if (GameHelper.PercentChanceRoll(Constants.PercentChanceForRareCard))
        {
            checkList = m_rareCards;
        }
        else
        {
            checkList = m_commonCards;
        }

        return GetCardFromList(checkList);
    }

    public static GameCard GetRandomStandardEntityCard()
    {
        if (!m_hasInit)
        {
            Init();
        }

        List<GameCard> checkList;

        if (GameHelper.PercentChanceRoll(Constants.PercentChanceForUncommonCard))
        {
            checkList = m_uncommonEntityCards;
        }
        else if (GameHelper.PercentChanceRoll(Constants.PercentChanceForRareCard))
        {
            checkList = m_rareEntityCards;
        }
        else
        {
            checkList = m_commonEntityCards;
        }

        return GetCardFromList(checkList);
    }

    public static GameCard GetRandomStandardSpellCard()
    {
        if (!m_hasInit)
        {
            Init();
        }

        List<GameCard> checkList;

        if (GameHelper.PercentChanceRoll(Constants.PercentChanceForUncommonCard))
        {
            checkList = m_uncommonSpellCards;
        }
        else if (GameHelper.PercentChanceRoll(Constants.PercentChanceForRareCard))
        {
            checkList = m_rareSpellCards;
        }
        else
        {
            checkList = m_commonSpellCards;
        }
        return GetCardFromList(checkList);
    }

    public static GameCard GetCardClone(GameCard toClone)
    {
        return (GameCard)Activator.CreateInstance(toClone.GetType());
    }

    private static GameCard GetCardFromList(List<GameCard> list)
    {
        int r = UnityEngine.Random.Range(0, list.Count);

        return GetCardClone(list[r]);
    }
}

