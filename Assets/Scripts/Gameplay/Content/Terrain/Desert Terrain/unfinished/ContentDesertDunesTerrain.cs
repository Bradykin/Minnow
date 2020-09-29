using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertDunesTerrain : GameTerrainBase
{
    public ContentDesertDunesTerrain()
    {
        m_costToPass = 2;

        m_name = "DesertDunes";
        m_desc = "2 AP movement.";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_canBurn = false;

        m_isHot = true;

        m_addedEventType = typeof(ContentDesertDunesRuinsTerrain);

        LateInit();
    }
}
