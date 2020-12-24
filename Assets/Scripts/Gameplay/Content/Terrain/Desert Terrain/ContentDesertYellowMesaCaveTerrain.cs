using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertYellowMesaCaveTerrain : GameTerrainBase
{
    public ContentDesertYellowMesaCaveTerrain()
    {
        m_rangeModifier = Constants.HillsRangeModifier;
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "Desert Yellow Mesa Cave";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isHot = true;
        m_isHill = true;

        LateInit();
    }
}
