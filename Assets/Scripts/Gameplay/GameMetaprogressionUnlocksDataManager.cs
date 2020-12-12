using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMetaprogressionUnlocksDataManager
{
    private static bool m_isInit;

    private static List<GameMetaprogressionDataElement> m_dataElements = new List<GameMetaprogressionDataElement>();

    private static Dictionary<int, GameMetaprogressionReward> m_cardRewards = new Dictionary<int, GameMetaprogressionReward>();
    private static Dictionary<int, GameMetaprogressionReward> m_relicRewards = new Dictionary<int, GameMetaprogressionReward>();
    private static Dictionary<int, GameMetaprogressionReward> m_mapRewards = new Dictionary<int, GameMetaprogressionReward>();

    private static List<GameCard> m_initialCards = new List<GameCard>();
    private static List<GameRelic> m_initialRelics = new List<GameRelic>();
    private static List<GameMap> m_initialMaps = new List<GameMap>();

    public static void InitData()
    {
        FillInitialUnlocks();

        GameMap lakesideMap = new ContentLakesideMap(); //Tutorial map
        GameMap deltaMap = new ContentDeltaMap();
        GameMap mountainPass = new ContentMountainPassMap();
        GameMap crimsonIslandsMap = new ContentCrimsonIslandsMap();
        GameMap snowmeltMap = new ContentSnowmeltMap();
        GameMap desertPassMap = new ContentDesertPassMap();
        GameMap volcanoRun = new ContentVolcanoRunMap();
        GameMap lakesideHardMap = new ContentLakesideHardMap();
        GameMap themarshlands = new ContentTheMarshlandsMap();
        GameMap frozenLake = new ContentFrozenLakeMap();

        GameCard lizardSoldierCard = new ContentLizardSoldierCard();
        GameCard undeadMammothCard = new ContentUndeadMammothCard();

        GameCard sandwalkerCard = new ContentSandwalkerCard();
        GameCard mechanizedBeastCard = new ContentMechanizedBeastCard();

        GameCard cureWoundsCard = new ContentCureWoundsCard();
        GameCard joltCard = new ContentJoltCard(); //nmartino - still needs a map

        GameCard drainingBoltCard = new ContentDrainingBoltCard();
        GameCard weakeningBoltCard = new ContentWeakeningBoltCard(); //nmartino - still needs a map

        GameCard staminaTrainingCard = new ContentStaminaTrainingCard();
        GameCard optimizeCard = new ContentOptimizeCard(); //nmartino - still needs a map

        GameRelic wolvenFangRelic = new ContentWolvenFangRelic();
        GameRelic orbOfEnergyRelic = new ContentOrbOfEnergyRelic();
        GameRelic loadedChestRelic = new ContentLoadedChestRelic();  //nmartino - still needs a map
        GameRelic hoovesOfProductionRelic = new ContentHoovesOfProductionRelic(); //nmartino - still needs a map

        FillMapData(deltaMap, lizardSoldierCard);
        FillMapData(mountainPass, sandwalkerCard);
        FillMapData(crimsonIslandsMap, cureWoundsCard);
        FillMapData(snowmeltMap, drainingBoltCard);
        FillMapData(desertPassMap, staminaTrainingCard);
        FillMapData(volcanoRun, undeadMammothCard);
        FillMapData(lakesideHardMap, mechanizedBeastCard);
        FillMapData(themarshlands, wolvenFangRelic);
        FillMapData(frozenLake, orbOfEnergyRelic);

        //2
        AddCardRewards();
        AddRelicRewards();

        m_mapRewards.Add(2, CreateMapLevelReward(deltaMap.GetBaseName(),
            deltaMap.GetDesc(),
            deltaMap));

        m_mapRewards.Add(4, CreateMapLevelReward(mountainPass.GetBaseName(),
            mountainPass.GetDesc(),
            mountainPass));

        m_mapRewards.Add(6, CreateMapLevelReward(volcanoRun.GetBaseName(),
            volcanoRun.GetDesc(),
            volcanoRun));

        m_mapRewards.Add(9, CreateMapLevelReward(snowmeltMap.GetBaseName(),
            snowmeltMap.GetDesc(),
            snowmeltMap));

        m_mapRewards.Add(13, CreateMapLevelReward(crimsonIslandsMap.GetBaseName(),
            crimsonIslandsMap.GetDesc(),
            crimsonIslandsMap));

        m_mapRewards.Add(17, CreateMapLevelReward(frozenLake.GetBaseName(),
            frozenLake.GetDesc(),
            frozenLake));

        m_mapRewards.Add(22, CreateMapLevelReward(themarshlands.GetBaseName(),
            themarshlands.GetDesc(),
            themarshlands));

        m_mapRewards.Add(29, CreateMapLevelReward(desertPassMap.GetBaseName(),
            desertPassMap.GetDesc(),
            desertPassMap));

        m_mapRewards.Add(35, CreateMapLevelReward(lakesideHardMap.GetBaseName(),
            lakesideHardMap.GetDesc(),
            lakesideHardMap));

        m_isInit = true;
    }

    private static void AddCardRewards()
    {
        //Creations - 2
        //Max Stamina - 3
        //Monsters - 4
        //Spellcraft- 5
        //Shivs - 6
        //Knowledgeable - 7
        //Humanoids - 8
        //Stamina Regen - 9
        //Terrain - 10
        //Tanks - 11
        //Reanimation - 12
        //Enrage - 13
        //Gold - 14
        //Buffs - 15
        //Buff Target - 16
        //Silver Bullets - 17
        //Flexible - 18

        m_cardRewards.Add(2, CreateCardLevelReward("Creations",
            "<b>Creation</b> units are tricksy. They are frequently reanimated, and use stats like maximum stamina for surprising sources of might!",
            new ContentGrasperCard(), new ContentOverlordCard(), new ContentSkeletonCard()));

        m_cardRewards.Add(3, CreateCardLevelReward("Max Stamina",
            "Increasing a unit's maximum stamina can be a strange source of power!",
            new ContentBatteryPackCard(), new ContentMechanizeCard(), new ContentOverchargeCard()));

        m_cardRewards.Add(4, CreateCardLevelReward("Monsters",
            "<b>Monster<b> units are generally larger, and they like <b>Enrage</b>, <b>Momentum</b>, and <b>Victorious</b>!",
            new ContentDevourerCard(), new ContentGoblinLegendCard(), new ContentFuryCard())); //nmartino - rework goblin to use keywords

        m_cardRewards.Add(5, CreateCardLevelReward("Spellcraft",
            "<b>Spellcraft</b> triggers whenever a spell is cast within range 3 of the unit with <b>Spellcraft</b>!",
            new ContentBonecasterCard(), new ContentInsightCard(), new ContentRunicBladeCard()));

        m_cardRewards.Add(6, CreateCardLevelReward("Shivs",
            "<b>Shivs</b> are 4 damage, 0 cost spells that can be generated by various effects!",
            new ContentDwarfShivcasterCard(), new ContentLegionOfBladesCard(), new ContentDwarvenSoldierCard())); //nmartino - need 1 more shiv card

        m_cardRewards.Add(7, CreateCardLevelReward("Knowledgeable",
            "<b>Knowledgeable</b> happens whenever you draw a card.  You'll need a lot of cards to make sure you can keep drawing every turn!",
            new ContentMageCard(), new ContentAncientTextsCard(), new ContentHomonculusCard()));

        m_cardRewards.Add(8, CreateCardLevelReward("Humanoids",
            "<b>Humanoids</b> are a flexible type; often supporting other types as much as their own!",
            new ContentGuardCaptainCard(), new ContentDwarfArchitectCard(), new ContentDwarvenSoldierCard())); //nmartino - need 1 more humanoid (that buffs monsters) here

        m_cardRewards.Add(9, CreateCardLevelReward("Stamina Regen",
            "Stamina is a critical resource; ways to restore it during combat are invaluable!",
            new ContentControlMoralCard(), new ContentDemonicAspectCard(), new ContentMonsterProdCard()));

        m_cardRewards.Add(10, CreateCardLevelReward("Terrain",
            "Some cards are much better when on or around certain kinds of terrain!",
            new ContentGroundskeeperCard(), new ContentMetalGolemCard(), new ContentDwarvenSoldierCard())); //nmartino - need 1 more cares about water unit here

        m_cardRewards.Add(11, CreateCardLevelReward("Tanks",
            "Hefty frontline troops can help prevent your enemies from reaching critical junctions!",
            new ContentStoneGolemCard(), new ContentSummoningCard(), new ContentTrollFormCard()));

        m_cardRewards.Add(12, CreateCardLevelReward("Reanimation",
            "When troops keep dying, the solution is simple. Bring them back!",
            new ContentReforgingCard(), new ContentBloodSacrificeCard(), new ContentSabobotCard()));

        m_cardRewards.Add(13, CreateCardLevelReward("Enrage",
            "<b>Enrage</b> triggers when the unit in question is hit!",
            new ContentEncouragementCard(), new ContentRoarOfVictoryCard(), new ContentBullheadedCard()));

        m_cardRewards.Add(14, CreateCardLevelReward("Gold",
            "Gaining enough money can allow you to power up with large kingdoms!",
            new ContentMinerCard(), new ContentBloodMoneyCard(), new ContentLootingsCard()));

        m_cardRewards.Add(15, CreateCardLevelReward("Buffs",
            "Use spells to augment your units natural abilities!",
            new ContentNightWingsCard(), new ContentDrainPowerCard(), new ContentCurseOfInactionCard()));

        m_cardRewards.Add(16, CreateCardLevelReward("Buff Targets",
            "Some units work great when given the right buff spell!",
            new ContentElvenSentinelCard(), new ContentConjuredImpCard(), new ContentRaptorCard()));

        m_cardRewards.Add(17, CreateCardLevelReward("Silver Bullets",
            "While these may be specific, when you can get good use out of them they pull far above their weight!",
            new ContentFletchingCard(), new ContentMarkedForDeathCard(), new ContentFossilizeCard()));

        m_cardRewards.Add(18, CreateCardLevelReward("Flexible",
            "Flexibility has a value of it's own!",
            new ContentFirestormCard(), new ContentPurgeCard(), new ContentWisdomOfThePastCard()));

        m_cardRewards.Add(19, CreateCardLevelReward("More Cards",
            "",
            new ContentWildfolkCard(), new ContentPirateCaptainCard(), new ContentFrogShamanCard()));

        m_cardRewards.Add(20, CreateCardLevelReward("More Cards",
            "",
            new ContentDeadeyeCard(), new ContentMarksmanCard(), new ContentFadeCard()));

        m_cardRewards.Add(21, CreateCardLevelReward("More Cards",
            "",
            new ContentDrainingTalonsCard(), new ContentToxicTonicCard(), new ContentDarkHeartCard()));

        m_cardRewards.Add(22, CreateCardLevelReward("More Cards",
            "",
            new ContentContellationsCard(), new ContentEndCard(), new ContentPathCard()));

        m_cardRewards.Add(23, CreateCardLevelReward("More Cards",
            "",
            new ContentExperienceCard(), new ContentRhinoProtectorCard(), new ContentDesertSwordsmanCard()));
    }

    private static void AddRelicRewards()
    {
        //Creations - 2
        //Max Stamina - 3
        //Monsters - 4
        //Spellcraft- 5
        //Shivs - 6
        //Knowledgeable - 7
        //Humanoids - 8
        //Stamina Regen - 9
        //Terrain - 10
        //Tanks - 11
        // - 12 - Feathers
        // - 13 - Full Types
        //Gold - 14
        //Buffs - 15
        // - 16 - Easy Attacks
        //Silver Bullets - 17
        //Flexible - 18
        // - 19 - Random
        // - 20 - Elite Hunting
        // - 21 - More Relics
        // - 22 - More Relics
        // - 23 - More Tanks
        // - 24 - More Relics
        // - 25 - More Relics

        m_relicRewards.Add(2, CreateRelicLevelReward("Creations",
            "<b>Creation</b> units are tricksy. They are frequently reanimated, and use stats like maximum stamina for surprising sources of might!",
            new ContentDesignSchematicsRelic(), new ContentInstructionsRelic(), new ContentVoiceOfTheDefenderRelic()));

        m_relicRewards.Add(3, CreateRelicLevelReward("Max Stamina",
            "Increasing a unit's maximum stamina can be a strange source of power!",
            new ContentBeaconOfSanityRelic(), new ContentPriceOfFreedomRelic(), new ContentTotemOfRevengeRelic()));

        m_relicRewards.Add(4, CreateRelicLevelReward("Monsters",
            "<b>Monster<b> units are generally larger, and they like <b>Enrage</b>, <b>Momentum</b>, and <b>Victorious</b>!",
            new ContentLegacyOfMonstersRelic(), new ContentPlagueMaskRelic(), new ContentBestialWrathRelic()));

        m_relicRewards.Add(5, CreateRelicLevelReward("Spellcraft",
            "<b>Spellcraft</b> triggers whenever a spell is cast within range 3 of the unit with <b>Spellcraft</b>!",
            new ContentLastHopeRelic(), new ContentProclamationOfSurrenderRelic(), new ContentTombOfTheDefenderRelic()));

        m_relicRewards.Add(6, CreateRelicLevelReward("Shivs",
            "<b>Shivs</b> are 4 damage, 0 cost spells that can be generated by various effects!",
            new ContentBurningShivsRelic(), new ContentPoisonedShivsRelic(), new ContentShardOfSorrowRelic()));

        m_relicRewards.Add(7, CreateRelicLevelReward("Knowledgeable",
            "<b>Knowledgeable</b> happens whenever you draw a card.  You'll need a lot of cards to make sure you can keep drawing every turn!",
            new ContentAncientMysteryRelic(), new ContentForbiddenKnowledge(), new ContentMaskOfSpeedRelic()));

        m_relicRewards.Add(8, CreateRelicLevelReward("Humanoids",
            "<b>Humanoids</b> are a flexible type; often supporting other types as much as their own!",
            new ContentMedKitRelic(), new ContentSigilOfTheSwordsmanRelic(), new ContentTokenOfFriendshipRelic()));

        m_relicRewards.Add(9, CreateRelicLevelReward("Stamina Regen",
            "Stamina is a critical resource; ways to restore it during combat are invaluable!",
            new ContentSecretSoupRelic(), new ContentIotalRelic(), new ContentLifebringerRelic()));

        m_relicRewards.Add(10, CreateRelicLevelReward("Terrain",
            "Some cards are much better when on or around certain kinds of terrain!",
            new ContentEyeOfMoragRelic(), new ContentEyeOfTelsimirRelic(), new ContentSecretOfTheDeepRelic()));

        m_relicRewards.Add(11, CreateRelicLevelReward("Tanks",
            "Hefty frontline troops can help prevent your enemies from reaching critical junctions!",
            new ContentThornsOfRayRelic(), new ContentFadingLightRelic(), new ContentDestinyRelic()));

        m_relicRewards.Add(12, CreateRelicLevelReward("Full Types",
            "If you can get all of the tribes together; the power is immense!",
            new ContentGrandPactRelic(), new ContentSymbolOfTheAllianceRelic(), new ContentToldiranMiracleRelic()));

        m_relicRewards.Add(13, CreateRelicLevelReward("Gold",
            "Gaining enough money can allow you to power up with large kingdoms!",
            new ContentSackOfSoulsRelic(), new ContentDiscountTokenRelic(), new ContentLivingStoneRelic()));

        m_relicRewards.Add(14, CreateRelicLevelReward("Buffs",
            "Use spells to augment your units natural abilities!",
            new ContentBeadofJoyRelic(), new ContentNectarOfTheSeaGodRelic(), new ContentEverflowingCanteenRelic()));

        m_relicRewards.Add(15, CreateRelicLevelReward("Easy Attacks",
            "Attacking is much easier when it costs 1 stamina instead of 2!",
            new ContentUrbanTacticsRelic(), new ContentNamelessFlaskRelic(), new ContentAncientRitualRelic()));

        m_relicRewards.Add(16, CreateRelicLevelReward("Silver Bullets",
            "While these may be specific, when you can get good use out of them they pull far above their weight!",
            new ContentTalonOfTheCruelRelic(), new ContentAdvancedWeaponryRelic(), new ContentAncientEvilRelic()));

        m_relicRewards.Add(17, CreateRelicLevelReward("Flexible",
            "Flexibility has a value of it's own!",
            new ContentGoldenKnotRelic(), new ContentSporetechRelic(), new ContentHistoryInBloodRelic()));

        m_relicRewards.Add(18, CreateRelicLevelReward("Random",
            "Sometimes, fate just takes control.",
            new ContentMysticRuneRelic(), new ContentJugOfTordrimRelic(), new ContentAlterOfTordrimRelic()));

        m_relicRewards.Add(19, CreateRelicLevelReward("Elite Hunting",
            "For when just a new relic isn't enough.",
            new ContentRelicOfVictoryRelic(), new ContentHeroicTrophyRelic(), new ContentAncientCoinsRelic()));

        m_relicRewards.Add(20, CreateRelicLevelReward("More Relics",
            "",
            new ContentTokenOfTheUprisingRelic(), new ContentSecretTiesRelic(), new ContentSachelOfDeceptionRelic()));

        m_relicRewards.Add(21, CreateRelicLevelReward("More Relics",
            "",
            new ContentTauntingPipeRelic(), new ContentBondOfFamilyRelic(), new ContentChargingRingRelic()));

        m_relicRewards.Add(22, CreateRelicLevelReward("More Tanks",
            "Even more toughness!",
            new ContentCarapaceOfTutuiun(), new ContentCanvasOfHistoryRelic(), new ContentPrimeRibRelic()));

        m_relicRewards.Add(23, CreateRelicLevelReward("More Relics",
            "",
            new ContentCursedAmuletRelic(), new ContentMemoryOfTheDefenderRelic(), new ContentBeadsOfProphecyRelic()));

        m_relicRewards.Add(24, CreateRelicLevelReward("More Relics",
            "",
            new ContentPinnacleOfFearRelic(), new ContentCursedAmuletRelic(), new ContentCursedAmuletRelic()));
    }

    private static void FillMapData(GameMap map, GameCard rewardCard)
    {
        m_dataElements.Add(new GameMetaprogressionDataElement(map, rewardCard));
    }

    private static void FillMapData(GameMap map, GameRelic rewardRelic)
    {
        m_dataElements.Add(new GameMetaprogressionDataElement(map, rewardRelic));
    }

    private static GameMetaprogressionReward CreateCardLevelReward(string title, string desc, GameCard card1, GameCard card2, GameCard card3)
    {
        List<GameCard> cards = new List<GameCard>();
        cards.Add(card1);
        cards.Add(card2);
        cards.Add(card3);

        return new GameMetaprogressionReward(title, desc, cards);
    }

    private static GameMetaprogressionReward CreateRelicLevelReward(string title, string desc, GameRelic relic1, GameRelic relic2, GameRelic relic3)
    {
        List<GameRelic> relics = new List<GameRelic>();
        relics.Add(relic1);
        relics.Add(relic2);
        relics.Add(relic3);

        return new GameMetaprogressionReward(title, desc, relics);
    }

    private static GameMetaprogressionReward CreateMapLevelReward(string title, string desc, GameMap map)
    {
        return new GameMetaprogressionReward(title, desc, map);
    }

    public static GameMetaprogressionDataElement GetReward(int mapId)
    {
        if (!m_isInit)
        {
            InitData();
        }

        for (int i = 0; i < m_dataElements.Count; i++)
        {
            if (m_dataElements[i].GetMap().m_id == mapId)
            {
                return m_dataElements[i];
            }
        }

        return null;
    }

    public static void CompleteMapAtChaosFirstTime(int mapId, int chaosNum)
    {
        PlayerAccountData accountData = PlayerDataManager.PlayerAccountData;

        if (mapId == 0 && chaosNum == 0)
        {
            accountData.m_altarsUnlockedOnAccount = true;
        }
        
        for (int i = 0; i < m_dataElements.Count; i++)
        {
            if (m_dataElements[i].GetMap().m_id == mapId)
            {
                if (chaosNum == 2)
                {
                    GameCard card = m_dataElements[i].GetCard();
                    if (card != null)
                    {
                        accountData.m_starterCardUnlockLevels.Add(card.GetBaseName(), 0);
                        UIMetaprogressionNotificationController.AddReward(
                            new GameMetaprogressionReward("Starter Card",
                            "A new card option for use in your starter deck.",
                            card));
                    }

                    GameRelic relic = m_dataElements[i].GetRelic();
                    if (relic != null)
                    {
                        accountData.m_starterRelicUnlockLevels.Add(relic.GetBaseName(), 0);
                        UIMetaprogressionNotificationController.AddReward(
                            new GameMetaprogressionReward("Starter Relic",
                            "A new relic option you can start with.",
                            relic));
                    }
                }
            }
        }
    }
    
    private static void FillInitialUnlocks()
    {
        //Starter Cards
        m_initialCards.Add(new ContentDwarvenSoldierCard());
        m_initialCards.Add(new ContentAlphaBoarCard());
        m_initialCards.Add(new ContentFireboltCard());
        m_initialCards.Add(new ContentAegisCard());
        m_initialCards.Add(new ContentGrowTalonsCard());

        //Starter Relics
        m_initialRelics.Add(new ContentMaskOfAgesRelic());

        //Initial Maps
        m_initialMaps.Add(new ContentLakesideMap());

        //Initial Cards
        //Units
        m_initialCards.Add(new ContentElvenWizardCard());
        m_initialCards.Add(new ContentElvenRogueCard());
        m_initialCards.Add(new ContentGladiatorCard());
        m_initialCards.Add(new ContentHeroCard());
        m_initialCards.Add(new ContentInjuredTrollCard());
        m_initialCards.Add(new ContentNaturalScoutCard());
        m_initialCards.Add(new ContentRangerCard());
        m_initialCards.Add(new ContentShadowWarlockCard());
        m_initialCards.Add(new ContentWandererCard());
        m_initialCards.Add(new ContentCyclopsCard());
        m_initialCards.Add(new ContentArmouredMonkCard());
        m_initialCards.Add(new ContentMetalProtectorCard());
        m_initialCards.Add(new ContentPolarHunterCard());
        m_initialCards.Add(new ContentMountainBeastCard());
        m_initialCards.Add(new ContentPyromageCard());

        //Spells
        m_initialCards.Add(new ContentArcaneBoltCard());
        m_initialCards.Add(new ContentAssassinationContractCard());
        m_initialCards.Add(new ContentCosmicPactCard());
        m_initialCards.Add(new ContentDreamCard());
        m_initialCards.Add(new ContentEnergizeCard());
        m_initialCards.Add(new ContentImmolationCard());
        m_initialCards.Add(new ContentLootingsCard());
        m_initialCards.Add(new ContentNightWingsCard());
        m_initialCards.Add(new ContentNecromanticTouchCard());
        m_initialCards.Add(new ContentPhalanxCard());
        m_initialCards.Add(new ContentMeteorCard());
        m_initialCards.Add(new ContentChainLightningCard());
        m_initialCards.Add(new ContentTwinCard());
        m_initialCards.Add(new ContentWillOfNatureCard());
        m_initialCards.Add(new ContentBurningStormCard());
        m_initialCards.Add(new ContentTonicOfFortitudeCard());
        m_initialCards.Add(new ContentTonicOfStrengthCard());
        m_initialCards.Add(new ContentSprintCard());
        m_initialCards.Add(new ContentMageArmorCard());
        m_initialCards.Add(new ContentGreedyKillCard());
        m_initialCards.Add(new ContentBladesCard());
        m_initialCards.Add(new ContentGrowthCard());
        m_initialCards.Add(new ContentMoonbeamCard());
        m_initialCards.Add(new ContentProductionCard());
        m_initialCards.Add(new ContentHeroismCard());
        m_initialCards.Add(new ContentQuickStrikesCard());
        m_initialCards.Add(new ContentBrokenLinkCard());
        m_initialCards.Add(new ContentProtectionCard());
        m_initialCards.Add(new ContentFireworksCard());
        m_initialCards.Add(new ContentCometOfThePastCard());

        //Initial Relics
        m_initialRelics.Add(new ContentHourglassOfSpeedRelic());
        m_initialRelics.Add(new ContentMorlemainsSkullRelic());
        m_initialRelics.Add(new ContentOrbOfHealthRelic());
        m_initialRelics.Add(new ContentSoulTrapRelic());
        m_initialRelics.Add(new ContentSpiritCatcherRelic());
        m_initialRelics.Add(new ContentSackOfManyShapesRelic());
        m_initialRelics.Add(new ContentLegendaryFragmentRelic());
        m_initialRelics.Add(new ContentCallOfTheSeaRelic());
        m_initialRelics.Add(new ContentTheGreatestGiftRelic());
        m_initialRelics.Add(new ContentImpaliumRelic());
        m_initialRelics.Add(new ContentStarOfDenumainRelic());
        m_initialRelics.Add(new ContentHarvestOfTelumRelic());
        m_initialRelics.Add(new ContentTailOfLifeRelic());
        m_initialRelics.Add(new ContentSecretsOfNatureRelic());
        m_initialRelics.Add(new ContentTalonOfTheMeradominRelic());
        m_initialRelics.Add(new ContentHealthFlaskRelic());
        m_initialRelics.Add(new ContentToolOfTheDeadmanRelic());
        m_initialRelics.Add(new ContentAngelicFeatherRelic());
        m_initialRelics.Add(new ContentBloodFeatherRelic());
        m_initialRelics.Add(new ContentGoldenFeatherRelic());
        m_initialRelics.Add(new ContentNaturalProtectionRelic());
        m_initialRelics.Add(new ContentMarkOfTordrimRelic());
    }

    public static bool HasUnlocked(GameRelic toCheck)
    {
        if (!m_isInit)
        {
            InitData();
        }

        if (IsInitialUnlock(toCheck))
        {
            return true;
        }

        List<GameMetaprogressionReward> obtainedRewards = new List<GameMetaprogressionReward>();
        for (int i = 0; i < PlayerDataManager.GetCurLevel() + 1; i++)
        {
            if (m_relicRewards.ContainsKey(i))
            {
                obtainedRewards.Add(m_relicRewards[i]);
            }
        }

        for (int i = 0; i < obtainedRewards.Count; i++)
        {
            if (obtainedRewards[i].HasRelic(toCheck))
            {
                return true;
            }
        }

        return false;
    }

    public static bool HasUnlocked(GameCard toCheck)
    {
        if (!m_isInit)
        {
            InitData();
        }

        if (IsInitialUnlock(toCheck))
        {
            return true;
        }

        List<GameMetaprogressionReward> obtainedRewards = new List<GameMetaprogressionReward>();
        for (int i = 0; i < PlayerDataManager.GetCurLevel() + 1; i++)
        {
            if (m_cardRewards.ContainsKey(i))
            {
                obtainedRewards.Add(m_cardRewards[i]);
            }
        }

        for (int i = 0; i < obtainedRewards.Count; i++)
        {
            if (obtainedRewards[i].HasCard(toCheck))
            {
                return true;
            }
        }

        return false;
    }

    public static bool HasUnlocked(GameMap toCheck)
    {
        if (!m_isInit)
        {
            InitData();
        }

        if (IsInitialUnlock(toCheck))
        {
            return true;
        }

        List<GameMetaprogressionReward> obtainedRewards = new List<GameMetaprogressionReward>();
        for (int i = 0; i < PlayerDataManager.GetCurLevel()+1; i++)
        {
            if (m_mapRewards.ContainsKey(i))
            {
                obtainedRewards.Add(m_mapRewards[i]);
            }
        }

        for (int i = 0; i < obtainedRewards.Count; i++)
        {
            if (obtainedRewards[i].HasMap(toCheck))
            {
                return true;
            }
        }

        return false;
    }

    public static bool HasUnlockedStarterCard(GameCard toCheck)
    {
        if (!m_isInit)
        {
            InitData();
        }

        if (IsInitialUnlock(toCheck))
        {
            return true;
        }

        GameMap map = GetMapForStarterCard(toCheck);

        if (map == null)
        {
            return false;
        }

        if (PlayerDataManager.PlayerAccountData.HasPreviouslyBeatenMapChaosLevel(map.m_id, 2))
        {
            return true;
        }

        return false;
    }

    public static bool HasUnlockedStarterRelic(GameRelic toCheck)
    {
        if (!m_isInit)
        {
            InitData();
        }

        if (IsInitialUnlock(toCheck))
        {
            return true;
        }

        GameMap map = GetMapForStarterRelic(toCheck);

        if (map == null)
        {
            return false;
        }

        if (PlayerDataManager.PlayerAccountData.HasPreviouslyBeatenMapChaosLevel(map.m_id, 2))
        {
            return true;
        }

        return false;
    }

    public static GameMap GetMapForStarterCard(GameCard toCheck)
    {
        for (int i = 0; i < m_dataElements.Count; i++)
        {
            if (m_dataElements[i].GetCard() != null && m_dataElements[i].GetCard().GetBaseName() == toCheck.GetBaseName())
            {
                return m_dataElements[i].GetMap();
            }
        }
        return null;
    }

    public static GameMap GetMapForStarterRelic(GameRelic toCheck)
    {
        for (int i = 0; i < m_dataElements.Count; i++)
        {
            if (m_dataElements[i].GetRelic() != null && m_dataElements[i].GetRelic().GetBaseName() == toCheck.GetBaseName())
            {
                return m_dataElements[i].GetMap();
            }
        }
        return null;
    }

    public static List<GameMetaprogressionReward> GetRewardsForLevel(int level)
    {
        List<GameMetaprogressionReward> rewards = new List<GameMetaprogressionReward>();

        if (m_cardRewards.ContainsKey(level))
        {
            rewards.Add(m_cardRewards[level]);
        }

        if (m_relicRewards.ContainsKey(level))
        {
            rewards.Add(m_relicRewards[level]);
        }

        if (m_mapRewards.ContainsKey(level))
        {
            rewards.Add(m_mapRewards[level]);
        }
        
        return rewards;
    }
    
    private static bool IsInitialUnlock(GameCard toCheck)
    {
        for (int i = 0; i < m_initialCards.Count; i++)
        {
            if (m_initialCards[i].GetBaseName() == toCheck.GetBaseName())
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsInitialUnlock(GameRelic toCheck)
    {
        for (int i = 0; i < m_initialRelics.Count; i++)
        {
            if (m_initialRelics[i].GetBaseName() == toCheck.GetBaseName())
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsInitialUnlock(GameMap toCheck)
    {
        for (int i = 0; i < m_initialMaps.Count; i++)
        {
            if (m_initialMaps[i].m_id == toCheck.m_id)
            {
                return true;
            }
        }

        return false;
    }

    public static int GetNumStarterCardsUnlocked()
    {
        if (!m_isInit)
        {
            InitData();
        }

        List<GameCard> starterCards = new List<GameCard>();

        for (int i = 0; i < m_dataElements.Count; i++)
        {
            if (m_dataElements[i].GetCard() != null)
            {
                starterCards.Add(m_dataElements[i].GetCard());
            }
        }

        if (Constants.UnlockAllContent)
        {
            return starterCards.Count;
        }

        int toReturn = 0;
        for (int i = 0; i < starterCards.Count; i++)
        {
            if (HasUnlockedStarterCard(starterCards[i]))
            {
                toReturn++;
            }
        }

        return toReturn;
    }

    public static int GetNumStarterRelicsUnlocked()
    {
        if (!m_isInit)
        {
            InitData();
        }

        List<GameRelic> starterRelics = new List<GameRelic>();

        for (int i = 0; i < m_dataElements.Count; i++)
        {
            if (m_dataElements[i].GetRelic() != null)
            {
                starterRelics.Add(m_dataElements[i].GetRelic());
            }
        }

        if (Constants.UnlockAllContent)
        {
            return starterRelics.Count;
        }

        int toReturn = 0;
        for (int i = 0; i < starterRelics.Count; i++)
        {
            if (HasUnlockedStarterRelic(starterRelics[i]))
            {
                toReturn++;
            }
        }

        return toReturn;
    }
}
