using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedDirtPlainsRuinsTerrain : GameTerrainBase
{
    public ContentDesertRedDirtPlainsRuinsTerrain()
    {
        m_costToPass = 2;
        
        m_name = "DesertRedDirtPlainsRuins";
        m_desc = "2 AP movement.";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_canBurn = false;

        m_isHot = true;
        m_isEventTerrain = true;

        m_unburnedTerrainType = typeof(ContentDesertRedGrassPlainsTerrain);
        m_completedEventType = typeof(ContentDesertRedDirtPlainsTerrain);

        LateInit();
    }
}
