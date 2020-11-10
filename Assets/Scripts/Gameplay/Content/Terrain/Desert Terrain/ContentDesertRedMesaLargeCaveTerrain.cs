using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedMesaLargeCaveTerrain : GameTerrainBase
{
    public ContentDesertRedMesaLargeCaveTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;
        m_isPassable = false;

        m_name = "DesertRedMesaLargeCave";
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;

        m_isMountain = true;
        m_isHot = true;
        m_isCave = true;

        LateInit();
    }
}
