using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentIceWaterTerrain : GameTerrainBase
{
    public ContentIceWaterTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Normal;
        m_isPassable = false;

        m_name = "IceWater";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isWater = true;
        m_isSnow = true;

        LateInit();
    }
}

