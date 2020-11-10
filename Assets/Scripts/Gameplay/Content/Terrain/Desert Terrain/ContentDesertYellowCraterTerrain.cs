using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowCraterTerrain : GameTerrainBase
{
    public ContentDesertYellowCraterTerrain()
    {
        m_coverType = CoverType.None;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "DesertYellowCrater";
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;

        m_isPassable = true;
        m_canBurn = false;
        m_isHot = true;

        LateInit();
    }
}