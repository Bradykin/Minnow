using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMetaprogressionUnlocksDataManager
{
    private static bool m_isInit;

    private static List<GameMetaprogressionDataElement> m_dataElements = new List<GameMetaprogressionDataElement>();

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

        m_isInit = true;
    }

    private static void FillMapData(int mapId, GameCard rewardCard)
    {
        m_dataElements.Add(new GameMetaprogressionDataElement(mapId, rewardCard));
    }

    private static void FillMapData(int mapId, GameRelic rewardRelic)
    {
        m_dataElements.Add(new GameMetaprogressionDataElement(mapId, rewardRelic));
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

                GameCard card = m_dataElements[i].GetCard();
                if (card != null)
                {
                    accountData.m_starterCardUnlockLevels.Add(card.GetBaseName(), 0);
                }
            }
        }
    }

    public static bool HasUnlocked(GameRelic toCheck)
    {
        return false;
    }
}
