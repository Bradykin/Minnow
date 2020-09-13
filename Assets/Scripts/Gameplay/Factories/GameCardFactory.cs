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
        //Entity Cards
        m_cards.Add(new ContentCaveDragonCard());
        m_cards.Add(new ContentConjuredImpCard());
        m_cards.Add(new ContentCyclopsCard());
        m_cards.Add(new ContentDemonSoldierCard());
        m_cards.Add(new ContentDevourerCard());
        m_cards.Add(new ContentDwarfArchitectCard());
        m_cards.Add(new ContentDwarfHealerCard());
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

        //Spell Cards
        m_cards.Add(new ContentArcaneBoltCard());
        m_cards.Add(new ContentBlastingPurpleBeamCard());
        m_cards.Add(new ContentBloodSacrificeCard());
        m_cards.Add(new ContentClearcutCard());
        m_cards.Add(new ContentCureWoundsCard());
        m_cards.Add(new ContentDemoncAspectCard());
        m_cards.Add(new ContentDrainCard());
        m_cards.Add(new ContentDreamCard());
        m_cards.Add(new ContentEnergizeCard());
        m_cards.Add(new ContentEnergizingPurpleBeamCard());
        m_cards.Add(new ContentEntangleCard());
        m_cards.Add(new ContentFireboltCard());
        m_cards.Add(new ContentFortifyingPurpleBeamCard());
        m_cards.Add(new ContentGrowTalonsCard());
        m_cards.Add(new ContentGrowthCard());
        m_cards.Add(new ContentInsightCard());
        m_cards.Add(new ContentJoltCard());
        m_cards.Add(new ContentNecromanticTouchCard());
        m_cards.Add(new ContentNightWingsCard());
        m_cards.Add(new ContentPurePurpleBeamCard());
        m_cards.Add(new ContentPurpleBeamSwordCard());
        m_cards.Add(new ContentReplenishingPurpleBeamCard());
        m_cards.Add(new ContentSkypierceCard());
        m_cards.Add(new ContentTrollFormCard());


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

        List<GameCard> checkList = GetCheckList(m_commonCards, m_uncommonCards, m_rareCards);

        return GetCardFromList(checkList);
    }

    public static GameCard GetRandomStandardEntityCard()
    {
        if (!m_hasInit)
        {
            Init();
        }

        List<GameCard> checkList = GetCheckList(m_commonEntityCards, m_uncommonEntityCards, m_rareEntityCards);

        return GetCardFromList(checkList);
    }

    public static GameCard GetRandomStandardSpellCard()
    {
        if (!m_hasInit)
        {
            Init();
        }

        List<GameCard> checkList = GetCheckList(m_commonSpellCards, m_uncommonSpellCards, m_rareSpellCards);

        return GetCardFromList(checkList);
    }

    private static List<GameCard> GetCheckList(List<GameCard> commonCards, List<GameCard> uncommonCards, List<GameCard> rareCards)
    {
        int chanceForUncommon = Constants.PercentChanceForUncommonCard;
        int chanceForRare = Constants.PercentChanceForRareCard;

        if (GameHelper.IsValidChaosLevel(3))
        {
            chanceForUncommon = chanceForUncommon / 2;
        }
        if (GameHelper.IsValidChaosLevel(4))
        {
            chanceForRare = chanceForRare / 2;
        }

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

        if (toClone is GameCardEntityBase && clone is GameCardEntityBase)
        {
            GameEntity toCloneEntity = ((GameCardEntityBase)toClone).GetEntity();
            GameEntity cloneEntity = ((GameCardEntityBase)clone).GetEntity();

            cloneEntity.CopyOff(toCloneEntity);
        }

        return clone;
    }

    private static GameCard GetCardFromList(List<GameCard> list)
    {
        int r = UnityEngine.Random.Range(0, list.Count);

        return GetCardClone(list[r]);
    }
}

