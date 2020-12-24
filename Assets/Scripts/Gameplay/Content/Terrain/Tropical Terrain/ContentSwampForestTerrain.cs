using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentSwampForestTerrain : GameTerrainBase
{
    public ContentSwampForestTerrain()
    {
        m_coverType = CoverType.Cover;
        m_movementType = TerrainMovementType.Difficult;

        m_name = "SwampForest";
        m_maxTerrainImageNumber = 4;
        m_terrainImageNumber = Random.Range(1, m_maxTerrainImageNumber + 1);

        m_isPassable = true;
        m_isForest = true;

        LateInit();
    }
}
