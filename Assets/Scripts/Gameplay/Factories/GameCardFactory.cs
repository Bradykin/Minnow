﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public static List<GameCard> m_tribalCards = new List<GameCard>();

    public static List<GameCard> m_exileSpells = new List<GameCard>();

    private static List<GameCard> m_exclusionCards = new List<GameCard>();

    private static bool m_hasInit = false;

    public static void Init(List<GameCard> exclusionCards = null)
    {
        m_hasInit = true;

        m_cards = new List<GameCard>();
        m_unitCards = new List<GameCard>();
        m_enemyCards = new List<GameCard>();
        m_standardCards = new List<GameCard>();
        m_standardSpellCards = new List<GameCard>();
        m_standardUnitCards = new List<GameCard>();

        m_rareCards = new List<GameCard>();
        m_uncommonCards = new List<GameCard>();
        m_commonCards = new List<GameCard>();

        m_rareUnitCards = new List<GameCard>();
        m_uncommonUnitCards = new List<GameCard>();
        m_commonUnitCards = new List<GameCard>();

        m_rareSpellCards = new List<GameCard>();
        m_uncommonSpellCards = new List<GameCard>();
        m_commonSpellCards = new List<GameCard>();

        m_tribalCards = new List<GameCard>();
        m_exclusionCards.Clear();

        if (exclusionCards != null)
        {
            m_exclusionCards.AddRange(exclusionCards);
        }

        //Unit Cards
        m_cards.Add(new ContentArmouredMonkCard());
        m_cards.Add(new ContentBonecasterCard());
        m_cards.Add(new ContentConjuredImpCard());
        m_cards.Add(new ContentCyclopsCard());
        m_cards.Add(new ContentDevourerCard());
        m_cards.Add(new ContentDwarfArchitectCard());
        m_cards.Add(new ContentDwarfforgedConstructCard());
        m_cards.Add(new ContentDwarfShivcasterCard());
        m_cards.Add(new ContentElvenRogueCard());
        m_cards.Add(new ContentElvenSentinelCard());
        m_cards.Add(new ContentElvenWizardCard());
        m_cards.Add(new ContentEnergyConstructCard());
        m_cards.Add(new ContentFrogShamanCard());
        m_cards.Add(new ContentGladiatorCard());
        m_cards.Add(new ContentGoblinLegendCard());
        m_cards.Add(new ContentGrasperCard());
        m_cards.Add(new ContentGroundskeeperCard());
        m_cards.Add(new ContentGuardCaptainCard());
        m_cards.Add(new ContentHeroCard());
        m_cards.Add(new ContentHomonculusCard());
        m_cards.Add(new ContentInjuredTrollCard());
        m_cards.Add(new ContentMageCard());
        m_cards.Add(new ContentMetalGolemCard());
        m_cards.Add(new ContentMetalProtectorCard());
        m_cards.Add(new ContentMountainBeastCard());
        m_cards.Add(new ContentMinerCard());
        m_cards.Add(new ContentNaturalScoutCard());
        m_cards.Add(new ContentOverlordCard());
        m_cards.Add(new ContentPirateCaptainCard());
        m_cards.Add(new ContentPolarHunterCard());
        m_cards.Add(new ContentPyromageCard());
        m_cards.Add(new ContentRangerCard());
        m_cards.Add(new ContentRaptorCard());
        m_cards.Add(new ContentRhinoProtectorCard());
        m_cards.Add(new ContentSabobotCard());
        m_cards.Add(new ContentShadowWarlockCard());
        m_cards.Add(new ContentSkeletonCard());
        m_cards.Add(new ContentStoneGolemCard());
        m_cards.Add(new ContentWandererCard());
        m_cards.Add(new ContentWildfolkCard());
        m_cards.Add(new ContentStormChannelerCard());
        m_cards.Add(new ContentDesertSwordsmanCard());
        m_cards.Add(new ContentWarriorPriestessCard());
        m_cards.Add(new ContentWildwoodExplorerCard());
        m_cards.Add(new ContentWildwoodSkirmisherCard());
        m_cards.Add(new ContentMysticWitchCard());
        m_cards.Add(new ContentEtherealStagCard());
        m_cards.Add(new ContentStagBearCard());
        m_cards.Add(new ContentGolemProtectorCard());   

        //Starter Cards
        m_cards.Add(new ContentLizardSoldierCard());
        m_cards.Add(new ContentUndeadMammothCard());
        m_cards.Add(new ContentSandwalkerCard());
        m_cards.Add(new ContentMechanizedBeastCard());
        m_cards.Add(new ContentAlphaBoarCard());
        m_cards.Add(new ContentDwarvenSoldierCard());

        //Building Unit Cards
        m_cards.Add(new ContentRoyalCaravanCard());

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
        m_cards.Add(new ContentDemonicAspectCard());
        m_cards.Add(new ContentDrainingBoltCard());
        m_cards.Add(new ContentControlMoralCard());
        m_cards.Add(new ContentDreamCard());
        m_cards.Add(new ContentEncouragementCard());
        m_cards.Add(new ContentEnergizeCard());
        m_cards.Add(new ContentFireboltCard());
        m_cards.Add(new ContentFletchingCard());
        m_cards.Add(new ContentFossilizeCard());
        m_cards.Add(new ContentFuryCard());
        m_cards.Add(new ContentGrowTalonsCard());
        m_cards.Add(new ContentImmolationCard());
        m_cards.Add(new ContentInsightCard());
        m_cards.Add(new ContentJoltCard());
        m_cards.Add(new ContentLegionOfBladesCard());
        m_cards.Add(new ContentLootingsCard());
        m_cards.Add(new ContentMechanizeCard());
        m_cards.Add(new ContentMonsterProdCard());
        m_cards.Add(new ContentNecromanticTouchCard());
        m_cards.Add(new ContentNightWingsCard());
        m_cards.Add(new ContentOptimizeCard());
        m_cards.Add(new ContentOverchargeCard());
        m_cards.Add(new ContentPhalanxCard());
        m_cards.Add(new ContentReforgingCard());
        m_cards.Add(new ContentRoarOfVictoryCard());
        m_cards.Add(new ContentRunicBladeCard());
        m_cards.Add(new ContentShivCard());
        m_cards.Add(new ContentStaminaTrainingCard());
        m_cards.Add(new ContentSummoningCard());
        m_cards.Add(new ContentTonicOfFortitudeCard());
        m_cards.Add(new ContentTonicOfStrengthCard());
        m_cards.Add(new ContentTrollFormCard());
        m_cards.Add(new ContentWeakeningBoltCard());
        m_cards.Add(new ContentMeteorCard());
        m_cards.Add(new ContentChainLightningCard());
        m_cards.Add(new ContentTwinCard());
        m_cards.Add(new ContentWillOfNatureCard());
        m_cards.Add(new ContentDrainPowerCard());
        m_cards.Add(new ContentBurningStormCard());
        m_cards.Add(new ContentDeadeyeCard());
        m_cards.Add(new ContentMarksmanCard());
        m_cards.Add(new ContentSprintCard());
        m_cards.Add(new ContentFadeCard());
        m_cards.Add(new ContentDrainingTalonsCard());
        m_cards.Add(new ContentMageArmorCard());
        m_cards.Add(new ContentGreedyKillCard());
        m_cards.Add(new ContentToxicTonicCard());
        m_cards.Add(new ContentDarkHeartCard());
        m_cards.Add(new ContentBladesCard());
        m_cards.Add(new ContentGrowthCard());
        m_cards.Add(new ContentMoonbeamCard());
        m_cards.Add(new ContentProductionCard());
        m_cards.Add(new ContentHeroismCard());
        m_cards.Add(new ContentConstellationsCard());
        m_cards.Add(new ContentEndCard());
        m_cards.Add(new ContentQuickStrikesCard());
        m_cards.Add(new ContentPathCard());
        m_cards.Add(new ContentProtectionCard());
        m_cards.Add(new ContentExperienceCard());
        m_cards.Add(new ContentFireworksCard());
        m_cards.Add(new ContentCometOfThePastCard());

        //Event Cards
        m_cards.Add(new ContentLivingBombCard());

        //Tribal cards, for exclusion list
        m_tribalCards.Add(new ContentDwarfArchitectCard());

        m_tribalCards.Add(new ContentAncientTextsCard());
        m_tribalCards.Add(new ContentDreamCard());
        //m_tribalCards.Add(new ContentFletchingCard());
        m_tribalCards.Add(new ContentInsightCard());
        m_tribalCards.Add(new ContentOverchargeCard());
        m_tribalCards.Add(new ContentReforgingCard());
        m_tribalCards.Add(new ContentRoarOfVictoryCard());

        //Enemy Cards
        m_enemyCards.Add(new ContentAngryBirdEnemyCard());
        //m_enemyCards.Add(new ContentAngryTreantEnemyCard());
        //m_enemyCards.Add(new ContentArcticYetiEnemyCard());
        m_enemyCards.Add(new ContentBasiliskEnemyCard());
        m_enemyCards.Add(new ContentBerserkerEnemyCard());
        m_enemyCards.Add(new ContentBlindBeastEnemyCard());
        //m_enemyCards.Add(new ContentBlueMageEnemyCard());
        m_enemyCards.Add(new ContentBurningMonstrosityEnemyCard());
        m_enemyCards.Add(new ContentCharybdisEnemyCard());
        m_enemyCards.Add(new ContentChillflameBeastEnemyCard());
        m_enemyCards.Add(new ContentCrumblingAncientEnemyCard());
        //m_enemyCards.Add(new ContentDarkMageEnemyCard());
        m_enemyCards.Add(new ContentDarkWarriorEnemyCard());
        m_enemyCards.Add(new ContentDemonMagicianEnemyCard());
        //m_enemyCards.Add(new ContentDemonSkyterrorEnemyCard());
        m_enemyCards.Add(new ContentDjinnEnemyCard());
        //m_enemyCards.Add(new ContentDraconicPlesiosaurEnemyCard());
        //m_enemyCards.Add(new ContentEnergizedTreantEnemyCard());
        //m_enemyCards.Add(new ContentFailedExperimentEnemyCard());
        m_enemyCards.Add(new ContentFireLizardEnemyCard());
        m_enemyCards.Add(new ContentFlameImpEnemyCard());
        //m_enemyCards.Add(new ContentFlameNecromancerEnemyCard());
        //m_enemyCards.Add(new ContentFlamingWerewolfEnemyCard());
        //m_enemyCards.Add(new ContentFleshConstructEnemyCard());
        m_enemyCards.Add(new ContentFrostGiantEnemyCard());
        m_enemyCards.Add(new ContentFrozenGuardianEnemyCard());
        m_enemyCards.Add(new ContentFrozenImpEnemyCard());
        //m_enemyCards.Add(new ContentGiantBrawlerEnemyCard());
        //m_enemyCards.Add(new ContentGilaLizardEnemyCard());
        m_enemyCards.Add(new ContentGoblinShamanEnemyCard());
        m_enemyCards.Add(new ContentGoblinWarriorEnemyCard());
        m_enemyCards.Add(new ContentGreatFrostlizardEnemyCard());
        m_enemyCards.Add(new ContentGreenRockGiantEnemyCard());
        m_enemyCards.Add(new ContentGriffonEnemyCard());
        m_enemyCards.Add(new ContentHellhoundEnemyCard());
        //m_enemyCards.Add(new ContentIcebladeWarriorEnemyCard());
        m_enemyCards.Add(new ContentIcefisherEnemyCard());
        m_enemyCards.Add(new ContentIceWurmEnemyCard());
        //m_enemyCards.Add(new ContentIfritEnemyCard());
        //m_enemyCards.Add(new ContentInfernalZombieEnemyCard());
        m_enemyCards.Add(new ContentJackalEnemyCard());
        //m_enemyCards.Add(new ContentJungleOrcEnemyCard());
        m_enemyCards.Add(new ContentLancerEnemyCard());
        //m_enemyCards.Add(new ContentLavaGoblinEnemyCard());
        m_enemyCards.Add(new ContentLavaHellionEnemyCard());
        m_enemyCards.Add(new ContentLavaRhinoCard());
        m_enemyCards.Add(new ContentLichEnemyCard());
        m_enemyCards.Add(new ContentLizardmanEnemyCard());
        m_enemyCards.Add(new ContentLordOfChaosEnemyCard());
        m_enemyCards.Add(new ContentLordOfEruptionsEnemyCard());
        m_enemyCards.Add(new ContentLordOfFrostEnemyCard());
        m_enemyCards.Add(new ContentLordOfShadowsEnemyCard());
        m_enemyCards.Add(new ContentLordOfWinterEnemyCard());
        m_enemyCards.Add(new ContentLurkerEnemyCard());
        //m_enemyCards.Add(new ContentMageShadeEnemyCard());
        //m_enemyCards.Add(new ContentMarshSerpentEnemyCard());
        m_enemyCards.Add(new ContentMarauderEnemyCard());
        m_enemyCards.Add(new ContentMetalManticoreEnemyCard());
        m_enemyCards.Add(new ContentMobolaEnemyCard());
        m_enemyCards.Add(new ContentMummyEnemyCard());
        m_enemyCards.Add(new ContentMummyPharaohEnemyCard());
        //m_enemyCards.Add(new ContentNagaWardenEnemyCard());
        //m_enemyCards.Add(new ContentNightmareWorkerEnemyCard());
        m_enemyCards.Add(new ContentOrcEnemyCard());
        m_enemyCards.Add(new ContentOrcShamanEnemyCard());
        m_enemyCards.Add(new ContentPhoenixEnemyCard());
        m_enemyCards.Add(new ContentPolarWarriorEnemyCard());
        //m_enemyCards.Add(new ContentPoisonGargantuanEnemyCard());
        m_enemyCards.Add(new ContentRedDragonEnemyCard());
        m_enemyCards.Add(new ContentRiverlordEnemyCard());
        m_enemyCards.Add(new ContentSandWyvernEnemyCard());
        m_enemyCards.Add(new ContentSandVortexEnemyCard());
        m_enemyCards.Add(new ContentScorchingSerpentEnemyCard());
        m_enemyCards.Add(new ContentSerpentineConstructEnemyCard());
        //m_enemyCards.Add(new ContentSpikewolfEnemyCard());
        //m_enemyCards.Add(new ContentSandWurmEnemyCard());
        //m_enemyCards.Add(new ContentScytheBarbarianEnemyCard());
        m_enemyCards.Add(new ContentShadeEnemyCard());
        m_enemyCards.Add(new ContentSkeletalCaptainEnemyCard());
        m_enemyCards.Add(new ContentSkeletalPirateEnemyCard());
        m_enemyCards.Add(new ContentSlimeEnemyCard());
        m_enemyCards.Add(new ContentSnakeEnemyCard());
        m_enemyCards.Add(new ContentSpiralSerpentEnemyCard());
        m_enemyCards.Add(new ContentStoneflingerEnemyCard());
        m_enemyCards.Add(new ContentSnowprowlerEnemyCard());
        //m_enemyCards.Add(new ContentSnowTrollEnemyCard());
        //m_enemyCards.Add(new ContentSwampwalkerEnemyCard());
        m_enemyCards.Add(new ContentToadEnemyCard());
        //m_enemyCards.Add(new ContentTreantMageEnemyCard());
        //m_enemyCards.Add(new ContentUndeadForestMonsterEnemyCard());
        //m_enemyCards.Add(new ContentUndeadFrostDragonEnemyCard());
        m_enemyCards.Add(new ContentVolcanoCrabEnemyCard());
        m_enemyCards.Add(new ContentVolcanoGolemEnemyCard());
        m_enemyCards.Add(new ContentValgulaEnemyCard());
        m_enemyCards.Add(new ContentWerewolfEnemyCard());
        //m_enemyCards.Add(new ContentWingedFlameImpEnemyCard());
        //m_enemyCards.Add(new ContentWinterDragonEnemyCard());
        m_enemyCards.Add(new ContentYetiEnemyCard());
        //m_enemyCards.Add(new ContentWildOgreEnemyCard());
        //m_enemyCards.Add(new ContentWitchEnemyCard());
        m_enemyCards.Add(new ContentZombieCrabEnemyCard());
        m_enemyCards.Add(new ContentZombieShipEnemyCard());

        m_enemyCards.Add(new ContentSabertoothWyvernEnemyCard());
        m_enemyCards.Add(new ContentFireServantEnemyCard());
        m_enemyCards.Add(new ContentFlameDemonEnemyCard());
        m_enemyCards.Add(new ContentHuskEnemyCard());
        m_enemyCards.Add(new ContentImmortalSpearEnemyCard());
        m_enemyCards.Add(new ContentImmortalBowEnemyCard());
        m_enemyCards.Add(new ContentImmortalShieldEnemyCard());
        m_enemyCards.Add(new ContentOrcWarleaderEnemyCard());
        m_enemyCards.Add(new ContentRiverlurkerEnemyCard());


        for (int i = 0; i < m_cards.Count; i++)
        {
            if (m_exclusionCards.Any(c => c.GetBaseName() == m_cards[i].GetBaseName()))
            {
                continue;
            }
            
            bool isUnit = m_cards[i] is GameUnitCard;
            if (isUnit)
            {
                m_unitCards.Add(m_cards[i]);
            }
            else
            {
                if (m_cards[i].m_shouldExile)
                {
                    m_exileSpells.Add(m_cards[i]);
                }
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

    public static GameElementBase.GameRarity GetRandomRarity()
    {
        float random = UnityEngine.Random.Range(0.0f, 100.0f);

        if (random <= Constants.PercentChanceForRareCard)
        {
            return GameElementBase.GameRarity.Rare;
        }
        else if (random <= Constants.PercentChanceForRareCard + Constants.PercentChanceForUncommonCard)
        {
            return GameElementBase.GameRarity.Uncommon;
        }
        else
        {
            return GameElementBase.GameRarity.Common;
        }
    }

    public static IReadOnlyList<GameCard> GetCardListOfTypeAtRarity(GameElementBase.GameRarity gameRarity, bool getUnitCardsInsteadOfSpells)
    {
        if (getUnitCardsInsteadOfSpells)
        {
            switch (gameRarity)
            {
                case GameElementBase.GameRarity.Common:
                    return m_commonUnitCards;
                case GameElementBase.GameRarity.Uncommon:
                    return m_uncommonUnitCards;
                case GameElementBase.GameRarity.Rare:
                    return m_rareUnitCards;
                default:
                    Debug.LogError("Empty Card List for Rarity");
                    return new List<GameCard>();
            }
        }
        else
        {
            switch (gameRarity)
            {
                case GameElementBase.GameRarity.Common:
                    return m_commonSpellCards;
                case GameElementBase.GameRarity.Uncommon:
                    return m_uncommonSpellCards;
                case GameElementBase.GameRarity.Rare:
                    return m_rareSpellCards;
                default:
                    Debug.LogError("Empty Card List for Rarity");
                    return new List<GameCard>();
            }
        }
    }

    public static GameCard GetRandomStandardCard(List<GameCard> exclusionList = null)
    {
        if (!m_hasInit)
        {
            Init();
        }

        List<GameCard> checkList = GetCheckList(m_commonCards, m_uncommonCards, m_rareCards);

        return GetCardFromList(checkList, exclusionList);
    }

    public static GameCard GetRandomStandardUnitCard(List<GameCard> exclusionList = null)
    {
        if (!m_hasInit)
        {
            Init();
        }

        List<GameCard> checkList = GetCheckList(m_commonUnitCards, m_uncommonUnitCards, m_rareUnitCards);

        return GetCardFromList(checkList, exclusionList);
    }

    public static GameCard GetRandomStandardUnitCard(GameElementBase.GameRarity rarity, List<GameCard> exclusionList = null, int exclusionCost = -1, bool excludeCardThreeOrHigher = false)
    {
        if (!m_hasInit)
        {
            Init();
        }

        if (rarity == GameElementBase.GameRarity.Common)
        {
            return GetCardFromList(m_commonUnitCards, exclusionList, exclusionCost, excludeCardThreeOrHigher);
        }
        else if (rarity == GameElementBase.GameRarity.Uncommon)
        {
            return GetCardFromList(m_uncommonUnitCards, exclusionList, exclusionCost, excludeCardThreeOrHigher);
        }
        else if (rarity == GameElementBase.GameRarity.Rare)
        {
            return GetCardFromList(m_rareUnitCards, exclusionList, exclusionCost, excludeCardThreeOrHigher);
        }

        Debug.LogError("Invalid rarity selected for getting a random card.");
        return null;
    }

    public static GameCard GetRandomStandardSpellCard(List<GameCard> exclusionList = null)
    {
        if (!m_hasInit)
        {
            Init();
        }

        List<GameCard> checkList = GetCheckList(m_commonSpellCards, m_uncommonSpellCards, m_rareSpellCards);

        return GetCardFromList(checkList, exclusionList);
    }

    public static GameCard GetRandomStandardSpellCard(GameElementBase.GameRarity rarity, List<GameCard> exclusionList = null, int exclusionCost = -1, bool excludeCardThreeOrHigher = false, bool excludeExileCards = false)
    {
        if (!m_hasInit)
        {
            Init();
        }

        if (rarity == GameElementBase.GameRarity.Common)
        {
            return GetCardFromList(m_commonSpellCards, exclusionList, exclusionCost, excludeCardThreeOrHigher, excludeExileCards);
        }
        else if (rarity == GameElementBase.GameRarity.Uncommon)
        {
            return GetCardFromList(m_uncommonSpellCards, exclusionList, exclusionCost, excludeCardThreeOrHigher, excludeExileCards);
        }
        else if (rarity == GameElementBase.GameRarity.Rare)
        {
            return GetCardFromList(m_rareSpellCards, exclusionList, exclusionCost, excludeCardThreeOrHigher, excludeExileCards);
        }

        Debug.LogError("Invalid rarity selected for getting a random card.");
        return null;
    }

    private static List<GameCard> GetCheckList(List<GameCard> commonCards, List<GameCard> uncommonCards, List<GameCard> rareCards)
    {
        if (!m_hasInit)
        {
            Init();
        }

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
        if (!m_hasInit)
        {
            Init();
        }

        return (GameCard)Activator.CreateInstance(toClone.GetType());
    }

    public static GameCard GetCardDup(GameCard toClone)
    {
        if (!m_hasInit)
        {
            Init();
        }

        GameCard clone = (GameCard)Activator.CreateInstance(toClone.GetType());

        if (toClone is GameUnitCard && clone is GameUnitCard)
        {
            GameUnit toCloneUnit = ((GameUnitCard)toClone).GetUnit();
            GameUnit cloneUnit = ((GameUnitCard)clone).GetUnit();

            cloneUnit.CopyOff(toCloneUnit);
        }

        return clone;
    }

    private static GameCard GetCardFromList(List<GameCard> list, List<GameCard> exclusionList, int exclusionCost = -1, bool excludeCardFourOrHigher = false, bool excludeExileCards = false)
    {
        if (!m_hasInit)
        {
            Init();
        }

        //Fill the list by removing anything that was excluded.
        List<GameCard> finalList = new List<GameCard>();

        for (int i = 0; i < list.Count; i++)
        {
            if (!Constants.UnlockAllContent && !GameMetaprogressionUnlocksDataManager.HasUnlocked(list[i]))
            {
                continue;
            }

            if (excludeCardFourOrHigher && list[i].GetCost() >= 4)
            {
                continue;
            }

            if (excludeExileCards && list[i].m_shouldExile)
            {
                continue;
            }

            if (exclusionList == null)
            {
                finalList.Add(list[i]);
            }
            else
            {
                bool hasInExclusion = false;
                for (int c = 0; c < exclusionList.Count; c++)
                {
                    if (exclusionList[c].GetName() == list[i].GetName())
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

        if (finalList.Count == 0)
        {
            Debug.LogError("No viable cards found");
            return null;
        }

        List<GameCard> finalListBeforeTagWeights = new List<GameCard>();
        finalListBeforeTagWeights.AddRange(finalList);

        //Use the tag weights + randomization to get the correct card here.
        if (Constants.GameDirectorTestPrints && exclusionList == null)
        {
            Debug.Log("Checking tag weights for all options on this random card query.");
        }
        int totalWeight = 0;
        for (int i = 0; i < finalList.Count; i++)
        {
            int tagWeight = GameTagHolder.GetTagValueFor(finalList[i]);
            if (Constants.GameDirectorTestPrints && exclusionList == null)
            {
                Debug.Log($"{finalList[i].GetBaseName()} with tag weight {tagWeight}");
            }
            if (tagWeight > 0)
            {
                totalWeight += tagWeight;
                finalList[i].m_storedTagWeight = totalWeight;
                if (Constants.GameDirectorTestPrints && exclusionList == null)
                {
                    //Debug.Log($"{finalList[i].GetBaseName()} with total weight {finalList[i].m_storedTagWeight}");
                }
            }
            else
            {
                finalList.RemoveAt(i);
                i--;
            }
        }

        int r = UnityEngine.Random.Range(0, totalWeight);
        //Debug.Log($"Value roll = {r}");

        for (int i = 0; i < finalList.Count; i++)
        {
            //Debug.Log($"Comparing {finalList[i].GetBaseName()} with comparing Value roll {r} to tag weight {finalList[i].m_storedTagWeight}");
            if (r <= finalList[i].m_storedTagWeight)
            {
                //Debug.Log($"Choosing {finalList[i].GetBaseName()} with comparing Value roll {r} to tag weight {finalList[i].m_storedTagWeight}");
                return GetCardClone(finalList[i]);
            }
        }

        Debug.LogWarning("Failed to find any card when trying get one (likely caused by tag weighting issues).");
        return finalListBeforeTagWeights[UnityEngine.Random.Range(0, finalListBeforeTagWeights.Count)];
    }

    public static GameUnitCard GetCardFromUnit(GameUnit unit)
    {
        if (!m_hasInit)
        {
            Init();
        }

        if (unit.GetTeam() == Team.Player)
        {
            for (int i = 0; i < m_unitCards.Count; i++)
            {
                GameUnitCard unitCard = (GameUnitCard)m_unitCards[i];

                if (unitCard.GetUnit().GetBaseName() == unit.GetBaseName())
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

                if (unitCard.GetUnit().GetBaseName() == unit.GetBaseName())
                {
                    GameUnitCard cardClone = (GameUnitCard)GetCardClone(m_enemyCards[i]);
                    cardClone.SetUnit(unit);

                    return cardClone;
                }
            }
        }

        return null;
    }

    public static GameCard GetCardFromJson(JsonGameCardData jsonData)
    {
        if (!m_hasInit)
        {
            Init();
        }

        int i = m_cards.FindIndex(t => t.GetBaseName() == jsonData.baseName);

        GameCard newCard = (GameCard)Activator.CreateInstance(m_cards[i].GetType());
        newCard.LoadFromJson(jsonData);

        return newCard;
    }

    public static List<GameCard> GetTotalCardList()
    {
        if (!m_hasInit)
        {
            Init();
        }

        return m_cards;
    }

    public static GameCard GetCardByName(string name)
    {
        if (!m_hasInit)
        {
            Init();
        }

        for (int i = 0; i < m_cards.Count; i++)
        {
            if (m_cards[i].GetBaseName().ToLower() == name.ToLower())
            {
                return GetCardClone(m_cards[i]);
            }
        }

        return null;
    }
}

