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
        GameCard joltCard= new ContentJoltCard();

        GameCard drainingBoltCard = new ContentDrainingBoltCard();
        GameCard weakeningBoltCard = new ContentWeakeningBoltCard();

        GameCard staminaTrainingCard = new ContentStaminaTrainingCard();
        GameCard optimizeCard = new ContentOptimizeCard(); //nmartino add optimize to a map (need 5 more maps + final map)

        FillMapData(deltaMap.m_id, lizardSoldierCard);
        FillMapData(mountainPass.m_id, sandwalkerCard);
        FillMapData(crimsonIslandsMap.m_id, cureWoundsCard);
        FillMapData(snowmeltMap.m_id, drainingBoltCard);
        FillMapData(desertPassMap.m_id, staminaTrainingCard);
        FillMapData(volcanoRun.m_id, undeadMammothCard);
        FillMapData(lakesideHardMap.m_id, mechanizedBeastCard);
        FillMapData(themarshlands.m_id, joltCard);
        FillMapData(frozenLake.m_id, weakeningBoltCard);

        m_isInit = true;
    }

    private static void FillMapData(int mapId, GameCard rewardCard)
    {
        int unlockReward = 1;
        int bonusExp1 = 2;
        int upgradeReward1 = 3;
        int bonusExp2 = 4;
        int upgradeReward2 = 5;

        m_dataElements.Add(new GameMetaprogressionDataElement(mapId, unlockReward, rewardCard, 0));
        m_dataElements.Add(new GameMetaprogressionDataElement(mapId, bonusExp1, 250));
        m_dataElements.Add(new GameMetaprogressionDataElement(mapId, upgradeReward1, rewardCard, 1));
        m_dataElements.Add(new GameMetaprogressionDataElement(mapId, bonusExp2, 500));
        m_dataElements.Add(new GameMetaprogressionDataElement(mapId, upgradeReward2, rewardCard, 2));
    }

    public static Sprite GetIconForMapAndChaos(int mapId, int chaosNum)
    {
        if (!m_isInit)
        {
            InitData();
        }

        for (int i = 0; i < m_dataElements.Count; i++)
        {
            if (m_dataElements[i].GetMapId() == mapId && m_dataElements[i].GetChaosLevel() == chaosNum)
            {
                return m_dataElements[i].GetIcon();
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
            if (m_dataElements[i].GetMapId() == mapId && m_dataElements[i].GetChaosLevel() == chaosNum)
            {
                bonusExpAmount += m_dataElements[i].GetBonusExp();

                GameCard card = m_dataElements[i].GetCard();
                if (card != null)
                {
                    if (accountData.m_starterCardUnlockLevels.ContainsKey(card.GetBaseName()))
                    {
                        accountData.m_starterCardUnlockLevels[card.GetBaseName()] = Mathf.Max(accountData.m_starterCardUnlockLevels[card.GetBaseName()], m_dataElements[i].GetLevel());
                    }
                    else
                    {
                        accountData.m_starterCardUnlockLevels.Add(card.GetBaseName(), m_dataElements[i].GetLevel());
                    }
                }
            }
        }
    }
}
