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
                }
                else
                {
                    m_standardSpellCards.Add(m_cards[i]);
                }
            }
        }

        m_hasInit = true;
    }

    public static GameCard GetRandomStandardCard()
    {
        if (!m_hasInit)
        {
            Init();
        }

        int r = UnityEngine.Random.Range(0, m_standardCards.Count);

        return GetCardClone(m_standardCards[r]);
    }

    public static GameCard GetRandomStandardEntityCard()
    {
        if (!m_hasInit)
        {
            Init();
        }

        int r = UnityEngine.Random.Range(0, m_standardEntityCards.Count);

        return GetCardClone(m_standardEntityCards[r]);
    }

    public static GameCard GetRandomStandardSpellCard()
    {
        if (!m_hasInit)
        {
            Init();
        }

        int r = UnityEngine.Random.Range(0, m_standardSpellCards.Count);

        return GetCardClone(m_standardSpellCards[r]);
    }

    public static GameCard GetCardClone(GameCard toClone)
    {
        return (GameCard)Activator.CreateInstance(toClone.GetType());
    }
}

