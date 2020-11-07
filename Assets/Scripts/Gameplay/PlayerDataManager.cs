﻿using System.Collections.Generic;
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
        int previousLevel = GetCurLevel();

        PlayerAccountData.m_playerExperience += experienceAmount;
        PlayerAccountData.m_numPlaySessions++;

        if (endType == RunEndType.Win)
        {
            if (!PlayerAccountData.m_mapChaosLevels.ContainsKey(mapID))
            {
                PlayerAccountData.m_mapChaosLevels.Add(mapID, curChaos);
            }
            else if (PlayerAccountData.m_mapChaosLevels[mapID] < curChaos)
            {
                PlayerAccountData.m_mapChaosLevels[mapID] = curChaos;
            }

            if (PlayerDataManager.PlayerAccountData.m_mapChaosUIAutoset.ContainsKey(mapID))
            {
                PlayerDataManager.PlayerAccountData.m_mapChaosUIAutoset[mapID] = Globals.m_curChaos + 1;
            }
            else
            {
                PlayerDataManager.PlayerAccountData.m_mapChaosUIAutoset.Add(mapID, Globals.m_curChaos + 1);
            }
        }

        int curLevel = GetCurLevel();

        if (curLevel > previousLevel)
        {
            for (int i = previousLevel + 1; i <= curLevel; i++)
            {
                //Do levelup things

                List<GameCard> newCardUnlocks = GameCardFactory.GetTotalCardList().Where(c => c.GetPlayerUnlockLevel() == i).ToList();
            }
        }
    }
}
