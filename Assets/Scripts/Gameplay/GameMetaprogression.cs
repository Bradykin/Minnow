using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameMetaProgression
{
    public static GamePlayerSaveData GamePlayerSaveData
    {
        get
        {
            if (m_gamePlayerSaveData == null)
            {
                m_gamePlayerSaveData = GameFiles.ImportPlayerSaveData();
            }
            return m_gamePlayerSaveData;
        }
        set
        {
            m_gamePlayerSaveData = value;
        }
    }
    private static GamePlayerSaveData m_gamePlayerSaveData;

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

        return Mathf.FloorToInt((float)(GamePlayerSaveData.m_playerExperience)/1000.0f);
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

        if (card.m_unlockLevel <= GetCurLevel())
        {
            return true;
        }

        return false;
    }

    public static void UpdatePlayerSaveDataOnEndPlaythrough(PlaythroughEndType endType, int experienceAmount, int mapID, int curChaos)
    {
        int previousLevel = GetCurLevel();

        GamePlayerSaveData.m_playerExperience += experienceAmount;
        GamePlayerSaveData.m_numPlaySessions++;

        if (endType == PlaythroughEndType.Win)
        {
            if (!GamePlayerSaveData.m_mapChaosLevels.ContainsKey(mapID))
            {
                GamePlayerSaveData.m_mapChaosLevels.Add(mapID, curChaos);
            }
            else if (GamePlayerSaveData.m_mapChaosLevels[mapID] < curChaos)
            {
                GamePlayerSaveData.m_mapChaosLevels[mapID] = curChaos;
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
