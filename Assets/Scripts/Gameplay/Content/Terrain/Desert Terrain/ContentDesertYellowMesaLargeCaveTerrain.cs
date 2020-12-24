using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowMesaLargeCaveTerrain : GameTerrainBase
{
    public ContentDesertYellowMesaLargeCaveTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;
        m_isPassable = false;

        m_name = "Desert Yellow Mesa Large Cave";
        m_maxTerrainImageNumber = 2;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isMountain = true;
        m_isHot = true;

        LateInit();
    }
}
