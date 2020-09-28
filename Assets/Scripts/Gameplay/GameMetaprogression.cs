using System.Collections;
using System.Collections.Generic;
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
        m_gamePlayerSaveData = GameFiles.ImportPlayerSaveData();
        m_gamePlayerSaveData.m_numPlaySessions++;

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

    //Design -- Gain exp during a run, add it after the run to the metaprogression
}
