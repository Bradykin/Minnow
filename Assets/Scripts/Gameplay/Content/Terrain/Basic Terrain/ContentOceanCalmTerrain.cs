using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOceanCalmTerrain : GameTerrainBase
{
    public ContentOceanCalmTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;
        m_isPassable = false;

        m_name = "OceanCalm";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isWater = true;

        m_marshTideLowerTerrainType = typeof(ContentBogTerrain);

        LateInit();
    }
}

