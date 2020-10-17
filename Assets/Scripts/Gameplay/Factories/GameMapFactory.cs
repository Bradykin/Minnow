using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMapFactory
{
    private static bool m_hasInit;

    public static List<GameMap> m_maps = new List<GameMap>();

    public static void Init()
    {
        m_hasInit = true;

        m_maps.Add(new ContentLakesideMap());
        m_maps.Add(new ContentLakesideHardMap());
        m_maps.Add(new ContentSnowmeltMap());
        m_maps.Add(new ContentDesertPassMap());
        m_maps.Add(new ContentCrimsonIslandsMap());
        m_maps.Add(new ContentDeltaMap());
    }

    public static GameMap GetMapById(int id)
    {
        if (!m_hasInit)
        {
            Init();
        }

        GameMap returnMap = null;

        for (int i = 0; i < m_maps.Count; i++)
        {
            if (m_maps[i].m_id == id)
            {
                if (returnMap != null)
                {
                    Debug.LogError("Multiple maps with the same id.  Critical to fix this. Maps: " + returnMap.m_name + ", " + m_maps[i].m_name + ".");
                    return returnMap;
                }
                else
                {
                    returnMap = m_maps[i];
                }
            }
        }

        return returnMap;
    }
}
