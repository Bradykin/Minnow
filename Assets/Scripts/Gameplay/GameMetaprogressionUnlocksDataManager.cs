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

        m_mapRewards.Add(3, CreateMapLevelReward(mountainPass.GetBaseName(),
            mountainPass.GetDesc(),
            mountainPass));

        m_mapRewards.Add(6, CreateMapLevelReward(volcanoRun.GetBaseName(),
            volcanoRun.GetDesc(),
            volcanoRun));

        m_mapRewards.Add(10, CreateMapLevelReward(snowmeltMap.GetBaseName(),
            snowmeltMap.GetDesc(),
            snowmeltMap));

        m_mapRewards.Add(15, CreateMapLevelReward(crimsonIslandsMap.GetBaseName(),
            crimsonIslandsMap.GetDesc(),
            crimsonIslandsMap));

        m_mapRewards.Add(21, CreateMapLevelReward(frozenLake.GetBaseName(),
            frozenLake.GetDesc(),
            frozenLake));

        m_mapRewards.Add(28, CreateMapLevelReward(themarshlands.GetBaseName(),
            themarshlands.GetDesc(),
            themarshlands));

        m_mapRewards.Add(38, CreateMapLevelReward(desertPassMap.GetBaseName(),
            desertPassMap.GetDesc(),
            desertPassMap));

        m_mapRewards.Add(45, CreateMapLevelReward(lakesideHardMap.GetBaseName(),
            lakesideHardMap.GetDesc(),
            lakesideHardMap));

        m_isInit = true;
    }

    private static void AddCardRewards()
    {
        m_cardRewards.Add(2, CreateCardLevelReward("Enrage",
            "<b>Enrage</b> triggers whenever the unit gets hit by anything!",
            new ContentBloodSacrificeCard(), new ContentAncientTextsCard(), new ContentFuryCard()));
    }

    private static void AddRelicRewards()
    {
        m_relicRewards.Add(2, CreateRelicLevelReward("Stamina",
            "Normally, units regenerate Stamina each turn equal to the number of green dots on their card!",
            new ContentIotalRelic(), new ContentLegacyOfMonstersRelic(), new ContentLegendaryFragmentRelic()));
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

    public static void CompleteMapAtChaosFirstTime(int mapId, int chaosNum, out int bonusExpAmount)
    {
        bonusExpAmount = 0;
        PlayerAccountData accountData = PlayerDataManager.PlayerAccountData;

        if (mapId == 0 && chaosNum == 0)
        {
            accountData.m_altarsUnlockedOnAccount = true;
        }
        
        for (int i = 0; i < m_dataElements.Count; i++)
        {
            if (m_dataElements[i].GetMap().m_id == mapId)
            {
                bonusExpAmount += m_dataElements[i].GetBonusExp();

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
        m_initialRelics.Add(new ContentEyeOfDorosonRelic());
        m_initialRelics.Add(new ContentTalonOfTheMeradominRelic());
        m_initialRelics.Add(new ContentHealthFlaskRelic());
        m_initialRelics.Add(new ContentToolOfTheDeadmanRelic());
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
}
