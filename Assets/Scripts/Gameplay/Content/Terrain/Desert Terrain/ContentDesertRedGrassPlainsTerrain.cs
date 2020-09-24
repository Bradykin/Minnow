using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedGrassPlainsTerrain : GameTerrainBase
{
    public ContentDesertRedGrassPlainsTerrain()
    {
        m_costToPass = 1;

        m_name = "DesertRedGrassPlains";
        m_desc = "Simple, no changes.";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);
        m_color = Color.white;

        m_isPassable = true;
        m_isPlains = true;
        m_canBurn = true;

        m_isHot = true;

        m_burnedTerrainType = typeof(ContentDesertRedDirtPlainsTerrain);
        m_addedEventType = typeof(ContentDesertRedGrassPlainsRuinsTerrain);

        LateInit();
    }
}
