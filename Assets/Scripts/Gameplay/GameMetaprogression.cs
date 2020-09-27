using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMetaprogression
{
    public static int m_curExp = 0;

    public static int GetCurLevel()
    {
        return Mathf.FloorToInt((float)(m_curExp)/1000.0f);
    }

    public static bool IsMapUnlocked(int mapId)
    {
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
        if (card.m_unlockLevel <= GetCurLevel())
        {
            return true;
        }

        return false;
    }

    //Design -- Gain exp during a run, add it after the run to the metaprogression
}
