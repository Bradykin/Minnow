using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedGrassPlainsTerrain : GameTerrainBase
{
    public ContentDesertRedGrassPlainsTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "DesertRedGrassPlains";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_canBurn = true;
        m_isHot = true;

        m_burnedTerrainType = typeof(ContentDesertRedDirtPlainsTerrain);
        m_addedEventTerrainType = typeof(ContentDesertRedGrassPlainsRuinsTerrain);

        LateInit();
    }
}
