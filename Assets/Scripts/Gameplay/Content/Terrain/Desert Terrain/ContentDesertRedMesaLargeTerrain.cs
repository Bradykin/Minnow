using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedMesaLargeTerrain : GameTerrainBase
{
    public ContentDesertRedMesaLargeTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;
        m_isPassable = false;

        m_name = "DesertRedMesaLarge";
        m_maxTerrainImageNumber = 1;
        m_terrainImageNumber = 1;

        m_isMountain = true;
        m_isHot = true;

        LateInit();
    }
}
