using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSnowMountainCaveTerrain : GameTerrainBase
{
    public ContentSnowMountainCaveTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;
        m_isPassable = false;

        m_name = "SnowMountainCave";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isMountain = true;
        m_isSnow = true;

        LateInit();
    }
}
