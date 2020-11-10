using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedGrassPlainsPondTerrain : GameTerrainBase
{
    public ContentDesertRedGrassPlainsPondTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;

        m_name = "DesertRedGrassPlainsPond";
        m_maxTerrainImageNumber = 2;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isPlains = true;
        m_canBurn = true;
        m_isHot = true;
        m_isWaterSource = true;

        m_burnedTerrainType = typeof(ContentDesertRedDirtPlainsTerrain);

        LateInit();
    }
}
