using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedDirtPlainsTerrain : GameTerrainBase
{
    public ContentDesertRedDirtPlainsTerrain()
    {
        m_costToPass = 1;
        
        m_name = "DesertRedDirtPlains";
        m_desc = "Simple, no changes.";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);
        m_color = Color.yellow;

        m_isPassable = true;
        m_isPlains = true;
        m_canBurn = false;

        m_isHot = true;

        m_unburnedTerrainType = typeof(ContentDesertRedGrassPlainsTerrain);
        m_addedEventType = typeof(ContentDesertRedDirtPlainsRuinsTerrain);

        LateInit();
    }
}
