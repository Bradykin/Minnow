using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDesertRedHillsPondTerrain : GameTerrainBase
{
    public ContentDesertRedHillsPondTerrain()
    {
        m_rangeModifier = Constants.HillsRangeModifier;

        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "DesertRedHillsPond";
        m_maxTerrainImageNumber = 2;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isHot = true;
        m_isHill = true;
        m_isWaterSource = true;

        LateInit();
    }
}
