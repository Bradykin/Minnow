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

    public static void InitData()
    {
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

        FillMapData(deltaMap.m_id, lizardSoldierCard);
        FillMapData(mountainPass.m_id, sandwalkerCard);
        FillMapData(crimsonIslandsMap.m_id, cureWoundsCard);
        FillMapData(snowmeltMap.m_id, drainingBoltCard);
        FillMapData(desertPassMap.m_id, staminaTrainingCard);
        FillMapData(volcanoRun.m_id, undeadMammothCard);
        FillMapData(lakesideHardMap.m_id, mechanizedBeastCard);
        FillMapData(themarshlands.m_id, wolvenFangRelic);
        FillMapData(frozenLake.m_id, orbOfEnergyRelic);

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
        /*Default Unlocks
        //Units (4, 3, 3)
        ContentElvenWizardCard, ContentElvenRogueCard, ContentGladiatorCard, 
        ContentHeroCard, ContentInjuredTrollCard, ContentNaturalScoutCard, 
        ContentRangerCard, ContentShadowWarlockCard, ContentWandererCard,
        ContentCyclopsCard

        //Spells (4, 3, 3)
        ContentArcaneBoltCard, ContentAssassinationContractCard, ContentCosmicPactCard, 
        ContentDreamCard, ContentEnergizeCard, ContentImmolationCard,
        ContentLootingsCard, ContentNightWingsCard, ContentNecromanticTouchCard,
        ContentPhalanxCard
        */

        m_cardRewards.Add(2, CreateCardLevelReward("Enrage",
            "<b>Enrage</b> triggers whenever the unit gets hit by anything!",
            new ContentDevourerCard(), new ContentHeroCard(), new ContentFuryCard()));
    }

    private static void AddRelicRewards()
    {
        /*Default Unlocks (9, 5, 4)
        ContentHourglassOfSpeedRelic, ContentMorlemainsSkullRelic, ContentOrbOfHealthRelic,
        ContentSoulTrapRelic, ContentSpiritCatcherRelic, ContentSackOfManyShapesRelic,
        ContentLegendaryFragmentRelic, ContentCallOfTheSeaRelic, ContentTheGreatestGiftRelic,
        ContentImpaliumRelic, ContentStarOfDenumainRelic, ContentHarvestOfTelumRelic,
        ContentTailOfLifeRelic, ContentSecretsOfNatureRelic, ContentEyeOfDorosonRelic,
        ContentTalonOfTheMeradominRelic, ContentHealthFlaskRelic, ContentToolOfTheDeadmanRelic

        */

        m_relicRewards.Add(2, CreateRelicLevelReward("Stamina",
            "Normally, units regenerate Stamina each turn equal to the number of green dots on their card!",
            new ContentIotalRelic(), new ContentLegacyOfMonstersRelic(), new ContentLegendaryFragmentRelic()));
    }

    private static void FillMapData(int mapId, GameCard rewardCard)
    {
        m_dataElements.Add(new GameMetaprogressionDataElement(mapId, rewardCard));
    }

    private static void FillMapData(int mapId, GameRelic rewardRelic)
    {
        m_dataElements.Add(new GameMetaprogressionDataElement(mapId, rewardRelic));
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
            if (m_dataElements[i].GetMapId() == mapId)
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
            if (m_dataElements[i].GetMapId() == mapId)
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

    public static bool HasUnlocked(GameRelic toCheck)
    {
        return false;
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
}
