using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedDirtPlainsTerrain : GameTerrainBase
{
    public ContentDesertRedDirtPlainsTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "DesertRedDirtPlains";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_canBurn = false;
        m_isHot = true;

        m_unburnedTerrainType = typeof(ContentDesertRedGrassPlainsTerrain);
        m_addedEventTerrainType = typeof(ContentDesertRedDirtPlainsRuinsTerrain);

        LateInit();
    }
}
