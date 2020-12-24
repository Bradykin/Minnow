using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentVolcanoActiveTerrain : GameTerrainBase
{
    public ContentVolcanoActiveTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;
        m_isPassable = false;

        m_name = "Volcano Active";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isMountain = true;
        m_isHot = true;
        m_isVolcano = true;

        LateInit();
    }
}
