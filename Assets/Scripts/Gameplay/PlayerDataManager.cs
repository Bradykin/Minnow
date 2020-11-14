using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerDataManager
{
    public static PlayerAccountData PlayerAccountData
    {
        get
        {
            if (m_playerAccountData == null)
            {
                m_playerAccountData = Files.ImportPlayerAccountData();
            }
            return m_playerAccountData;
        }
        set
        {
            m_playerAccountData = value;
        }
    }
    private static PlayerAccountData m_playerAccountData;

    public static bool m_hasInit;

    public static void Init()
    {
        m_hasInit = true;
    }

    public static int GetCurLevel()
    {
        if (!m_hasInit)
        {
            Init();
        }

        return Mathf.FloorToInt((float)(PlayerAccountData.m_playerExperience)/1000.0f) + 1;
    }

    public static (int, int) GetProgressToNextLevel()
    {
        return ((int)(PlayerAccountData.m_playerExperience % 1000), 1000);
    }

    public static bool IsChaosLevelAchieved(int mapId, int chaosLevel)
    {
        if (!m_hasInit)
        {
            Init();
        }

        if (!PlayerAccountData.m_mapChaosLevels.ContainsKey(mapId))
        {
            return false;
        }

        return PlayerAccountData.m_mapChaosLevels[mapId] >= chaosLevel;
    }

    public static bool IsMapUnlocked(int mapId)
    {
        if (!m_hasInit)
        {
            Init();
        }

        if (mapId == 0)
        {
            return GetCurLevel() >= 1;
        }
        else if (mapId == 1 || mapId == 2)
        {
            return GetCurLevel() >= 2;
        }
        else if (mapId == 3 || mapId == 4)
        {
            return GetCurLevel() >= 5;
        }
        else if (mapId == 5 || mapId == 6)
        {
            return GetCurLevel() >= 8;
        }

        return false;
    }

    public static bool IsCardUnlocked(GameCard card)
    {
        if (!m_hasInit)
        {
            Init();
        }

        if (card.PlayerHasUnlockedCard())
        {
            return true;
        }

        return false;
    }

    public static void UpdatePlayerAccountDataOnEndRun(RunEndType endType, int experienceAmount, int mapID, int curChaos)
    {
        if (endType == RunEndType.Win)
        {
            bool firstTimeCompleteChaosLevel = false;
            
            if (!PlayerAccountData.m_mapChaosLevels.ContainsKey(mapID))
            {
                PlayerAccountData.m_mapChaosLevels.Add(mapID, curChaos);
                firstTimeCompleteChaosLevel = true;
            }
            else if (PlayerAccountData.m_mapChaosLevels[mapID] < curChaos)
            {
                PlayerAccountData.m_mapChaosLevels[mapID] = curChaos;
                firstTimeCompleteChaosLevel = true;
            }

            if (firstTimeCompleteChaosLevel)
            {
                GameMetaprogressionUnlocksDataManager.CompleteMapAtChaosFirstTime(mapID, curChaos, out int bonusExpAmount);
                experienceAmount += bonusExpAmount;

                if (Globals.m_curChaos <= 4 && mapID != 0)
                {
                    if (PlayerDataManager.PlayerAccountData.m_mapChaosUIAutoset.ContainsKey(mapID))
                    {
                        PlayerDataManager.PlayerAccountData.m_mapChaosUIAutoset[mapID] = Globals.m_curChaos + 1;
                    }
                    else
                    {
                        PlayerDataManager.PlayerAccountData.m_mapChaosUIAutoset.Add(mapID, Globals.m_curChaos + 1);
                    }
                }
            }
        }

        HandleEXPGain(experienceAmount);
        PlayerAccountData.m_numPlaySessions++;
        PlayerAccountData.ClearRunData();
    }

    public static void HandleEXPGain(int toGain)
    {
        int previousLevel = GetCurLevel();

        PlayerAccountData.m_playerExperience += toGain;

        int curLevel = GetCurLevel();

        if (curLevel > previousLevel)
        {
            for (int i = previousLevel + 1; i <= curLevel; i++)
            {
                List<GameMetaprogressionReward> rewards = GameMetaprogressionUnlocksDataManager.GetRewardsForLevel(i);

                for (int c = 0; c < rewards.Count; c++)
                {
                    UIMetaprogressionNotificationController.AddReward(rewards[c]);
                }
            }
        }
    }

    public static void ClearPlayerAccountData()
    {
        PlayerAccountData = null;
    }
}
