using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentVolcanoInactiveTerrain : GameTerrainBase
{
    public ContentVolcanoInactiveTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;
        m_isPassable = false;

        m_name = "VolcanoInactive";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isMountain = true;
        m_isVolcano = true;

        m_volcanoEruptTerrainType = typeof(ContentVolcanoActiveTerrain);

        LateInit();
    }
}
